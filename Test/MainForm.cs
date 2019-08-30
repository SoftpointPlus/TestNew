using CommonLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinanceProAdmin
{
    public delegate void BulkInsertAnswer(List<string> answers);
    public partial class MainForm : Form
    {
        bool ignoreCheck = true;
        QuestionsLoader loader;
        ToolTip toolTip = new ToolTip();
        public MainForm()
        {
            InitializeComponent();
            toolTip.SetToolTip(button1, "Добавить вопрос");
            toolTip.SetToolTip(button3, "Добавить ответ");
            toolTip.SetToolTip(button2, "Удалить вопрос");
            toolTip.SetToolTip(button4, "Удалить ответ");
            toolTip.SetToolTip(lbAnswers, "Двойной щелчек вызывает редактор");
            toolTip.SetToolTip(cbAnswers, "Двойной щелчек вызывает редактор");
            CreateQestions();
        }

        private DialogResult ShowAddAnswerForm(string data)
        {
            AddAnswerForm addForm = new AddAnswerForm();
            CallBackMy.callbackEventHandler(data);
            return addForm.ShowDialog();

        }
        private void CreateQestions()
        {
            loader = new QuestionsLoader();
            CallBackMy.AddFormCaptionEnentHandler = SetFormCaption;
            Questions questions = loader.Questions;
            tbCaption.Text = loader.Caption;
            tbQuestionLimit.Text = loader.QuestionLimit.ToString();
            lbAnswers.DataSource = questions.QuestionsList;
            this.Text = "Finance pro [Администратор - Новый]";

        }
        private void LoadQuestions(string path)
        {
            loader = new QuestionsLoader();
            loader.SetTestNameHandler += loader_SetTestNameHandler;
            loader.SetQuestionLimitHandler += SetQuestionLimit;    
            CallBackMy.AddFormCaptionEnentHandler = SetFormCaption;
            loader.LoadQuestions(path);
            Questions questions = loader.Questions;
            lbAnswers.DataSource = questions.QuestionsList;

            
        }

        private void loader_SetTestNameHandler(string name)
        {
            tbCaption.Text = name;
        }

        private void SetQuestionLimit(int limit)
        {
            tbQuestionLimit.Text = limit.ToString();
        }
        

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (loader.Changed)
            {
                DialogResult result = MessageBox.Show("Сохранить изменения?", "Внимание!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if(result == DialogResult.Yes)
                {
                    if (loader.Path == "")
                    {
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            loader.SaveQuestionsAs(saveFileDialog.FileName);
                        }
                    }
                    else
                        loader.SaveQuestions();
                    e.Cancel = false;
                }
                else if(result == DialogResult.No)
                {
                    e.Cancel = false;
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }

            }
            
        }


        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (ignoreCheck) e.NewValue = e.CurrentValue;
            Answer answer = cbAnswers.SelectedItem as Answer;
            if (answer != null)
            {
                answer.Verity = (e.NewValue == CheckState.Checked ? true : false);
            }
        }

        private void checkedListBox1_MouseClick(object sender, MouseEventArgs e)
        {
            ignoreCheck = e.X > SystemInformation.MenuCheckSize.Width;
            Console.WriteLine("MouseClick");
        }

        private void checkedListBox1_MouseUp(object sender, MouseEventArgs e)
        {
     
        }

        private void checkedListBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (cbAnswers.SelectedItem != null)
            {
                CallBackMy.AddQuestionTextEventHandler = EditAnswerText;
                ShowAddAnswerForm(cbAnswers.SelectedItem.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            CallBackMy.AddQuestionTextEventHandler = AddAnswerText;
            ShowAddAnswerForm("");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CallBackMy.AddQuestionTextEventHandler = AddQuestionText;
            ShowAddAnswerForm("");
        }



        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Question question;
            if (lbAnswers.SelectedItem != null)
            {
                question = lbAnswers.SelectedItem as Question;
                if (question != null)
                {
                    CallBackMy.AddQuestionTextEventHandler = EditQuestionText;
                    ShowAddAnswerForm(question.Text);


                }
            }

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lbAnswers.SelectedIndex != -1)
            {
                DialogResult result = MessageBox.Show("Удалить вопрос?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    loader.Questions.Delete(lbAnswers.SelectedIndex);
                    BindData(lbAnswers, loader.Questions.QuestionsList);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Question question = lbAnswers.SelectedItem as Question;
            Answer answer;
            if (question != null)
            {
                DialogResult result = MessageBox.Show("Удалить вариант ответа?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    question.Answers.Delete(cbAnswers.SelectedIndex);
                    BindData(cbAnswers, question.Answers.AnswerList);
                    for (int count = 0; count < cbAnswers.Items.Count; count++)
                    {
                        answer = cbAnswers.Items[count] as Answer;
                        if (answer != null)
                        {
                            if (answer.Verity)
                            {
                                cbAnswers.ItemCheck -= checkedListBox1_ItemCheck;
                                cbAnswers.SetItemChecked(count, true);
                                cbAnswers.ItemCheck += checkedListBox1_ItemCheck;
                            }
                        }
                    }
                }
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadQuestions(openFileDialog.FileName);
            }

        }

        private void AddQuestionText(string data)
        {
            Question question = loader.Questions.Add(data);
            BindData(lbAnswers, loader.Questions.QuestionsList);
            lbAnswers.SelectedIndex = loader.Questions.Count - 1;
        }

        private void AddAnswerText(string data)
        {
            Question question = lbAnswers.SelectedItem as Question;
            Answer answer;
            if (question != null)
            {
                question.Add(data, false);
                BindData(cbAnswers, question.Answers.AnswerList);
                for (int count = 0; count < cbAnswers.Items.Count; count++)
                {
                    answer = cbAnswers.Items[count] as Answer;
                    if (answer != null)
                    {
                        if (answer.Verity)
                        {
                            cbAnswers.ItemCheck -= checkedListBox1_ItemCheck;
                            cbAnswers.SetItemChecked(count, true);
                            cbAnswers.ItemCheck += checkedListBox1_ItemCheck;
                        }
                    }
                }

            }
        }
        private void BulkInsertAnswer(List<string> answers)
        {
            Question question = lbAnswers.SelectedItem as Question;
            if (question != null)
            {
                Answer answer;
                if (answers != null)
                {
                    foreach (string ans in answers)
                    {
                        question.Add(ans, false);
                        
                    }
                    BindData(cbAnswers, question.Answers.AnswerList);
                    for (int count = 0; count < cbAnswers.Items.Count; count++)
                    {
                        answer = cbAnswers.Items[count] as Answer;
                        if (answer != null)
                        {
                            if (answer.Verity)
                            {
                                cbAnswers.ItemCheck -= checkedListBox1_ItemCheck;
                                cbAnswers.SetItemChecked(count, true);
                                cbAnswers.ItemCheck += checkedListBox1_ItemCheck;
                            }
                        }
                    }
                }
            }
        }

        private void EditQuestionText(string data)
        {
            Question question = lbAnswers.SelectedItem as Question;
            if (question != null)
            {
                question.Text = data;
                BindData(lbAnswers, loader.Questions.QuestionsList);
            }
        }
        private void EditAnswerText(string data)
        {
            Answer answer = cbAnswers.SelectedItem as Answer;
            Question question = lbAnswers.SelectedItem as Question;
            if (question != null){
                if (answer != null)
                {
                    answer.Data = data;
                    BindData(cbAnswers, question.Answers.AnswerList);
                    for (int count = 0; count < cbAnswers.Items.Count; count++)
                    {
                        answer = cbAnswers.Items[count] as Answer;
                        if (answer != null)
                        {
                            if (answer.Verity)
                            {
                                cbAnswers.ItemCheck -= checkedListBox1_ItemCheck;
                                cbAnswers.SetItemChecked(count, true);
                                cbAnswers.ItemCheck += checkedListBox1_ItemCheck;
                            }
                        }
                    }
                }
            }
        }
        private void BindData(ListControl source, IList list)
        {
            source.DataSource = null;
            source.DataSource = list;
        }
        private void DeleteItem(IList list, int index)
        {
            if (list != null)
            {
                lbAnswers.BeginUpdate();
                if (index < list.Count)
                {
                    list.RemoveAt(index);
                }
                BindData(lbAnswers, loader.Questions.QuestionsList);
                lbAnswers.EndUpdate();
            }
        }

        private void lbAnswers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Question question = lbAnswers.SelectedItem as Question;
            Answer answer;
            if (question != null)
            {
                BindData(cbAnswers, question.Answers.AnswerList);
               
            }
            for (int count = 0; count < cbAnswers.Items.Count; count++)
            {
                answer = cbAnswers.Items[count] as Answer;
                if (answer != null) 
                {
                    if (answer.Verity)
                    {
                        cbAnswers.ItemCheck -= checkedListBox1_ItemCheck;
                        cbAnswers.SetItemChecked(count, true);
                        cbAnswers.ItemCheck += checkedListBox1_ItemCheck;
                    }
                }
            }

        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                loader.SaveQuestionsAs(saveFileDialog.FileName);
                LoadQuestions(saveFileDialog.FileName);
            }
            
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loader.Path == "" )
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    loader.SaveQuestionsAs(saveFileDialog.FileName);
                }
            }
            else
                loader.SaveQuestions();
        }

        private void SetMenuEnable(string name, bool enable)
        {
            ToolStripMenuItem menuItem = menuStrip.Items["file"] as ToolStripMenuItem;
            ToolStripMenuItem sourceItem = menuItem.DropDownItems[name] as ToolStripMenuItem;
            if (sourceItem != null)
            {
                sourceItem.Enabled = enable;
            }

        }
        private void SetFormCaption(string path)
        {
            this.Text = "Finance pro [Администратор - " + Path.GetFileName(path) + "]";
        }

        private void tbCaption_TextChanged(object sender, EventArgs e)
        {
            if (loader != null)
            {
                loader.Caption = tbCaption.Text;
            }
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (loader.Changed)
            {
                DialogResult result = MessageBox.Show("Сохранить изменения?", "Внимание!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    if (loader.Path == "")
                    {
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            loader.SaveQuestionsAs(saveFileDialog.FileName);
                        }
                    }
                    else
                        loader.SaveQuestions();
                    CreateQestions();
                }
                else if (result == DialogResult.No)
                {
                    CreateQestions();
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }

            }

            
            
        }

        private void lbAnswers_DisplayMemberChanged(object sender, EventArgs e)
        {

        }

        private void lbAnswers_Format(object sender, ListControlConvertEventArgs e)
        {
            Question ques = e.ListItem as Question;
            if (ques != null)
            {
                var items = (sender as ListControl).DataSource as IList<Question>;
                if (items != null)
                    e.Value = items.IndexOf(ques) + 1 + ". " + ques.Text;
            }
        }

        private void lbAnswers_DataSourceChanged(object sender, EventArgs e)
        {

        }

        private void cbAnswers_Format(object sender, ListControlConvertEventArgs e)
        {
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == 8);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (loader != null)
            {
                int res = 0 ;
                if (Int32.TryParse(tbQuestionLimit.Text, out res))
                {
                    loader.QuestionLimit = res;
                }
                else
                    loader.QuestionLimit = 10;
                        
            }
        }

        private void btInsAll_Click(object sender, EventArgs e)
        {
            InsAnswerForm insForm = new InsAnswerForm();
            insForm.bulkInsertAnswersHandler += BulkInsertAnswer;
            insForm.ShowDialog();
        }
    }
}
