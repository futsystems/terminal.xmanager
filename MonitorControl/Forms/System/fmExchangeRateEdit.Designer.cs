namespace TradingLib.MoniterControl
{
    partial class fmExchangeRateEdit
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
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.currency = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.inter_rate = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.ask_rate = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.bid_rate = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.btnSubmit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btnSubmit);
            this.kryptonPanel1.Controls.Add(this.bid_rate);
            this.kryptonPanel1.Controls.Add(this.ask_rate);
            this.kryptonPanel1.Controls.Add(this.inter_rate);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.currency);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(181, 179);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(16, 12);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel1.TabIndex = 1;
            this.kryptonLabel1.Values.Text = "货币:";
            // 
            // currency
            // 
            this.currency.Location = new System.Drawing.Point(56, 12);
            this.currency.Name = "currency";
            this.currency.Size = new System.Drawing.Size(20, 20);
            this.currency.TabIndex = 2;
            this.currency.Values.Text = "--";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(3, 38);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(51, 20);
            this.kryptonLabel2.TabIndex = 3;
            this.kryptonLabel2.Values.Text = "中间价:";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(16, 64);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel3.TabIndex = 4;
            this.kryptonLabel3.Values.Text = "卖价:";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(16, 90);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel4.TabIndex = 5;
            this.kryptonLabel4.Values.Text = "买价:";
            // 
            // inter_rate
            // 
            this.inter_rate.DecimalPlaces = 6;
            this.inter_rate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.inter_rate.Location = new System.Drawing.Point(60, 36);
            this.inter_rate.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.inter_rate.Name = "inter_rate";
            this.inter_rate.Size = new System.Drawing.Size(105, 22);
            this.inter_rate.TabIndex = 6;
            // 
            // ask_rate
            // 
            this.ask_rate.DecimalPlaces = 6;
            this.ask_rate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.ask_rate.Location = new System.Drawing.Point(60, 64);
            this.ask_rate.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ask_rate.Name = "ask_rate";
            this.ask_rate.Size = new System.Drawing.Size(105, 22);
            this.ask_rate.TabIndex = 7;
            // 
            // bid_rate
            // 
            this.bid_rate.DecimalPlaces = 6;
            this.bid_rate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.bid_rate.Location = new System.Drawing.Point(60, 90);
            this.bid_rate.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.bid_rate.Name = "bid_rate";
            this.bid_rate.Size = new System.Drawing.Size(105, 22);
            this.bid_rate.TabIndex = 8;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(92, 143);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(73, 25);
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Values.Text = "更 新";
            // 
            // fmExchangeRateEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(181, 179);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "fmExchangeRateEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "汇率编辑";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel currency;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown inter_rate;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown bid_rate;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown ask_rate;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSubmit;
    }
}