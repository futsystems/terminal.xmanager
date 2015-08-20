namespace TradingLib.MoniterControl
{
    partial class ctFollowCentre
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
            this.ctFollowItemList1 = new TradingLib.MoniterControl.ctFollowItemList();
            this.ctFollowStrategyMoniter1 = new TradingLib.MoniterControl.ctFollowStrategyMoniter();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.ctFollowItemList1);
            this.kryptonPanel1.Controls.Add(this.ctFollowStrategyMoniter1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(836, 445);
            this.kryptonPanel1.TabIndex = 1;
            // 
            // ctFollowItemList1
            // 
            this.ctFollowItemList1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ctFollowItemList1.Location = new System.Drawing.Point(0, 108);
            this.ctFollowItemList1.Name = "ctFollowItemList1";
            this.ctFollowItemList1.Size = new System.Drawing.Size(836, 337);
            this.ctFollowItemList1.TabIndex = 1;
            // 
            // ctFollowStrategyMoniter1
            // 
            this.ctFollowStrategyMoniter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctFollowStrategyMoniter1.Location = new System.Drawing.Point(0, 0);
            this.ctFollowStrategyMoniter1.Name = "ctFollowStrategyMoniter1";
            this.ctFollowStrategyMoniter1.Size = new System.Drawing.Size(836, 102);
            this.ctFollowStrategyMoniter1.TabIndex = 0;
            // 
            // ctFollowCentre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "ctFollowCentre";
            this.Size = new System.Drawing.Size(836, 445);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ctFollowStrategyMoniter ctFollowStrategyMoniter1;
        private ctFollowItemList ctFollowItemList1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
    }
}
