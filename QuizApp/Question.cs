using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    public class Question
    {
        private String questionText;
        private int correctlyAnswered;
        private int totalAttempts;
        private MultipleChoice mCAnswers;
        private TrueFalse tFAnswers;
        private FillInBlank fIBAnswers;
        public QuestionType typeQuestion;


        public enum QuestionType { MultipleChoice, FillInBlank, TrueFalse }

        public Question() { }

        public Question(string typeQuestion)
        {
            if (typeQuestion == "MC")
            {
                MCAnswers = new MultipleChoice();
                this.typeQuestion = QuestionType.MultipleChoice;
            }
            else if (typeQuestion == "TF")
            {
                TFAnswers = new TrueFalse();
                this.typeQuestion = QuestionType.TrueFalse;
            }
            else if (typeQuestion == "FIB")
            {
                FIBAnswers = new FillInBlank();
                this.typeQuestion = QuestionType.FillInBlank;
            }
        }

        /// <summary>
        /// totalQuestionsScore Function
        /// Calculates the total overall score of a particular question
        /// </summary>
        /// <returns>double: question score returned</returns>
        public int totalQuestionScore()
        {
            return 100*(this.correctlyAnswered / this.totalAttempts);
        }

        public override string ToString()
        {
            return ("Question: {0}-----Answer {1}");
        }

        /// <summary>
        /// Public properties section
        /// </summary>
        public string QuestionText
        {
            get
            {
                return questionText;
            }

            set
            {
                if (value == "")
                {
                    throw new ArgumentException("QuestionText cannot be empty");
                }
                questionText = value;
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

        public MultipleChoice MCAnswers
        {
            get
            {
                return mCAnswers;
            }

            set
            {
                mCAnswers = value;
            }
        }

        public TrueFalse TFAnswers
        {
            get
            {
                return tFAnswers;
            }

            set
            {
                tFAnswers = value;
            }
        }

        public FillInBlank FIBAnswers
        {
            get
            {
                return fIBAnswers;
            }

            set
            {
                fIBAnswers = value;
            }
        }

        
    }
}
