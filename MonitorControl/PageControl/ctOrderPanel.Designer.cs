namespace TradingLib.MoniterControl
{
    partial class ctOrderPanel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.side_sell = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.side_buy = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.btnInsertTrade = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSubmit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.price = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.size = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cboffsetflag = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.account = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbSymbolList = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboffsetflag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSymbolList)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.cbSymbolList);
            this.kryptonPanel1.Controls.Add(this.side_sell);
            this.kryptonPanel1.Controls.Add(this.side_buy);
            this.kryptonPanel1.Controls.Add(this.btnInsertTrade);
            this.kryptonPanel1.Controls.Add(this.btnSubmit);
            this.kryptonPanel1.Controls.Add(this.price);
            this.kryptonPanel1.Controls.Add(this.size);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel7);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel8);
            this.kryptonPanel1.Controls.Add(this.cboffsetflag);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel6);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel5);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel1.Controls.Add(this.account);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(212, 237);
            this.kryptonPanel1.TabIndex = 1;
            // 
            // side_sell
            // 
            this.side_sell.Location = new System.Drawing.Point(123, 57);
            this.side_sell.Name = "side_sell";
            this.side_sell.Size = new System.Drawing.Size(47, 20);
            this.side_sell.TabIndex = 16;
            this.side_sell.Values.Text = "卖出";
            // 
            // side_buy
            // 
            this.side_buy.Location = new System.Drawing.Point(67, 57);
            this.side_buy.Name = "side_buy";
            this.side_buy.Size = new System.Drawing.Size(47, 20);
            this.side_buy.TabIndex = 15;
            this.side_buy.Values.Text = "买入";
            // 
            // btnInsertTrade
            // 
            this.btnInsertTrade.Location = new System.Drawing.Point(11, 171);
            this.btnInsertTrade.Name = "btnInsertTrade";
            this.btnInsertTrade.Size = new System.Drawing.Size(55, 48);
            this.btnInsertTrade.StateCommon.Content.ShortText.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnInsertTrade.TabIndex = 14;
            this.btnInsertTrade.Values.Text = "插 入";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(72, 171);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(111, 48);
            this.btnSubmit.StateCommon.Content.ShortText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSubmit.TabIndex = 12;
            this.btnSubmit.Values.Text = "下    单";
            // 
            // price
            // 
            this.price.Location = new System.Drawing.Point(67, 131);
            this.price.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.price.Name = "price";
            this.price.Size = new System.Drawing.Size(116, 22);
            this.price.TabIndex = 11;
            // 
            // size
            // 
            this.size.Location = new System.Drawing.Point(67, 105);
            this.size.Name = "size";
            this.size.Size = new System.Drawing.Size(116, 22);
            this.size.TabIndex = 10;
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(20, 133);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel7.TabIndex = 9;
            this.kryptonLabel7.Values.Text = "价格:";
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(20, 107);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel8.TabIndex = 8;
            this.kryptonLabel8.Values.Text = "手数:";
            // 
            // cboffsetflag
            // 
            this.cboffsetflag.DropDownWidth = 89;
            this.cboffsetflag.Location = new System.Drawing.Point(67, 78);
            this.cboffsetflag.Name = "cboffsetflag";
            this.cboffsetflag.Size = new System.Drawing.Size(116, 21);
            this.cboffsetflag.TabIndex = 7;
            this.cboffsetflag.Text = "--";
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(20, 81);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel6.TabIndex = 5;
            this.kryptonLabel6.Values.Text = "开平:";
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(20, 57);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel5.TabIndex = 4;
            this.kryptonLabel5.Values.Text = "买卖:";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(20, 33);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel4.TabIndex = 2;
            this.kryptonLabel4.Values.Text = "合约:";
            // 
            // account
            // 
            this.account.Location = new System.Drawing.Point(72, 15);
            this.account.Name = "account";
            this.account.Size = new System.Drawing.Size(23, 16);
            this.account.StateCommon.ShortText.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.account.TabIndex = 1;
            this.account.Values.Text = "--";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(20, 13);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "帐户:";
            // 
            // cbSymbolList
            // 
            this.cbSymbolList.DropDownWidth = 116;
            this.cbSymbolList.Location = new System.Drawing.Point(67, 30);
            this.cbSymbolList.Name = "cbSymbolList";
            this.cbSymbolList.Size = new System.Drawing.Size(116, 21);
            this.cbSymbolList.TabIndex = 17;
            // 
            // ctOrderPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "ctOrderPanel";
            this.Size = new System.Drawing.Size(212, 237);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboffsetflag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSymbolList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel account;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cboffsetflag;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnInsertTrade;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSubmit;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown price;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown size;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton side_buy;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton side_sell;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbSymbolList;
    }
}
