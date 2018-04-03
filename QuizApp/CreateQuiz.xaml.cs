using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace QuizApp
{
    /// <summary>
    /// Interaction logic for CreateQuiz.xaml
    /// </summary>
    public partial class CreateQuiz : Page
    {
        private double height;
        private double width;
        private QuizSettings quizSettings;
        QuestionsDeckJSON_IO fileOperator = new QuestionsDeckJSON_IO();

        /// <summary>
        /// CreateQuiz Constructor
        /// </summary>
        public CreateQuiz()
        {
            InitializeComponent();
            Height1 = Application.Current.MainWindow.ActualHeight;
            Width1 = Application.Current.MainWindow.ActualWidth;
            CreateQuizTabControl.Width = Width1;
            CreateQuizTabControl.Height = Height1 * (0.85);
            NumberOfMinutesTextBox.Visibility = Visibility.Hidden;
            SetFontSize();
            QuizSettings = new QuizSettings();
        }

        
        /// <summary>
        /// SetFontSize
        /// Adjusts the fontsize of Xaml textboxes and text blocks
        /// </summary>
        public void SetFontSize()
        {
            //MessageBox.Show(Convert.ToString(QuizSettingsTab.Height));
            double textHeight = Height1 / 25;
            TextBlock1.FontSize = textHeight;
            TextBlock2.FontSize = textHeight;
            TextBlock3.FontSize = textHeight;
            TextBlock4.FontSize = textHeight;
            QuizNameTextBox.FontSize = textHeight;
            NumberOfQuestionsTextBox.FontSize = textHeight;
            NumberOfMinutesTextBox.FontSize = textHeight;
            YesButton.FontSize = textHeight * (0.75);
            NoButton.FontSize = textHeight * (0.75);
        }
        
        /// <summary>
        /// YesButton_Click
        /// Toggles on the Yes Button and toggles off the No Button
        /// Also sets Number of Minutes TextBox to visible
        /// </summary>
        /// <param name="sender">button handler</param>
        /// <param name="e">button handler</param>
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            if (NoButton.IsChecked == true)
            {
                NoButton.IsChecked = false;                
            }
            NumberOfMinutesTextBox.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Toggles on the No Button and toggles off the Yes Button
        /// Also deletes the value in Number of Minutes TextBox and
        /// sets it to invisible
        /// </summary>
        /// <param name="sender">button handler</param>
        /// <param name="e">button handler</param>
        private void NoButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (YesButton.IsChecked == true)
            {
                YesButton.IsChecked = false;
            }
            NumberOfMinutesTextBox.Text = "";
            NumberOfMinutesTextBox.Visibility = Visibility.Hidden;
        }

        //**********************UNFINISHED************************************************//
        //*********************Still needs checking for decks verification ***************//

        /// <summary>
        /// SubmitQuizButton_Click
        /// Checks all the textbox entries for valid values
        /// If all is valid, sets the quiz settings and stores them
        /// in a JSON file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitQuizButton_Click(object sender, RoutedEventArgs e)
        {
            int parsedValue;

            // Checks QuizSettings Name for validity
            if (fileOperator.IsValidFilename(QuizNameTextBox.Text) && QuizNameTextBox.Text != "")
            {
                QuizSettings.QuizName = QuizNameTextBox.Text;
            }
            else
            {
                MessageBox.Show("Your Quiz needs a name.");
                return;
            }

            // Checks if Number of Minutes is a valid integer
            if (!int.TryParse(NumberOfQuestionsTextBox.Text, out parsedValue))
            {
                MessageBox.Show("Number of Questions is not an integer.");
                return;
            }
            else if (Convert.ToInt32(NumberOfQuestionsTextBox.Text) < 1)
            {
                MessageBox.Show("You have to have at least 1 question for you quiz.");
                return;
            }

            // Checks if Number of Minutes is a valid integer
            if (YesButton.IsChecked == true)
            {
                
                if (!int.TryParse(NumberOfMinutesTextBox.Text, out parsedValue))
                {
                    MessageBox.Show("Number of Minutes is not an integer.");
                    return;
                }
                else if (Convert.ToInt32(NumberOfMinutesTextBox.Text) < 5)
                {
                    MessageBox.Show("Number of Minutes needs to be at 5.");
                    return;
                }
                else
                {
                    QuizSettings.IsTimed = true;
                    QuizSettings.QuizMinutes = Convert.ToInt32(NumberOfMinutesTextBox.Text);
                }
            }
            else
            {
                QuizSettings.IsTimed = false;
                QuizSettings.QuizMinutes = -1;
            }

            


        }
        //**********************************************************************************//
        public double Height1
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }

        public double Width1
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public QuizSettings QuizSettings
        {
            get
            {
                return quizSettings;
            }

            set
            {
                quizSettings = value;
            }
        }

        
    }
}
