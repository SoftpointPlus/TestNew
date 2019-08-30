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
    public partial class LoginForm : Form
    {
        public int Mode { get; set; }
        public LoginForm()
        {
            InitializeComponent();
            cbRole.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbRole.Text == "Администратор" && tbPassword.Text == "admin")
            {
                Mode = 0;
                DialogResult = DialogResult.OK;
               
            }
            else
                {
                    MessageBox.Show("Неверный пароль!" );
                }
         }
        
    }
}
