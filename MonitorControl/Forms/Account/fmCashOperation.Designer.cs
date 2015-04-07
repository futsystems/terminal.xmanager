namespace TradingLib.MoniterControl
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
            this.kryptonGroupBox2 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.cbEquityTypeList = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnCashOperation = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.cashop_comment = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cashop_ref = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cashop_amount = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cashop_type = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cashop_sync = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.panel_syncmainacct = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).BeginInit();
            this.kryptonGroupBox2.Panel.SuspendLayout();
            this.kryptonGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbEquityTypeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cashop_type)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel_syncmainacct)).BeginInit();
            this.panel_syncmainacct.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox2);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(671, 324);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonGroupBox2
            // 
            this.kryptonGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonGroupBox2.Location = new System.Drawing.Point(3, 3);
            this.kryptonGroupBox2.Name = "kryptonGroupBox2";
            // 
            // kryptonGroupBox2.Panel
            // 
            this.kryptonGroupBox2.Panel.Controls.Add(this.panel_syncmainacct);
            this.kryptonGroupBox2.Panel.Controls.Add(this.cbEquityTypeList);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kryptonLabel2);
            this.kryptonGroupBox2.Panel.Controls.Add(this.btnCashOperation);
            this.kryptonGroupBox2.Panel.Controls.Add(this.cashop_comment);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kryptonLabel8);
            this.kryptonGroupBox2.Panel.Controls.Add(this.cashop_ref);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kryptonLabel7);
            this.kryptonGroupBox2.Panel.Controls.Add(this.cashop_amount);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kryptonLabel6);
            this.kryptonGroupBox2.Panel.Controls.Add(this.cashop_type);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kryptonLabel5);
            this.kryptonGroupBox2.Size = new System.Drawing.Size(303, 321);
            this.kryptonGroupBox2.TabIndex = 3;
            this.kryptonGroupBox2.Values.Heading = "出入金";
            // 
            // cbEquityTypeList
            // 
            this.cbEquityTypeList.DropDownWidth = 121;
            this.cbEquityTypeList.Location = new System.Drawing.Point(102, 67);
            this.cbEquityTypeList.Name = "cbEquityTypeList";
            this.cbEquityTypeList.Size = new System.Drawing.Size(121, 21);
            this.cbEquityTypeList.TabIndex = 10;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(25, 67);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(68, 18);
            this.kryptonLabel2.TabIndex = 9;
            this.kryptonLabel2.Values.Text = "资金类型:";
            // 
            // btnCashOperation
            // 
            this.btnCashOperation.Location = new System.Drawing.Point(199, 256);
            this.btnCashOperation.Name = "btnCashOperation";
            this.btnCashOperation.Size = new System.Drawing.Size(87, 33);
            this.btnCashOperation.TabIndex = 8;
            this.btnCashOperation.Values.Text = "提 交";
            // 
            // cashop_comment
            // 
            this.cashop_comment.Location = new System.Drawing.Point(102, 119);
            this.cashop_comment.Multiline = true;
            this.cashop_comment.Name = "cashop_comment";
            this.cashop_comment.Size = new System.Drawing.Size(184, 51);
            this.cashop_comment.TabIndex = 7;
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(50, 119);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(41, 18);
            this.kryptonLabel8.TabIndex = 6;
            this.kryptonLabel8.Values.Text = "说明:";
            // 
            // cashop_ref
            // 
            this.cashop_ref.Location = new System.Drawing.Point(102, 94);
            this.cashop_ref.Name = "cashop_ref";
            this.cashop_ref.Size = new System.Drawing.Size(184, 21);
            this.cashop_ref.TabIndex = 5;
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(25, 94);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(68, 18);
            this.kryptonLabel7.TabIndex = 4;
            this.kryptonLabel7.Values.Text = "流水编号:";
            // 
            // cashop_amount
            // 
            this.cashop_amount.Location = new System.Drawing.Point(102, 39);
            this.cashop_amount.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.cashop_amount.Name = "cashop_amount";
            this.cashop_amount.Size = new System.Drawing.Size(121, 20);
            this.cashop_amount.TabIndex = 3;
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(50, 43);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(41, 18);
            this.kryptonLabel6.TabIndex = 2;
            this.kryptonLabel6.Values.Text = "金额:";
            // 
            // cashop_type
            // 
            this.cashop_type.DropDownWidth = 121;
            this.cashop_type.Location = new System.Drawing.Point(102, 14);
            this.cashop_type.Name = "cashop_type";
            this.cashop_type.Size = new System.Drawing.Size(121, 21);
            this.cashop_type.TabIndex = 1;
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(13, 18);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(82, 18);
            this.kryptonLabel5.TabIndex = 0;
            this.kryptonLabel5.Values.Text = "出入金类别:";
            // 
            // cashop_sync
            // 
            this.cashop_sync.Location = new System.Drawing.Point(3, 5);
            this.cashop_sync.Name = "cashop_sync";
            this.cashop_sync.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cashop_sync.Size = new System.Drawing.Size(136, 18);
            this.cashop_sync.TabIndex = 11;
            this.cashop_sync.Values.Text = ":同步操作底层帐户";
            // 
            // panel_syncmainacct
            // 
            this.panel_syncmainacct.Controls.Add(this.kryptonLabel3);
            this.panel_syncmainacct.Controls.Add(this.kryptonLabel1);
            this.panel_syncmainacct.Controls.Add(this.cashop_sync);
            this.panel_syncmainacct.Location = new System.Drawing.Point(50, 171);
            this.panel_syncmainacct.Name = "panel_syncmainacct";
            this.panel_syncmainacct.Size = new System.Drawing.Size(236, 79);
            this.panel_syncmainacct.TabIndex = 12;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(19, 29);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(214, 18);
            this.kryptonLabel1.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.kryptonLabel1.TabIndex = 12;
            this.kryptonLabel1.Values.Text = "需要在主帐户设置中提供资金密码";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(19, 44);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(187, 18);
            this.kryptonLabel3.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.kryptonLabel3.TabIndex = 13;
            this.kryptonLabel3.Values.Text = "否则主帐户出入金操作将失败";
            // 
            // fmCashOperation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 324);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmCashOperation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "出入金操作与帐户查询";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonGroupBox2.Panel.ResumeLayout(false);
            this.kryptonGroupBox2.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).EndInit();
            this.kryptonGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbEquityTypeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cashop_type)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel_syncmainacct)).EndInit();
            this.panel_syncmainacct.ResumeLayout(false);
            this.panel_syncmainacct.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbEquityTypeList;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCashOperation;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox cashop_comment;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox cashop_ref;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown cashop_amount;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cashop_type;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox cashop_sync;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel panel_syncmainacct;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
    }
}