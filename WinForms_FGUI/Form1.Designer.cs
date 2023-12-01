namespace WinForms_FGUI
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
            this.fguiPath = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.btnPackage = new System.Windows.Forms.Button();
            this.btnRef = new System.Windows.Forms.Button();
            this.fguiPKGTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ignoreTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ignoreIconCommon = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // fguiPath
            // 
            this.fguiPath.Location = new System.Drawing.Point(66, 6);
            this.fguiPath.Name = "fguiPath";
            this.fguiPath.Size = new System.Drawing.Size(376, 23);
            this.fguiPath.TabIndex = 0;
            this.fguiPath.Text = "G:\\Bingganren2021_SVN\\client\\FguiProject\\assets";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(604, 75);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(104, 25);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "图片无引用的";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtConsole
            // 
            this.txtConsole.Location = new System.Drawing.Point(12, 114);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.Size = new System.Drawing.Size(776, 735);
            this.txtConsole.TabIndex = 2;
            // 
            // btnPackage
            // 
            this.btnPackage.Location = new System.Drawing.Point(328, 38);
            this.btnPackage.Name = "btnPackage";
            this.btnPackage.Size = new System.Drawing.Size(77, 24);
            this.btnPackage.TabIndex = 3;
            this.btnPackage.Text = "拥有的包";
            this.btnPackage.UseVisualStyleBackColor = true;
            this.btnPackage.Click += new System.EventHandler(this.btnPackage_Click);
            // 
            // btnRef
            // 
            this.btnRef.Location = new System.Drawing.Point(411, 38);
            this.btnRef.Name = "btnRef";
            this.btnRef.Size = new System.Drawing.Size(102, 24);
            this.btnRef.TabIndex = 4;
            this.btnRef.Text = "查询依赖";
            this.btnRef.UseVisualStyleBackColor = true;
            this.btnRef.Click += new System.EventHandler(this.btnRef_Click);
            // 
            // fguiPKGTxt
            // 
            this.fguiPKGTxt.Location = new System.Drawing.Point(498, 76);
            this.fguiPKGTxt.Name = "fguiPKGTxt";
            this.fguiPKGTxt.Size = new System.Drawing.Size(100, 23);
            this.fguiPKGTxt.TabIndex = 5;
            this.fguiPKGTxt.Text = "Common";
            this.fguiPKGTxt.TextChanged += new System.EventHandler(this.fguiPKGTxt_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(319, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "若不填(空白),则默认为整个工程";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "工程目录";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "公共包";
            // 
            // ignoreTxt
            // 
            this.ignoreTxt.Location = new System.Drawing.Point(55, 38);
            this.ignoreTxt.Name = "ignoreTxt";
            this.ignoreTxt.Size = new System.Drawing.Size(267, 23);
            this.ignoreTxt.TabIndex = 9;
            this.ignoreTxt.Text = "Common;Items;Font";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "icon包;常驻包";
            // 
            // ignoreIconCommon
            // 
            this.ignoreIconCommon.Location = new System.Drawing.Point(91, 76);
            this.ignoreIconCommon.Name = "ignoreIconCommon";
            this.ignoreIconCommon.Size = new System.Drawing.Size(222, 23);
            this.ignoreIconCommon.TabIndex = 11;
            this.ignoreIconCommon.Text = "Items;Common";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 861);
            this.Controls.Add(this.ignoreIconCommon);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ignoreTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fguiPKGTxt);
            this.Controls.Add(this.btnRef);
            this.Controls.Add(this.btnPackage);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.fguiPath);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}