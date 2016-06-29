namespace TinyMgr.Pages
{
    partial class PageSTKSymbol
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
            this.ctSymbol1 = new TradingLib.TinyMGRControl.ctSTKSymbol();
            this.SuspendLayout();
            // 
            // ctSymbol1
            // 
            this.ctSymbol1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctSymbol1.Location = new System.Drawing.Point(0, 0);
            this.ctSymbol1.Name = "ctSymbol1";
            this.ctSymbol1.Size = new System.Drawing.Size(827, 437);
            this.ctSymbol1.TabIndex = 0;
            // 
            // PageSymbol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctSymbol1);
            this.Name = "PageSymbol";
            this.Size = new System.Drawing.Size(827, 437);
            this.ResumeLayout(false);

        }

        #endregion

        private TradingLib.TinyMGRControl.ctSTKSymbol ctSymbol1;
    }
}
