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
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
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

        private QuestionsDeck questionsDeck;
        private Question question;
        private FillInBlankCanvas fibCanvas;
        private TrueFalseCanvas tfCanvas;
        private MultipleChoiceCanvas mcCanvas;
        private bool misClick;
        Random rnd = new Random();

        int CurrPos = 0;

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
                this.question = new ;
            }
        }

        /// <summary>
        /// trueFalse_Clicked Method
        /// Adds a True False cnavas to the grid
        /// Initializes a new True False question
        /// </summary>
        /// <param name="sender">button handler</param>
        /// <param name="e">button handler</param>
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

        /// <summary>
        /// multipleChoice_Clicked Method
        /// Adds a Multiple Choice Canvas to the grid
        /// Initializes a new Multiple Choice questions
        /// </summary>
        /// <param name="sender">button handler</param>
        /// <param name="e">button handler</param>
        private void multipleChoice_Clicked(object sender, RoutedEventArgs e)
        {
            
            
                QuestionsCanvasTemplate.Children.Clear();
                double height = QuestionsCanvasTemplate.ActualHeight;
                double width = QuestionsCanvasTemplate.ActualWidth;
                McCanvas = new MultipleChoiceCanvas(height, width);
                QuestionsCanvasTemplate.Children.Add(McCanvas.BottomCanvas);
                this.question = new ;

        }


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



        // This button opens the window explorer and allows the user to select a quiz setting  to edit.
        private void SelectADeckbtn_Click(object sender, RoutedEventArgs e)
        {
            String filePath;
            Microsoft.Win32.OpenFileDialog openFileDialog = new OpenFileDialog();
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


                        // shuffle the decks
                    
                        for (int i = 1; i < Deck.IncludedDecks.Count; i++)
                        {
                            int pos = rnd.Next(i + 1);
                            var x = Deck.IncludedDecks[i];
                            Deck.IncludedDecks[i] = Deck.IncludedDecks[pos];
                            Deck.IncludedDecks[pos] = x;
                        }

                        Choosen = Deck.IncludedDecks[0];
                        NextBtn_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Sorry, You can only select a Quiz Setting on this page. If you need to create a Quiz setting"
                            + " then go back to the Quiz settings page.", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }


        void DisplayQuestion(Question ques)
        {
            QuestionsCanvasTemplate.Children.Clear();
            double height = QuestionsCanvasTemplate.ActualHeight;
            double width = QuestionsCanvasTemplate.ActualWidth;
            MessageBox.Show("This is the question type: " + (ques).GetType(), "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);


            if (ques.getQuestionType == Question.QuestionType.FillInBlank)
            {
                FibCanvas = new FillInBlankCanvas(height, width);
                QuestionsCanvasTemplate.Children.Add(FibCanvas.BottomCanvas);
                FibCanvas.Tb1.Text = ques.QuestionText;
                FibCanvas.Tb1.IsReadOnly = true;

            }

            if (ques.getQuestionType == Question.QuestionType.TrueFalse)
            {
                tfCanvas = new TrueFalseCanvas(height, width);
                QuestionsCanvasTemplate.Children.Add(tfCanvas.BottomCanvas);
                TfCanvas.QuestionBox.Text = ques.QuestionText;
                tfCanvas.QuestionBox.IsReadOnly = true;

            }

            if (ques.getQuestionType == Question.QuestionType.MultipleChoice)
            {
                McCanvas = new MultipleChoiceCanvas(height, width);
                QuestionsCanvasTemplate.Children.Add(McCanvas.BottomCanvas);
                McCanvas.QuestionBox.Text = ques.QuestionText;
                McCanvas.QuestionBox.IsReadOnly = true;

                for (int i = 0; i < 5; i++)
                {
                    if (i < ques.MCAnswers.Choices.Count)
                    {
                        McCanvas.AnswerBoxes[i].Text = ques.MCAnswers.Choices[i];
                    }
                    else
                    {
                        McCanvas.AnswerBoxes[i].Visibility = Visibility.Hidden;

                        McCanvas.AnswerButtons[i].Visibility = Visibility.Hidden;
                    }
                }

                // MultipleChoice temp = (MultipleChoice)ques;


                // DisplayMcQuestions(temp) ;
            }
        }


        /*
        void DisplayMcQuestions(MultipleChoice ques)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i < ques.Choices.Count)
                {
                    McCanvas.AnswerBoxes[i].Text = ques.Choices[i];
                }
                else
                {
                    McCanvas.AnswerBoxes[i].Visibility = Visibility.Hidden;
                    McCanvas.AnswerButtons[i].Visibility = Visibility.Hidden;
                }
            }
        }
        */



        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("This is the question type: " + Choosen.QuestionList[0].QuestionType, "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);

            // shuffle deck
            
            
            for (int i = 1; i < Deck.IncludedDecks.Count; i++)
            {
                int pos = rnd.Next(i + 1);
                var x = Deck.IncludedDecks[i];
                Deck.IncludedDecks[i] = Deck.IncludedDecks[pos];
                Deck.IncludedDecks[pos] = x;
            }
            

            // pick deck
            Choosen = Deck.IncludedDecks[0];

            
            // shuffle question
            for (int i = 1; i < Choosen.QuestionList.Count; i++)
            {
                int pos = rnd.Next(i + 1);
                var x = Choosen.QuestionList[i];
                Choosen.QuestionList[i] = Choosen.QuestionList[pos];
                Choosen.QuestionList[pos] = x;
            }
            
            

            if (CurrPos < Deck.NumberOfQuestions)
            {
                CurrPos++;
                CurrentQuestion.Content = Convert.ToString(CurrPos);
                //(Choosen.QuestionList[0]).GetType();

                
                DisplayQuestion(Choosen.QuestionList[0]);
                
               
            }
        }




























        /***********************************************************************/
        public QuestionsDeck QuestionsDeck
        {
            get
            {
                return questionsDeck;
            }

            set
            {
                questionsDeck = value;
            }
        }

        public Question Question
        {
            get
            {
                return question;
            }

            set
            {
                question = value;
            }
        }

        public FillInBlankCanvas FibCanvas
        {
            get
            {
                return fibCanvas;
            }

            set
            {
                fibCanvas = value;
            }
        }

        public TrueFalseCanvas TfCanvas
        {
            get
            {
                return tfCanvas;
            }

            set
            {
                tfCanvas = value;
            }
        }

        public MultipleChoiceCanvas McCanvas
        {
            get
            {
                return mcCanvas;
            }

            set
            {
                mcCanvas = value;
            }
        }
        /*************************************************************************/
    }
}
