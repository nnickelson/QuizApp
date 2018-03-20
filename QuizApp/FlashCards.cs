using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    class StudyDeck
    {
        public List<Flashcards> MyDeck { get; set; }

        public StudyDeck()
        {
            MyDeck = new List<Flashcards>();
        }
    }

    class Flashcards
    {
        private string _term { get; set; }
        private string _definition { get; set; }
        private string _optionalImagePath{ get; set; }


        public Flashcards(string term, string defintion)
        {
            _term = term;
            _definition = defintion;
        }

        public override string ToString()
        {
            return string.Format("Term: {0} \n Definition: {1}", _term, _definition);
        }

        public void ModifyCardTerm(string updatedTerm)
        {
            _term = updatedTerm;
        }

        public void ModifyCardDefinition(string updatedDefinition)
        {
            _definition = updatedDefinition;
        }
        public void ModifyCardImage(string updatedOptionalImage)
        {
            _optionalImagePath = updatedOptionalImage;
        }
    }
}
