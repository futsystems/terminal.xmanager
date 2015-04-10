namespace TradingLib.MoniterControl
{
    partial class fmEditVendorFlatRule
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
            this.lbNight = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lbWarn = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lbFlat = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.equity = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.btnSubmit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.nighthold = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.warnlevel = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.flatlevel = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnDel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btnDel);
            this.kryptonPanel1.Controls.Add(this.lbNight);
            this.kryptonPanel1.Controls.Add(this.lbWarn);
            this.kryptonPanel1.Controls.Add(this.lbFlat);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel7);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel6);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel5);
            this.kryptonPanel1.Controls.Add(this.equity);
            this.kryptonPanel1.Controls.Add(this.btnSubmit);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel1.Controls.Add(this.nighthold);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel1.Controls.Add(this.warnlevel);
            this.kryptonPanel1.Controls.Add(this.flatlevel);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(276, 167);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // lbNight
            // 
            this.lbNight.Location = new System.Drawing.Point(188, 87);
            this.lbNight.Name = "lbNight";
            this.lbNight.Size = new System.Drawing.Size(19, 18);
            this.lbNight.TabIndex = 14;
            this.lbNight.Values.Text = "--";
            // 
            // lbWarn
            // 
            this.lbWarn.Location = new System.Drawing.Point(188, 60);
            this.lbWarn.Name = "lbWarn";
            this.lbWarn.Size = new System.Drawing.Size(19, 18);
            this.lbWarn.TabIndex = 13;
            this.lbWarn.Values.Text = "--";
            // 
            // lbFlat
            // 
            this.lbFlat.Location = new System.Drawing.Point(188, 37);
            this.lbFlat.Name = "lbFlat";
            this.lbFlat.Size = new System.Drawing.Size(19, 18);
            this.lbFlat.TabIndex = 12;
            this.lbFlat.Values.Text = "--";
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(161, 87);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(24, 18);
            this.kryptonLabel7.TabIndex = 11;
            this.kryptonLabel7.Values.Text = "倍";
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(161, 60);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(21, 18);
            this.kryptonLabel6.TabIndex = 10;
            this.kryptonLabel6.Values.Text = "%";
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(161, 37);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(21, 18);
            this.kryptonLabel5.TabIndex = 9;
            this.kryptonLabel5.Values.Text = "%";
            // 
            // equity
            // 
            this.equity.Location = new System.Drawing.Point(104, 10);
            this.equity.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.equity.Name = "equity";
            this.equity.Size = new System.Drawing.Size(84, 20);
            this.equity.TabIndex = 8;
            this.equity.ValueChanged += new System.EventHandler(this.equity_ValueChanged);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(194, 130);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(70, 25);
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Values.Text = "提 交";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(26, 12);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(68, 18);
            this.kryptonLabel4.TabIndex = 6;
            this.kryptonLabel4.Values.Text = "初始权益:";
            // 
            // nighthold
            // 
            this.nighthold.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nighthold.Location = new System.Drawing.Point(104, 85);
            this.nighthold.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nighthold.Name = "nighthold";
            this.nighthold.Size = new System.Drawing.Size(51, 20);
            this.nighthold.TabIndex = 5;
            this.nighthold.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nighthold.ValueChanged += new System.EventHandler(this.nighthold_ValueChanged);
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(26, 87);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(68, 18);
            this.kryptonLabel3.TabIndex = 4;
            this.kryptonLabel3.Values.Text = "过夜比例:";
            // 
            // warnlevel
            // 
            this.warnlevel.Location = new System.Drawing.Point(104, 60);
            this.warnlevel.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.warnlevel.Name = "warnlevel";
            this.warnlevel.Size = new System.Drawing.Size(51, 20);
            this.warnlevel.TabIndex = 3;
            this.warnlevel.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.warnlevel.ValueChanged += new System.EventHandler(this.warnlevel_ValueChanged);
            // 
            // flatlevel
            // 
            this.flatlevel.Location = new System.Drawing.Point(104, 35);
            this.flatlevel.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.flatlevel.Name = "flatlevel";
            this.flatlevel.Size = new System.Drawing.Size(51, 20);
            this.flatlevel.TabIndex = 2;
            this.flatlevel.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.flatlevel.ValueChanged += new System.EventHandler(this.flatlevel_ValueChanged);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(39, 62);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(55, 18);
            this.kryptonLabel2.TabIndex = 1;
            this.kryptonLabel2.Values.Text = "警戒线:";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(39, 37);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(55, 18);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "强平线:";
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDel.Location = new System.Drawing.Point(104, 130);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(70, 25);
            this.btnDel.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDel.TabIndex = 15;
            this.btnDel.Values.Text = "删 除";
            // 
            // fmEditVendorFlatRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 167);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmEditVendorFlatRule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "强平设置";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown warnlevel;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown flatlevel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown nighthold;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSubmit;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown equity;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lbNight;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lbWarn;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lbFlat;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDel;
    }
}