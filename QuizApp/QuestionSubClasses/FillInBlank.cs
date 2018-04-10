using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    public class FillInBlank
    {
        private String correctAnswer;

        public FillInBlank()
        {
            //Possible constructor edit
        }
        public override string ToString()
        {
            return String.Format("{0}-----Answer", CorrectAnswer);
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
