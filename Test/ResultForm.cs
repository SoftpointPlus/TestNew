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
    public partial class ResultForm : Form
    {
        public ResultForm()
        {
            InitializeComponent();
            CallBackMy.ShowTestResultDetail = SetTestResult;
        }
        public void SetTestResult(string student, string group, int valid, int novalid)
        {
            lbStudent.Text = student;
            lbGroup.Text = group;
            lbtrue.Text = valid.ToString();
            lbFalse.Text = novalid.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ResultForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Закрыть тест?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
