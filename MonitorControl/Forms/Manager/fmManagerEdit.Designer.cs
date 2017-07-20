namespace TradingLib.MoniterControl
{
    partial class fmManagerEdit
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
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonGroupBox2 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.ctBankList = new TradingLib.MoniterControl.ctBankList();
            this.branch = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.bankac = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel11 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.agentBox = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel10 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.acclimit = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.agentType = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.agentlimit = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonGroupBox1 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.kryptonLabel14 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.memo = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel13 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.idcard = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel12 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.email = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.name = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.qq = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.mobile = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel9 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnSubmit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.type = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.login = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).BeginInit();
            this.kryptonGroupBox2.Panel.SuspendLayout();
            this.kryptonGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.agentBox)).BeginInit();
            this.agentBox.Panel.SuspendLayout();
            this.agentBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.agentType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.type)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox2);
            this.kryptonPanel1.Controls.Add(this.agentBox);
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox1);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel9);
            this.kryptonPanel1.Controls.Add(this.btnSubmit);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel1.Controls.Add(this.type);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel1.Controls.Add(this.login);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(311, 603);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonGroupBox2
            // 
            this.kryptonGroupBox2.Location = new System.Drawing.Point(12, 299);
            this.kryptonGroupBox2.Name = "kryptonGroupBox2";
            // 
            // kryptonGroupBox2.Panel
            // 
            this.kryptonGroupBox2.Panel.Controls.Add(this.ctBankList);
            this.kryptonGroupBox2.Panel.Controls.Add(this.branch);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kryptonLabel2);
            this.kryptonGroupBox2.Panel.Controls.Add(this.bankac);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kryptonLabel11);
            this.kryptonGroupBox2.Size = new System.Drawing.Size(284, 107);
            this.kryptonGroupBox2.TabIndex = 24;
            this.kryptonGroupBox2.Values.Heading = "财务信息";
            // 
            // ctBankList
            // 
            this.ctBankList.BankSelected = 0;
            this.ctBankList.Location = new System.Drawing.Point(34, 3);
            this.ctBankList.Name = "ctBankList";
            this.ctBankList.Size = new System.Drawing.Size(173, 21);
            this.ctBankList.TabIndex = 24;
            // 
            // branch
            // 
            this.branch.Location = new System.Drawing.Point(84, 30);
            this.branch.Name = "branch";
            this.branch.Size = new System.Drawing.Size(161, 21);
            this.branch.TabIndex = 13;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(22, 32);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(55, 18);
            this.kryptonLabel2.TabIndex = 14;
            this.kryptonLabel2.Values.Text = "开户行:";
            // 
            // bankac
            // 
            this.bankac.Location = new System.Drawing.Point(84, 56);
            this.bankac.Name = "bankac";
            this.bankac.Size = new System.Drawing.Size(161, 21);
            this.bankac.TabIndex = 15;
            // 
            // kryptonLabel11
            // 
            this.kryptonLabel11.Location = new System.Drawing.Point(8, 58);
            this.kryptonLabel11.Name = "kryptonLabel11";
            this.kryptonLabel11.Size = new System.Drawing.Size(68, 18);
            this.kryptonLabel11.TabIndex = 16;
            this.kryptonLabel11.Values.Text = "银行账户:";
            // 
            // agentBox
            // 
            this.agentBox.Location = new System.Drawing.Point(12, 422);
            this.agentBox.Name = "agentBox";
            // 
            // agentBox.Panel
            // 
            this.agentBox.Panel.Controls.Add(this.kryptonLabel8);
            this.agentBox.Panel.Controls.Add(this.kryptonLabel10);
            this.agentBox.Panel.Controls.Add(this.kryptonLabel1);
            this.agentBox.Panel.Controls.Add(this.acclimit);
            this.agentBox.Panel.Controls.Add(this.agentType);
            this.agentBox.Panel.Controls.Add(this.agentlimit);
            this.agentBox.Size = new System.Drawing.Size(284, 133);
            this.agentBox.TabIndex = 23;
            this.agentBox.Values.Heading = "会员信息";
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(13, 45);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(68, 18);
            this.kryptonLabel8.TabIndex = 13;
            this.kryptonLabel8.Values.Text = "可开帐户:";
            // 
            // kryptonLabel10
            // 
            this.kryptonLabel10.Location = new System.Drawing.Point(13, 71);
            this.kryptonLabel10.Name = "kryptonLabel10";
            this.kryptonLabel10.Size = new System.Drawing.Size(68, 18);
            this.kryptonLabel10.TabIndex = 18;
            this.kryptonLabel10.Values.Text = "可开会员:";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(13, 19);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(68, 18);
            this.kryptonLabel1.TabIndex = 21;
            this.kryptonLabel1.Values.Text = "会员类别:";
            // 
            // acclimit
            // 
            this.acclimit.Location = new System.Drawing.Point(86, 43);
            this.acclimit.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.acclimit.Name = "acclimit";
            this.acclimit.Size = new System.Drawing.Size(101, 20);
            this.acclimit.TabIndex = 8;
            // 
            // agentType
            // 
            this.agentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.agentType.DropDownWidth = 101;
            this.agentType.Location = new System.Drawing.Point(86, 19);
            this.agentType.Name = "agentType";
            this.agentType.Size = new System.Drawing.Size(101, 21);
            this.agentType.TabIndex = 20;
            // 
            // agentlimit
            // 
            this.agentlimit.Location = new System.Drawing.Point(86, 71);
            this.agentlimit.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.agentlimit.Name = "agentlimit";
            this.agentlimit.Size = new System.Drawing.Size(101, 20);
            this.agentlimit.TabIndex = 19;
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Location = new System.Drawing.Point(12, 70);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel14);
            this.kryptonGroupBox1.Panel.Controls.Add(this.memo);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel13);
            this.kryptonGroupBox1.Panel.Controls.Add(this.idcard);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel12);
            this.kryptonGroupBox1.Panel.Controls.Add(this.email);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel5);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel6);
            this.kryptonGroupBox1.Panel.Controls.Add(this.name);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel7);
            this.kryptonGroupBox1.Panel.Controls.Add(this.qq);
            this.kryptonGroupBox1.Panel.Controls.Add(this.mobile);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(285, 223);
            this.kryptonGroupBox1.TabIndex = 22;
            this.kryptonGroupBox1.Values.Heading = "基础信息";
            // 
            // kryptonLabel14
            // 
            this.kryptonLabel14.Location = new System.Drawing.Point(40, 147);
            this.kryptonLabel14.Name = "kryptonLabel14";
            this.kryptonLabel14.Size = new System.Drawing.Size(41, 18);
            this.kryptonLabel14.TabIndex = 22;
            this.kryptonLabel14.Values.Text = "备注:";
            // 
            // memo
            // 
            this.memo.Location = new System.Drawing.Point(87, 145);
            this.memo.Name = "memo";
            this.memo.Size = new System.Drawing.Size(161, 21);
            this.memo.TabIndex = 21;
            // 
            // kryptonLabel13
            // 
            this.kryptonLabel13.Location = new System.Drawing.Point(27, 121);
            this.kryptonLabel13.Name = "kryptonLabel13";
            this.kryptonLabel13.Size = new System.Drawing.Size(55, 18);
            this.kryptonLabel13.TabIndex = 20;
            this.kryptonLabel13.Values.Text = "身份证:";
            // 
            // idcard
            // 
            this.idcard.Location = new System.Drawing.Point(86, 119);
            this.idcard.Name = "idcard";
            this.idcard.Size = new System.Drawing.Size(161, 21);
            this.idcard.TabIndex = 19;
            // 
            // kryptonLabel12
            // 
            this.kryptonLabel12.Location = new System.Drawing.Point(35, 95);
            this.kryptonLabel12.Name = "kryptonLabel12";
            this.kryptonLabel12.Size = new System.Drawing.Size(45, 18);
            this.kryptonLabel12.TabIndex = 18;
            this.kryptonLabel12.Values.Text = "Email:";
            // 
            // email
            // 
            this.email.Location = new System.Drawing.Point(86, 93);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(101, 21);
            this.email.TabIndex = 17;
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(39, 15);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(41, 18);
            this.kryptonLabel5.TabIndex = 10;
            this.kryptonLabel5.Values.Text = "姓名:";
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(39, 41);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(41, 18);
            this.kryptonLabel6.TabIndex = 11;
            this.kryptonLabel6.Values.Text = "手机:";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(86, 11);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(101, 21);
            this.name.TabIndex = 5;
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(45, 69);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(33, 18);
            this.kryptonLabel7.TabIndex = 12;
            this.kryptonLabel7.Values.Text = "QQ:";
            // 
            // qq
            // 
            this.qq.Location = new System.Drawing.Point(86, 67);
            this.qq.Name = "qq";
            this.qq.Size = new System.Drawing.Size(101, 21);
            this.qq.TabIndex = 7;
            // 
            // mobile
            // 
            this.mobile.Location = new System.Drawing.Point(86, 39);
            this.mobile.Name = "mobile";
            this.mobile.Size = new System.Drawing.Size(101, 21);
            this.mobile.TabIndex = 6;
            // 
            // kryptonLabel9
            // 
            this.kryptonLabel9.Location = new System.Drawing.Point(224, 12);
            this.kryptonLabel9.Name = "kryptonLabel9";
            this.kryptonLabel9.Size = new System.Drawing.Size(78, 18);
            this.kryptonLabel9.TabIndex = 17;
            this.kryptonLabel9.Values.Text = "数字与字母";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(239, 566);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(60, 25);
            this.btnSubmit.TabIndex = 16;
            this.btnSubmit.Values.Text = "提 交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(54, 44);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(41, 18);
            this.kryptonLabel4.TabIndex = 9;
            this.kryptonLabel4.Values.Text = "类型:";
            // 
            // type
            // 
            this.type.DropDownWidth = 135;
            this.type.Location = new System.Drawing.Point(101, 41);
            this.type.Name = "type";
            this.type.Size = new System.Drawing.Size(101, 21);
            this.type.TabIndex = 4;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(40, 16);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(55, 18);
            this.kryptonLabel3.TabIndex = 3;
            this.kryptonLabel3.Values.Text = "用户名:";
            // 
            // login
            // 
            this.login.Location = new System.Drawing.Point(101, 12);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(101, 21);
            this.login.TabIndex = 2;
            // 
            // fmManagerEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 603);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmManagerEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.kryptonGroupBox2.Panel.ResumeLayout(false);
            this.kryptonGroupBox2.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).EndInit();
            this.kryptonGroupBox2.ResumeLayout(false);
            this.agentBox.Panel.ResumeLayout(false);
            this.agentBox.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.agentBox)).EndInit();
            this.agentBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.agentType)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.type)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown acclimit;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox qq;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox mobile;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox name;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox type;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox login;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSubmit;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel9;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel10;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown agentlimit;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox agentType;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox agentBox;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel11;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox bankac;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox branch;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel12;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox email;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel13;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox idcard;
        private ctBankList ctBankList;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel14;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox memo;
    }
}