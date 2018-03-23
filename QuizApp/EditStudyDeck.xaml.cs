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
    /// <summary>
    /// Interaction logic for EditStudyDeck.xaml
    /// </summary>
    ///     
    public partial class EditStudyDeck : Page
    {
        StudyDeck Deck = new StudyDeck();
        string JSONflashcards;
        int index = 0;
        Flashcards f;

        // Create serializer object to convert to and from the JSON format
        JavaScriptSerializer ser = new JavaScriptSerializer();

        public EditStudyDeck()
        {
            InitializeComponent();
        }

        private void SelectADeckbtn_Click(object sender, RoutedEventArgs e)
        {
           OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            { currentlyEditing.Text = openFileDialog.FileName; }
            openFileDialog.Filter = "JSON files (*.JSON)|*.JSON|All files (*.*)|*.*";
            
            JSONflashcards = File.ReadAllText(currentlyEditing.Text);
            Deck = ser.Deserialize<StudyDeck>(JSONflashcards);

            TotalCardstextBox.Text = (Deck.cards.Count).ToString();
            f = Deck.cards[index];
            TermtextBox.Text = f.Front;
            DefinitiontextBox.Text = f.Back;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(f.image);
            bitmap.EndInit();
            ImageViewer1.Source = bitmap;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            if(dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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

        private void Savebutton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (index == Deck.cards.Count-1)
            {
                MessageBox.Show("This is the last card in the deck.", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                {
                index++;
                f = Deck.cards[index];
                TermtextBox.Text = f.Front;
                DefinitiontextBox.Text = f.Back;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(f.image);
                bitmap.EndInit();
                ImageViewer1.Source = bitmap;
            }
    
        }

        private void Previousbtn_Click(object sender, RoutedEventArgs e)
        {
            if (index != 0)
            { 
                index--;
                f = Deck.cards[index];
                TermtextBox.Text = f.Front;
                DefinitiontextBox.Text = f.Back;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(f.image);
                bitmap.EndInit();
                ImageViewer1.Source = bitmap;
            }
        }

        private void Deletebtn_Click(object sender, RoutedEventArgs e)
        {
            Deck.cards.Remove(f);
            index--;
        }
    }
}
