using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    class QuizSettings
    {
        /// <summary>
        /// Private Backing Fields
        /// </summary>
        private String quizName;
        private bool isTimed;
        private int quizMinutes;
        private int numberOfQuestions;
        private List<double> pastQuizScores;
        private List<DateTime> pastQuizDates;
        private List<QuestionsDeck> includedDecks;


        /// <summary>
        /// Public Properties
        /// </summary>
        public string QuizName
        {
            get
            {
                return quizName;
            }

            set
            {
                quizName = value;
            }
        }

        public bool IsTimed
        {
            get
            {
                return isTimed;
            }

            set
            {
                isTimed = value;
            }
        }

        public int QuizMinutes
        {
            get
            {
                return quizMinutes;
            }

            set
            {
                quizMinutes = value;
            }
        }

        public int NumberOfQuestions
        {
            get
            {
                return numberOfQuestions;
            }

            set
            {
                numberOfQuestions = value;
            }
        }

        public List<double> PastQuizScores
        {
            get
            {
                return pastQuizScores;
            }

            set
            {
                pastQuizScores = value;
            }
        }

        public List<DateTime> PastQuizDates
        {
            get
            {
                return pastQuizDates;
            }

            set
            {
                pastQuizDates = value;
            }
        }

        public List<QuestionsDeck> IncludedDecks
        {
            get
            {
                return includedDecks;
            }

            set
            {
                includedDecks = value;
            }
        }
    }
}
