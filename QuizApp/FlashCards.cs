
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace QuizApp

{
    class StudyDeck
    {
        public List<Flashcards> cards { get; set; }
        public StudyDeck()
        {
            cards = new List<Flashcards>();

        }
    }


    class Flashcards
    {

        public string Front { get; set; }
        public string Back { get; set; }
        public Flashcards(string term, string defintion)
        {
            Front = term;
            Back = defintion;

        }

        public override string ToString()

        {
            return string.Format("Term: {0} \n Definition: {1}", Front, Back);

        }

    }

}