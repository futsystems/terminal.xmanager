namespace TradingLib.TinyMGRControl
{
    partial class fmCashOperation
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
            ((System.ComponentModel.ISupportInitialize)(this.cbEquityTypeList)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btnAccountWithdraw);
            this.kryptonPanel1.Controls.Add(this.cbEquityTypeList);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel6);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.cashop_amount);
            this.kryptonPanel1.Controls.Add(this.btnAccountDeposit);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel8);
            this.kryptonPanel1.Controls.Add(this.cashop_comment);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(242, 171);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // btnAccountWithdraw
            // 
            this.btnAccountWithdraw.Location = new System.Drawing.Point(131, 125);
            this.btnAccountWithdraw.Name = "btnAccountWithdraw";
            this.btnAccountWithdraw.Size = new System.Drawing.Size(80, 35);
            this.btnAccountWithdraw.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAccountWithdraw.TabIndex = 13;
            this.btnAccountWithdraw.Values.Text = "出 金";
            // 
            // cbEquityTypeList
            // 
            this.cbEquityTypeList.DropDownWidth = 121;
            this.cbEquityTypeList.Location = new System.Drawing.Point(82, 33);
            this.cbEquityTypeList.Name = "cbEquityTypeList";
            this.cbEquityTypeList.Size = new System.Drawing.Size(100, 21);
            this.cbEquityTypeList.TabIndex = 10;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(8, 33);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel2.TabIndex = 9;
            this.kryptonLabel2.Values.Text = "资金类型:";
            // 
            // btnAccountDeposit
            // 
            this.btnAccountDeposit.Location = new System.Drawing.Point(33, 125);
            this.btnAccountDeposit.Name = "btnAccountDeposit";
            this.btnAccountDeposit.Size = new System.Drawing.Size(80, 35);
            this.btnAccountDeposit.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnAccountDeposit.TabIndex = 8;
            this.btnAccountDeposit.Values.Text = "入 金";
            // 
            // cashop_comment
            // 
            this.cashop_comment.Location = new System.Drawing.Point(82, 57);
            this.cashop_comment.Multiline = true;
            this.cashop_comment.Name = "cashop_comment";
            this.cashop_comment.Size = new System.Drawing.Size(129, 38);
            this.cashop_comment.TabIndex = 7;
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(30, 57);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel8.TabIndex = 6;
            this.kryptonLabel8.Values.Text = "说明:";
            // 
            // cashop_amount
            // 
            this.cashop_amount.DecimalPlaces = 2;
            this.cashop_amount.Location = new System.Drawing.Point(82, 10);
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
            this.kryptonLabel6.Location = new System.Drawing.Point(35, 12);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel6.TabIndex = 2;
            this.kryptonLabel6.Values.Text = "金额:";
            // 
            // fmCashOperation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 171);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "fmCashOperation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "出入金操作";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbEquityTypeList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
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