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
        private bool misClick;

        
        /// <summary>
        /// CreateQuestionSet method
        /// Initializes and resizes the window and makes visible the choose a deck name option
        /// also initializes the QuestionsDeck
        /// </summary>
        public CreateQuestionSet()
        {
            InitializeComponent();
            Application.Current.MainWindow.Width = 700;
            Application.Current.MainWindow.Height = 650;
            misClick = false;
            this.QuestionsDeck = new QuestionsDeck();
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
            if (DeckTitletextBox.Text != "")
            {
                QuestionsDeck.DeckName = DeckTitletextBox.Text;
            }
            else
            {
                MessageBox.Show("Deck must have a name");
                return;
            }
            setVisiblebuttons();
        }

        //Button Handlers//

        /// <summary>
        /// fillInTheBlank_Clicked Method
        /// Sets the Fill in the blank grid to visible
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
                this.question = new FillInBlank();
            }
        }

        /// <summary>
        /// trueFalse_Clicked Method
        /// Sets the True False Grid to visible
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
                this.question = new TrueFalse();
            }
        }

        /// <summary>
        /// multipleChoice_Clicked Method
        /// Sets the Multiple choice grid to visible
        /// Initializes a new Multiple Choice questions
        /// </summary>
        /// <param name="sender">button handler</param>
        /// <param name="e">button handler</param>
        private void multipleChoice_Clicked(object sender, RoutedEventArgs e)
        {

            this.question = new MultipleChoice();
        }

        /// <summary>
        /// ************************************************************************************************UNFINISHED METHOD
        /// </summary>
        /// <param name="sender">button handler</param>
        /// <param name="e">button handler</param>
        private void finishedbutton_Click(object sender, ContextMenuEventArgs e)
        {

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
                AddToDeck((MultipleChoice)question);
            if (question is TrueFalse)
            {
                AddToDeck((TrueFalse)question);
                trueFalse_Clicked(sender, e);
            }

        }

        /// <summary>
        /// AddToDeck Method
        /// adds a fill in the blank question to the questionsDeck if the approrpiate fields have values
        /// returns if not
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
        /// ************************************************************************************************UNFINISHED METHOD
        /// </summary>
        /// <param name="multipleChoiceQuestion">type of MultipleChoice</param>
        private void AddToDeck(MultipleChoice multipleChoiceQuestion)
        {

        }

        /// <summary>
        /// AddToDeck Method
        /// Adds a truefalse question to a questionsDeck if the appropriate fields have values
        /// returns if not
        /// </summary>
        /// <param name="trueFalseQuestion">type of TrueFalse</param>
        private void AddToDeck(TrueFalse trueFalseQuestion)
        {
            if (TfCanvas.Tb1.Text == "" || (TfCanvas.ButtonTrue.IsChecked == false && TfCanvas.ButtonFalse.IsChecked == false))
            {
                misClick = true;
                return;
            }
            else
            {
                trueFalseQuestion.QuestionText = TfCanvas.Tb1.Text;
                if (TfCanvas.ButtonTrue.IsChecked == true)
                {
                    MessageBox.Show("true");
                    trueFalseQuestion.CorrectAnswer = true;
                }
                if (TfCanvas.ButtonFalse.IsChecked == true)
                {
                    MessageBox.Show("false");
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
            QuestionNumBox.Text = Convert.ToString(this.questionsDeck.QuestionList.Count + 1);
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
    }
}
