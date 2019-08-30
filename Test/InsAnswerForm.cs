using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinanceProAdmin
{
    public partial class InsAnswerForm : Form
    {
        public BulkInsertAnswer bulkInsertAnswersHandler;
        public InsAnswerForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (bulkInsertAnswersHandler != null)
            {
                Regex reg = new Regex(@"^.\)\s(.+?)$", RegexOptions.Multiline);
                List<string> answerList = new List<string>();
                string input = tbText.Text;
                string mach;
                foreach (Match m in reg.Matches(input)) 
                {
                    mach = m.Groups[1].ToString().Replace("*", "");
                    answerList.Add(mach);
                }
              
                bulkInsertAnswersHandler(answerList);
                DialogResult = DialogResult.OK;
            }
        }
    }
}
