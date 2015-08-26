namespace TradingLib.MoniterControl
{
    partial class ctFollowItemTrade
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
            this.xPrice = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.xSize = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.tradeIDs = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.xPrice);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel7);
            this.kryptonPanel1.Controls.Add(this.xSize);
            this.kryptonPanel1.Controls.Add(this.tradeIDs);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(561, 20);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // xPrice
            // 
            this.xPrice.Location = new System.Drawing.Point(379, 0);
            this.xPrice.Name = "xPrice";
            this.xPrice.Size = new System.Drawing.Size(46, 20);
            this.xPrice.TabIndex = 6;
            this.xPrice.Values.Text = "3800.3";
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(351, 0);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(22, 20);
            this.kryptonLabel7.TabIndex = 5;
            this.kryptonLabel7.Values.Text = "@";
            // 
            // xSize
            // 
            this.xSize.Location = new System.Drawing.Point(325, 0);
            this.xSize.Name = "xSize";
            this.xSize.Size = new System.Drawing.Size(17, 20);
            this.xSize.TabIndex = 4;
            this.xSize.Values.Text = "2";
            // 
            // tradeIDs
            // 
            this.tradeIDs.Location = new System.Drawing.Point(105, 0);
            this.tradeIDs.Name = "tradeIDs";
            this.tradeIDs.Size = new System.Drawing.Size(129, 20);
            this.tradeIDs.TabIndex = 1;
            this.tradeIDs.Values.Text = "25674/123124242134";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(99, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "        +成交编号:";
            // 
            // ctFollowItemTrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "ctFollowItemTrade";
            this.Size = new System.Drawing.Size(561, 20);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel tradeIDs;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel xPrice;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel xSize;
    }
}
