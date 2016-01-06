namespace TradingLib.MoniterControl
{
    partial class fmAccountRule_MaxChange
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
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.seclist = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lbDesp = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnSubmit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.up_pect = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.dn_pect = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonLabel7);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel6);
            this.kryptonPanel1.Controls.Add(this.dn_pect);
            this.kryptonPanel1.Controls.Add(this.up_pect);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel5);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.seclist);
            this.kryptonPanel1.Controls.Add(this.lbDesp);
            this.kryptonPanel1.Controls.Add(this.btnSubmit);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(271, 268);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(138, 169);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(123, 20);
            this.kryptonLabel5.TabIndex = 20;
            this.kryptonLabel5.Values.Text = "不填为检查所有持仓";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(33, 143);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(228, 20);
            this.kryptonLabel4.TabIndex = 19;
            this.kryptonLabel4.Values.Text = "品种列表,用逗号分隔,注意大小写与逗号";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(25, 68);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(60, 20);
            this.kryptonLabel2.TabIndex = 18;
            this.kryptonLabel2.Values.Text = "品种列表";
            // 
            // seclist
            // 
            this.seclist.Location = new System.Drawing.Point(93, 68);
            this.seclist.Multiline = true;
            this.seclist.Name = "seclist";
            this.seclist.Size = new System.Drawing.Size(166, 69);
            this.seclist.TabIndex = 17;
            // 
            // lbDesp
            // 
            this.lbDesp.Location = new System.Drawing.Point(12, 204);
            this.lbDesp.Name = "lbDesp";
            this.lbDesp.Size = new System.Drawing.Size(20, 20);
            this.lbDesp.TabIndex = 9;
            this.lbDesp.Values.Text = "--";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(192, 231);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(67, 25);
            this.btnSubmit.TabIndex = 8;
            this.btnSubmit.Values.Text = "提 交";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(192, 14);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(21, 20);
            this.kryptonLabel3.TabIndex = 3;
            this.kryptonLabel3.Values.Text = "%";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(50, 10);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(35, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "涨幅";
            // 
            // up_pect
            // 
            this.up_pect.DecimalPlaces = 2;
            this.up_pect.Location = new System.Drawing.Point(91, 12);
            this.up_pect.Name = "up_pect";
            this.up_pect.Size = new System.Drawing.Size(99, 22);
            this.up_pect.TabIndex = 21;
            // 
            // dn_pect
            // 
            this.dn_pect.DecimalPlaces = 2;
            this.dn_pect.Location = new System.Drawing.Point(91, 40);
            this.dn_pect.Name = "dn_pect";
            this.dn_pect.Size = new System.Drawing.Size(99, 22);
            this.dn_pect.TabIndex = 22;
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(50, 42);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(35, 20);
            this.kryptonLabel6.TabIndex = 23;
            this.kryptonLabel6.Values.Text = "跌幅";
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(192, 40);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(21, 20);
            this.kryptonLabel7.TabIndex = 24;
            this.kryptonLabel7.Values.Text = "%";
            // 
            // fmAccountRule_MaxChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 268);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmAccountRule_MaxChange";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "强平-帐户盈利";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSubmit;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lbDesp;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox seclist;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown dn_pect;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown up_pect;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
    }
}