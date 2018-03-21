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
        /// Initialized the window and makes visible the choose a deck name option
        /// also initializes the QuestionsDeck
        /// </summary>
        public CreateQuestionSet()
        {
            InitializeComponent();
            this.QuestionsDeck = new QuestionsDeck();
            InitializeVisibility();
        }


        /// <summary>
        /// AddQuestionToDeck_Click
        /// Once the 'add' button is clicked in the window, this method calls
        /// the check question method for verification before adding the question to 
        /// the question deck. After adding the question, all the window's values 
        /// except the question number and the Question Deck name will be reset.
        /// </summary>
        /// <param name="sender">Button even handler</param>
        /// <param name="e">Button event handler</param>
        private void AddQuestionToDeck_Click(object sender, RoutedEventArgs e)
        {
            if (checkQuestion(this.Question))
            {
                questionsDeck.QuestionList.Add(this.Question);
            }
            resetValuesInWindow();

// ************* a check to see if the most recent question has been added properly to the deck. Edit out later **************************///
            MessageBox.Show(questionsDeck.QuestionList[questionsDeck.QuestionList.Count - 1].ToString());
            
        }


        /// <summary>
        /// SubmitDeckName method
        /// This method verifies the deck name chosen is not currently being used
        /// Also verifies that the Deck name string is not empty
        /// Once verified, it sets the deckname in the QuestionsDeck object
        /// and allows visibility of other options in the window
        /// </summary>
        /// <param name="sender">button event handler</param>
        /// <param name="e">button even handler</param>
        private void SubmitDeckName(object sender, RoutedEventArgs e)
        {
            if (DeckTitletextBox.Text == "")
            {
                throw new ArgumentException("Deck Name cannot be empty");
            }

            string filename = DeckTitletextBox.Text + ".json";
            if (File.Exists(filename))
            {
                throw new ArgumentException("That filename exists");
            }
            this.toggleQuestionChoiceVisibility();
            this.QuestionsDeck.DeckName = DeckTitletextBox.Text;
            QuestionNumBox.Text = Convert.ToString(questionsDeck.QuestionList.Count + 1);
        }

        /// <summary>
        /// fillInTheBlank_Checked method
        /// Initializes the question as a fill in the blank question
        /// toggles on the fill in the blank options of the window
        /// </summary>
        /// <param name="sender">Button event handler</param>
        /// <param name="e">Button even handler</param>
        private void fillInTheBlank_Checked(object sender, RoutedEventArgs e)
        {
            this.Question = new FillInBlank();
            toggleFillInBlankOptionsButtons();    
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        private bool checkQuestion(Question question)
        {
            if (question is FillInBlank)
            {
                return (FillInBlankCheck((FillInBlank)question)); 
            }
            if (question is TrueFalse)
            {

            }
            if (question is MultipleChoice)
            {

            }
            return true;
        }

        /// <summary>
        /// FillInBlanksCheck method
        /// This method checks to see if all textboxes that need to be
        /// populated are populated. It then fills the in the fields for the
        /// FillInBlank class object.
        /// </summary>
        /// <param name="question">FillInBlank object that is not null</param>
        /// <returns>TRUE if all fields have been filled, FALSE otherwise</returns>
        private bool FillInBlankCheck(FillInBlank question)
        {
            if (QuestionTextBox.Text == "")
            {
                MessageBox.Show("There is no question in the question box!");
                return false;
            }
            else if (AnswerTextBox.Text == "")
            {
                MessageBox.Show("There is no answer in the answer box");
                return false;
            }
            question.QuestionText = QuestionTextBox.Text;
            question.CorrectAnswer = AnswerTextBox.Text;
            return true;
        }

        /// <summary>
        /// Resets the values in all windows  ************will need to be updated as more types of questions are added*******
        /// </summary>
        private void resetValuesInWindow()
        {
            QuestionTextBox.Text = String.Empty;
            AnswerTextBox.Text = String.Empty;
            QuestionNumBox.Text = Convert.ToString(questionsDeck.QuestionList.Count + 1);
        }
        
        /// <summary>
        /// Checks fill in the blank text boxes for values before allowing
        /// showing the 'Add' and 'Finished' Buttons
        /// </summary>
        /// <param name="sender">button handler</param>
        /// <param name="e">button handler</param>
        private void QuestionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (QuestionTextBox.Text == "" || AnswerTextBox.Text == "")
            {
                AddDeckButtonsHide();
            }
            else
            {
                addDeckButtonsShow();
            }
        }




        /// <summary>
        /// Buttons and Text Boxes visibility Toggles
        /// </summary>
        private void toggleQuestionChoiceVisibility()
        {
            multipleChoice.Visibility = Visibility.Visible;
            trueFalse.Visibility = Visibility.Visible;
            fillInTheBlank.Visibility = Visibility.Visible;
        }

        private void toggleFillInBlankOptionsButtons()
        {
            SubmitDeck.Visibility = Visibility.Hidden;
            QuestionNumBox.Visibility = Visibility.Visible;
            QuestionTextBox.Visibility = Visibility.Visible;
            AnswerTextBox.Visibility = Visibility.Visible;
            QuestionNum.Visibility = Visibility.Visible;
            QuestionText.Visibility = Visibility.Visible;
            AnswerText.Visibility = Visibility.Visible;  
        }

        private void addDeckButtonsShow()
        {
            AddQuestionToDeck.Visibility = Visibility.Visible;
            Finishedbutton.Visibility = Visibility.Visible;
        }

        private void AddDeckButtonsHide()
        {
            AddQuestionToDeck.Visibility = Visibility.Hidden;
            Finishedbutton.Visibility = Visibility.Hidden;
        }

        private void InitializeVisibility()
        {
            SubmitDeck.Visibility = Visibility.Visible;
            Finishedbutton.Visibility = Visibility.Hidden;
            QuestionNumBox.Visibility = Visibility.Hidden;
            QuestionTextBox.Visibility = Visibility.Hidden;
            AnswerTextBox.Visibility = Visibility.Hidden;
            QuestionNum.Visibility = Visibility.Hidden;
            QuestionText.Visibility = Visibility.Hidden;
            AnswerText.Visibility = Visibility.Hidden;
            AddQuestionToDeck.Visibility = Visibility.Hidden;
            fillInTheBlank.Visibility = Visibility.Hidden;
            trueFalse.Visibility = Visibility.Hidden;
            multipleChoice.Visibility = Visibility.Hidden;
        }


        /// <summary>
        /// Properties Section
        /// </summary>
        internal QuestionsDeck QuestionsDeck
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
