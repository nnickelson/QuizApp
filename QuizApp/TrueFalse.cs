﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    class TrueFalse : Question
    {
        private bool correctAnswer;

        public TrueFalse()
        {
            //possibly edit
        }

        public bool CorrectAnswer
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
