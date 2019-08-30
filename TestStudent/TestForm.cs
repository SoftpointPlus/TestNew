﻿using CommonLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinanceProStudent
{
    public partial class TestForm : Form
    {
        private QuestionsLoader loader;
        public TestForm()
        {
            string fileName;
            InitializeComponent();

            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml");
            if (files.Length == 1)
                fileName = files[0];
            else
            {
                if (files.Length == 0)
                {
                    MessageBox.Show("Файл тестов не найден", "Внимание!");
                    return;
                }
                else
                {
                    SelectFileForm selectForm = new SelectFileForm();
                    CallBackMy.FillFileNameEventHendler(new List<string>(files));
                    selectForm.ShowDialog();
                    fileName = selectForm.SelectFile;
                }

            }
            
            loader = new QuestionsLoader();
            if (File.Exists(fileName))
            {
                loader.LoadQuestions(fileName);
                CallBackMy.ShowQuestionEventHendler = ShowQuestion;
                CallBackMy.ShowTestResult = ShowResultTestDetail;
                CallBackMy.QuestionResultEnentHandler = QuestionResult;
                loader.StartTest();
            }
            else
            {
                MessageBox.Show("Файл тестов не найден", "Внимание!");
                btAnswer.Enabled = false;
            }
        }

        private void QuestionResult(string data)
        {
            
        }

        private void ShowResultTestDetail(string student, string group, int valid, int novalid)
        {
            ResultForm resultForm = new ResultForm();
            CallBackMy.ShowTestResultDetail(student, group, valid, novalid);
            resultForm.ShowDialog();
            Application.Exit();
        }

        private void ShowQuestion(Question question, int currIndex, int count)
        {
            if(question != null)
            {
                lbQuesCaption.Text = "Вопрос ( " + currIndex.ToString() + "/" + count.ToString()+ " )";
                lbQuestionText.Text = question.Text;
                lbVariant.Items.Clear();
                if (question.Answers.AnswerList.Count > 0) btAnswer.Enabled = true;
                foreach (Answer answ in question.Answers.AnswerList)
                {
                    lbVariant.Items.Add(answ.Data);
                }
            }
        }

        private void btNext_Click(object sender, EventArgs e)
        {
     
        }

        private void TestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void btAnswer_Click(object sender, EventArgs e)
        {
            List<int> resList = new List<int>() ;
            foreach (ListViewItem item in lbVariant.Items)
            {
                if (item.Checked) {
                    resList.Add(item.Index);
                }
            }
            loader.CompareResult(resList);
            loader.Next();
        }

    }
}
