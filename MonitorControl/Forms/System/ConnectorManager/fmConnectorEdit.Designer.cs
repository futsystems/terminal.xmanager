namespace TradingLib.MoniterControl
{
    partial class fmConnectorEdit
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
            this.btnSubmit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBox2 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.tokenvalid = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel13 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.name = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.cbinterfacelist = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.port = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.address = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.token = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel12 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonGroupBox1 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.pass = new ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox();
            this.uf2 = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.uf1 = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.username = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel11 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel10 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel9 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).BeginInit();
            this.kryptonGroupBox2.Panel.SuspendLayout();
            this.kryptonGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbinterfacelist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.tokenvalid);
            this.kryptonPanel1.Controls.Add(this.btnSubmit);
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox2);
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox1);
            this.kryptonPanel1.Controls.Add(this.token);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(330, 389);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(237, 352);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(70, 25);
            this.btnSubmit.TabIndex = 9;
            this.btnSubmit.Values.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // kryptonGroupBox2
            // 
            this.kryptonGroupBox2.Location = new System.Drawing.Point(20, 37);
            this.kryptonGroupBox2.Name = "kryptonGroupBox2";
            // 
            // kryptonGroupBox2.Panel
            // 
            this.kryptonGroupBox2.Panel.Controls.Add(this.uf1);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kryptonLabel10);
            this.kryptonGroupBox2.Panel.Controls.Add(this.cbinterfacelist);
            this.kryptonGroupBox2.Panel.Controls.Add(this.port);
            this.kryptonGroupBox2.Panel.Controls.Add(this.address);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kryptonLabel12);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kryptonLabel3);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kryptonLabel4);
            this.kryptonGroupBox2.Size = new System.Drawing.Size(287, 139);
            this.kryptonGroupBox2.TabIndex = 8;
            this.kryptonGroupBox2.Values.Heading = "交易服务器信息";
            // 
            // tokenvalid
            // 
            this.tokenvalid.Location = new System.Drawing.Point(299, 7);
            this.tokenvalid.Name = "tokenvalid";
            this.tokenvalid.Size = new System.Drawing.Size(19, 18);
            this.tokenvalid.TabIndex = 22;
            this.tokenvalid.Values.Text = "--";
            // 
            // kryptonLabel13
            // 
            this.kryptonLabel13.Location = new System.Drawing.Point(32, 6);
            this.kryptonLabel13.Name = "kryptonLabel13";
            this.kryptonLabel13.Size = new System.Drawing.Size(82, 18);
            this.kryptonLabel13.TabIndex = 21;
            this.kryptonLabel13.Values.Text = "投资者姓名:";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(121, 3);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(118, 21);
            this.name.TabIndex = 20;
            // 
            // cbinterfacelist
            // 
            this.cbinterfacelist.DropDownWidth = 151;
            this.cbinterfacelist.Location = new System.Drawing.Point(118, 3);
            this.cbinterfacelist.Name = "cbinterfacelist";
            this.cbinterfacelist.Size = new System.Drawing.Size(151, 21);
            this.cbinterfacelist.TabIndex = 18;
            this.cbinterfacelist.Text = "kryptonComboBox1";
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(119, 53);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(151, 21);
            this.port.TabIndex = 14;
            // 
            // address
            // 
            this.address.Location = new System.Drawing.Point(119, 28);
            this.address.Name = "address";
            this.address.Size = new System.Drawing.Size(151, 21);
            this.address.TabIndex = 13;
            // 
            // token
            // 
            this.token.Location = new System.Drawing.Point(140, 7);
            this.token.Name = "token";
            this.token.Size = new System.Drawing.Size(151, 21);
            this.token.TabIndex = 12;
            // 
            // kryptonLabel12
            // 
            this.kryptonLabel12.Location = new System.Drawing.Point(45, 6);
            this.kryptonLabel12.Name = "kryptonLabel12";
            this.kryptonLabel12.Size = new System.Drawing.Size(68, 18);
            this.kryptonLabel12.TabIndex = 7;
            this.kryptonLabel12.Values.Text = "交易协议:";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(3, 31);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(109, 18);
            this.kryptonLabel3.TabIndex = 2;
            this.kryptonLabel3.Values.Text = "交易服务器地址:";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(52, 10);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(82, 18);
            this.kryptonLabel2.TabIndex = 1;
            this.kryptonLabel2.Values.Text = "主帐户编号:";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(71, 56);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(41, 18);
            this.kryptonLabel4.TabIndex = 3;
            this.kryptonLabel4.Values.Text = "端口:";
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Location = new System.Drawing.Point(20, 182);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel5);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel13);
            this.kryptonGroupBox1.Panel.Controls.Add(this.pass);
            this.kryptonGroupBox1.Panel.Controls.Add(this.uf2);
            this.kryptonGroupBox1.Panel.Controls.Add(this.name);
            this.kryptonGroupBox1.Panel.Controls.Add(this.username);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel11);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel9);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel8);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(287, 157);
            this.kryptonGroupBox1.TabIndex = 7;
            this.kryptonGroupBox1.Values.Heading = "登入信息";
            // 
            // pass
            // 
            this.pass.Location = new System.Drawing.Point(121, 53);
            this.pass.Name = "pass";
            this.pass.PasswordChar = '*';
            this.pass.Size = new System.Drawing.Size(118, 21);
            this.pass.TabIndex = 15;
            // 
            // uf2
            // 
            this.uf2.Location = new System.Drawing.Point(121, 78);
            this.uf2.Name = "uf2";
            this.uf2.Size = new System.Drawing.Size(118, 21);
            this.uf2.TabIndex = 14;
            // 
            // uf1
            // 
            this.uf1.Location = new System.Drawing.Point(119, 80);
            this.uf1.Name = "uf1";
            this.uf1.Size = new System.Drawing.Size(151, 21);
            this.uf1.TabIndex = 13;
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(121, 28);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(118, 21);
            this.username.TabIndex = 11;
            // 
            // kryptonLabel11
            // 
            this.kryptonLabel11.Location = new System.Drawing.Point(44, 81);
            this.kryptonLabel11.Name = "kryptonLabel11";
            this.kryptonLabel11.Size = new System.Drawing.Size(68, 18);
            this.kryptonLabel11.TabIndex = 10;
            this.kryptonLabel11.Values.Text = "资金密码:";
            // 
            // kryptonLabel10
            // 
            this.kryptonLabel10.Location = new System.Drawing.Point(17, 83);
            this.kryptonLabel10.Name = "kryptonLabel10";
            this.kryptonLabel10.Size = new System.Drawing.Size(95, 18);
            this.kryptonLabel10.TabIndex = 9;
            this.kryptonLabel10.Values.Text = "期货公司代码:";
            // 
            // kryptonLabel9
            // 
            this.kryptonLabel9.Location = new System.Drawing.Point(44, 56);
            this.kryptonLabel9.Name = "kryptonLabel9";
            this.kryptonLabel9.Size = new System.Drawing.Size(68, 18);
            this.kryptonLabel9.TabIndex = 8;
            this.kryptonLabel9.Values.Text = "交易密码:";
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(44, 31);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(68, 18);
            this.kryptonLabel8.TabIndex = 7;
            this.kryptonLabel8.Values.Text = "交易帐号:";
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(121, 105);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(141, 18);
            this.kryptonLabel5.TabIndex = 16;
            this.kryptonLabel5.Values.Text = "*可选,用于自动出入金";
            // 
            // fmConnectorEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 389);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmConnectorEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "主帐户设置";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.kryptonGroupBox2.Panel.ResumeLayout(false);
            this.kryptonGroupBox2.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).EndInit();
            this.kryptonGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbinterfacelist)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel11;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel10;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel9;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel12;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbinterfacelist;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox port;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox address;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox token;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox uf2;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox uf1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox username;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSubmit;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel13;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox name;
        private ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox pass;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel tokenvalid;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
    }
}