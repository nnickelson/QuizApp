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
        private List<QuestionsDeck> deckList = new List<QuestionsDeck>();
        private QuestionsDeckJSON_IO fileOperator = new QuestionsDeckJSON_IO();

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
            SetTabTemplate();
            PopulateListBox();
            QuizSettings = new QuizSettings();
        }

        
        /// <summary>
        /// SetTabTemplate Method
        /// Initializes the UI configurations of the current page
        /// </summary>
        public void SetTabTemplate()
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

            QuizDecks.Width = Width1 * (0.25);
            AddedToList.Width = Width1 * (0.25);
            AddedToList.Margin = new Thickness(Width1 * (0.05), Height1 * (0.05), Width1 * (0.15), 10);
            AddedToList.Margin = new Thickness(Width1 * (0.55), Height1 * (0.05), Width1 * (0.15), 10);
            AddTo.Margin = new Thickness(Width1 * (0.40), Height1 * (0.1), 0, 0);
            DeleteFrom.Margin = new Thickness(Width1 * (0.40), Height1 * (0.15), 0, 0);
        }
        
        /// <summary>
        /// PopulateListBox Method
        /// This method calls a method from QuestionsDeckJSON_IO
        /// It goes to the desktop file and reads all decks that are there and 
        /// adds them to the xaml listbox for selection
        /// </summary>
        public void PopulateListBox()
        {
            deckList = fileOperator.QuestionsDeckReturn();
            foreach ( var deck in deckList)
            {
                QuizDecks.Items.Add(deck.DeckName);
            }
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

            // Checks if Number of Questions is a valid integer
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
            else
            {
                QuizSettings.NumberOfQuestions = Convert.ToInt32(NumberOfQuestionsTextBox.Text);
            }

            // Checks if Number of Minutes is a valid integer
            if (YesButton.IsChecked == true)
            {
                
                if (!int.TryParse(NumberOfMinutesTextBox.Text, out parsedValue))
                {
                    MessageBox.Show("Sorry, the number of minutes is must be a natural number (1 or more).");
                    return;
                }
                else if (Convert.ToInt32(NumberOfMinutesTextBox.Text) < 1)
                {
                    MessageBox.Show("Sorry, The lowest time you can set is 1 minute.");
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
            if (QuizSettings.IncludedDecks.Count < 1)
            {
                MessageBox.Show("You have no decks for your quiz");
                return;
            }
            fileOperator.WriteQuizSettings(QuizSettings);
            NavigationService.Navigate(new Uri("/DeckBuilder.xaml", UriKind.Relative));
        }

        /// <summary>
        /// AddDeckToList Method
        /// This method adds the selected QuestionsDeck to the QuizSettings
        /// if the deckName does not exist in the list of QuizSettings QuestionsDecks
        /// It also populates the left listbox with the decks added.
        /// If no listbox item is selected or the deck is already included,
        /// the method returns and does nothing.
        /// </summary>
        /// <param name="sender">button handler</param>
        /// <param name="e">button handler</param>
        private void AddDeckToList(object sender, RoutedEventArgs e)
        {
            if (QuizDecks.SelectedItem == null)
                return;
            else
            {
                foreach (var deck in QuizSettings.IncludedDecks)
                {
                    if (deck.DeckName == Convert.ToString(QuizDecks.SelectedItem))
                    {
                        MessageBox.Show("This deck is already included");
                        return;
                    }
                }
                int index = QuizDecks.Items.IndexOf(QuizDecks.SelectedItem);
                AddedToList.Items.Add(QuizDecks.SelectedItem);
                QuizSettings.IncludedDecks.Add(deckList[index]);
            }
            //MessageBox.Show("Number of Decks = " + Convert.ToString(QuizSettings.IncludedDecks.Count));
        }

        /// <summary>
        /// DeleteDeckFromList Method
        /// If a user changes their mind and does not want a deck included in their QuizSettings
        /// this button removes that deck from the IncludedDecks list in QuizSettings.
        /// It also removes the name of the deck from the right-side List box
        /// If no item is selected from the listbox, the method returns without changing anything.
        /// </summary>
        /// <param name="sender">button handler</param>
        /// <param name="e">button handler</param>
        private void DeleteDeckFromList(object sender, RoutedEventArgs e)
        {
            if (AddedToList.SelectedItem == null)
            {
                return;
            }
            else
            {   
                foreach(QuestionsDeck deck in QuizSettings.IncludedDecks)
                {
                    if(deck.DeckName == Convert.ToString(AddedToList.SelectedItem))
                    {
                        QuizSettings.IncludedDecks.Remove(deck);
                        MessageBox.Show("Deck Removed");
                        break;
                    }
                    if (QuizSettings.IncludedDecks.Count == 0)
                        break;
                }
                AddedToList.Items.Remove(AddedToList.SelectedItem);
                MessageBox.Show("Number of Decks = " + Convert.ToString(QuizSettings.IncludedDecks.Count));
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


        // Nav menu
        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Home.xaml", UriKind.Relative));
        }
        private void StudyADeckbtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("StudyMyDeck.xaml", UriKind.Relative));
        }
        private void CreateADeckbtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/DeckBuilder.xaml", UriKind.Relative));
        }
        private void ImportExportbtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Import.xaml", UriKind.Relative));
        }
        private void CreateQuizbtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("CreateQuiz.xaml", UriKind.Relative));
        }
        private void TakeAQuizbtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Test.xaml", UriKind.Relative));
        }
    }
}
