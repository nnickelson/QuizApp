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
using System.Windows.Threading;

namespace QuizApp
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Page
    {
        QuizSettings QuizDecks = new QuizSettings();
        string JSONquestion;
        JavaScriptSerializer ser = new JavaScriptSerializer();
        QuestionsDeck Chosen = new QuestionsDeck();

        private QuestionsDeck questionsDeck;
        private Question question;
        private FillInBlankCanvas fibCanvas;
        private TrueFalseCanvas tfCanvas;
        private MultipleChoiceCanvas mcCanvas;
        Random rnd = new Random();


        DispatcherTimer _timer;
        TimeSpan _time;

        int CurrPos = 1;

        int Total_Correct = 0;

        private void fillInTheBlank_Clicked(object sender, RoutedEventArgs e)
        {
            QuestionsCanvasTemplate.Children.Clear();
            double height = QuestionsCanvasTemplate.ActualHeight;
            double width = QuestionsCanvasTemplate.ActualWidth;
            FibCanvas = new FillInBlankCanvas(height, width);
            QuestionsCanvasTemplate.Children.Add(FibCanvas.BottomCanvas);
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
            QuestionsCanvasTemplate.Children.Clear();
            double height = QuestionsCanvasTemplate.ActualHeight;
            double width = QuestionsCanvasTemplate.ActualWidth;
            TfCanvas = new TrueFalseCanvas(height, width);
            QuestionsCanvasTemplate.Children.Add(TfCanvas.BottomCanvas);
            //this.question = new TrueFalse { QuestionType = "true false" };
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

                        JSONquestion = File.ReadAllText(filePath);
                        QuizDecks = ser.Deserialize<QuizSettings>(JSONquestion);

                        // Get total questions
                        TotalQuestions.Content = QuizDecks.NumberOfQuestions;

                        // Get the quiz name
                        TestName.Text = QuizDecks.QuizName;

                        for (int i = 0; i < QuizDecks.NumberOfQuestions; i++)
                        {
                            Question nextQuestion = RandomQuestion(QuizDecks.IncludedDecks);
                            DisplayQuestion(nextQuestion);
                        }

                      
                        CurrentQuestion.Content = Convert.ToString(CurrPos);

                        // If the quiz is timed then start the timer
                        if (QuizDecks.IsTimed)
                        {
                            _time = TimeSpan.FromMinutes(QuizDecks.QuizMinutes);

                            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                            {
                                tb.Text = _time.ToString("c");
                                if (_time == TimeSpan.Zero) _timer.Stop();
                                _time = _time.Add(TimeSpan.FromSeconds(-1));
                            }, Application.Current.Dispatcher);

                            _timer.Start();

                        }
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

            if (ques.typeQuestion == Question.QuestionType.FillInBlank)
            {
                FibCanvas = new FillInBlankCanvas(height, width);
                QuestionsCanvasTemplate.Children.Add(FibCanvas.BottomCanvas);
                FibCanvas.Tb1.Text = ques.QuestionText;
                FibCanvas.Tb1.IsReadOnly = true;

            }

            if (ques.typeQuestion == Question.QuestionType.TrueFalse)
            {
                tfCanvas = new TrueFalseCanvas(height, width);
                QuestionsCanvasTemplate.Children.Add(tfCanvas.BottomCanvas);
                TfCanvas.QuestionBox.Text = ques.QuestionText;
                tfCanvas.QuestionBox.IsReadOnly = true;
            }

            if (ques.typeQuestion == Question.QuestionType.MultipleChoice)
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
            }
        }

        public Question RandomQuestion(List<QuestionsDeck> deckList)
        {
            int deckNum = rnd.Next(deckList.Count);
            int questionNum = rnd.Next(deckList[deckNum].QuestionList.Count);
            return deckList[deckNum].QuestionList[questionNum];
        }

        //*******************************************************HERE****************************************************************************

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            Question temp = new Question();

            // shuffle the decks         
            for (int i = 1; i < QuizDecks.IncludedDecks.Count; i++)
            {
                int pos = rnd.Next(i + 1);
                var x = QuizDecks.IncludedDecks[i];
                QuizDecks.IncludedDecks[i] = QuizDecks.IncludedDecks[pos];
                QuizDecks.IncludedDecks[pos] = x;
            }


            // pick a deck from the collection
            Chosen = QuizDecks.IncludedDecks[0];


            // shuffle question in the selected deck
            for (int i = 1; i < Chosen.QuestionList.Count; i++)
            {
                int pos = rnd.Next(i + 1);
                var x = Chosen.QuestionList[i];
                Chosen.QuestionList[i] = Chosen.QuestionList[pos];
                temp = Chosen.QuestionList[pos] = x;
            }



            if (CurrPos <= QuizDecks.NumberOfQuestions)
            {

                CurrentQuestion.Content = Convert.ToString(CurrPos);
                DisplayQuestion(Chosen.QuestionList[0]); // display canvas

                // check if the question is correct or wrong
                /***********************************************************************************************************/
                if (temp.typeQuestion == Question.QuestionType.TrueFalse) // Check if true & false answer is right
                {
                    if (tfCanvas.ButtonTrue.IsChecked == true && temp.TFAnswers.CorrectAnswer)
                        Total_Correct++;
                    else if (tfCanvas.ButtonFalse.IsChecked == true && !temp.TFAnswers.CorrectAnswer)
                        Total_Correct++;

                }

                else if (temp.typeQuestion == Question.QuestionType.MultipleChoice) // check if MC answer choice is  correct
                {
                    int i = 0;
                    foreach (var Button in McCanvas.AnswerButtons)
                    {
                        if (Button.IsChecked == true)
                        {
                            if (i == temp.MCAnswers.CorrectAnswer)
                                Total_Correct++;
                        }
                        i++;
                    }
                }
                else if (temp.typeQuestion == Question.QuestionType.FillInBlank)
                {
                    if ((FibCanvas.Tb2.Text).Trim().ToLower() == (temp.FIBAnswers.CorrectAnswer).Trim().ToLower())
                        Total_Correct++;
                }
                else
                {
                    return;
                }

                /****************************************************************************************************************/
                CorrectNumber.Content = Total_Correct; // answer
                CurrPos++;   
            }

           // CorrectNumber.Content = Convert.ToString(CurrPos);
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

        private void finish_Click(object sender, RoutedEventArgs e)
        {
            // 1) calculate final score

        }

        double finalScore(int TotalCorrect)
        {
            return (TotalCorrect / QuizDecks.NumberOfQuestions) * 100;
        }
        /*************************************************************************/
    }
}
