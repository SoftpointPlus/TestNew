using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public class Answer
    {
        public event SetCahnged SetChengedEvent;
        private string data;
        private bool verity;

        public string Data { get { return data; }
            set
            {
                data = value;
                if (SetChengedEvent != null)
                {
                    SetChengedEvent(true);
                }
            }
        }
        public bool Verity
        {
            get { return verity; }
            set
            {
                verity = value;
                if (SetChengedEvent != null)
                {
                    SetChengedEvent(true);
                }
            }
        }
        public Answer(string data, bool verity)
        {
            this.data = data;
            this.verity = verity;
  
        }

        public override string ToString()
        {
            return Data;
        }
    }
}

