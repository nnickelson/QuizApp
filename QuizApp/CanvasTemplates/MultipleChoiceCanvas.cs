using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace QuizApp
{
    public class MultipleChoiceCanvas
    {
        private Canvas bottomCanvas;
        private TextBox questionBox;
        private List<TextBox> answerBoxes;
        private List<RadioButton> answerButtons;

        public MultipleChoiceCanvas(double height, double width)
        {
            BottomCanvas = new Canvas();
            BottomCanvas.Height = height * (1.0);
            BottomCanvas.Width = width * (1.0);
            //BottomCanvas.Background = new SolidColorBrush(Colors.ForestGreen);
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromRgb(246, 246, 246);
            BottomCanvas.Background = mySolidColorBrush;

            //*****************************************************************
            QuestionBox = new TextBox();

            QuestionBox.Width = BottomCanvas.Width * (0.7);
            QuestionBox.Height = BottomCanvas.Height * (0.15);
            QuestionBox.FontSize = Convert.ToInt32(QuestionBox.Height * (0.30));

            Canvas.SetTop(QuestionBox, BottomCanvas.Height * (0.10));
            Canvas.SetLeft(QuestionBox, BottomCanvas.Width * (0.15));

            QuestionBox.TextWrapping = System.Windows.TextWrapping.Wrap;
            BottomCanvas.Children.Add(QuestionBox);

            //****************************************************************
            answerBoxes = new List<TextBox>();
            answerButtons = new List<RadioButton>();
            for (int i = 0; i < 5; i++)
            {
                TextBox tb = new TextBox();
                tb.Width = BottomCanvas.Width * (0.7);
                tb.Height = BottomCanvas.Height * (0.10);
                tb.FontSize = Convert.ToInt32(tb.Height * (0.30));

                Canvas.SetTop(tb, BottomCanvas.Height * (0.35 + 0.13*i));
                Canvas.SetLeft(tb, BottomCanvas.Width * (0.15));
                tb.TextWrapping = System.Windows.TextWrapping.Wrap;
                answerBoxes.Add(tb);
                BottomCanvas.Children.Add(answerBoxes[i]);

                RadioButton rb = new RadioButton();
                Canvas.SetTop(rb, BottomCanvas.Height * (0.38 + 0.13 * i));
                Canvas.SetLeft(rb, BottomCanvas.Width * (0.10));
                AnswerButtons.Add(rb);
                BottomCanvas.Children.Add(AnswerButtons[i]);
            }

            TextBlock qblock = new TextBlock { Text = "Question: "};
            qblock.FontSize = Convert.ToInt32(BottomCanvas.Height * (0.05));
            Canvas.SetTop(qblock, BottomCanvas.Height * (0.02));
            Canvas.SetLeft(qblock, BottomCanvas.Width * (0.15));
            qblock.Width = BottomCanvas.Width * (0.7);
            qblock.Height = BottomCanvas.Height * (0.10);

            TextBlock ablock = new TextBlock { Text = "Answers: " };
            ablock.FontSize = Convert.ToInt32(BottomCanvas.Height * (0.05));
            Canvas.SetTop(ablock, BottomCanvas.Height * (0.27));
            Canvas.SetLeft(ablock, BottomCanvas.Width * (0.15));
            ablock.Width = BottomCanvas.Width * (0.7);
            ablock.Height = BottomCanvas.Height * (0.10);

            BottomCanvas.Children.Add(qblock);
            BottomCanvas.Children.Add(ablock);
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

        public List<TextBox> AnswerBoxes
        {
            get
            {
                return answerBoxes;
            }

            set
            {
                answerBoxes = value;
            }
        }

        public List<RadioButton> AnswerButtons
        {
            get
            {
                return answerButtons;
            }

            set
            {
                answerButtons = value;
            }
        }
    }
}
