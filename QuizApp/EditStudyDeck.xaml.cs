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
using Microsoft.Win32;

namespace QuizApp
{

    public partial class EditStudyDeck : Page
    {
        //Work variable
        StudyDeck Deck = new StudyDeck();
        string JSONflashcards;
        int index = 0;
        Flashcards f;
        string filePath;
        JavaScriptSerializer ser = new JavaScriptSerializer();

        public EditStudyDeck()
        {
            InitializeComponent();
        }

        // This button opens the window explorer and allows the user to select a study deck to edit.
        private void SelectADeckbtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.JSON)|*.JSON";
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;// Get the files path.
               
                // If a study deck exist then populate the rest of the form with the appropriate fields.
                if (File.Exists(filePath))
                {
                    // Before the form is populated, make sure it is a StudyDeck and not a QuestionDeck.
                    if (filePath.Contains(".StudyDeck"))
                    {
                        currentlyEditing.Text = System.IO.Path.GetFileNameWithoutExtension(filePath); // Get the files name only.
                        String[] arr = (currentlyEditing.Text).Split('.');
                        currentlyEditing.Text = arr[0];

                        // Make the rest of form visibile once a deck has been selected.
                        button.Visibility = Visibility.Visible;
                        TermtextBox.Visibility = Visibility.Visible;
                        Termlabel.Visibility = Visibility.Visible;
                        DefinitiontextBox.Visibility = Visibility.Visible;
                        Definitionlabel.Visibility = Visibility.Visible;
                        Deletebtn.Visibility = Visibility.Visible;
                        Savebutton.Visibility = Visibility.Visible;
                        NextBtn.Visibility = Visibility.Visible;
                        Previousbtn.Visibility = Visibility.Visible;
                        finish.Visibility = Visibility.Visible;

                        JSONflashcards = File.ReadAllText(filePath);
                        Deck = ser.Deserialize<StudyDeck>(JSONflashcards);
                        TotalCardstextBox.Text = (Deck.cards.Count).ToString();
                        f = Deck.cards[index];

                        TermtextBox.Text = f.Front;
                        DefinitiontextBox.Text = f.Back;
                        CurrentCardTextbox.Text = (index + 1).ToString();
                        loadImage();
                    }
                    else
                    {
                        MessageBox.Show("Sorry, You can only edit a Study Deck on this page. If you need to edit a Question deck"
                            +" then go back to the deck builder page and click the 'Edit Question Deck' button.", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }

        // This button opens the file explorer and allows the user to select an image for a flashcard.
        private void button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg| (*.png)|*.png";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String selectedFileName = dlg.FileName;
       
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                ImageViewer1.Source = bitmap;
            }
        }

        // This button manually saves and edits made to the current flash card.
        private void Savebutton_Click(object sender, RoutedEventArgs e)
        {
            // If both the term and defintion exist then add it to the set
            if (!string.IsNullOrEmpty(TermtextBox.Text) && !string.IsNullOrEmpty(DefinitiontextBox.Text))
            {
                if (ImageViewer1.Source != null)
                {
                    f.Front = TermtextBox.Text;
                    f.Back = DefinitiontextBox.Text;
                    f.image = (ImageViewer1.Source).ToString();
                    Deck.cards[index] = f;
                }
                else
                {
                    f.Front = TermtextBox.Text;
                    f.Back = DefinitiontextBox.Text;
                    f.image = null;
                    Deck.cards[index] = f;
                }
                // Reserialize the object and store it as a String
                string outputJSON = ser.Serialize(Deck);
                // Write that string back to the JSON file
                File.WriteAllText(filePath, outputJSON);
            }
            // If the term and definiton DOES NOT EXIST
            else if (string.IsNullOrEmpty(TermtextBox.Text) && string.IsNullOrEmpty(DefinitiontextBox.Text))
            {
                MessageBox.Show("You did not enter a Definition or an Answer! Please try again", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);
            }//If the term DOES NOT EXIST
            else if (string.IsNullOrEmpty(TermtextBox.Text))
            {
                MessageBox.Show("You did not enter a Definition or an Answer! Please try again", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);
            }//If the Definition DOES NOT EXIST
            else if (string.IsNullOrEmpty(DefinitiontextBox.Text))
            {
                MessageBox.Show("You did not enter a Term! Please try again", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // This button moves to the next flashcard provided it is not the last card in the deck.
        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (index < Deck.cards.Count - 1)
            {
                index++;

                // Load flashcards onto the form.
                f = Deck.cards[index];
                TermtextBox.Text = f.Front;
                DefinitiontextBox.Text = f.Back;
                CurrentCardTextbox.Text = (index + 1).ToString();

                // Load the image if it exist onto the form.
                loadImage();
            }

        }

        // This button moves to the previous flashcard provided it is not the first card in the deck.
        private void Previousbtn_Click(object sender, RoutedEventArgs e)
        {
            if (index > 0)
            {
                index--;

                // Load flashcards onto the form.
                f = Deck.cards[index];
                TermtextBox.Text = f.Front;
                DefinitiontextBox.Text = f.Back;
                CurrentCardTextbox.Text = (index + 1).ToString();

                // Load the image if it exist onto the form.
                loadImage();
            }
        }

        // This button deletes the current flashcard from the deck.
        private void Deletebtn_Click(object sender, RoutedEventArgs e)
        {
            if (Deck.cards.Count > 1) // At least one card in the deck
            {
                Deck.cards.Remove(f);
                index--;
                CurrentCardTextbox.Text = (index + 1).ToString();
                TotalCardstextBox.Text = (Deck.cards.Count).ToString();

                // Reserialize the object and store it as a String
                string outputJSON = ser.Serialize(Deck);

                // Write that string back to the JSON file
                File.WriteAllText(filePath, outputJSON);

                if (index > 1) // Go back one card on delete;
                {
                    Previousbtn_Click(sender, e);
                }
                else
                {
                    NextBtn_Click(sender, e); // Move forward to the next card on delete;
                }
            }
            else // Delete the deck
            {
                File.Delete(filePath);
                index = 0;
                button.Visibility = Visibility.Hidden;
                TermtextBox.Visibility = Visibility.Hidden;
                Termlabel.Visibility = Visibility.Hidden;
                DefinitiontextBox.Visibility = Visibility.Hidden;
                Definitionlabel.Visibility = Visibility.Hidden;
                Deletebtn.Visibility = Visibility.Hidden;
                Savebutton.Visibility = Visibility.Hidden;
                NextBtn.Visibility = Visibility.Hidden;
                Previousbtn.Visibility = Visibility.Hidden;
                finish.Visibility = Visibility.Visible;
                CurrentCardTextbox.Text = "";
                TotalCardstextBox.Text = "";
                currentlyEditing.Text = "";
            }
        }

        // This button takes the user back to the Edit study deck page.
        private void finish_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(
               new Uri("/CreateStudySet.xaml", UriKind.Relative));
        }

        void loadImage()
        {
            if (f.image != null)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(f.image);
                bitmap.EndInit();
                ImageViewer1.Source = bitmap;
              
            }
            else
            {
                ImageViewer1.Source = null;
               
            }
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
