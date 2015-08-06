namespace TradingLib.MoniterControl
{
    partial class ctFollowStrategyMoniter
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
            this.strategyGrid = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.strategyGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // strategyGrid
            // 
            this.strategyGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.strategyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.strategyGrid.Location = new System.Drawing.Point(0, 0);
            this.strategyGrid.Name = "strategyGrid";
            this.strategyGrid.RowTemplate.Height = 23;
            this.strategyGrid.Size = new System.Drawing.Size(741, 327);
            this.strategyGrid.TabIndex = 0;
            // 
            // ctFollowStrategyMoniter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.strategyGrid);
            this.Name = "ctFollowStrategyMoniter";
            this.Size = new System.Drawing.Size(741, 327);
            ((System.ComponentModel.ISupportInitialize)(this.strategyGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView strategyGrid;
    }
}
