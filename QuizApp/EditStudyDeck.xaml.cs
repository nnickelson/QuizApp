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
using System.IO;
using Microsoft.Win32;

namespace QuizApp
{
    /// <summary>
    /// Interaction logic for EditStudyDeck.xaml
    /// </summary>
    public partial class EditStudyDeck : Page
    {
        string FileToEdit;
        public EditStudyDeck()
        {
            InitializeComponent();
        }

        private void SelectADeckbtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                DeckTitletextBox_Copy.Text = File.ReadAllText(openFileDialog.FileName);
            openFileDialog.Filter = "JSON files (*.JSON)|*.JSON|All files (*.*)|*.*";
        }
    }
}
