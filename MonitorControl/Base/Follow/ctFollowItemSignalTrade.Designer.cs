namespace TradingLib.MoniterControl
{
    partial class ctFollowItemSignalTrade
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
            this.price = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.size = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.symbol = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.side = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.tradeIDs = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.price);
            this.kryptonPanel1.Controls.Add(this.size);
            this.kryptonPanel1.Controls.Add(this.symbol);
            this.kryptonPanel1.Controls.Add(this.side);
            this.kryptonPanel1.Controls.Add(this.tradeIDs);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel6);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(659, 20);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // price
            // 
            this.price.Location = new System.Drawing.Point(477, 0);
            this.price.Name = "price";
            this.price.Size = new System.Drawing.Size(20, 20);
            this.price.TabIndex = 11;
            this.price.Values.Text = "--";
            // 
            // size
            // 
            this.size.Location = new System.Drawing.Point(423, 0);
            this.size.Name = "size";
            this.size.Size = new System.Drawing.Size(20, 20);
            this.size.TabIndex = 10;
            this.size.Values.Text = "--";
            // 
            // symbol
            // 
            this.symbol.Location = new System.Drawing.Point(376, 0);
            this.symbol.Name = "symbol";
            this.symbol.Size = new System.Drawing.Size(20, 20);
            this.symbol.TabIndex = 9;
            this.symbol.Values.Text = "--";
            // 
            // side
            // 
            this.side.Location = new System.Drawing.Point(350, 0);
            this.side.Name = "side";
            this.side.Size = new System.Drawing.Size(20, 20);
            this.side.TabIndex = 8;
            this.side.Values.Text = "--";
            // 
            // tradeIDs
            // 
            this.tradeIDs.Location = new System.Drawing.Point(90, 0);
            this.tradeIDs.Name = "tradeIDs";
            this.tradeIDs.Size = new System.Drawing.Size(20, 20);
            this.tradeIDs.TabIndex = 6;
            this.tradeIDs.Values.Text = "--";
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(449, 0);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(22, 20);
            this.kryptonLabel6.TabIndex = 5;
            this.kryptonLabel6.Values.Text = "@";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(88, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "信号成交编号:";
            // 
            // ctFollowItemSignalTrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "ctFollowItemSignalTrade";
            this.Size = new System.Drawing.Size(659, 20);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel price;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel size;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel symbol;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel side;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel tradeIDs;
    }
}
