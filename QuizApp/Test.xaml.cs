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
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Page
    {
        QuizSettings Deck = new QuizSettings();
        string JSONflashcards;
        JavaScriptSerializer ser = new JavaScriptSerializer();
        QuestionsDeck Choosen = new QuestionsDeck();


        public static List<T> Randomize<T>(List<T> list)
        {
            List<T> randomizedList = new List<T>();
            Random rnd = new Random();
            while (list.Count > 0)
            {
                int index = rnd.Next(0, list.Count); //pick a random item from the master list
                randomizedList.Add(list[index]); //place it at the end of the randomized list
                list.RemoveAt(index);
            }
            return randomizedList;
        }

        public Test()
        {
            InitializeComponent();
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
        private void TakeAQuizbtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Test.xaml", UriKind.Relative));
        }

        /*
        private void fillInTheBlank_Clicked(object sender, RoutedEventArgs e)
        {
            if (misClick == true)
            {
                misClick = false;
            }
            else
            {
                QuestionsCanvasTemplate.Children.Clear();

                double height = QuestionsCanvasTemplate.ActualHeight;
                double width = QuestionsCanvasTemplate.ActualWidth;
                FibCanvas = new FillInBlankCanvas(height, width);
                QuestionsCanvasTemplate.Children.Add(FibCanvas.BottomCanvas);
                this.question = new FillInBlank { QuestionType = "Fill In Blank" };
            }
        }

        private void trueFalse_Clicked(object sender, RoutedEventArgs e)
        {
            if (misClick == true)
            {
                misClick = false;
            }
            else
            {
                QuestionsCanvasTemplate.Children.Clear();
                double height = QuestionsCanvasTemplate.ActualHeight;
                double width = QuestionsCanvasTemplate.ActualWidth;
                TfCanvas = new TrueFalseCanvas(height, width);
                QuestionsCanvasTemplate.Children.Add(TfCanvas.BottomCanvas);
                this.question = new TrueFalse { QuestionType = "true false" };
            }
        }

        private void multipleChoice_Clicked(object sender, RoutedEventArgs e)
        {
            if (misClick == true)
            {
                misClick = false;
            }
            else
            {
                QuestionsCanvasTemplate.Children.Clear();
                double height = QuestionsCanvasTemplate.ActualHeight;
                double width = QuestionsCanvasTemplate.ActualWidth;
                McCanvas = new MultipleChoiceCanvas(height, width);
                QuestionsCanvasTemplate.Children.Add(McCanvas.BottomCanvas);
                this.question = new MultipleChoice { QuestionType = "multiple choice" };

            }
        }
        */

        // This button opens the window explorer and allows the user to select a study deck to edit.
        private void SelectADeckbtn_Click(object sender, RoutedEventArgs e)
        {
            String filePath;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.JSON)|*.JSON";
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;// Get the files path.

                // If a study deck exist then populate the rest of the form with the appropriate fields.
                if (File.Exists(filePath))
                {
                    // Before the form is populated, make sure it is a Quiz setting
                    if (filePath.Contains(".QuizSettings"))
                    {
                        // Hide top layer
                        SelectADeckbtn.Visibility = Visibility.Hidden;
                        TopLayer.Visibility = Visibility.Hidden;                   

                        JSONflashcards = File.ReadAllText(filePath);
                        Deck = ser.Deserialize<QuizSettings>(JSONflashcards);

                        // Get total questions
                        TotalQuestions.Content = Convert.ToInt32(Deck.NumberOfQuestions);

                        // Get the quiz name
                        TestName.Text = Deck.QuizName;

                        /* *************************************************************************************************** */

                        // Shuffle the question decks randomly and select the deck at index [0] to pull questions from.
                        Random rnd = new Random();
                        for (int i = 1; i < Deck.IncludedDecks.Count; i++)
                        {
                            int pos = rnd.Next(i + 1);
                            var x = Deck.IncludedDecks[i];
                            Deck.IncludedDecks[i] = Deck.IncludedDecks[pos];
                            Deck.IncludedDecks[pos] = x;
                        }

                        Choosen = Deck.IncludedDecks[0];

                        // Now shuffle questions in that deck 
                        for (int i = 1; i < Choosen.QuestionList.Count(); i++)
                        {
                            int pos = rnd.Next(i + 1);
                            var x = Choosen.QuestionList[i];
                            Choosen.QuestionList[i] = Choosen.QuestionList[pos];
                            Choosen.QuestionList[pos] = x;
                        }

                        // determine the question type and display the apporiate canvas
                        for (int i = 0; i < Choosen.QuestionList.Count(); i++)
                        {
                           
                        }

                        
                       // tBox.Text = Choosen.QuestionList[0].QuestionText;
                        /* ************************************************************************************************** */
                    }
                    else
                    {
                        MessageBox.Show("Sorry, You can only select a Quiz Setting on this page. If you need to create a Quiz setting"
                            + " then go back to the Quiz settings page.", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }

        

    }
}
