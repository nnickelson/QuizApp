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
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }

        private void CreateADeckbtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/DeckBuilder.xaml", UriKind.Relative));
        }

        private void StudyADeckbtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate( new Uri("StudyMyDeck.xaml",UriKind.Relative));
        }

        private void ImportExportbtn_Click(object sender, RoutedEventArgs e)
        {
            // Create Root directory and subfolders if they do not exist yet
            string RootPath, StudyDecksPath, ImagePath, QuestionsDeckPath;
            //-------------- Create a root directory and subfolders on desktop if they haven't already been created yet------------
            RootPath = StudyDecksPath = ImagePath = QuestionsDeckPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            RootPath += @"\QuizApp\";
            StudyDecksPath = RootPath + @"\StudyDecks\";
            ImagePath = RootPath + @"\Images\";
            QuestionsDeckPath = RootPath + @"\QuestionDecks\";
            if (!Directory.Exists(RootPath))
            {
                Directory.CreateDirectory(RootPath);
                Directory.CreateDirectory(StudyDecksPath);
                Directory.CreateDirectory(ImagePath);
                Directory.CreateDirectory(QuestionsDeckPath);
            }
            //---------------------------------------------------------------------------
            NavigationService.Navigate(new Uri("Import.xaml", UriKind.Relative));


        }

        private void CreateQuizbtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("CreateQuiz.xaml", UriKind.Relative));
        }
    }
}
