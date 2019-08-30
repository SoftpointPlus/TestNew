using CommonLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinanceProAdmin
{
    public partial class AddAnswerForm : Form
    {
        public AddAnswerForm()
        {
            InitializeComponent();
            CallBackMy.callbackEventHandler += SetAnswerText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tbAnswerText.Text.Length != 0)
            {
                if (CallBackMy.AddQuestionTextEventHandler != null)
                {
                    CallBackMy.AddQuestionTextEventHandler(tbAnswerText.Text);
                }
            }
            DialogResult = DialogResult.OK;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void SetAnswerText(string data)
        {
            tbAnswerText.Text = data;
        }
        
    }
}
