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

namespace QuizApp
{
    public partial class CreateQuestionSet: Page
    {

        /// <summary>
        /// Private Backing Fields
        /// </summary>
        private QuestionsDeck questionsDeck;
        private Question question;
        private FillInBlankCanvas fibCanvas;
        private TrueFalseCanvas tfCanvas;
        private MultipleChoiceCanvas mcCanvas;
        private bool misClick;
        QuestionsDeckJSON_IO fileOperator = new QuestionsDeckJSON_IO();


        /// <summary>
        /// CreateQuestionSet method
        /// Initializes and resizes the window and makes visible the choose a deck name option
        /// also initializes the QuestionsDeck
        /// </summary>
        public CreateQuestionSet()
        {
            InitializeComponent();
            editCreateChoice.Width = Application.Current.MainWindow.ActualWidth;
            editCreateChoice.Height = Application.Current.MainWindow.ActualHeight;
            addEditCreateButtons();
            misClick = false;
           
        }

        /// <summary>
        /// submitDeck_Clicked
        /// Button Hnadler. When user clicks the Submit Deck button,
        /// the handler verifies values are in the the text box to assign
        /// the the questionsDeck.DeckName. Shows message box for an
        /// empty string
        /// </summary>
        /// <param name="sender">button handler</param>
        /// <param name="e">button handler</param>
        private void submitDeck_Clicked(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(Convert.ToString(fileOperator.IsValidFilename(DeckTitletextBox.Text)));
            if (DeckTitletextBox.Text != "" && fileOperator.IsValidFilename(DeckTitletextBox.Text))
            {
                QuestionsDeck.DeckName = (DeckTitletextBox.Text).Trim();
                setVisiblebuttons();
                SubmitDeck.Visibility = Visibility.Hidden;
                DeckTitletextBox.IsReadOnly = true;

            }
            else
            {
                MessageBox.Show("Sorry, you entered an invalid deck name. Please try again", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);
                DeckTitletextBox.Text = "";
            }
        }


        //Button Handlers//


        private void AddQuestions_btnClick()
        {
            QuestionsDeck = fileOperator.ReadDeck();
            if (QuestionsDeck.DeckName == null)
                return;
            //MessageBox.Show("*****" + QuestionsDeck.DeckName + "****");
            editCreateChoice.Visibility = Visibility.Hidden;
            SubmitDeck.Visibility = Visibility.Hidden;
            DeckTitletextBox.IsReadOnly = true;
            setVisiblebuttons();
        }

        private void NewDeck_BtnClick()
        {
            QuestionsDeck = new QuestionsDeck();
            editCreateChoice.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// fillInTheBlank_Clicked Method
        /// Adds a Fillin the Blank canvas to the grid area
        /// initializes a new fill in the blank question
        /// </summary>
        /// <param name="sender">button handler</param>
        /// <param name="e">button handler</param>
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
                this.question = new FillInBlank { QuestionType = "Fill In Blank"};
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
                this.question = new TrueFalse { QuestionType = "true false"};
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
                this.question = new MultipleChoice{ QuestionType = "multiple choice" };
            }
        }


        /// <summary>
        /// finishedbutton_Click Method
        /// checks to see that a deck has been named and has at least one question
        /// If so, then the deck is passed to a method to read into a JSON file
        /// if not, a message box appears and the method is returned
        /// </summary>
        /// <param name="sender">button handler</param>
        /// <param name="e">button handler</param>
        
        private void finishedbutton_Click(object sender, RoutedEventArgs e)
        {
            if (DeckTitletextBox.Text == "")
            {
                MessageBox.Show("The deck doesn't have a name");
                return;
            }
            else
            if (QuestionsDeck.QuestionList.Count < 1)
            {
                MessageBox.Show("There are no questions in the deck. Add a question first.");
                return;
            }
            else
            {
                fileOperator.WriteQuestionsDeck(QuestionsDeck);   
            }

            NavigationService.Navigate(new Uri("/DeckBuilder.xaml", UriKind.Relative));
        }



        /// <summary>
        /// addQuestionToDeck_Clicked Method
        /// calls the appropriate method to add a question to a deck
        /// resets the grids only if a question has been added to the deck
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addQuestionToDeck_Clicked(object sender, RoutedEventArgs e)
        {
            if (question is FillInBlank)
            {
                AddToDeck((FillInBlank)question);
                fillInTheBlank_Clicked(sender, e);
            }
            if (question is MultipleChoice)
            {
                AddToDeck((MultipleChoice)question);
                multipleChoice_Clicked(sender, e);
            }
            if (question is TrueFalse)
            {
                AddToDeck((TrueFalse)question);
                trueFalse_Clicked(sender, e);
            }

        }

        /// <summary>
        /// AddToDeck Method - Fill In The Blank
        /// This method checks to see if the question text boxe and answer text
        /// box have some values in them. If both are true, the question is added
        /// to the questions deck, otherwise the mislcick is set to false and the method is returned
        /// </summary>
        /// <param name="fillinBlankQuestion">type of FillInBlank</param>
        private void AddToDeck(FillInBlank fillinBlankQuestion)
        {
            if (FibCanvas.Tb1.Text == "" || FibCanvas.Tb2.Text == "")
            {
                misClick = true;
                return;
            }
            else
            {
                fillinBlankQuestion.QuestionText = FibCanvas.Tb1.Text;
                fillinBlankQuestion.CorrectAnswer = FibCanvas.Tb2.Text;
                this.questionsDeck.QuestionList.Add(fillinBlankQuestion);
                QuestionNumBox.Text = Convert.ToString(questionsDeck.QuestionList.Count + 1);
            }
        }

        /// <summary>
        /// AddToDeck Method - Multiple Choice
        /// This method checks to see if the question text is filled, at least 2
        /// answer boxes have text in them, and one of those text boxes has its
        /// radio button checked. If all are true, then the multiple question
        /// is added to the question deck, otherwise misclick is set to false and 
        /// the method returns until all the checks pass
        /// </summary>
        /// <param name="multipleChoiceQuestion">type of MultipleChoice</param>
        private void AddToDeck(MultipleChoice multipleChoiceQuestion)
        {
            bool answerSet = false;
            foreach (TextBox answer in McCanvas.AnswerBoxes)
            {
                int answerNum = McCanvas.AnswerBoxes.IndexOf(answer);
                if (answer.Text != "")
                {
                    multipleChoiceQuestion.Choices.Add(answer.Text);
                    if (McCanvas.AnswerButtons[answerNum].IsChecked == true)
                    {
                        multipleChoiceQuestion.CorrectAnswer = multipleChoiceQuestion.Choices.Count - 1;
                        answerSet = true;
                    }
                }
            }
            if (McCanvas.QuestionBox.Text == "" || multipleChoiceQuestion.Choices.Count < 2 || answerSet == false)
            {
                misClick = true;
                multipleChoiceQuestion.Choices.Clear();
                return;
            }
            multipleChoiceQuestion.QuestionText = McCanvas.QuestionBox.Text;
            this.questionsDeck.QuestionList.Add(multipleChoiceQuestion);
            QuestionNumBox.Text = Convert.ToString(questionsDeck.QuestionList.Count + 1);

        }

        /// <summary>
        /// AddToDeck Method - True False
        /// Checks to see if the question text has a value and one of the radio buttons
        /// has been checked. If so, then the question is added to the questions deck, 
        /// otherwise misclick is set to false and the method is returned.
        /// </summary>
        /// <param name="trueFalseQuestion">type of TrueFalse</param>
        private void AddToDeck(TrueFalse trueFalseQuestion)
        {
            if (TfCanvas.QuestionBox.Text == "" || (TfCanvas.ButtonTrue.IsChecked == false && TfCanvas.ButtonFalse.IsChecked == false))
            {
                misClick = true;
                return;
            }
            else
            {
                trueFalseQuestion.QuestionText = TfCanvas.QuestionBox.Text;
                if (TfCanvas.ButtonTrue.IsChecked == true)
                {
                    //MessageBox.Show("true");
                    trueFalseQuestion.CorrectAnswer = true;
                }
                if (TfCanvas.ButtonFalse.IsChecked == true)
                {
                    //MessageBox.Show("false");
                    trueFalseQuestion.CorrectAnswer = false;
                }
                this.questionsDeck.QuestionList.Add(trueFalseQuestion);
                QuestionNumBox.Text = Convert.ToString(questionsDeck.QuestionList.Count + 1);
            }

        }

        

        /// <summary>
        /// setVisibilebuttons Method
        /// Sets visibility to visible of some buttons once a deck name has been submitted
        /// </summary>
        private void setVisiblebuttons()
        {
            fillInTheBlank.Visibility = Visibility.Visible;
            multipleChoice.Visibility = Visibility.Visible;
            trueFalse.Visibility = Visibility.Visible;
            QuestionNum.Visibility = Visibility.Visible;
            QuestionNumBox.Visibility = Visibility.Visible;
            AddQuestionToDeck.Visibility = Visibility.Visible;
            Finishedbutton.Visibility = Visibility.Visible;
            QuestionNumBox.Text = Convert.ToString(QuestionsDeck.QuestionList.Count + 1);
            DeckTitletextBox.Text = QuestionsDeck.DeckName;
            
        }

        /// <summary>
        /// addEditCreateButtons
        /// Hard coded template which makes edit deck and new deck buttons
        /// and gives them a function
        /// </summary>
        private void addEditCreateButtons()
        {
            Button editDeckButton = new Button{ Content = "Edit Deck"};
            Button newDeckButton = new Button { Content = "New Deck" };

            editDeckButton.Click += delegate
            {
                AddQuestions_btnClick();
            };

            newDeckButton.Click += delegate
            {
                NewDeck_BtnClick();
            };

            Canvas.SetTop(editDeckButton, editCreateChoice.Height * (0.33));
            Canvas.SetLeft(editDeckButton, editCreateChoice.Width * (0.25));

            editDeckButton.Width = editCreateChoice.Width * (0.25);
            editDeckButton.Height = editCreateChoice.Height * (0.33);

            Canvas.SetTop(newDeckButton, editCreateChoice.Height * (0.33));
            Canvas.SetLeft(newDeckButton, editCreateChoice.Width * (0.50));

            newDeckButton.Width = editCreateChoice.Width * (0.25);
            newDeckButton.Height = editCreateChoice.Height * (0.33);

            editCreateChoice.Children.Add(editDeckButton);
            editCreateChoice.Children.Add(newDeckButton);
        }

        /// <summary>
        /// Properties Section
        /// </summary>
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
}
