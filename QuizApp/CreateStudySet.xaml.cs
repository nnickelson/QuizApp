using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Web.Script.Serialization;
using System.IO;

namespace QuizApp

{
    public partial class CreateStudySet : Page
    {
        public CreateStudySet()
        {
            InitializeComponent();
        }

        StudyDeck Deck = new StudyDeck();
        string fileName = "";
        string JSONflashcards;
        string front = "", back = "";
        int TotalCards = 0;


        // Create serializer object to convert to and from the JSON format
        JavaScriptSerializer ser = new JavaScriptSerializer();

        // This is for the image box
        private void button_Click(object sender, RoutedEventArgs e)
        {

            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
      
                String selectedFileName = dlg.FileName;
                FileNameLabel.Content = selectedFileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                ImageViewer1.Source = bitmap;
            }
        }



        /* This button will add a card to a set only if the term and definition exist */
        private void Addbutton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(DeckTitletextBox.Text))
            {
                if (TotalCards == 0)
                {
                    fileName = DeckTitletextBox.Text;
                    fileName = fileName + ".json";
              
                    // if the file exists we update it - if it doesn't we'll create it later
                    if (File.Exists(fileName))
                    {
                       // This reads the JSON file as one big long string                    
                        JSONflashcards = File.ReadAllText(fileName);
                    }
                }


                // If both the term and defintion exist then add it to the set
                if (!string.IsNullOrEmpty(TermtextBox.Text) && !string.IsNullOrEmpty(DefinitiontextBox.Text))
                {
                    front = TermtextBox.Text;
                    back = DefinitiontextBox.Text;

                    // Create a root directory and subfolder
                    string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                    // string extension = DeckTitletextBox.Text;
                    filePath += @"\StudyDecks\";// + extension;
                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);

                    // Create a JSON file
                    fileName = DeckTitletextBox.Text;
                    fileName = fileName + ".json";

                    // Populate flashcards
                    Flashcards populateFlashCards = new Flashcards(front, back);
                    Deck.cards.Add(populateFlashCards);
                   
                    // Reserialize the object and store it as a String
                    string outputJSON = ser.Serialize(Deck);
                    
                    // Write that string back to the JSON file
                    File.WriteAllText(filePath + fileName, outputJSON);

                    // increment total cards and reset text boxes
                    TotalCards++;              
                    TermtextBox.Text = "";
                    DefinitiontextBox.Text = "";
                    CardNumberLabeltextBox.Text = (TotalCards).ToString();
                }

                // If the Question and Answer DOES NOT EXIST
                else if (string.IsNullOrEmpty(TermtextBox.Text) && string.IsNullOrEmpty(DefinitiontextBox.Text))
                {
                    TermtextBox.Text = "";
                    DefinitiontextBox.Text = "";
                    MessageBox.Show("You did not enter a Definition or an Answer! Please try again", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);
                }//If the Question DOES NOT EXIST
                else if (string.IsNullOrEmpty(TermtextBox.Text))
                {
                    MessageBox.Show("You did not enter a Definition or an Answer! Please try again", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);

                }//If the Answer DOES NOT EXIST
                else if (string.IsNullOrEmpty(DefinitiontextBox.Text))
                {
                    MessageBox.Show("You did not enter a Term! Please try again", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("You must provide a deck title to save to. You only need to do this once and leave the field alone after that", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

    }

}