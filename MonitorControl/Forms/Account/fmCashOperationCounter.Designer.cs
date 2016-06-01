namespace TradingLib.MoniterControl
{
    partial class fmCashOperationCounter
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
            this.ctFinanceInfo1 = new TradingLib.MoniterControl.ctFinanceInfo();
            this.kryptonGroupBox2 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.btnAccountWithdraw = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.cbEquityTypeList = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnAccountDeposit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.cashop_comment = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cashop_amount = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).BeginInit();
            this.kryptonGroupBox2.Panel.SuspendLayout();
            this.kryptonGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbEquityTypeList)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox1);
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox2);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(751, 196);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Location = new System.Drawing.Point(229, 3);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.ctFinanceInfo1);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(522, 190);
            this.kryptonGroupBox1.TabIndex = 6;
            this.kryptonGroupBox1.Values.Heading = "帐户查询";
            // 
            // ctFinanceInfo1
            // 
            this.ctFinanceInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctFinanceInfo1.Location = new System.Drawing.Point(0, 0);
            this.ctFinanceInfo1.Name = "ctFinanceInfo1";
            this.ctFinanceInfo1.Size = new System.Drawing.Size(518, 166);
            this.ctFinanceInfo1.TabIndex = 0;
            // 
            // kryptonGroupBox2
            // 
            this.kryptonGroupBox2.Location = new System.Drawing.Point(0, 3);
            this.kryptonGroupBox2.Name = "kryptonGroupBox2";
            // 
            // kryptonGroupBox2.Panel
            // 
            this.kryptonGroupBox2.Panel.Controls.Add(this.btnAccountWithdraw);
            this.kryptonGroupBox2.Panel.Controls.Add(this.cbEquityTypeList);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kryptonLabel2);
            this.kryptonGroupBox2.Panel.Controls.Add(this.btnAccountDeposit);
            this.kryptonGroupBox2.Panel.Controls.Add(this.cashop_comment);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kryptonLabel8);
            this.kryptonGroupBox2.Panel.Controls.Add(this.cashop_amount);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kryptonLabel6);
            this.kryptonGroupBox2.Size = new System.Drawing.Size(223, 190);
            this.kryptonGroupBox2.TabIndex = 5;
            this.kryptonGroupBox2.Values.Heading = "出入金";
            // 
            // btnAccountWithdraw
            // 
            this.btnAccountWithdraw.Location = new System.Drawing.Point(126, 121);
            this.btnAccountWithdraw.Name = "btnAccountWithdraw";
            this.btnAccountWithdraw.Size = new System.Drawing.Size(80, 35);
            this.btnAccountWithdraw.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAccountWithdraw.TabIndex = 13;
            this.btnAccountWithdraw.Values.Text = "出 金";
            // 
            // cbEquityTypeList
            // 
            this.cbEquityTypeList.DropDownWidth = 121;
            this.cbEquityTypeList.Location = new System.Drawing.Point(77, 29);
            this.cbEquityTypeList.Name = "cbEquityTypeList";
            this.cbEquityTypeList.Size = new System.Drawing.Size(100, 21);
            this.cbEquityTypeList.TabIndex = 10;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(3, 29);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel2.TabIndex = 9;
            this.kryptonLabel2.Values.Text = "资金类型:";
            // 
            // btnAccountDeposit
            // 
            this.btnAccountDeposit.Location = new System.Drawing.Point(28, 121);
            this.btnAccountDeposit.Name = "btnAccountDeposit";
            this.btnAccountDeposit.Size = new System.Drawing.Size(80, 35);
            this.btnAccountDeposit.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnAccountDeposit.TabIndex = 8;
            this.btnAccountDeposit.Values.Text = "入 金";
            // 
            // cashop_comment
            // 
            this.cashop_comment.Location = new System.Drawing.Point(77, 53);
            this.cashop_comment.Multiline = true;
            this.cashop_comment.Name = "cashop_comment";
            this.cashop_comment.Size = new System.Drawing.Size(129, 38);
            this.cashop_comment.TabIndex = 7;
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(25, 53);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel8.TabIndex = 6;
            this.kryptonLabel8.Values.Text = "说明:";
            // 
            // cashop_amount
            // 
            this.cashop_amount.DecimalPlaces = 2;
            this.cashop_amount.Location = new System.Drawing.Point(77, 6);
            this.cashop_amount.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.cashop_amount.Name = "cashop_amount";
            this.cashop_amount.Size = new System.Drawing.Size(100, 22);
            this.cashop_amount.TabIndex = 3;
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(30, 8);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel6.TabIndex = 2;
            this.kryptonLabel6.Values.Text = "金额:";
            // 
            // fmCashOperationCounter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 196);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmCashOperationCounter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "出入金与帐户查询";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).EndInit();
            this.kryptonGroupBox2.Panel.ResumeLayout(false);
            this.kryptonGroupBox2.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).EndInit();
            this.kryptonGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbEquityTypeList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private ctFinanceInfo ctFinanceInfo1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnAccountWithdraw;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbEquityTypeList;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnAccountDeposit;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox cashop_comment;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown cashop_amount;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
    }
}