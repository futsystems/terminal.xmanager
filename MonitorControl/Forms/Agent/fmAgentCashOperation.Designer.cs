namespace TradingLib.MoniterControl
{
    partial class fmAgentCashOperation
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
            this.financeInfoGroup = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.ctAgentFinanceInfo1 = new TradingLib.MoniterControl.ctAgentFinanceInfo();
            this.cashopGroup = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.cbCurrency = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
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
            ((System.ComponentModel.ISupportInitialize)(this.financeInfoGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.financeInfoGroup.Panel)).BeginInit();
            this.financeInfoGroup.Panel.SuspendLayout();
            this.financeInfoGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cashopGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cashopGroup.Panel)).BeginInit();
            this.cashopGroup.Panel.SuspendLayout();
            this.cashopGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbEquityTypeList)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.financeInfoGroup);
            this.kryptonPanel1.Controls.Add(this.cashopGroup);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(754, 252);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // financeInfoGroup
            // 
            this.financeInfoGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.financeInfoGroup.Location = new System.Drawing.Point(229, 3);
            this.financeInfoGroup.Name = "financeInfoGroup";
            // 
            // financeInfoGroup.Panel
            // 
            this.financeInfoGroup.Panel.Controls.Add(this.ctAgentFinanceInfo1);
            this.financeInfoGroup.Size = new System.Drawing.Size(522, 246);
            this.financeInfoGroup.TabIndex = 6;
            this.financeInfoGroup.Values.Heading = "帐户查询";
            // 
            // ctAgentFinanceInfo1
            // 
            this.ctAgentFinanceInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctAgentFinanceInfo1.Location = new System.Drawing.Point(0, 0);
            this.ctAgentFinanceInfo1.Name = "ctAgentFinanceInfo1";
            this.ctAgentFinanceInfo1.Size = new System.Drawing.Size(518, 222);
            this.ctAgentFinanceInfo1.TabIndex = 0;
            // 
            // cashopGroup
            // 
            this.cashopGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cashopGroup.Location = new System.Drawing.Point(0, 3);
            this.cashopGroup.Name = "cashopGroup";
            // 
            // cashopGroup.Panel
            // 
            this.cashopGroup.Panel.Controls.Add(this.cbCurrency);
            this.cashopGroup.Panel.Controls.Add(this.kryptonLabel1);
            this.cashopGroup.Panel.Controls.Add(this.btnAccountWithdraw);
            this.cashopGroup.Panel.Controls.Add(this.cbEquityTypeList);
            this.cashopGroup.Panel.Controls.Add(this.kryptonLabel2);
            this.cashopGroup.Panel.Controls.Add(this.btnAccountDeposit);
            this.cashopGroup.Panel.Controls.Add(this.cashop_comment);
            this.cashopGroup.Panel.Controls.Add(this.kryptonLabel8);
            this.cashopGroup.Panel.Controls.Add(this.cashop_amount);
            this.cashopGroup.Panel.Controls.Add(this.kryptonLabel6);
            this.cashopGroup.Size = new System.Drawing.Size(223, 246);
            this.cashopGroup.TabIndex = 5;
            this.cashopGroup.Values.Heading = "出入金";
            // 
            // cbCurrency
            // 
            this.cbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCurrency.DropDownWidth = 181;
            this.cbCurrency.Location = new System.Drawing.Point(77, 54);
            this.cbCurrency.Name = "cbCurrency";
            this.cbCurrency.Size = new System.Drawing.Size(100, 21);
            this.cbCurrency.TabIndex = 23;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(25, 55);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel1.TabIndex = 14;
            this.kryptonLabel1.Values.Text = "货币:";
            // 
            // btnAccountWithdraw
            // 
            this.btnAccountWithdraw.Location = new System.Drawing.Point(126, 155);
            this.btnAccountWithdraw.Name = "btnAccountWithdraw";
            this.btnAccountWithdraw.Size = new System.Drawing.Size(80, 35);
            this.btnAccountWithdraw.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAccountWithdraw.TabIndex = 13;
            this.btnAccountWithdraw.Values.Text = "出 金";
            // 
            // cbEquityTypeList
            // 
            this.cbEquityTypeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.btnAccountDeposit.Location = new System.Drawing.Point(28, 155);
            this.btnAccountDeposit.Name = "btnAccountDeposit";
            this.btnAccountDeposit.Size = new System.Drawing.Size(80, 35);
            this.btnAccountDeposit.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnAccountDeposit.TabIndex = 8;
            this.btnAccountDeposit.Values.Text = "入 金";
            // 
            // cashop_comment
            // 
            this.cashop_comment.Location = new System.Drawing.Point(77, 81);
            this.cashop_comment.Multiline = true;
            this.cashop_comment.Name = "cashop_comment";
            this.cashop_comment.Size = new System.Drawing.Size(129, 38);
            this.cashop_comment.TabIndex = 7;
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(25, 81);
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
            10000000,
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
            // fmAgentCashOperation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 252);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmAgentCashOperation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "出入金与帐户查询【代理】";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.financeInfoGroup.Panel)).EndInit();
            this.financeInfoGroup.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.financeInfoGroup)).EndInit();
            this.financeInfoGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cashopGroup.Panel)).EndInit();
            this.cashopGroup.Panel.ResumeLayout(false);
            this.cashopGroup.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cashopGroup)).EndInit();
            this.cashopGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbEquityTypeList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox financeInfoGroup;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox cashopGroup;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnAccountWithdraw;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbEquityTypeList;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnAccountDeposit;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox cashop_comment;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown cashop_amount;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbCurrency;
        private ctAgentFinanceInfo ctAgentFinanceInfo1;
    }
}