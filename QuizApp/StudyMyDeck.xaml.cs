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
    public partial class StudyMyDeck : Page
    {
        // Work variables
        StudyDeck Deck = new StudyDeck();
        string JSONflashcards;
        int index = 0;
        JavaScriptSerializer ser = new JavaScriptSerializer();
        Flashcards f;
        String path = "";
        Boolean IsFrontShowing = true;

        // Make the front of the flashcard visible.
         void makeFrontVisible()
        {
            // First, turn OFF the back.
            ImageViewer1.Visibility = Visibility.Hidden;
            DefinitionBox.Visibility = Visibility.Hidden;

            // Then turn ON the front.         
            TermBlock.Visibility = Visibility.Visible;
            Previousbtn.Visibility = Visibility.Visible;           
        }
        
        // Make the back of the flashcard visible
        void makeBackVisible()
        {
            // First, turn OFF the front
            TermBlock.Visibility = Visibility.Hidden;

            // Then turn ON the back
            ImageViewer1.Visibility = Visibility.Visible;
            DefinitionBox.Visibility = Visibility.Visible;
        }
        
        public StudyMyDeck()
        {
            InitializeComponent();
        }

        // This button opens the file explorer so the user can select a study deck to edit.
        private void SelectADeckbtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.JSON)|*.JSON";
            if (openFileDialog.ShowDialog() == true)
            {
                path = openFileDialog.FileName; // get file path
                DeckTitlebox.Text= System.IO.Path.GetFileNameWithoutExtension(path); // get file name only
                makeFrontVisible();

                // Show flashcard border and buttons
                Rectangle1.Visibility = Visibility.Visible;
                Nextbtn.Visibility = Visibility.Visible;
                Flipbtn.Visibility = Visibility.Visible;
                Finishedbtn.Visibility = Visibility.Visible;
            }

            if (path != "")
            {
                // Load the contents of the flashcard
                JSONflashcards = File.ReadAllText(path);
                Deck = ser.Deserialize<StudyDeck>(JSONflashcards);
                TotalCardsBox.Text = (Deck.cards.Count).ToString();
                f = Deck.cards[index];
                TermBlock.Text = f.Front;
                DefinitionBox.Text = f.Back;
                CurrentCardTextbox.Text = (index + 1).ToString();
                loadImage();
            }
        }

        // This button loads the next flashcard in the deck.
        private void Nextbtn_Click(object sender, RoutedEventArgs e)
        {          
            //  Note: Each time next is hit, always show front of the card and hide back 
            if (index < Deck.cards.Count - 1)
            {
                index++;
                f = Deck.cards[index];
                TermBlock.Text = f.Front;
                DefinitionBox.Text = f.Back;
                CurrentCardTextbox.Text = (index + 1).ToString();
                loadImage();
                makeFrontVisible();
                IsFrontShowing = true;
            }
        }

        // This button loads the previous flashcard in the deck.
        private void Previousbtn_Click(object sender, RoutedEventArgs e)
        {
            if (index != 0)
            {
                index--;
                f = Deck.cards[index];
                TermBlock.Text = f.Front;
                DefinitionBox.Text = f.Back;
                CurrentCardTextbox.Text = (index + 1).ToString();
                loadImage();
                makeFrontVisible();
                IsFrontShowing = true;
            }
        }

        // This button takese the user back to the home page.
        private void Finishedbtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(
                    new Uri("/Home.xaml", UriKind.Relative));
        }

        // This button will alternate between the front and back of the flashcard on each click.
        private void Flipbtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsFrontShowing == false)
            {
                makeFrontVisible();
                IsFrontShowing = true;
            }
            else
            {
                makeBackVisible();
                IsFrontShowing = false;
            }
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
                ImageViewer1.Source = null;
        }

    }


}
