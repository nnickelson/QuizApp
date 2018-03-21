using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    class FillInBlank : Question
    {
        private String correctAnswer;

        public FillInBlank()
        {
            //Possible constructor edit
        }
        public override string ToString()
        {
            return String.Format("Question: {0}-----Answer {1}", QuestionText, CorrectAnswer);
        }

        /// <summary>
        /// Public properties section
        /// </summary>
        public string CorrectAnswer
        {
            get
            {
                return correctAnswer;
            }

            set
            {
                if (value == "")
                {
                    throw new ArgumentException("correctAnswer cannot be empty");
                }
                correctAnswer = value;
            }
        }
    }
}
