using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    class FillInBlank
    {
        private String correctAnswer;

        public FillInBlank()
        {
            //Possible constructor edit
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
