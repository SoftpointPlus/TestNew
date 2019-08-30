using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public class Questions 
    {
        public event SetCahnged SetChengedEvent;
        private List<Question> questionsList;
        public Questions()
        {
            questionsList = new List<Question>();
            
        }
        public Question Add(string data)
        {
            Question question;
            question = new Question(data);
            question.SetChengedEvent += SetChengedEvent;
            questionsList.Add(question);
            if (SetChengedEvent != null)
            {
                SetChengedEvent(true);
            }
            return question;
        }
        public void Delete(int index)
        {
            if (index < 0 || index >= questionsList.Count) return;
            questionsList.RemoveAt(index);
            if (SetChengedEvent != null)
            {
                SetChengedEvent(true);
            }
        }
        public int Count { get { return questionsList.Count; } }
        public List<Question> QuestionsList { get { return questionsList; } }
    }
}
