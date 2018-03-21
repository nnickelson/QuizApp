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
        private QuestionsDeck questionsDeck;
        private FillInBlank fillInBlankquestion;
        private TrueFalse trueFalseQuestion;
        private MultipleChoice multipleChoiceQuestion;

        

        public CreateQuestionSet()
        {
            InitializeComponent();
            this.QuestionsDeck = new QuestionsDeck();
            InitializeVisibility();
        }

        private void AddQuestionToDeck_Click(object sender, RoutedEventArgs e)
        {
           
            
        }

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
        }

        private void fillInTheBlank_Checked(object sender, RoutedEventArgs e)
        {
            fillInBlankquestion = new FillInBlank();
            toggleFillInBlankOptionsButtons();
        }









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

        private void addDeckButtonsAvailable()
        {
            AddQuestionToDeck.Visibility = Visibility.Visible;
            Finishedbutton.Visibility = Visibility.Visible;
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

        internal FillInBlank FillInBlankquestion
        {
            get
            {
                return fillInBlankquestion;
            }

            set
            {
                fillInBlankquestion = value;
            }
        }

        internal TrueFalse TrueFalseQuestion
        {
            get
            {
                return trueFalseQuestion;
            }

            set
            {
                trueFalseQuestion = value;
            }
        }

        internal MultipleChoice MultipleChoiceQuestion
        {
            get
            {
                return multipleChoiceQuestion;
            }

            set
            {
                multipleChoiceQuestion = value;
            }
        }



    }
}
