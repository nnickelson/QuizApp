using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Script.Serialization;
using System.IO;
using System.Windows;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace QuizApp
{
    public class QuestionsDeckJSON_IO
    {
        private QuestionsDeck deck;
        private QuizSettings quizSettings;
        private String fileName;
        private String JSONquestions;
        private List<QuestionsDeck> deckList;
        JavaScriptSerializer ser;
        //private String addQuestion;
       
        public QuestionsDeckJSON_IO()
        {
            Deck = new QuestionsDeck();
            QuizSettings = new QuizSettings();
            DeckList = new List<QuestionsDeck>();
            ser = new JavaScriptSerializer();
        }

        /// <summary>
        /// WriteQuestionsDeck Method
        /// Checks to see if a deck is named and initialized properly.
        /// If not a message box prompts the user to correct the deck
        /// if the deck is good, a file check makes sure another deck 
        /// of the same name isn't in the file, otherwise tags it with
        /// a duplicate. 
        /// Then it takes the questionsDeck object and reads it into a JSON file
        /// </summary>
        /// <param name="questionsDeck">QuestionsDeck object - cannot be null</param>
        /// 
        public bool IsValidDeck (QuestionsDeck questionsDeck)
        {
            if (questionsDeck == null || questionsDeck.DeckName == "")
            {              
                return false;
            }
            else
                return true;
        }
        public void WriteQuestionsDeck(QuestionsDeck questionsDeck)
        {
            if ( !IsValidDeck(questionsDeck))
            {
                MessageBox.Show("Deck is not set properly");
                return;
            }

            Deck = questionsDeck;
            //JavaScriptSerializer ser = new JavaScriptSerializer();
            String outputJSON = ser.Serialize(Deck);

            // Get the rootpath, locate the quiz directory and then the QuestionsDeck subfolder. 
            // Once that is located, write the JSON file to the destination.
            String RootPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            RootPath += @"\QuizApp\";
            String QuestionsDeckPath = RootPath + @"\QuestionDecks\";
            File.WriteAllText(QuestionsDeckPath + (questionsDeck.DeckName) + ".QuestionsDeck.json", outputJSON);
            MessageBox.Show("file: " + FileName + " written.");
        }

        /// <summary>
        /// IsValidFileName Method
        /// This method ensures that deck names and other JSON names have the proper Windows filename
        /// characters to be stored as files
        /// </summary>
        /// <param name="testName">string - cannot be null, cannot have invalid filename chars</param>
        /// <returns>true if filename valid - false otherwise</returns>
        public bool IsValidFilename(string testName)
        {
            string strTheseAreInvalidFileNameChars = new string(System.IO.Path.GetInvalidFileNameChars());
            Regex regInvalidFileName = new Regex("[" + Regex.Escape(strTheseAreInvalidFileNameChars) + "]");

            if (regInvalidFileName.IsMatch(testName)) { return false; };

            return true;
        }

        /// <summary>
        /// ReadDeck Method
        /// This method opens an open file dialog to retrieve the filepath of a questionsDeck.
        /// It then uses this filepath to read the JSON questions deck into questionsDeck object.
        /// Once it has been read into the object, this method returns that QuestionsDeck object.
        /// </summary>
        /// <returns> type of QuestionsDeck</returns>
        public QuestionsDeck ReadDeck()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.JSON)|*.JSON";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;// Get the files path.

                // If a study deck exist then populate the rest of the form with the appropriate fields.
                if (File.Exists(filePath))
                {
                    // Before the form is populated, make sure it is a StudyDeck and not a QuestionDeck.
                    if (filePath.Contains(".QuestionsDeck"))
                    {
                        //MessageBox.Show("deck beaing read");
                        //JavaScriptSerializer ser = new JavaScriptSerializer();
                        //Deck.DeckName = System.IO.Path.GetFileNameWithoutExtension(filePath);
                        JSONquestions1 = File.ReadAllText(filePath);
                        QuestionsDeck readDeck = ser.Deserialize<QuestionsDeck>(JSONquestions1);
                        Deck = readDeck;
                    }
                }
            }
            return Deck;
        }
        
        /// <summary>
        /// QuestionsDeckReturn Method
        /// This method opens the QuizApp folder on the desktop and reads all the available
        /// QuestionsDeck objects into a List<QuestionsDeck> 
        /// </summary>
        /// <returns>List<QuestionsDeck> data type</returns>
        public List<QuestionsDeck> QuestionsDeckReturn()
        {
            DeckList = new List<QuestionsDeck>();
            String RootPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            RootPath += @"\QuizApp\QuestionDecks";
            String[] allFiles = Directory.GetFiles(RootPath, "*.*", SearchOption.AllDirectories);
            foreach (var file in allFiles)
            {
                //JavaScriptSerializer ser = new JavaScriptSerializer();
                JSONquestions1 = File.ReadAllText(file);
                QuestionsDeck readDeck = ser.Deserialize<QuestionsDeck>(JSONquestions1);
                DeckList.Add(readDeck);
            }
            return DeckList;
        }

        public void WriteQuizSettings (QuizSettings setQuizSettings)
        {
            if (setQuizSettings == null || setQuizSettings.QuizName == "")
            {
                MessageBox.Show("You have to name your quiz");
                return;
            }
            QuizSettings = setQuizSettings;
            JavaScriptSerializer ser = new JavaScriptSerializer();
            String outputJSON = ser.Serialize(QuizSettings);

            // Get the rootpath, locate the quiz directory and then the QuestionsDeck subfolder. 
            // Once that is located, write the JSON file to the destination.
            String RootPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            RootPath += @"\QuizApp\";
            String QuizSettingsPath = RootPath + @"\QuizSettings\";
            File.WriteAllText(QuizSettingsPath + setQuizSettings.QuizName + ".QuizSettings.json", outputJSON);
            MessageBox.Show("file: " + FileName + " written.");
        }

        public QuizSettings ReadQuizSettings()
        {
            String filePath;
            Microsoft.Win32.OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.JSON)|*.JSON";
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;// Get the files path.

                // If a study deck exist then populate the rest of the form with the appropriate fields.
                if (File.Exists(filePath))
                {
                    // Before the form is populated, make sure it is a Quiz setting
                    if (filePath.Contains(".QuizSettings"))
                    {
                        JSONquestions = File.ReadAllText(filePath);
                        QuizSettings = ser.Deserialize<QuizSettings>(JSONquestions);
                    }
                    else
                    {
                        MessageBox.Show("Sorry, You can only select a Quiz Setting on this page. If you need to create a Quiz setting"
                            + " then go back to the Quiz settings page.", "Help Window", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            return QuizSettings;
        }

        /// <summary>
        /// Properties section
        /// </summary>
        public string FileName
        {
            get
            {
                return fileName;
            }

            set
            {
                fileName = value;
            }
        }

        public string JSONquestions1
        {
            get
            {
                return JSONquestions;
            }

            set
            {
                JSONquestions = value;
            }
        }

        public List<QuestionsDeck> DeckList
        {
            get
            {
                return deckList;
            }

            set
            {
                deckList = value;
            }
        }

        public QuestionsDeck Deck
        {
            get
            {
                return deck;
            }

            set
            {
                deck = value;
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

    
