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
            components = new System.ComponentModel.Container();
            fguiProjectPath = new TextBox();
            btn_SelfImg = new Button();
            txtConsole = new TextBox();
            btnPackage = new Button();
            btnRef = new Button();
            txtNoneImg = new TextBox();
            lblNoneImg = new Label();
            label2 = new Label();
            lblCommon = new Label();
            txt_Ignore = new TextBox();
            checkImgBtn = new Button();
            lblNoneCom = new Label();
            txtNoneCom = new TextBox();
            btn_SelfCom = new Button();
            btn_GlobalCom = new Button();
            btn_GlobalImg = new Button();
            globalTip = new ToolTip(components);
            btn_ProjectCom = new Button();
            btn_ProjectImg = new Button();
            txtScriptJson = new TextBox();
            cbExtend = new CheckBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // fguiProjectPath
            // 
            fguiProjectPath.Location = new Point(67, 6);
            fguiProjectPath.Name = "fguiProjectPath";
            fguiProjectPath.Size = new Size(476, 23);
            fguiProjectPath.TabIndex = 0;
            fguiProjectPath.Text = "G:\\Bingganren2021_SVN\\client\\FguiProject\\assets";
            // 
            // btn_SelfImg
            // 
            btn_SelfImg.Location = new Point(265, 103);
            btn_SelfImg.Name = "btn_SelfImg";
            btn_SelfImg.Size = new Size(129, 25);
            btn_SelfImg.TabIndex = 1;
            btn_SelfImg.Text = "仅本包_图片无引用的";
            btn_SelfImg.UseVisualStyleBackColor = true;
            btn_SelfImg.Click += btn_SelfImg_Click;
            // 
            // txtConsole
            // 
            txtConsole.Location = new Point(12, 144);
            txtConsole.Multiline = true;
            txtConsole.Name = "txtConsole";
            txtConsole.Size = new Size(776, 765);
            txtConsole.TabIndex = 2;
            // 
            // btnPackage
            // 
            btnPackage.Location = new Point(548, 5);
            btnPackage.Name = "btnPackage";
            btnPackage.Size = new Size(84, 24);
            btnPackage.TabIndex = 3;
            btnPackage.Text = "拥有的包";
            btnPackage.UseVisualStyleBackColor = true;
            btnPackage.Click += btnPackage_Click;
            // 
            // btnRef
            // 
            btnRef.Location = new Point(638, 38);
            btnRef.Name = "btnRef";
            btnRef.Size = new Size(137, 24);
            btnRef.TabIndex = 4;
            btnRef.Text = "查_组件错误依赖的";
            btnRef.UseVisualStyleBackColor = true;
            btnRef.Click += btnRef_Click;
            // 
            // txtNoneImg
            // 
            txtNoneImg.Location = new Point(114, 105);
            txtNoneImg.Name = "txtNoneImg";
            txtNoneImg.Size = new Size(145, 23);
            txtNoneImg.TabIndex = 5;
            txtNoneImg.Text = "Common";
            txtNoneImg.TextChanged += txtNoneImg_TextChanged;
            // 
            // lblNoneImg
            // 
            lblNoneImg.AutoSize = true;
            lblNoneImg.Location = new Point(12, 107);
            lblNoneImg.Name = "lblNoneImg";
            lblNoneImg.Size = new Size(97, 17);
            lblNoneImg.TabIndex = 6;
            lblNoneImg.Text = "查_无引用的图片";
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
            // lblCommon
            // 
            lblCommon.AutoSize = true;
            lblCommon.Location = new Point(12, 40);
            lblCommon.Name = "lblCommon";
            lblCommon.Size = new Size(194, 17);
            lblCommon.TabIndex = 8;
            lblCommon.Text = "共用的业务包;策划配表ItemIcon包";
            // 
            // txt_Ignore
            // 
            txt_Ignore.Location = new Point(203, 38);
            txt_Ignore.Name = "txt_Ignore";
            txt_Ignore.Size = new Size(429, 23);
            txt_Ignore.TabIndex = 9;
            txt_Ignore.Text = "Common;Font;ItemPKG";
            // 
            // checkImgBtn
            // 
            checkImgBtn.Location = new Point(684, 5);
            checkImgBtn.Name = "checkImgBtn";
            checkImgBtn.Size = new Size(93, 24);
            checkImgBtn.TabIndex = 12;
            checkImgBtn.Text = "检测重复图片";
            checkImgBtn.UseVisualStyleBackColor = true;
            checkImgBtn.Click += checkImgBtn_Click;
            // 
            // lblNoneCom
            // 
            lblNoneCom.AutoSize = true;
            lblNoneCom.Location = new Point(11, 73);
            lblNoneCom.Name = "lblNoneCom";
            lblNoneCom.Size = new Size(97, 17);
            lblNoneCom.TabIndex = 13;
            lblNoneCom.Text = "查_无引用的组件";
            // 
            // txtNoneCom
            // 
            txtNoneCom.Location = new Point(114, 71);
            txtNoneCom.Name = "txtNoneCom";
            txtNoneCom.Size = new Size(145, 23);
            txtNoneCom.TabIndex = 14;
            txtNoneCom.Text = "WorkShop";
            txtNoneCom.TextChanged += txtNoneCom_TextChanged;
            // 
            // btn_SelfCom
            // 
            btn_SelfCom.Location = new Point(265, 71);
            btn_SelfCom.Name = "btn_SelfCom";
            btn_SelfCom.Size = new Size(129, 25);
            btn_SelfCom.TabIndex = 15;
            btn_SelfCom.Text = "仅本包_组件无引用的";
            btn_SelfCom.UseVisualStyleBackColor = true;
            btn_SelfCom.Click += btn_SelfCom_Click;
            // 
            // btn_GlobalCom
            // 
            btn_GlobalCom.Location = new Point(397, 71);
            btn_GlobalCom.Name = "btn_GlobalCom";
            btn_GlobalCom.Size = new Size(118, 25);
            btn_GlobalCom.TabIndex = 16;
            btn_GlobalCom.Text = "全局与本包_无引用";
            btn_GlobalCom.UseVisualStyleBackColor = true;
            btn_GlobalCom.Click += btn_GlobalCom_Click;
            // 
            // btn_GlobalImg
            // 
            btn_GlobalImg.Location = new Point(397, 103);
            btn_GlobalImg.Name = "btn_GlobalImg";
            btn_GlobalImg.Size = new Size(118, 25);
            btn_GlobalImg.TabIndex = 17;
            btn_GlobalImg.Text = "全局与本包_无引用";
            btn_GlobalImg.UseVisualStyleBackColor = true;
            btn_GlobalImg.Click += btn_GlobalImg_Click;
            // 
            // btn_ProjectCom
            // 
            btn_ProjectCom.Location = new Point(267, 71);
            btn_ProjectCom.Name = "btn_ProjectCom";
            btn_ProjectCom.Size = new Size(72, 25);
            btn_ProjectCom.TabIndex = 18;
            btn_ProjectCom.Text = "全搜索";
            btn_ProjectCom.UseVisualStyleBackColor = true;
            btn_ProjectCom.Click += btn_ProjectCom_Click;
            // 
            // btn_ProjectImg
            // 
            btn_ProjectImg.Location = new Point(267, 102);
            btn_ProjectImg.Name = "btn_ProjectImg";
            btn_ProjectImg.Size = new Size(72, 25);
            btn_ProjectImg.TabIndex = 19;
            btn_ProjectImg.Text = "全搜索";
            btn_ProjectImg.UseVisualStyleBackColor = true;
            btn_ProjectImg.Click += btn_ProjectImg_Click;
            // 
            // txtScriptJson
            // 
            txtScriptJson.Font = new Font("Microsoft YaHei UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            txtScriptJson.Location = new Point(521, 98);
            txtScriptJson.Multiline = true;
            txtScriptJson.Name = "txtScriptJson";
            txtScriptJson.Size = new Size(267, 41);
            txtScriptJson.TabIndex = 20;
            txtScriptJson.Text = "D:\\WorkProject\\UnityClient\\Unity\\Assets";
            // 
            // cbExtend
            // 
            cbExtend.AutoSize = true;
            cbExtend.Location = new Point(521, 71);
            cbExtend.Name = "cbExtend";
            cbExtend.Size = new Size(123, 21);
            cbExtend.TabIndex = 21;
            cbExtend.Text = "只检测设为导出的";
            cbExtend.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(703, 77);
            label1.Name = "label1";
            label1.Size = new Size(85, 17);
            label1.TabIndex = 22;
            label1.Text = "Unity工程目录";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 921);
            Controls.Add(label1);
            Controls.Add(cbExtend);
            Controls.Add(txtScriptJson);
            Controls.Add(btn_ProjectImg);
            Controls.Add(btn_ProjectCom);
            Controls.Add(btn_GlobalImg);
            Controls.Add(btn_GlobalCom);
            Controls.Add(btn_SelfCom);
            Controls.Add(txtNoneCom);
            Controls.Add(lblNoneCom);
            Controls.Add(checkImgBtn);
            Controls.Add(txt_Ignore);
            Controls.Add(lblCommon);
            Controls.Add(label2);
            Controls.Add(lblNoneImg);
            Controls.Add(txtNoneImg);
            Controls.Add(btnRef);
            Controls.Add(btnPackage);
            Controls.Add(txtConsole);
            Controls.Add(btn_SelfImg);
            Controls.Add(fguiProjectPath);
            Name = "Form1";
            Text = "FGUI检测_工程越大越耗时";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox fguiProjectPath;
        private Button btn_SelfImg;
        private TextBox txtConsole;
        private Button btnPackage;
        private Button btnRef;
        private TextBox txtNoneImg;
        private Label lblNoneImg;
        private Label label2;
        private Label lblCommon;
        private TextBox txt_Ignore;
        private Button checkImgBtn;
        private Label lblNoneCom;
        private TextBox txtNoneCom;
        private Button btn_SelfCom;
        private Button btn_GlobalCom;
        private Button btn_GlobalImg;
        private ToolTip globalTip;
        private Button btn_ProjectCom;
        private Button btn_ProjectImg;
        private TextBox txtScriptJson;
        private CheckBox cbExtend;
        private Label label1;
    }
}