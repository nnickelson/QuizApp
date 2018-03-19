using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    class Questions
    {
        public string text { get; set; }
        public string answer { get; set; }

        //public Questions() { }

        public Questions(string mtext, string manswer)
        {
            this.text = mtext;
            this.answer = manswer;
        }

        public override string ToString()
        {
            return string.Format("Question: {0} \n Answer: {1}", this.text, this.answer);
        }
    }

    class QuestionDeck
    {
        public List<Questions> Cards { get; set; }
        public QuestionDeck() { Cards = new List<Questions>(); }
    }
}
