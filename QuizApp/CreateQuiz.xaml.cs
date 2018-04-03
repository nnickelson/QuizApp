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

        public CreateQuiz()
        {
            InitializeComponent();
            Height1 = Application.Current.MainWindow.ActualHeight;
            Width1 = Application.Current.MainWindow.ActualWidth;
            CreateQuizTabControl.Width = Width1;
            CreateQuizTabControl.Height = Height1 * (0.85);
            NumberOfMinutesTextBox.Visibility = Visibility.Hidden;
            SetFontSize();
        }

        

        public void SetFontSize()
        {
            //MessageBox.Show(Convert.ToString(QuizSettingsTab.Height));
            double textHeight = Height1 / 22;
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

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            if (NoButton.IsChecked == true)
            {
                NoButton.IsChecked = false;                
            }
            NumberOfMinutesTextBox.Visibility = Visibility.Visible;
        }

        private void NoButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (YesButton.IsChecked == true)
            {
                YesButton.IsChecked = false;
            }
            NumberOfMinutesTextBox.Text = "";
            NumberOfMinutesTextBox.Visibility = Visibility.Hidden;
        }

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

        
    }
}
