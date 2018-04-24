using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace QuizApp
{
    public class TrueFalseCanvas
    {
        int fontSize = 16;
        private Canvas bottomCanvas;
        private TextBox questionBox;
        private RadioButton buttonTrue, buttonFalse; 

        public TrueFalseCanvas(double height, double width)
        {
            BottomCanvas = new Canvas();
            BottomCanvas.Height = height * (1.0);
            BottomCanvas.Width = width * (1.0);
            // BottomCanvas.Background = new SolidColorBrush(Colors.ForestGreen);

            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromRgb(246, 246, 246);
            BottomCanvas.Background = mySolidColorBrush;
            

            //************************************************************
            QuestionBox = new TextBox();

            QuestionBox.Width = BottomCanvas.Width * (0.9);
            QuestionBox.Height = BottomCanvas.Height * (0.30);
            QuestionBox.FontSize = fontSize;        // Convert.ToInt32(QuestionBox.Height * (0.2));

            //QuestionBox.BorderThickness = new System.Windows.Thickness(0);

            //QuestionBox.BorderBrush = null;
            //QuestionBox.Background = mySolidColorBrush;

            Canvas.SetTop(QuestionBox, BottomCanvas.Height * (0.05));
            Canvas.SetLeft(QuestionBox, BottomCanvas.Width * (0.05));

            QuestionBox.TextWrapping = System.Windows.TextWrapping.Wrap;
            BottomCanvas.Children.Add(QuestionBox);

            
            //**************************************************************
            /*
            TextBlock block1 = new TextBlock();
            block1.FontSize = Convert.ToInt32(BottomCanvas.Height * (0.10));
            Canvas.SetTop(block1, BottomCanvas.Height * (0.05));
            Canvas.SetLeft(block1, BottomCanvas.Width * (0.15));
            block1.Text = "Question: ";
            BottomCanvas.Children.Add(block1);
            */

            //**************************************************************

            ButtonTrue = new RadioButton { Content = "True" };
            ButtonTrue.FontSize = fontSize;
            Canvas.SetTop(ButtonTrue, BottomCanvas.Height * (0.55));
            Canvas.SetLeft(ButtonTrue, BottomCanvas.Width * (0.05));

            //*************************************************************
            ButtonFalse = new RadioButton { Content = "False" };
            ButtonFalse.FontSize = fontSize;
            Canvas.SetTop(ButtonFalse, BottomCanvas.Height * (0.75));
            Canvas.SetLeft(ButtonFalse, BottomCanvas.Width * (0.05));

            BottomCanvas.Children.Add(ButtonTrue);
            BottomCanvas.Children.Add(ButtonFalse);

        }

        public Canvas BottomCanvas
        {
            get
            {
                return bottomCanvas;
            }

            set
            {
                bottomCanvas = value;
            }
        }

        public TextBox QuestionBox
        {
            get
            {
                return questionBox;
            }

            set
            {
                questionBox = value;
            }
        }

        public RadioButton ButtonTrue
        {
            get
            {
                return buttonTrue;
            }

            set
            {
                buttonTrue = value;
            }
        }

        public RadioButton ButtonFalse
        {
            get
            {
                return buttonFalse;
            }

            set
            {
                buttonFalse = value;
            }
        }
    }
}

