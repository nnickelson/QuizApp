using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Script.Serialization;
using System.IO;
using System.Windows;

namespace QuizApp
{
    public class QuestionsDeckJSON_IO
    {
        QuestionsDeck Deck = new QuestionsDeck();
        String fileName;
        String JSONquestions;
        String addQuestion;

        public QuestionsDeckJSON_IO() { }
        
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
        public void WriteQuestionsDeck(QuestionsDeck questionsDeck)
        {
            if (questionsDeck == null || questionsDeck.DeckName == "")
            {
                MessageBox.Show("Deck is not set properly");
                return;
            }
            
            Deck = questionsDeck;
            JavaScriptSerializer ser = new JavaScriptSerializer();
            String outputJSON = ser.Serialize(Deck);

            // Get the rootpath, locate the quiz directory and then the QuestionsDeck subfolder. 
            // Once that is located, write the JSON file to the destination.
            String RootPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            RootPath += @"\QuizApp\";
            String QuestionsDeckPath = RootPath + @"\QuestionDecks\";
            File.WriteAllText(QuestionsDeckPath + (questionsDeck.DeckName) + ".QuestionDeck.json", outputJSON);
            MessageBox.Show("file: " + fileName + " written.");
        }
    }
}

    
