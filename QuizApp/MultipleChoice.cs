using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    public class MultipleChoice : Question
    {
        private List<String> choices;
        private int correctAnswer;

        public MultipleChoice()
        {
            Choices = new List<String>();
        }

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

        public int CorrectAnswer
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
