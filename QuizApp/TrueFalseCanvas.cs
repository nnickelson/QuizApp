﻿using System;
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
        private Canvas bottomCanvas;
        private TextBox questionBox;
        private RadioButton buttonTrue, buttonFalse; 

        public TrueFalseCanvas(double height, double width)
        {
            BottomCanvas = new Canvas();
            BottomCanvas.Height = height * (1.0);
            BottomCanvas.Width = width * (1.0);
            BottomCanvas.Background = new SolidColorBrush(Colors.ForestGreen);

            //************************************************************
            QuestionBox = new TextBox();

            QuestionBox.Width = BottomCanvas.Width * (0.7);
            QuestionBox.Height = BottomCanvas.Height * (0.25);
            QuestionBox.FontSize = Convert.ToInt32(QuestionBox.Height * (0.2));

            Canvas.SetTop(QuestionBox, BottomCanvas.Height * (0.20));
            Canvas.SetLeft(QuestionBox, BottomCanvas.Width * (0.15));

            QuestionBox.TextWrapping = System.Windows.TextWrapping.Wrap;
            BottomCanvas.Children.Add(QuestionBox);

            
            //**************************************************************
            TextBlock block1 = new TextBlock();
            block1.FontSize = Convert.ToInt32(BottomCanvas.Height * (0.10));
            Canvas.SetTop(block1, BottomCanvas.Height * (0.05));
            Canvas.SetLeft(block1, BottomCanvas.Width * (0.15));
            block1.Text = "Question: ";
            BottomCanvas.Children.Add(block1);

            //**************************************************************

            ButtonTrue = new RadioButton { Content = "True" };
            ButtonTrue.FontSize = Convert.ToInt32(BottomCanvas.Height * (0.10));
            Canvas.SetTop(ButtonTrue, BottomCanvas.Height * (0.55));
            Canvas.SetLeft(ButtonTrue, BottomCanvas.Width * (0.15));

            //*************************************************************
            ButtonFalse = new RadioButton { Content = "False" };
            ButtonFalse.FontSize = Convert.ToInt32(BottomCanvas.Height * (0.10));
            Canvas.SetTop(ButtonFalse, BottomCanvas.Height * (0.75));
            Canvas.SetLeft(ButtonFalse, BottomCanvas.Width * (0.15));

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

