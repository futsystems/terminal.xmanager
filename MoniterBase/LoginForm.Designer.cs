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
            this.components = new System.ComponentModel.Container();
            this.imageheader = new System.Windows.Forms.PictureBox();
            this.label0 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.label1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.label2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.username = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.password = new ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox();
            this.servers = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.btnLogin = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.ckremberuser = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.kryptonManager1 = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.ckremberpass = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.btnExit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbLoginStatus = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.imageheader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.servers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageheader
            // 
            this.imageheader.BackColor = System.Drawing.Color.White;
            this.imageheader.Dock = System.Windows.Forms.DockStyle.Top;
            this.imageheader.ErrorImage = null;
            this.imageheader.InitialImage = null;
            this.imageheader.Location = new System.Drawing.Point(0, 0);
            this.imageheader.Name = "imageheader";
            this.imageheader.Size = new System.Drawing.Size(406, 66);
            this.imageheader.TabIndex = 0;
            this.imageheader.TabStop = false;
            this.imageheader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageheader_MouseDown);
            this.imageheader.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageheader_MouseMove);
            this.imageheader.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageheader_MouseUp);
            // 
            // label0
            // 
            this.label0.Location = new System.Drawing.Point(62, 20);
            this.label0.Name = "label0";
            this.label0.Size = new System.Drawing.Size(68, 18);
            this.label0.TabIndex = 20;
            this.label0.Values.Text = "柜台地址:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(62, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 18);
            this.label1.TabIndex = 21;
            this.label1.Values.Text = "柜员代码:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(62, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 18);
            this.label2.TabIndex = 22;
            this.label2.Values.Text = "柜员密码:";
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(137, 49);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(147, 22);
            this.username.TabIndex = 23;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(137, 82);
            this.password.Name = "password";
            this.password.PasswordChar = '#';
            this.password.Size = new System.Drawing.Size(147, 22);
            this.password.TabIndex = 24;
            // 
            // servers
            // 
            this.servers.DropDownWidth = 121;
            this.servers.Location = new System.Drawing.Point(137, 17);
            this.servers.Name = "servers";
            this.servers.Size = new System.Drawing.Size(147, 21);
            this.servers.TabIndex = 25;
            this.servers.Text = "--";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(137, 110);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(60, 31);
            this.btnLogin.TabIndex = 27;
            this.btnLogin.Values.Text = "登 入";
            // 
            // ckremberuser
            // 
            this.ckremberuser.Location = new System.Drawing.Point(289, 53);
            this.ckremberuser.Name = "ckremberuser";
            this.ckremberuser.Size = new System.Drawing.Size(78, 18);
            this.ckremberuser.TabIndex = 28;
            this.ckremberuser.Values.Text = "记住代码";
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.GlobalPalette = this.kryptonPalette1;
            this.kryptonManager1.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.Custom;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.statusStrip1);
            this.kryptonPanel1.Controls.Add(this.ckremberpass);
            this.kryptonPanel1.Controls.Add(this.btnExit);
            this.kryptonPanel1.Controls.Add(this.label1);
            this.kryptonPanel1.Controls.Add(this.label0);
            this.kryptonPanel1.Controls.Add(this.label2);
            this.kryptonPanel1.Controls.Add(this.username);
            this.kryptonPanel1.Controls.Add(this.ckremberuser);
            this.kryptonPanel1.Controls.Add(this.password);
            this.kryptonPanel1.Controls.Add(this.btnLogin);
            this.kryptonPanel1.Controls.Add(this.servers);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 66);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(406, 188);
            this.kryptonPanel1.TabIndex = 33;
            // 
            // ckremberpass
            // 
            this.ckremberpass.Location = new System.Drawing.Point(289, 85);
            this.ckremberpass.Name = "ckremberpass";
            this.ckremberpass.Size = new System.Drawing.Size(78, 18);
            this.ckremberpass.TabIndex = 33;
            this.ckremberpass.Values.Text = "记住密码";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(224, 110);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(60, 31);
            this.btnExit.TabIndex = 32;
            this.btnExit.Values.Text = "退 出";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbLoginStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 166);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip1.Size = new System.Drawing.Size(406, 22);
            this.statusStrip1.TabIndex = 35;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbLoginStatus
            // 
            this.lbLoginStatus.Name = "lbLoginStatus";
            this.lbLoginStatus.Size = new System.Drawing.Size(15, 17);
            this.lbLoginStatus.Text = "--";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 254);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.imageheader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统登入";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.imageheader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.servers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox imageheader;
        //private Telerik.WinControls.Themes.Office2010SilverTheme office2010SilverTheme1;
        //private Telerik.WinControls.Themes.Office2010BlackTheme office2010BlackTheme1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel label0;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel label1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel label2;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox username;
        private ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox password;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox servers;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnLogin;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ckremberuser;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnExit;
        private System.Windows.Forms.BindingSource bindingSource1;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ckremberpass;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbLoginStatus;
        //private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
    }
}