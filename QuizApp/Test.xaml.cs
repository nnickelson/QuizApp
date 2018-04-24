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
using System.Windows.Media;

namespace QuizApp
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Page
    {

  

        private QuizSettings quizDecks;
        private QuestionsDeck chosen;
        private Question temp;
        private Question lastQuestion;
        //private QuestionsDeck questionsDeck;
        //private Question question;


        private FillInBlankCanvas fibCanvas;
        private TrueFalseCanvas tfCanvas;
        private MultipleChoiceCanvas mcCanvas;
        private Random rnd = new Random();
        private QuestionsDeckJSON_IO quizReader;

        DispatcherTimer _timer;
        TimeSpan _time;

        private int CurrPos;
        private int Total_Correct;
        //double countdown;

        /// <summary>
        /// Window Constructor
        /// </summary>
        public Test()
        {
            InitializeComponent();
            CurrPos = 1;
            Total_Correct = 0;
        }

        

        /// <summary>
        /// SelectADeckbtn_Click
        /// This is a button handler event making the user select a QuizSettings JSON
        /// file  and having it reaad into a QuizSettings object
        /// before clearing the TopLayer and revealing the actual quiz canvas
        /// underneath. It also copies various fields from the QuizSettings to the
        /// appropriate textblocks and boxes in the quizcanvas
        /// </summary>
        /// <param name="sender">button handler</param>
        /// <param name="e">button handler</param>
        private void SelectADeckbtn_Click(object sender, RoutedEventArgs e)
        {
            quizReader = new QuestionsDeckJSON_IO();
            QuizDecks = quizReader.ReadQuizSettings();
            if (QuizDecks.QuizName == null || QuizDecks.QuizName == "") { return; }
            SelectADeckbtn.Visibility = Visibility.Hidden;
            TopLayer.Visibility = Visibility.Hidden;
            // Get total questions
            TotalQuestions.Content = QuizDecks.NumberOfQuestions;

            // Get the quiz name
            TestName.Text = QuizDecks.QuizName;

            // Set Timer
            SetTimer(sender, e);

            NextBtn_Click(sender, e);
        }

        /// <summary>
        /// Set Timer
        /// Starts the timer if there is one once the Quiz Setting has been selected
        /// </summary>
        /// <param name="sender">button handler</param>
        /// <param name="e">button handler</param>
        private void SetTimer(object sender, RoutedEventArgs e)
        {
            // If the quiz is timed then start the timer
            if (QuizDecks.IsTimed)
            {
                tb.Visibility = Visibility.Visible;
                _time = TimeSpan.FromMinutes(QuizDecks.QuizMinutes);

                _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                {
                    {
                        tb.Text = _time.ToString("c");
                        if (_time == TimeSpan.Zero)
                        {
                            _timer.Stop();
                            QuizFinished();
                        }
                        _time = _time.Add(TimeSpan.FromSeconds(-1));
                    }
                }, Application.Current.Dispatcher);
                _timer.Start();
            }
            return;
        }

        /// <summary>
        /// DisplayQuestion method
        /// This method sets the QuestionCanvasTemplate to match the
        /// type of question passed in. It the fills in the question
        /// text box and the appropriate answer template for True False,
        /// Multiple Choice and Fill In The Blank questions
        /// </summary>
        /// <param name="ques">type of Question, cannot be null and needs a QuestionType</param>
        void DisplayQuestion(Question ques)
        {
            QuestionsCanvasTemplate.Children.Clear();
            double height = QuestionsCanvasTemplate.ActualHeight;
            double width = QuestionsCanvasTemplate.ActualWidth;

            // Sets the QuestionsCanvasTemplate to Fill In The Blank Template Canvas
            // when a QuetionType.FillInBlank from the Question ques has been passed in
            if (ques.typeQuestion == Question.QuestionType.FillInBlank)
            {
                FibCanvas = new FillInBlankCanvas(height, width);
                QuestionsCanvasTemplate.Children.Add(FibCanvas.BottomCanvas);
                FibCanvas.Tb1.Text = ques.QuestionText;

                FibCanvas.Tb1.Background = new SolidColorBrush(Color.FromRgb(246, 246, 246));
                FibCanvas.Tb1.BorderThickness = new System.Windows.Thickness(0);
                FibCanvas.Tb1.IsReadOnly = true;
                
            }

            // Sets the QuestionsCanvasTemplate to True False Template Canvas
            // when a QuetionType.TrueFalse from the Question ques has been passed in
            if (ques.typeQuestion == Question.QuestionType.TrueFalse)
            {
                tfCanvas = new TrueFalseCanvas(height, width);
                QuestionsCanvasTemplate.Children.Add(tfCanvas.BottomCanvas);
                TfCanvas.QuestionBox.Text = ques.QuestionText;

                TfCanvas.QuestionBox.Background = new SolidColorBrush(Color.FromRgb(246, 246, 246));

                TfCanvas.QuestionBox.BorderThickness = new System.Windows.Thickness(0);
                tfCanvas.QuestionBox.IsReadOnly = true;
            }

            // Sets the QuestionsCanvasTemplate to Multiple Choice Template Canvas
            // when a QuetionType.MultipleChoice from the Question ques has been passed in
            if (ques.typeQuestion == Question.QuestionType.MultipleChoice)
            {
                McCanvas = new MultipleChoiceCanvas(height, width);
                QuestionsCanvasTemplate.Children.Add(McCanvas.BottomCanvas);
                McCanvas.QuestionBox.Text = ques.QuestionText;
                McCanvas.QuestionBox.BorderThickness = new System.Windows.Thickness(0);
                McCanvas.QuestionBox.IsReadOnly = true;

                McCanvas.QuestionBox.Background = new SolidColorBrush(Color.FromRgb(246, 246, 246));

                for (int i = 0; i < 5; i++)
                {
                    if (i < ques.MCAnswers.Choices.Count)
                    {
                        McCanvas.AnswerBoxes[i].Text = ques.MCAnswers.Choices[i];
                        McCanvas.AnswerBoxes[i].BorderThickness = new System.Windows.Thickness(0);
                        McCanvas.AnswerBoxes[i].IsReadOnly = true;
                        McCanvas.AnswerBoxes[i].Background = new SolidColorBrush(Color.FromRgb(246, 246, 246));
                    }
                    else
                    {
                        McCanvas.AnswerBoxes[i].Visibility = Visibility.Hidden;
                        McCanvas.AnswerButtons[i].Visibility = Visibility.Hidden;
                        McCanvas.AnswerBoxes[i].BorderThickness = new System.Windows.Thickness(0);
                        McCanvas.AnswerBoxes[i].IsReadOnly = true;
                        McCanvas.AnswerBoxes[i].Background = new SolidColorBrush(Color.FromRgb(246, 246, 246));
                    }
                }
            }
        }


        /// <summary>
        /// Shuffle Method
        /// This method takes the existing included decks in Quiz Settings
        /// and shuffles them. Once they've been shuffled, a Question is returned
        /// </summary>
        /// <returns>Quedtion: Chosen.QuestionList[pos]</returns>
        public Question Shuffle()
        {
            int pos = 0;
            // shuffle the decks         
            for (int i = 1; i < QuizDecks.IncludedDecks.Count; i++)
            {
                pos = rnd.Next(i + 1);
                var x = QuizDecks.IncludedDecks[i];
                QuizDecks.IncludedDecks[i] = QuizDecks.IncludedDecks[pos];
                QuizDecks.IncludedDecks[pos] = x;
            }

            // pick a deck from the collection
            Chosen = QuizDecks.IncludedDecks[0];

            // shuffle question in the selected deck
            for (int i = 1; i < Chosen.QuestionList.Count; i++)
            {
                pos = rnd.Next(i + 1);
                var x = Chosen.QuestionList[i];
                Chosen.QuestionList[i] = Chosen.QuestionList[pos];
                Chosen.QuestionList[pos] = x;
            }

            return Chosen.QuestionList[pos];
        }

        /// <summary>
        /// CheckAnswer Method
        /// This checks the radio burttons or blanks for the answer given
        /// by the user. It then compares these answers to the answer stored
        /// in the correct answer of the Question object. If the answers are
        /// equal then Total_Correct is incremented. 
        /// </summary>
        private void CheckAnswer ()
        {
            //Check True False answer
            if (Temp.typeQuestion == Question.QuestionType.TrueFalse) // Check if true & false answer is right
            {
                if (tfCanvas.ButtonTrue.IsChecked == true && Temp.TFAnswers.CorrectAnswer)
                    Total_Correct++;
                else if (tfCanvas.ButtonFalse.IsChecked == true && !Temp.TFAnswers.CorrectAnswer)
                    Total_Correct++;
            }

            //Check Multiple Choice asnwer
            else if (Temp.typeQuestion == Question.QuestionType.MultipleChoice) // check if MC answer choice is  correct
            {
                int i = 0;
                foreach (var Button in McCanvas.AnswerButtons)
                {
                    if (Button.IsChecked == true)
                    {
                        if (i == Temp.MCAnswers.CorrectAnswer)
                            Total_Correct++;
                    }
                    i++;
                }
            }

            // Check Fill In The Blank answer
            else if (Temp.typeQuestion == Question.QuestionType.FillInBlank)
            {
                if ((FibCanvas.Tb2.Text).Trim().ToLower() == (Temp.FIBAnswers.CorrectAnswer).Trim().ToLower())
                    Total_Correct++;
            }
        }

        /// <summary>
        /// NextBtn_Click Method
        /// A button handler method that calls for checking the answer
        /// of the current question, checks to see if it is the end of the 
        /// quiz, if so, the QuizFinished method is called
        /// if not, DisplayQuestion is called for the next question
        /// CurrPos in incremented
        /// </summary>
        /// <param name="sender">button handler</param>
        /// <param name="e">button handler</param>
        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if ( CurrPos - 1 != 0 )
            {   
                CheckAnswer();
                CorrectNumber.Content = Total_Correct; // answer
            }

            if (CurrPos-1 == QuizDecks.NumberOfQuestions)
            {
                QuizFinished();
            }
            
            if (CurrPos - 1 > 1)
            {
                int x = 0;
                LastQuestion = Temp;
                Temp = Shuffle();
                while (LastQuestion.QuestionText == Temp.QuestionText)
                {
                    Temp = Shuffle();
                    x++; if (x > 3) break;
                }
            }
            else
            {
                Temp = Shuffle();
            }

            CurrentQuestion.Content = Convert.ToString(CurrPos);
            DisplayQuestion(Temp); // display canvas
            CurrPos++;       
        }

        

        double finalScore()
        {
            return Math.Round((Convert.ToDouble(Total_Correct) / Convert.ToDouble(QuizDecks.NumberOfQuestions)) * Convert.ToDouble(100), 1);
        }

        private void QuizFinished ()//(object sender, RoutedEventArgs e)
        {
            ResultLayer.Visibility = Visibility.Visible;
            Score.Content = finalScore();
        }


        //*************************** PAGE NAVIGATION BUTTON HANDLERS ***************************/

        private void ReturnHome_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Home.xaml", UriKind.Relative));
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

        //**************************** PUBLIC PROPERTIES SECTION *****************************/


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

        public QuizSettings QuizDecks
        {
            get
            {
                return quizDecks;
            }

            set
            {
                quizDecks = value;
            }
        }

        public QuestionsDeck Chosen
        {
            get
            {
                return chosen;
            }

            set
            {
                chosen = value;
            }
        }

        public Question Temp
        {
            get
            {
                return temp;
            }

            set
            {
                temp = value;
            }
        }

        public Question LastQuestion
        {
            get
            {
                return lastQuestion;
            }

            set
            {
                lastQuestion = value;
            }
        }
    }

    /******************************************* NO REFERENCES TO THIS CODE ... POSSIBLE REMOVAL ?
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

       public Question RandomQuestion(List<QuestionsDeck> deckList)
       {
           int deckNum = rnd.Next(deckList.Count);
           int questionNum = rnd.Next(deckList[deckNum].QuestionList.Count);
           return deckList[deckNum].QuestionList[questionNum];
       }
       *****************************************************************************************/

  
}
