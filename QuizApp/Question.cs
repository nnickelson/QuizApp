using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    class Question
    {
        private String questionText;
        private String questionType;
        private int correctlyAnswered;
        private int totalAttempts;

        public int totalQuestionScore()
        {
            return (this.correctlyAnswered / this.totalAttempts);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public string QuestionText
        {
            get
            {
                return questionText;
            }

            set
            {
                questionText = value;
            }
        }

        public string QuestionType
        {
            get
            {
                return questionType;
            }

            set
            {
                questionType = value;
            }
        }

        public int CorrectlyAnswered
        {
            get
            {
                return correctlyAnswered;
            }

            set
            {
                correctlyAnswered = value;
            }
        }

        public int TotalAtempts
        {
            get
            {
                return totalAttempts;
            }

            set
            {
                totalAttempts = value;
            }
        }
    }
}
