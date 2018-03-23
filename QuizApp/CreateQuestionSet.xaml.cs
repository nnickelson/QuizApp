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

            fillInTheBlank.Visibility = Visibility.Visible;
            multipleChoice.Visibility = Visibility.Visible;
            trueFalse.Visibility = Visibility.Visible;
            QuestionNum.Visibility = Visibility.Visible;
            QuestionNum.Visibility = Visibility.Visible;
            AddQuestionToDeck.Visibility = Visibility.Visible;
            Finishedbutton.Visibility = Visibility.Visible;
        }

        private void fillInTheBlank_Clicked(object sender, RoutedEventArgs e)
        {
            chooseGrid("FillinBlank");
            question = new FillInBlank();
        }

        private void trueFalse_Clicked(object sender, RoutedEventArgs e)
        {
            chooseGrid("TrueFalse");
            question = new TrueFalse();
        }

        private void multipleChoice_Clicked(object sender, RoutedEventArgs e)
        {
            chooseGrid("MultipleChoice");
            question = new MultipleChoice();
        }

        private void finishedbutton_Click(object sender, ContextMenuEventArgs e)
        {

        }

        private void addQuestionToDeck_Clicked(object sender, RoutedEventArgs e)
        {

        }


        private void chooseGrid(string grid)
        {
            resetGridValues();
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
            FillinBlankGrid.Visibility = Visibility.Hidden;
            MultipleChoiceGrid.Visibility = Visibility.Hidden;
            TrueFalseGrid.Visibility = Visibility.Hidden;
            questionTextFIB.Text = "";
            questionTextMC.Text = "";
            questionTextTF.Text = "";
            answerText.Text = "";
            answerTextMC1.Text = "";
            answerTextMC2.Text = "";
            answerTextMC3.Text = "";
            answerTextMC4.Text = "";
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
