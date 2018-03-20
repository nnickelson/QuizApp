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

        public Questions(string mtext, string manswer)
        {
            text = mtext;
            answer = manswer;
        }

        public override string ToString()
        {
            return string.Format("Question: {0} \n Answer: {1}", text, answer);
        }
    }

    class QuestionDeck
    {
        public List<Questions> Cards { get; set; }
        public QuestionDeck() { Cards = new List<Questions>(); }
    }
}
