﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public class Answers
    {
        public event SetCahnged SetChengedEvent;
        private List<Answer> answers;
        public Answers()
        {
            answers = new List<Answer>();
        }
        public void Add(Answer answer)
        {
            answers.Add(answer);
            if (SetChengedEvent != null)
            {
                SetChengedEvent(true);
            }
        }
        public void Delete(int index)
        {
            if (index >= 0 && index < answers.Count)
            {
                answers.RemoveAt(index);
                if (SetChengedEvent != null)
                {
                    SetChengedEvent(true);
                }
            }
        }
        public List<Answer> AnswerList { get { return answers; } }
        

        
    }
}
