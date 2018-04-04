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
using System.Text.RegularExpressions;

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
        string temp;

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
                temp = selectedFileName;
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
                // Get filepaths so that they may be written to the correct folder. 
                string RootPath, StudyDecksPath, ImagePath, QuestionsDeckPath;
                RootPath = StudyDecksPath = ImagePath = QuestionsDeckPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                RootPath += @"\QuizApp\";
                StudyDecksPath = RootPath + @"\StudyDecks\";
                ImagePath = RootPath + @"\Images\";
                QuestionsDeckPath = RootPath + @"\QuestionDecks\";

                if (currentCard == 0)
                {
                    fileName = DeckTitletextBox.Text;
                    fileName = fileName.Trim();


                    // If the file exists then update it.
                    if (File.Exists(StudyDecksPath + @"\" + fileName + ".StudyDeck.json"))
                    {
                        // This reads the JSON file as one big long string .                   
                        JSONflashcards = File.ReadAllText(StudyDecksPath + @"\" + fileName + ".StudyDeck.json");
                        // This parses that string back into an object.
                        Deck = ser.Deserialize<StudyDeck>(JSONflashcards);
                    }
                    else // Create a new file.
                        fileName = fileName + ".StudyDeck.json";
                }

                // If both the term and defintion exist then add it to the set.
                if (!string.IsNullOrEmpty(TermtextBox.Text) && !string.IsNullOrEmpty(DefinitiontextBox.Text))
                {
                    // Populate flashcards.
                    if (ImageExist == true)
                    {
                        Flashcards populateFlashCards = new Flashcards(TermtextBox.Text, DefinitiontextBox.Text, temp);
                        Deck.cards.Add(populateFlashCards);
                    }
                    else
                    {
                        Flashcards populateFlashCards = new Flashcards(TermtextBox.Text, DefinitiontextBox.Text, null);
                        Deck.cards.Add(populateFlashCards);
                    }

                    // Reserialize the object and store it as a String.
                    string outputJSON = ser.Serialize(Deck);

                    // Write that string back to the JSON file.
                    File.WriteAllText(StudyDecksPath + fileName, outputJSON);

                    // Increment total cards and reset text boxes.
                    currentCard++;
                    TermtextBox.Text = "";
                    DefinitiontextBox.Text = "";
                    CardNumberLabeltextBox.Text = (currentCard).ToString();
                    TotalCardsBox.Text = (Deck.cards.Count).ToString();
                    temp = "";
                    ImageExist = false;
                    // Reset Image box.
                    ImageViewer1.Source = null;
                }

                // If the Term and Definiton DOES NOT EXIST.
                else if (string.IsNullOrEmpty(TermtextBox.Text) && string.IsNullOrEmpty(DefinitiontextBox.Text))
                {
                    TermtextBox.Text = "";
                    DefinitiontextBox.Text = "";
                    MessageBox.Show("You did not enter a Definition or an Answer! Please try again", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);
                } // If the Term DOES NOT EXIST.
                else if (string.IsNullOrEmpty(TermtextBox.Text))
                    MessageBox.Show("You did not enter a Definition or an Answer! Please try again", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);
                // If the Definiton DOES NOT EXIST.
                else if (string.IsNullOrEmpty(DefinitiontextBox.Text))
                    MessageBox.Show("You did not enter a Term! Please try again", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("You must provide a deck title to save to. You only need to do this once and leave the field alone after that", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // This button takes the user back to the Deck builder page.
        private void Finishedbutton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(
                new Uri("/DeckBuilder.xaml", UriKind.Relative));
        }

        // The purpose of this function is to unhide the rest of the elements in the form.
        void ShowForm()
        {
            rect1.Visibility = Visibility.Visible;
            TotalCards.Visibility = Visibility.Visible;
            TotalCardsBox.Visibility = Visibility.Visible;
            CardNumberlabel.Visibility = Visibility.Visible; ;
            CardNumberLabeltextBox.Visibility = Visibility.Visible;
            button.Visibility = Visibility.Visible;
            TermtextBox.Visibility = Visibility.Visible;
            Termlabel.Visibility = Visibility.Visible;
            DefinitiontextBox.Visibility = Visibility.Visible;
            Definitionlabel.Visibility = Visibility.Visible;
            Addbutton.Visibility = Visibility.Visible;
            Finishedbutton.Visibility = Visibility.Visible;
            ImageViewer1.Visibility = Visibility.Visible;
           
        }

        // The purpose of this function is to check and see if a file already exists as well as if a user provided a valid file name.
        private void Gobtn_Click(object sender, RoutedEventArgs e)
        {
            // check if file exist
            // Get filepaths. 
            string RootPath, StudyDecksPath;
            RootPath = StudyDecksPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            RootPath += @"\QuizApp\";
            StudyDecksPath = RootPath + @"\StudyDecks\";

            fileName = DeckTitletextBox.Text;
            fileName = fileName.Trim();

            /* File names cannot contain a period or anyother illegal characters nor can it be a blank space*/
            if (IsValidFilename(fileName) != true || fileName.Contains(".") || fileName=="")
            {
                MessageBox.Show("Sorry, you entered an invalid filename. Please try again", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);
                DeckTitletextBox.Text = "";
            }
            else
            {
                // Check if file exist
                if (File.Exists(StudyDecksPath + @"\" + fileName + ".StudyDeck.json"))
                {
                    var result = MessageBox.Show("This file already exists. Would you like to add "
                        + "more cards to this deck?", "Help Window", MessageBoxButton.YesNo);

                    if (MessageBoxResult.Yes == result)
                    {
                        Gobtn.Visibility = Visibility.Hidden;
                        Warning.Visibility = Visibility.Hidden;
                        ShowForm();
                    }
                    else
                        DeckTitletextBox.Text = "";
                }
                else // file dosen't exist
                {
                    Gobtn.Visibility = Visibility.Hidden;
                    Warning.Visibility = Visibility.Hidden;
                    ShowForm();

                    // This just sets the current card number and total card number to 0 intially unless it exist
                    TotalCardsBox.Text = currentCard.ToString();
                    CardNumberLabeltextBox.Text = currentCard.ToString();
                }
            }
            
        }

        // The purpose of this function is to check if a filename is valid or not.
         bool IsValidFilename(string testName)
        {
            string strTheseAreInvalidFileNameChars = new string(System.IO.Path.GetInvalidFileNameChars());
            Regex regInvalidFileName = new Regex("[" + Regex.Escape(strTheseAreInvalidFileNameChars) + "]");

            if (regInvalidFileName.IsMatch(testName)) { return false; };

            return true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            button1.Visibility = Visibility.Hidden;
            button2.Visibility = Visibility.Hidden;
            button3.Visibility = Visibility.Hidden;
            label1.Visibility = Visibility.Hidden;
            label2.Visibility = Visibility.Hidden;
            label3.Visibility = Visibility.Hidden;
            NavigationService.Navigate(new Uri("/EditStudyDeck.xaml", UriKind.Relative));
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new Uri("/CreateStudySet.xaml", UriKind.Relative));
            button1.Visibility = Visibility.Hidden;
            button2.Visibility = Visibility.Hidden;
            button3.Visibility = Visibility.Hidden;
            label1.Visibility = Visibility.Hidden;
            label2.Visibility = Visibility.Hidden;
            label3.Visibility = Visibility.Hidden;
            showform();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            // NavigationService.Navigate(new Uri("/CreateStudySet.xaml", UriKind.Relative));
            button1.Visibility = Visibility.Hidden;
            button2.Visibility = Visibility.Hidden;
            button3.Visibility = Visibility.Hidden;
            label1.Visibility = Visibility.Hidden;
            label2.Visibility = Visibility.Hidden;
            label3.Visibility = Visibility.Hidden;
            showform();
        }

        void showform()
        {
            DeckTitleLabel.Visibility = Visibility.Visible;
            DeckTitletextBox.Visibility = Visibility.Visible;
            Warning.Visibility = Visibility.Visible;
            Gobtn.Visibility = Visibility.Visible;
            

            //hide
            button1.Visibility = Visibility.Hidden;
            button2.Visibility = Visibility.Hidden;
            button3.Visibility = Visibility.Hidden;
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(
               new Uri("/Home.xaml", UriKind.Relative));
        }

        private void StudyADeckbtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("StudyMyDeck.xaml", UriKind.Relative));
        }

        private void CreateADeckbtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/DeckBuilder.xaml", UriKind.Relative));
        }

        private void ImportExportbtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Import.xaml", UriKind.Relative));
        }

        private void CreateQuizbtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("CreateQuiz.xaml", UriKind.Relative));
        }
    }

}