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

namespace FinanceProStudent
{
    public partial class LoginForm : Form
    {
        public int Mode { get; set; }
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbName.Text == "" || tbGroup.Text == "") 
                {
                    MessageBox.Show("Необходимо заполнить поля", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Student.Name = tbName.Text;
                Student.Group = tbGroup.Text;
                DialogResult = DialogResult.OK;
        }
            
    }
}
