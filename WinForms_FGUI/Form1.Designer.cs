﻿namespace WinForms_FGUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            fguiPath = new TextBox();
            btnSearch = new Button();
            txtConsole = new TextBox();
            btnPackage = new Button();
            btnRef = new Button();
            fguiPKGTxt = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            ignoreTxt = new TextBox();
            label4 = new Label();
            ignoreIconCommon = new TextBox();
            checkImgBtn = new Button();
            label5 = new Label();
            textComView = new TextBox();
            comSearchBtn = new Button();
            SuspendLayout();
            // 
            // fguiPath
            // 
            fguiPath.Location = new Point(66, 6);
            fguiPath.Name = "fguiPath";
            fguiPath.Size = new Size(476, 23);
            fguiPath.TabIndex = 0;
            fguiPath.Text = "G:\\Bingganren2021_SVN\\client\\FguiProject\\assets";
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(289, 112);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(104, 25);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "图片无引用的";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtConsole
            // 
            txtConsole.Location = new Point(12, 151);
            txtConsole.Multiline = true;
            txtConsole.Name = "txtConsole";
            txtConsole.Size = new Size(776, 758);
            txtConsole.TabIndex = 2;
            // 
            // btnPackage
            // 
            btnPackage.Location = new Point(639, 36);
            btnPackage.Name = "btnPackage";
            btnPackage.Size = new Size(75, 24);
            btnPackage.TabIndex = 3;
            btnPackage.Text = "拥有的包";
            btnPackage.UseVisualStyleBackColor = true;
            btnPackage.Click += btnPackage_Click;
            // 
            // btnRef
            // 
            btnRef.Location = new Point(719, 36);
            btnRef.Name = "btnRef";
            btnRef.Size = new Size(75, 24);
            btnRef.TabIndex = 4;
            btnRef.Text = "查询依赖";
            btnRef.UseVisualStyleBackColor = true;
            btnRef.Click += btnRef_Click;
            // 
            // fguiPKGTxt
            // 
            fguiPKGTxt.Location = new Point(188, 113);
            fguiPKGTxt.Name = "fguiPKGTxt";
            fguiPKGTxt.Size = new Size(100, 23);
            fguiPKGTxt.TabIndex = 5;
            fguiPKGTxt.Text = "Common";
            fguiPKGTxt.TextChanged += fguiPKGTxt_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 116);
            label1.Name = "label1";
            label1.Size = new Size(175, 17);
            label1.TabIndex = 6;
            label1.Text = "若不填(空白),则默认为整个工程";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 9);
            label2.Name = "label2";
            label2.Size = new Size(56, 17);
            label2.TabIndex = 7;
            label2.Text = "工程目录";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 40);
            label3.Name = "label3";
            label3.Size = new Size(157, 17);
            label3.TabIndex = 8;
            label3.Text = "查业务包依赖业务包_公共包";
            // 
            // ignoreTxt
            // 
            ignoreTxt.Location = new Point(165, 38);
            ignoreTxt.Name = "ignoreTxt";
            ignoreTxt.Size = new Size(467, 23);
            ignoreTxt.TabIndex = 9;
            ignoreTxt.Text = "Common;Items;Font";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 79);
            label4.Name = "label4";
            label4.Size = new Size(172, 17);
            label4.TabIndex = 10;
            label4.Text = "查无引用的图片_icon包;常驻包";
            // 
            // ignoreIconCommon
            // 
            ignoreIconCommon.Location = new Point(183, 76);
            ignoreIconCommon.Name = "ignoreIconCommon";
            ignoreIconCommon.Size = new Size(222, 23);
            ignoreIconCommon.TabIndex = 11;
            ignoreIconCommon.Text = "Items;Common";
            // 
            // checkImgBtn
            // 
            checkImgBtn.Location = new Point(670, 5);
            checkImgBtn.Name = "checkImgBtn";
            checkImgBtn.Size = new Size(118, 24);
            checkImgBtn.TabIndex = 12;
            checkImgBtn.Text = "检测图片";
            checkImgBtn.UseVisualStyleBackColor = true;
            checkImgBtn.Click += checkImgBtn_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(430, 82);
            label5.Name = "label5";
            label5.Size = new Size(97, 17);
            label5.TabIndex = 13;
            label5.Text = "查_无引用的组件";
            // 
            // textComView
            // 
            textComView.Location = new Point(524, 79);
            textComView.Name = "textComView";
            textComView.Size = new Size(145, 23);
            textComView.TabIndex = 14;
            textComView.Text = "WorkShop";
            // 
            // comSearchBtn
            // 
            comSearchBtn.Location = new Point(675, 79);
            comSearchBtn.Name = "comSearchBtn";
            comSearchBtn.Size = new Size(54, 25);
            comSearchBtn.TabIndex = 15;
            comSearchBtn.Text = "引用";
            comSearchBtn.UseVisualStyleBackColor = true;
            comSearchBtn.Click += comSearchBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 921);
            Controls.Add(comSearchBtn);
            Controls.Add(textComView);
            Controls.Add(label5);
            Controls.Add(checkImgBtn);
            Controls.Add(ignoreIconCommon);
            Controls.Add(label4);
            Controls.Add(ignoreTxt);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(fguiPKGTxt);
            Controls.Add(btnRef);
            Controls.Add(btnPackage);
            Controls.Add(txtConsole);
            Controls.Add(btnSearch);
            Controls.Add(fguiPath);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox fguiPath;
        private Button btnSearch;
        private TextBox txtConsole;
        private Button btnPackage;
        private Button btnRef;
        private TextBox fguiPKGTxt;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox ignoreTxt;
        private Label label4;
        private TextBox ignoreIconCommon;
        private Button checkImgBtn;
        private Label label5;
        private TextBox textComView;
        private Button comSearchBtn;
    }
}