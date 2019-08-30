﻿namespace FinanceProStudent
{
    partial class TestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
            this.lbQuestionText = new System.Windows.Forms.Label();
            this.lbQuesCaption = new System.Windows.Forms.Label();
            this.btAnswer = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lbVariant = new WindowsFormsControlLibrary1.CustomListView();
            this.SuspendLayout();
            // 
            // lbQuestionText
            // 
            this.lbQuestionText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbQuestionText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbQuestionText.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbQuestionText.Location = new System.Drawing.Point(54, 103);
            this.lbQuestionText.Name = "lbQuestionText";
            this.lbQuestionText.Size = new System.Drawing.Size(1142, 135);
            this.lbQuestionText.TabIndex = 0;
            // 
            // lbQuesCaption
            // 
            this.lbQuesCaption.AutoSize = true;
            this.lbQuesCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbQuesCaption.Location = new System.Drawing.Point(54, 52);
            this.lbQuesCaption.Name = "lbQuesCaption";
            this.lbQuesCaption.Size = new System.Drawing.Size(112, 31);
            this.lbQuesCaption.TabIndex = 1;
            this.lbQuesCaption.Text = "Вопрос";
            // 
            // btAnswer
            // 
            this.btAnswer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAnswer.Enabled = false;
            this.btAnswer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btAnswer.Location = new System.Drawing.Point(1061, 580);
            this.btAnswer.Name = "btAnswer";
            this.btAnswer.Size = new System.Drawing.Size(137, 44);
            this.btAnswer.TabIndex = 3;
            this.btAnswer.Text = "Ответить";
            this.btAnswer.UseVisualStyleBackColor = true;
            this.btAnswer.Click += new System.EventHandler(this.btAnswer_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(54, 258);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(306, 35);
            this.label3.TabIndex = 5;
            this.label3.Text = "Варианты ответов";
            // 
            // lbVariant
            // 
            this.lbVariant.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbVariant.CheckBoxes = true;
            this.lbVariant.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.lbVariant.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lbVariant.Location = new System.Drawing.Point(65, 304);
            this.lbVariant.Name = "lbVariant";
            this.lbVariant.OwnerDraw = true;
            this.lbVariant.Size = new System.Drawing.Size(1131, 257);
            this.lbVariant.TabIndex = 6;
            this.lbVariant.UseCompatibleStateImageBehavior = false;
            this.lbVariant.View = System.Windows.Forms.View.Details;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1249, 636);
            this.Controls.Add(this.lbVariant);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btAnswer);
            this.Controls.Add(this.lbQuesCaption);
            this.Controls.Add(this.lbQuestionText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Finance pro [Тестирование]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbQuestionText;
        private System.Windows.Forms.Label lbQuesCaption;
        private System.Windows.Forms.Button btAnswer;
        private System.Windows.Forms.Label label3;
        private WindowsFormsControlLibrary1.CustomListView lbVariant;
    }
}