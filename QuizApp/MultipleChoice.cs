using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    class MultipleChoice : Question
    {
        private List<String> choices;
        private String correctAnswer;

        /// <summary>
        /// Public properties section
        /// </summary>
        public List<string> Choices
        {
            get
            {
                return choices;
            }

            set
            {
                choices = value;
            }
        }

        public string CorrectAnswer
        {
            get
            {
                return correctAnswer;
            }

            set
            {
                correctAnswer = value;
            }
        }
    }
}
