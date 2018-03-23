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
            chooseGrid("FillinBlank");
            this.question = new FillInBlank();
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
            chooseGrid("TrueFalse");
            this.question = new TrueFalse();
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
            chooseGrid("MultipleChoice");
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
            int tempCount = QuestionsDeck.QuestionList.Count;
            if (question is FillInBlank)
                AddToDeck((FillInBlank)question);
            if (question is MultipleChoice)
                AddToDeck((MultipleChoice)question);
            if (question is TrueFalse)
                AddToDeck((TrueFalse)question);
            if (tempCount < QuestionsDeck.QuestionList.Count)
                resetGridValues();
        }

        /// <summary>
        /// AddToDeck Method
        /// adds a fill in the blank question to the questionsDeck if the approrpiate fields have values
        /// returns if not
        /// </summary>
        /// <param name="fillinBlankQuestion">type of FillInBlank</param>
        private void AddToDeck(FillInBlank fillinBlankQuestion)
        {
            if (questionTextFIB.Text == "" || answerText.Text == "")
            {
                return;
            }
            else
            {
                fillinBlankQuestion.QuestionText = questionTextFIB.Text;
                fillinBlankQuestion.CorrectAnswer = answerText.Text;
                this.questionsDeck.QuestionList.Add(fillinBlankQuestion);
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
            if (questionTextTF.Text == "" || (trueRadioBtn.IsChecked == false && falseRadioBtn.IsChecked == false))
            {
                return;
            }
            else
            {
                trueFalseQuestion.QuestionText = questionTextTF.Text;
                if (trueRadioBtn.IsChecked == true)
                {
                    MessageBox.Show("true");
                    trueFalseQuestion.CorrectAnswer = true;
                }
                if (falseRadioBtn.IsChecked == true)
                {
                    MessageBox.Show("false");
                    trueFalseQuestion.CorrectAnswer = false;
                }
                this.questionsDeck.QuestionList.Add(trueFalseQuestion);
            }

        }

        /// <summary>
        /// chooseGrid Method
        /// Sets the approriate grid to 'visible' based on the passed in string
        /// </summary>
        /// <param name="grid">string value</param>
        private void chooseGrid(string grid)
        {
            resetGridValues();
            FillinBlankGrid.Visibility = Visibility.Hidden;
            MultipleChoiceGrid.Visibility = Visibility.Hidden;
            TrueFalseGrid.Visibility = Visibility.Hidden;
            if (grid == "FillinBlank")
            {
                FillinBlankGrid.Visibility = Visibility.Visible;
            }
            if (grid == "MultipleChoice")
            {
                MultipleChoiceGrid.Visibility = Visibility.Visible;
            }
            if (grid == "TrueFalse")
            {
                TrueFalseGrid.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// resetGridValues Method
        /// resets the text box values of all grid text boxes to an empty string
        /// </summary>
        private void resetGridValues()
        {   
            questionTextFIB.Text = "";
            questionTextMC.Text = "";
            questionTextTF.Text = "";
            answerText.Text = "";
            answerTextMC1.Text = "";
            answerTextMC2.Text = "";
            answerTextMC3.Text = "";
            answerTextMC4.Text = "";
            QuestionNumBox.Text = Convert.ToString(this.questionsDeck.QuestionList.Count + 1);
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

        
    }
}
