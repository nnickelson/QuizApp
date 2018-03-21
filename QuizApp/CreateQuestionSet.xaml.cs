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
        private Question question;

        public CreateQuestionSet()
        {
            InitializeComponent();
            Finishedbutton.Visibility.Equals("Hidden");
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

            this.toggleScreenButtons();

            
        }

        private void toggleScreenButtons()
        {
            SubmitDeck.Visibility = Visibility.Hidden;
            Finishedbutton.Visibility = Visibility.Visible;
            QuestionNumBox.Visibility = Visibility.Visible;
            QuestionTextBox.Visibility = Visibility.Visible;
            AnswerTextBox.Visibility = Visibility.Visible;
            QuestionNum.Visibility = Visibility.Visible;
            QuestionText.Visibility = Visibility.Visible;
            AnswerText.Visibility = Visibility.Visible;
            AddQuestionToDeck.Visibility = Visibility.Visible;
        }

    }
}
