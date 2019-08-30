﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinanceProAdmin
{
    
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoginForm loginForm = new LoginForm();
            DialogResult result = loginForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (loginForm.Mode == 0) 
                {
                    Application.Run(new MainForm());
                }
                if (loginForm.Mode == 1)
                {
                    Application.Run(new TestForm());
                }
            }
            

            
        }
    }
}

