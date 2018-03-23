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
            Application.Current.MainWindow.Width = 700;
            Application.Current.MainWindow.Height = 700;
            this.QuestionsDeck = new QuestionsDeck();
        }

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

        private void fillInTheBlank_Clicked(object sender, RoutedEventArgs e)
        {
            chooseGrid("FillinBlank");
            this.question = new FillInBlank();
        }

        private void trueFalse_Clicked(object sender, RoutedEventArgs e)
        {
            chooseGrid("TrueFalse");
            this.question = new TrueFalse();
        }

        private void multipleChoice_Clicked(object sender, RoutedEventArgs e)
        {
            chooseGrid("MultipleChoice");
            this.question = new MultipleChoice();
        }

        private void finishedbutton_Click(object sender, ContextMenuEventArgs e)
        {

        }

        private void addQuestionToDeck_Clicked(object sender, RoutedEventArgs e)
        {
            if (question is FillInBlank)
                AddToDeck((FillInBlank)question);
            if (question is MultipleChoice)
                AddToDeck((MultipleChoice)question);
            if (question is TrueFalse)
                AddToDeck((TrueFalse)question);
            resetGridValues();
        }

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
        private void AddToDeck(MultipleChoice multipleChoiceQuestion)
        {

        }
        private void AddToDeck(TrueFalse trueFalseQuestion)
        {
            if (questionTextTF.Text == "")
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
