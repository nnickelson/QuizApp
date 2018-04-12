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
        QuestionsDeckJSON_IO quizReader;
        QuestionsDeck Chosen = new QuestionsDeck();

        private QuestionsDeck questionsDeck;
        private Question question;
        private FillInBlankCanvas fibCanvas;
        private TrueFalseCanvas tfCanvas;
        private MultipleChoiceCanvas mcCanvas;
        Random rnd = new Random();
        Question temp = new Question();
        Question lastQuestion = new Question();

        DispatcherTimer _timer;
        TimeSpan _time;

        int CurrPos = 1;
        int Total_Correct = 0;
        //double countdown;

        /// <summary>
        /// Window Constructor
        /// </summary>
        public Test()
        {
            InitializeComponent();
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
                FibCanvas.Tb1.IsReadOnly = true;
            }

            // Sets the QuestionsCanvasTemplate to True False Template Canvas
            // when a QuetionType.TrueFalse from the Question ques has been passed in
            if (ques.typeQuestion == Question.QuestionType.TrueFalse)
            {
                tfCanvas = new TrueFalseCanvas(height, width);
                QuestionsCanvasTemplate.Children.Add(tfCanvas.BottomCanvas);
                TfCanvas.QuestionBox.Text = ques.QuestionText;
                tfCanvas.QuestionBox.IsReadOnly = true;
            }

            // Sets the QuestionsCanvasTemplate to Multiple Choice Template Canvas
            // when a QuetionType.MultipleChoice from the Question ques has been passed in
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
            if (temp.typeQuestion == Question.QuestionType.TrueFalse) // Check if true & false answer is right
            {
                if (tfCanvas.ButtonTrue.IsChecked == true && temp.TFAnswers.CorrectAnswer)
                    Total_Correct++;
                else if (tfCanvas.ButtonFalse.IsChecked == true && !temp.TFAnswers.CorrectAnswer)
                    Total_Correct++;
            }

            //Check Multiple Choice asnwer
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

            // Check Fill In The Blank answer
            else if (temp.typeQuestion == Question.QuestionType.FillInBlank)
            {
                if ((FibCanvas.Tb2.Text).Trim().ToLower() == (temp.FIBAnswers.CorrectAnswer).Trim().ToLower())
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

            if (CurrPos == QuizDecks.NumberOfQuestions)
            {
                QuizFinished();
            }

            lastQuestion = temp;
            temp = Shuffle();
            while (lastQuestion.QuestionText == temp.QuestionText)
            {
                temp = Shuffle();
            }

            CurrentQuestion.Content = Convert.ToString(CurrPos);
            DisplayQuestion(temp); // display canvas
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

    /****************************************************************
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
                        

                        JSONquestion = File.ReadAllText(filePath);
                        //QuizDecks = ser.Deserialize<QuizSettings>(JSONquestion);                       
                    }
                    else
                    {
                        MessageBox.Show("Sorry, You can only select a Quiz Setting on this page. If you need to create a Quiz setting"
                            + " then go back to the Quiz settings page.", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            *****************************************************************************************/

    /***********************************************************************************************************
        private void fillInTheBlank_Clicked(object sender, RoutedEventArgs e)
        {
            QuestionsCanvasTemplate.Children.Clear();
            double height = QuestionsCanvasTemplate.ActualHeight;
            double width = QuestionsCanvasTemplate.ActualWidth;
            FibCanvas = new FillInBlankCanvas(height, width);
            QuestionsCanvasTemplate.Children.Add(FibCanvas.BottomCanvas);
        }

        private void trueFalse_Clicked(object sender, RoutedEventArgs e)
        {
            QuestionsCanvasTemplate.Children.Clear();
            double height = QuestionsCanvasTemplate.ActualHeight;
            double width = QuestionsCanvasTemplate.ActualWidth;
            TfCanvas = new TrueFalseCanvas(height, width);
            QuestionsCanvasTemplate.Children.Add(TfCanvas.BottomCanvas);
        }

        private void multipleChoice_Clicked(object sender, RoutedEventArgs e)
        {
            QuestionsCanvasTemplate.Children.Clear();
            double height = QuestionsCanvasTemplate.ActualHeight;
            double width = QuestionsCanvasTemplate.ActualWidth;
            McCanvas = new MultipleChoiceCanvas(height, width);
            QuestionsCanvasTemplate.Children.Add(McCanvas.BottomCanvas);
        }
        *****************************************************************************/
}
