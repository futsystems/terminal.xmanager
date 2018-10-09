namespace TradingLib.MoniterBase
{
    partial class LoginForm
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
            this.imageheader = new System.Windows.Forms.PictureBox();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lbLoginStatus = new DevExpress.XtraEditors.LabelControl();
            this.linkExit = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.ckremberpass = new DevExpress.XtraEditors.CheckEdit();
            this.ckremberuser = new DevExpress.XtraEditors.CheckEdit();
            this.label0 = new DevExpress.XtraEditors.LabelControl();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.password = new DevExpress.XtraEditors.TextEdit();
            this.username = new DevExpress.XtraEditors.TextEdit();
            this.servers = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.imageheader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckremberpass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckremberuser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.password.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.username.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.servers.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // imageheader
            // 
            this.imageheader.BackColor = System.Drawing.Color.White;
            this.imageheader.Dock = System.Windows.Forms.DockStyle.Top;
            this.imageheader.ErrorImage = null;
            this.imageheader.Image = global::TradingLib.MoniterBase.Properties.Resources.login;
            this.imageheader.InitialImage = null;
            this.imageheader.Location = new System.Drawing.Point(0, 0);
            this.imageheader.Name = "imageheader";
            this.imageheader.Size = new System.Drawing.Size(420, 113);
            this.imageheader.TabIndex = 0;
            this.imageheader.TabStop = false;
            this.imageheader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageheader_MouseDown);
            this.imageheader.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageheader_MouseMove);
            this.imageheader.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageheader_MouseUp);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lbLoginStatus);
            this.panelControl1.Controls.Add(this.linkExit);
            this.panelControl1.Controls.Add(this.ckremberpass);
            this.panelControl1.Controls.Add(this.ckremberuser);
            this.panelControl1.Controls.Add(this.label0);
            this.panelControl1.Controls.Add(this.btnLogin);
            this.panelControl1.Controls.Add(this.password);
            this.panelControl1.Controls.Add(this.username);
            this.panelControl1.Controls.Add(this.servers);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 113);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(420, 176);
            this.panelControl1.TabIndex = 34;
            // 
            // lbLoginStatus
            // 
            this.lbLoginStatus.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lbLoginStatus.Appearance.Options.UseForeColor = true;
            this.lbLoginStatus.Location = new System.Drawing.Point(22, 150);
            this.lbLoginStatus.Name = "lbLoginStatus";
            this.lbLoginStatus.Size = new System.Drawing.Size(8, 14);
            this.lbLoginStatus.TabIndex = 37;
            this.lbLoginStatus.Text = "--";
            // 
            // linkExit
            // 
            this.linkExit.Location = new System.Drawing.Point(355, 108);
            this.linkExit.Name = "linkExit";
            this.linkExit.Size = new System.Drawing.Size(28, 14);
            this.linkExit.TabIndex = 36;
            this.linkExit.Text = "退 出";
            this.linkExit.Visible = false;
            // 
            // ckremberpass
            // 
            this.ckremberpass.Location = new System.Drawing.Point(216, 108);
            this.ckremberpass.Name = "ckremberpass";
            this.ckremberpass.Properties.Caption = "记住密码";
            this.ckremberpass.Size = new System.Drawing.Size(75, 19);
            this.ckremberpass.TabIndex = 9;
            // 
            // ckremberuser
            // 
            this.ckremberuser.Location = new System.Drawing.Point(121, 108);
            this.ckremberuser.Name = "ckremberuser";
            this.ckremberuser.Properties.Caption = "记住用户名";
            this.ckremberuser.Size = new System.Drawing.Size(89, 19);
            this.ckremberuser.TabIndex = 8;
            // 
            // label0
            // 
            this.label0.Location = new System.Drawing.Point(56, 12);
            this.label0.Name = "label0";
            this.label0.Size = new System.Drawing.Size(24, 14);
            this.label0.TabIndex = 7;
            this.label0.Text = "地址";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(297, 41);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(86, 53);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "登  入";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(125, 74);
            this.password.Name = "password";
            this.password.Properties.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(136, 20);
            this.password.TabIndex = 4;
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(125, 38);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(136, 20);
            this.username.TabIndex = 3;
            // 
            // servers
            // 
            this.servers.Location = new System.Drawing.Point(125, 6);
            this.servers.Name = "servers";
            this.servers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.servers.Size = new System.Drawing.Size(136, 20);
            this.servers.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(56, 80);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "密码";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(44, 44);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "用户名";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 289);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.imageheader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "巨融资管柜台系统";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.imageheader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckremberpass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckremberuser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.password.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.username.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.servers.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox imageheader;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit password;
        private DevExpress.XtraEditors.TextEdit username;
        private DevExpress.XtraEditors.ComboBoxEdit servers;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.XtraEditors.CheckEdit ckremberpass;
        private DevExpress.XtraEditors.CheckEdit ckremberuser;
        private DevExpress.XtraEditors.LabelControl label0;
        private DevExpress.XtraEditors.HyperlinkLabelControl linkExit;
        private DevExpress.XtraEditors.LabelControl lbLoginStatus;
    }
}