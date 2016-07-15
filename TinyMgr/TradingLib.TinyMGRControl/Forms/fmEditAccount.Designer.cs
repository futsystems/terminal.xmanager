namespace TradingLib.TinyMGRControl
{
    partial class fmEditAccount
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
            this.kryptonGroupBox1 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.btnChangePassword = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.newpass = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lbCurrentPass = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonGroupBox2 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.name = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.idcard = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.mobile = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.btnUpdateProfile = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).BeginInit();
            this.kryptonGroupBox2.Panel.SuspendLayout();
            this.kryptonGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox2);
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(286, 422);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.btnChangePassword);
            this.kryptonGroupBox1.Panel.Controls.Add(this.newpass);
            this.kryptonGroupBox1.Panel.Controls.Add(this.lbCurrentPass);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel2);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(283, 135);
            this.kryptonGroupBox1.TabIndex = 0;
            this.kryptonGroupBox1.Values.Heading = "交易密码";
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Location = new System.Drawing.Point(138, 74);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(90, 25);
            this.btnChangePassword.TabIndex = 4;
            this.btnChangePassword.Values.Text = "修改密码";
            // 
            // newpass
            // 
            this.newpass.Location = new System.Drawing.Point(76, 39);
            this.newpass.Name = "newpass";
            this.newpass.Size = new System.Drawing.Size(106, 20);
            this.newpass.TabIndex = 3;
            // 
            // lbCurrentPass
            // 
            this.lbCurrentPass.Location = new System.Drawing.Point(76, 13);
            this.lbCurrentPass.Name = "lbCurrentPass";
            this.lbCurrentPass.Size = new System.Drawing.Size(20, 20);
            this.lbCurrentPass.TabIndex = 2;
            this.lbCurrentPass.Values.Text = "--";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(7, 39);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel2.TabIndex = 1;
            this.kryptonLabel2.Values.Text = "修改密码:";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(7, 13);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "当前密码:";
            // 
            // kryptonGroupBox2
            // 
            this.kryptonGroupBox2.Location = new System.Drawing.Point(3, 144);
            this.kryptonGroupBox2.Name = "kryptonGroupBox2";
            // 
            // kryptonGroupBox2.Panel
            // 
            this.kryptonGroupBox2.Panel.Controls.Add(this.btnUpdateProfile);
            this.kryptonGroupBox2.Panel.Controls.Add(this.mobile);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kryptonLabel5);
            this.kryptonGroupBox2.Panel.Controls.Add(this.idcard);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kryptonLabel4);
            this.kryptonGroupBox2.Panel.Controls.Add(this.name);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kryptonLabel3);
            this.kryptonGroupBox2.Size = new System.Drawing.Size(283, 166);
            this.kryptonGroupBox2.TabIndex = 1;
            this.kryptonGroupBox2.Values.Heading = "投资者信息";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(32, 12);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel3.TabIndex = 5;
            this.kryptonLabel3.Values.Text = "姓名:";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(76, 12);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(106, 20);
            this.name.TabIndex = 6;
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(7, 38);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel4.TabIndex = 7;
            this.kryptonLabel4.Values.Text = "证件号码:";
            // 
            // idcard
            // 
            this.idcard.Location = new System.Drawing.Point(76, 38);
            this.idcard.Name = "idcard";
            this.idcard.Size = new System.Drawing.Size(152, 20);
            this.idcard.TabIndex = 8;
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(7, 64);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel5.TabIndex = 9;
            this.kryptonLabel5.Values.Text = "手机号码:";
            // 
            // mobile
            // 
            this.mobile.Location = new System.Drawing.Point(76, 64);
            this.mobile.Name = "mobile";
            this.mobile.Size = new System.Drawing.Size(152, 20);
            this.mobile.TabIndex = 10;
            // 
            // btnUpdateProfile
            // 
            this.btnUpdateProfile.Location = new System.Drawing.Point(117, 101);
            this.btnUpdateProfile.Name = "btnUpdateProfile";
            this.btnUpdateProfile.Size = new System.Drawing.Size(111, 25);
            this.btnUpdateProfile.TabIndex = 5;
            this.btnUpdateProfile.Values.Text = "修改投资者信息";
            // 
            // fmEditAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 422);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "fmEditAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改账户";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            this.kryptonGroupBox2.Panel.ResumeLayout(false);
            this.kryptonGroupBox2.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).EndInit();
            this.kryptonGroupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lbCurrentPass;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox newpass;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnChangePassword;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox name;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox idcard;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox mobile;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnUpdateProfile;
    }
}