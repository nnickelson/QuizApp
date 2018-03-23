using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    public class Results
    {
        private List<String> correctAnswers;
        private List<String> userAnswers;
        private int numTotalQuestions;
        private int numAnsweredCorrectly;

        /// <summary>
        /// Results Constructor
        /// Initializes correctAnswers List and userAnswers list to null
        /// Initializes numTotalQuestions and numAnsweredCorrectly to zero
        /// </summary>
        public Results()
        {
            this.correctAnswers = new List<string>();
            this.userAnswers = new List<string>();
            this.numTotalQuestions = 0;
            this.numAnsweredCorrectly = 0;
        }
        
        /// <summary>
        /// Public properties section
        /// </summary>
        public List<string> CorrectAnswers
        {
            get
            {
                return correctAnswers;
            }

            set
            {
                correctAnswers = value;
            }
        }

        public List<string> UserAnswers
        {
            get
            {
                return userAnswers;
            }

            set
            {
                userAnswers = value;
            }
        }

        public int NumTotalQuestions
        {
            get
            {
                return numTotalQuestions;
            }

            set
            {
                numTotalQuestions = value;
            }
        }

        public int NumAnsweredCorrectly
        {
            get
            {
                return numAnsweredCorrectly;
            }

            set
            {
                numAnsweredCorrectly = value;
            }
        }
    }
}
