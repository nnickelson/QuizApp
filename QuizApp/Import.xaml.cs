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
    /// Interaction logic for Import.xaml
    /// </summary>
    public partial class Import : Page
    {
        //Work variable
        string Destination;
        JavaScriptSerializer ser = new JavaScriptSerializer();

        public Import()
        {
            InitializeComponent();
        }

        // Thus button copies a question deck from the users computer to the questions deck folder.
        private void QuestionDeck_Click(object sender, RoutedEventArgs e)
        {
            // Hide save message
            textBlock.Visibility = Visibility.Hidden;

            Destination = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Destination += @"\QuizApp\";
            Destination += @"\QuestionDecks\";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.JSON)|*.JSON";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string Source in openFileDialog.FileNames)
                {
                    // Copy the file to the destination if it is not already in the specified destination
                    if (!File.Exists(Destination + System.IO.Path.GetFileName(Source)))
                        File.Copy(Source, Destination + System.IO.Path.GetFileName(Source));
                }
                // Show "Saved Successfully message"
                textBlock.Visibility = Visibility.Visible;
            }
        }

        // This button copies an image from the users computer to to the images folder.
        private void ImageDeck_Click(object sender, RoutedEventArgs e)
        {
            // Hide save message
            textBlock.Visibility = Visibility.Hidden;

            Destination = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Destination += @"\QuizApp\";
            Destination += @"\Images\";

            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg| (*.png)|*.png";
            dlg.RestoreDirectory = true;
            dlg.Multiselect = true;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string Source in dlg.FileNames)
                {
                    // Copy the file to the destination if it is not already in the specified destination
                    if (!File.Exists(Destination + System.IO.Path.GetFileName(Source)))
                        File.Copy(Source, Destination + System.IO.Path.GetFileName(Source));
                }
                // Show "Saved Successfully message"
                textBlock.Visibility = Visibility.Visible;
            }
        }

        // This button takes the user back to the home page.
        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(
                    new Uri("/Home.xaml", UriKind.Relative));
        }

        // This button copies a study deck from the users computer to the study deck folder.
        private void StudyDeck_Click(object sender, RoutedEventArgs e)
        {
            // Hide save message
            textBlock.Visibility = Visibility.Hidden;

            Destination = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Destination += @"\QuizApp\";
            Destination += @"\StudyDecks\";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.JSON)|*.JSON";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string Source in openFileDialog.FileNames)
                {
                    // Copy the file to the destination if it is not already in the specified destination
                    if (!File.Exists(Destination + System.IO.Path.GetFileName(Source)))
                        File.Copy(Source, Destination + System.IO.Path.GetFileName(Source));
                }
                // Show "Saved Successfully message"
                textBlock.Visibility = Visibility.Visible;
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
