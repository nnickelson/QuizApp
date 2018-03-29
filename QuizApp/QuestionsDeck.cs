using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    public class QuestionsDeck
    {
        private List<Question> questionList;
        private String deckName;
        private List<double> pastDeckScores;
        private List<DateTime> pastDeckDates;


        public QuestionsDeck()
        {
            this.questionList = new List<Question>();
            this.pastDeckDates = new List<DateTime>();
            this.pastDeckScores = new List<double>();
        }


        public List<Question> QuestionList
        {
            get
            {
                return questionList;
            }

            set
            {
                questionList = value;
            }
        }

        public string DeckName
        {
            get
            {
                return deckName;
            }

            set
            {
                deckName = value;
            }
        }

        public List<double> PastDeckScores
        {
            get
            {
                return pastDeckScores;
            }

            set
            {
                pastDeckScores = value;
            }
        }

        public List<DateTime> PastDeckDates
        {
            get
            {
                return pastDeckDates;
            }

            set
            {
                pastDeckDates = value;
            }
        }
    }
}
