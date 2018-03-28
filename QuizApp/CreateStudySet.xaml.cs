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
        // Work varibales
        StudyDeck Deck = new StudyDeck();
        string fileName = "";
        string JSONflashcards;
        int currentCard = 0;
        Boolean ImageExist = false;
        JavaScriptSerializer ser = new JavaScriptSerializer();

        public CreateStudySet()
        {
            InitializeComponent();
        }

        // This button opens the windows explorer and allows the user to select .JPG or .PNG images.
        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg| (*.png)|*.png";
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
                ImageExist = true;
            }
        }

        // This button will add a card to a set only if the term and definition exist.
        private void Addbutton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(DeckTitletextBox.Text))
            {
                string RootPath, StudyDecksPath, ImagePath, QuestionsDeckPath;
                //-------------- Create a root directory and subfolders on desktop if they haven't already been created yet------------
                RootPath = StudyDecksPath = ImagePath = QuestionsDeckPath= Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                RootPath += @"\QuizApp\";
                StudyDecksPath = RootPath+ @"\StudyDecks\";
                ImagePath = RootPath+ @"\Images\";
                QuestionsDeckPath = RootPath + @"\Questions\";
                if (!Directory.Exists(RootPath))
                {
                    Directory.CreateDirectory(RootPath);
                    Directory.CreateDirectory(StudyDecksPath);
                    Directory.CreateDirectory(ImagePath);
                    Directory.CreateDirectory(QuestionsDeckPath);
                }
                //---------------------------------------------------------------------------

                if (currentCard == 0)
                {
                    fileName = DeckTitletextBox.Text;
                    fileName = fileName + ".json";
              
                    // if the file exists we update it - if it doesn't we'll create it later
                    if (File.Exists(StudyDecksPath + @"\" + fileName))
                    {
                        // This reads the JSON file as one big long string                    
                        JSONflashcards = File.ReadAllText(StudyDecksPath + @"\"+fileName);
                    // This parses that string back into an object
                    Deck = ser.Deserialize<StudyDeck>(JSONflashcards);
                    }
               }
             
                // If both the term and defintion exist then add it to the set
                if (!string.IsNullOrEmpty(TermtextBox.Text) && !string.IsNullOrEmpty(DefinitiontextBox.Text))
                {                 
                    // Populate flashcards
                    if (ImageExist == true)
                    {
                        Flashcards populateFlashCards = new Flashcards(TermtextBox.Text, DefinitiontextBox.Text, FileNameLabel.Content.ToString());
                        Deck.cards.Add(populateFlashCards);
                    } else
                    {
                        Flashcards populateFlashCards = new Flashcards(TermtextBox.Text, DefinitiontextBox.Text, null);
                        Deck.cards.Add(populateFlashCards);
                    }          
                   
                    // Reserialize the object and store it as a String
                    string outputJSON = ser.Serialize(Deck);
                    
                    // Write that string back to the JSON file
                    File.WriteAllText(StudyDecksPath + fileName, outputJSON);

                    // increment total cards and reset text boxes
                    currentCard++;              
                    TermtextBox.Text = "";
                    DefinitiontextBox.Text = "";
                    CardNumberLabeltextBox.Text = (currentCard).ToString();
                    TotalCardsBox.Text = (Deck.cards.Count).ToString();
                    FileNameLabel.Content = "";
                    ImageExist = false;
                    // reset Image box
                    ImageViewer1.Source = null;
                }

                // If the Term and Definiton DOES NOT EXIST
                else if (string.IsNullOrEmpty(TermtextBox.Text) && string.IsNullOrEmpty(DefinitiontextBox.Text))
                {
                    TermtextBox.Text = "";
                    DefinitiontextBox.Text = "";
                    MessageBox.Show("You did not enter a Definition or an Answer! Please try again", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);
                }//If the Term DOES NOT EXIST
                else if (string.IsNullOrEmpty(TermtextBox.Text))
                {
                    MessageBox.Show("You did not enter a Definition or an Answer! Please try again", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);
                }//If the Definiton DOES NOT EXIST
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

        // This button takes the user back to the Deck builder page.
        private void Finishedbutton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(
                new Uri("/DeckBuilder.xaml", UriKind.Relative));
        }
    }

}