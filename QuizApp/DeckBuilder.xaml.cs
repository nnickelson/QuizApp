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

namespace QuizApp
{
    /// <summary>
    /// Interaction logic for DeckBuilder.xaml
    /// </summary>
    public partial class DeckBuilder : Page
    {
        public DeckBuilder()
        {
            InitializeComponent();
        }

        private void NewStudyDeckBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/CreateStudySet.xaml", UriKind.Relative));
        }

        private void NewQuestionDeckBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/CreateQuestionSet.xaml",UriKind.Relative));
        }



        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Home.xaml", UriKind.Relative));
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
