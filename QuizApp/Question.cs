﻿using System;
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
            return base.ToString();
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

        public string QuestionType
        {
            get
            {
                return questionType;
            }

            set
            {
                if (value == "")
                {
                    throw new ArgumentException("QuestionType cannot be empty");
                }
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
