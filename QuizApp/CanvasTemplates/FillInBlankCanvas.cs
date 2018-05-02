using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace QuizApp
{
    public class FillInBlankCanvas
    {
        private Canvas bottomCanvas;
        private TextBox tb1, tb2;
        private int fontsize = 16;

        public FillInBlankCanvas(double height, double width)
        {
            BottomCanvas = new Canvas();
            BottomCanvas.Height = height * (1.0);
            BottomCanvas.Width = width * (1.0);
            //BottomCanvas.Background = new SolidColorBrush();
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromRgb(246, 246, 246);
            BottomCanvas.Background = mySolidColorBrush;
            
            //************************************************************
            Tb1 = new TextBox();

            Tb1.Width = BottomCanvas.Width * (0.9);
            Tb1.Height = BottomCanvas.Height * (0.30);
            Tb1.FontSize = fontsize;

            //Tb1.BorderThickness = new System.Windows.Thickness(0);

            //Tb1.Background = mySolidColorBrush;
           // Tb1.BorderBrush = null;

            Canvas.SetTop(Tb1, BottomCanvas.Height * (0.05));
            Canvas.SetLeft(Tb1, BottomCanvas.Width * (0.05));

            Tb1.TextWrapping = System.Windows.TextWrapping.Wrap;
            BottomCanvas.Children.Add(Tb1);

            //*************************************************************
            Tb2 = new TextBox();

            Tb2.Width = BottomCanvas.Width * (0.9);
            Tb2.Height = BottomCanvas.Height * (0.25);
            Tb2.FontSize = Convert.ToInt32(Tb2.Height * (0.2));

            Canvas.SetTop(Tb2, BottomCanvas.Height * (0.66));
            Canvas.SetLeft(Tb2, BottomCanvas.Width * (0.05));

            Tb2.TextWrapping = System.Windows.TextWrapping.Wrap;
            BottomCanvas.Children.Add(Tb2);

            //**************************************************************
           // TextBlock block1 = new TextBlock();
           // block1.FontSize = Convert.ToInt32(BottomCanvas.Height * (0.10));
           // Canvas.SetTop(block1, BottomCanvas.Height * (0.05));
           // Canvas.SetLeft(block1, BottomCanvas.Width * (0.15));
           /// block1.Text = "Question: ";
            //BottomCanvas.Children.Add(block1);

            //*************************************************************
            //TextBlock block2 = new TextBlock();
           // block2.FontSize = Convert.ToInt32(BottomCanvas.Height * (0.10));
            //Canvas.SetTop(block2, BottomCanvas.Height * (0.51));
            //Canvas.SetLeft(block2, BottomCanvas.Width * (0.15));
            //block2.Text = "Answer: ";
            //BottomCanvas.Children.Add(block2);
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

        public TextBox Tb1
        {
            
            get
            {
                return tb1;
            }

            set
            {
                tb1 = value;
            }
        }

        public TextBox Tb2
        {
            get
            {
                return tb2;
            }

            set
            {
                tb2 = value;
            }
        }
    }
}
