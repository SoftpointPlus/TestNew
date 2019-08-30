using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public class Question
    {
        public event SetCahnged SetChengedEvent;
        private string text;
        private Answers answers;
        public string Text{
            get { return text; }
            set{
                if (value != "")
                {
                    text = value;
                    if (SetChengedEvent != null)
                    {
                        SetChengedEvent(true);
                    }
                }
            } 
        }
        public string Name { get {return "question";} private set{} }

        public Question(string data)
        {
            answers = new Answers();
            text = data;
        }
        public Answer Add(string data, bool verity)
        {
            Answer answer;
            answer = new Answer(data, verity);
            answers.Add(answer);
            answer.SetChengedEvent += SetChengedEvent;

            return answer;
            
        }
        public override string ToString()
        {
            return Text;
        }

        public Answers Answers { get { return answers; } }
    }
}
