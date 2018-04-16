using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizApp;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IsValidFilename_NameNotLegal_ReturnFalse()
        {
            // Arrange
            string fileName = "//";
            QuestionsDeckJSON_IO test = new QuestionsDeckJSON_IO();
            Assert.IsFalse(test.IsValidFilename(fileName));
        }

        [TestMethod]
        public void IsValidFilename_NameISLegal_ReturnTrue()
        {
            // Arrange
            string fileName = "Hello";
            QuestionsDeckJSON_IO test = new QuestionsDeckJSON_IO();
            Assert.IsTrue(test.IsValidFilename(fileName));
        }

        [TestMethod]
        public void WriteQuestionsDeck_DeckNameIsEmpty_ReturnFalse()
        {
            //Arrange
            QuestionsDeckJSON_IO test = new QuestionsDeckJSON_IO();
            QuestionsDeck Deck = new QuestionsDeck { DeckName = "" };

            //Act & Assert
            Assert.IsFalse(test.IsValidDeck(Deck));    
        }

        [TestMethod]
        public void WriteQuestionsDeck_DeckIsNUll_ReturnFalse()
        {
            //Arrange
            QuestionsDeckJSON_IO test = new QuestionsDeckJSON_IO();
            QuestionsDeck Deck1 = null;
            //Act & Assert
            Assert.IsFalse(test.IsValidDeck(Deck1));
        }

        [TestMethod]
        public void WriteQuestionsDeck_DeckIsIntilalized_ReturnTrue()
        {
            //Arrange
            QuestionsDeckJSON_IO test = new QuestionsDeckJSON_IO();
            QuestionsDeck Deck1 = new QuestionsDeck { DeckName = "Hello", PastDeckDates = null, QuestionList = null, PastDeckScores = null };
            //Act & Assert
            Assert.IsTrue(test.IsValidDeck(Deck1));
        }

        [TestMethod]
        public void WriteQuestionsDeck_DeckHasName_ReturnTrue()
        {
            //Arrange
            QuestionsDeckJSON_IO test = new QuestionsDeckJSON_IO();
            QuestionsDeck Deck1 = new QuestionsDeck { DeckName = "Hello" };
            //Act & Assert
            Assert.IsTrue(test.IsValidDeck(Deck1));
        }
    }
}
