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
            this.kryptonLabel9 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnSubmit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.acclimit = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.qq = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.mobile = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.name = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.type = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.login = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel10 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.agentlimit = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.type)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.agentlimit);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel10);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel9);
            this.kryptonPanel1.Controls.Add(this.btnSubmit);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel8);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel7);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel6);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel5);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel1.Controls.Add(this.acclimit);
            this.kryptonPanel1.Controls.Add(this.qq);
            this.kryptonPanel1.Controls.Add(this.mobile);
            this.kryptonPanel1.Controls.Add(this.name);
            this.kryptonPanel1.Controls.Add(this.type);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel1.Controls.Add(this.login);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(292, 248);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonLabel9
            // 
            this.kryptonLabel9.Location = new System.Drawing.Point(216, 12);
            this.kryptonLabel9.Name = "kryptonLabel9";
            this.kryptonLabel9.Size = new System.Drawing.Size(73, 20);
            this.kryptonLabel9.TabIndex = 17;
            this.kryptonLabel9.Values.Text = "数字与字母";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(216, 214);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(60, 25);
            this.btnSubmit.TabIndex = 16;
            this.btnSubmit.Values.Text = "提 交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(7, 152);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel8.TabIndex = 13;
            this.kryptonLabel8.Values.Text = "可开帐户:";
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(41, 128);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(32, 20);
            this.kryptonLabel7.TabIndex = 12;
            this.kryptonLabel7.Values.Text = "QQ:";
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(34, 100);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel6.TabIndex = 11;
            this.kryptonLabel6.Values.Text = "手机:";
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(33, 72);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel5.TabIndex = 10;
            this.kryptonLabel5.Values.Text = "姓名:";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(33, 44);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel4.TabIndex = 9;
            this.kryptonLabel4.Values.Text = "类型:";
            // 
            // acclimit
            // 
            this.acclimit.Location = new System.Drawing.Point(80, 153);
            this.acclimit.Name = "acclimit";
            this.acclimit.Size = new System.Drawing.Size(81, 22);
            this.acclimit.TabIndex = 8;
            // 
            // qq
            // 
            this.qq.Location = new System.Drawing.Point(80, 124);
            this.qq.Name = "qq";
            this.qq.Size = new System.Drawing.Size(101, 20);
            this.qq.TabIndex = 7;
            // 
            // mobile
            // 
            this.mobile.Location = new System.Drawing.Point(80, 96);
            this.mobile.Name = "mobile";
            this.mobile.Size = new System.Drawing.Size(101, 20);
            this.mobile.TabIndex = 6;
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(80, 68);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(101, 20);
            this.name.TabIndex = 5;
            // 
            // type
            // 
            this.type.DropDownWidth = 135;
            this.type.Location = new System.Drawing.Point(80, 41);
            this.type.Name = "type";
            this.type.Size = new System.Drawing.Size(101, 21);
            this.type.TabIndex = 4;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(19, 16);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(51, 20);
            this.kryptonLabel3.TabIndex = 3;
            this.kryptonLabel3.Values.Text = "登入名:";
            // 
            // login
            // 
            this.login.Location = new System.Drawing.Point(80, 12);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(101, 20);
            this.login.TabIndex = 2;
            // 
            // kryptonLabel10
            // 
            this.kryptonLabel10.Location = new System.Drawing.Point(7, 178);
            this.kryptonLabel10.Name = "kryptonLabel10";
            this.kryptonLabel10.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel10.TabIndex = 18;
            this.kryptonLabel10.Values.Text = "可开代理:";
            // 
            // agentlimit
            // 
            this.agentlimit.Location = new System.Drawing.Point(80, 178);
            this.agentlimit.Name = "agentlimit";
            this.agentlimit.Size = new System.Drawing.Size(81, 22);
            this.agentlimit.TabIndex = 19;
            // 
            // fmManagerEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 248);
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
    }
}