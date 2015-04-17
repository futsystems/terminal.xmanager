namespace TradingLib.MoniterControl
{
    partial class fmSyncEquity
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
            this.btnSync = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.targetEquit = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.targetCredit = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lbCredit = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lbEquity = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lbMainAcctStaticEquity = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btnSync);
            this.kryptonPanel1.Controls.Add(this.targetEquit);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel7);
            this.kryptonPanel1.Controls.Add(this.targetCredit);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel6);
            this.kryptonPanel1.Controls.Add(this.lbCredit);
            this.kryptonPanel1.Controls.Add(this.lbEquity);
            this.kryptonPanel1.Controls.Add(this.lbMainAcctStaticEquity);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(331, 200);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // btnSync
            // 
            this.btnSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSync.Location = new System.Drawing.Point(249, 163);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(70, 25);
            this.btnSync.TabIndex = 10;
            this.btnSync.Values.Text = "同 步";
            // 
            // targetEquit
            // 
            this.targetEquit.Location = new System.Drawing.Point(127, 118);
            this.targetEquit.Name = "targetEquit";
            this.targetEquit.Size = new System.Drawing.Size(19, 18);
            this.targetEquit.TabIndex = 9;
            this.targetEquit.Values.Text = "--";
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(12, 118);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(109, 18);
            this.kryptonLabel7.TabIndex = 8;
            this.kryptonLabel7.Values.Text = "同步后客户资金:";
            // 
            // targetCredit
            // 
            this.targetCredit.DecimalPlaces = 2;
            this.targetCredit.Location = new System.Drawing.Point(127, 92);
            this.targetCredit.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.targetCredit.Name = "targetCredit";
            this.targetCredit.Size = new System.Drawing.Size(120, 20);
            this.targetCredit.TabIndex = 7;
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(12, 94);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(109, 18);
            this.kryptonLabel6.TabIndex = 6;
            this.kryptonLabel6.Values.Text = "同步后优先资金:";
            // 
            // lbCredit
            // 
            this.lbCredit.Location = new System.Drawing.Point(127, 60);
            this.lbCredit.Name = "lbCredit";
            this.lbCredit.Size = new System.Drawing.Size(19, 18);
            this.lbCredit.TabIndex = 5;
            this.lbCredit.Values.Text = "--";
            // 
            // lbEquity
            // 
            this.lbEquity.Location = new System.Drawing.Point(127, 36);
            this.lbEquity.Name = "lbEquity";
            this.lbEquity.Size = new System.Drawing.Size(19, 18);
            this.lbEquity.TabIndex = 4;
            this.lbEquity.Values.Text = "--";
            // 
            // lbMainAcctStaticEquity
            // 
            this.lbMainAcctStaticEquity.Location = new System.Drawing.Point(127, 12);
            this.lbMainAcctStaticEquity.Name = "lbMainAcctStaticEquity";
            this.lbMainAcctStaticEquity.Size = new System.Drawing.Size(19, 18);
            this.lbMainAcctStaticEquity.TabIndex = 3;
            this.lbMainAcctStaticEquity.Values.Text = "--";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(26, 60);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(95, 18);
            this.kryptonLabel3.TabIndex = 2;
            this.kryptonLabel3.Values.Text = "当前优先资金:";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(26, 36);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(95, 18);
            this.kryptonLabel2.TabIndex = 1;
            this.kryptonLabel2.Values.Text = "当前客户资金:";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(12, 12);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(109, 18);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "主帐户静态权益:";
            // 
            // fmSyncEquity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 200);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmSyncEquity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "同步帐户资金";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lbMainAcctStaticEquity;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lbCredit;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lbEquity;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown targetCredit;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel targetEquit;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSync;
    }
}