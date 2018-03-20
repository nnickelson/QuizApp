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

using System.Web.Script.Serialization;
using System.IO;

namespace QuizApp
{
    public partial class CreateQuestionSet : Page
    {
        QuestionsDeck Deck = new QuestionsDeck();
        string fileName;
        string JSONquestions;
        string question,answer;
        int TotalCards = 0;
        JavaScriptSerializer ser = new JavaScriptSerializer();


        public CreateQuestionSet()
        {
            InitializeComponent();
        }

        private void Addbutton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(DeckTitletextBox.Text))
            {
                if (TotalCards == 0) // Only do this at the beginning of each Deck
                {
                    // Note: Deck Title is the name of the deck
                    fileName = DeckTitletextBox.Text;
                    fileName = fileName + ".json";

                    // if the file exists we update it - if it doesn't we'll create it later
                    if (File.Exists(fileName))
                        // This reads the JSON file as one big long string
                        JSONquestions = File.ReadAllText(fileName);
                }

                // If both the term and defintion exist then add it to the set
                if (!string.IsNullOrEmpty(TermtextBox.Text) && !string.IsNullOrEmpty(DefinitiontextBox.Text))
                {
                    question = TermtextBox.Text;
                    answer = DefinitiontextBox.Text;

                    fileName = DeckTitletextBox.Text;
                    fileName = fileName + ".json";

                    Question populateQuestions = new QuizApp.Question();
                    //Deck.Cards.Add(populateQuestions);

                    // Reserialize the object and store it as a String
                    string outputJSON = ser.Serialize(Deck);
                    // Write that string back to the JSON file
                    File.WriteAllText(fileName, outputJSON);

                    TotalCards++;
                    TermtextBox.Text = "";
                    DefinitiontextBox.Text = "";
                    CardNumberLabeltextBox.Text = (TotalCards).ToString();
                }
                // If the Question and Answer DOES NOT EXIST
                else if (string.IsNullOrEmpty(TermtextBox.Text) && string.IsNullOrEmpty(DefinitiontextBox.Text))
                {
                    TermtextBox.Text = "";
                    DefinitiontextBox.Text = "";
                    MessageBox.Show("You did not enter a Question or an Answer! Please try again", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);

                    //If the Question DOES NOT EXIST
                }
                else if (string.IsNullOrEmpty(TermtextBox.Text))
                {
                    MessageBox.Show("You did not enter a Question or an Answer! Please try again", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);
                }//If the Answer DOES NOT EXIST
                else if (string.IsNullOrEmpty(DefinitiontextBox.Text))
                {
                    MessageBox.Show("You did not enter an Answer! Please try again", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("You must provide a deck title to save to. You only need to do this once and leave the field alone after that", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);
            }
          
        }
    }
}
