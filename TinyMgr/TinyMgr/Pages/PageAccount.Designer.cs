﻿namespace TinyMgr
{
    partial class PageAccount
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
            this.kryptonSplitContainer1 = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.ctAccountMontier1 = new TradingLib.TinyMGRControl.ctAccountMontier();
            this.ctTradingInfo1 = new TradingLib.TinyMGRControl.ctTradingInfo();
            this.ctOrderSentderSTK1 = new TradingLib.TinyMGRControl.Control.ctOrderSentderSTK();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).BeginInit();
            this.kryptonSplitContainer1.Panel1.SuspendLayout();
            this.kryptonSplitContainer1.Panel2.SuspendLayout();
            this.kryptonSplitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonSplitContainer1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(782, 459);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonSplitContainer1
            // 
            this.kryptonSplitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.kryptonSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.kryptonSplitContainer1.Name = "kryptonSplitContainer1";
            this.kryptonSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // kryptonSplitContainer1.Panel1
            // 
            this.kryptonSplitContainer1.Panel1.Controls.Add(this.ctAccountMontier1);
            // 
            // kryptonSplitContainer1.Panel2
            // 
            this.kryptonSplitContainer1.Panel2.Controls.Add(this.ctOrderSentderSTK1);
            this.kryptonSplitContainer1.Panel2.Controls.Add(this.ctTradingInfo1);
            this.kryptonSplitContainer1.Size = new System.Drawing.Size(782, 459);
            this.kryptonSplitContainer1.SplitterDistance = 219;
            this.kryptonSplitContainer1.TabIndex = 0;
            // 
            // ctAccountMontier1
            // 
            this.ctAccountMontier1.DebugEnable = true;
            this.ctAccountMontier1.DebugLevel = TradingLib.API.QSEnumDebugLevel.INFO;
            this.ctAccountMontier1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctAccountMontier1.Location = new System.Drawing.Point(0, 0);
            this.ctAccountMontier1.Name = "ctAccountMontier1";
            this.ctAccountMontier1.Size = new System.Drawing.Size(782, 219);
            this.ctAccountMontier1.TabIndex = 0;
            // 
            // ctTradingInfo1
            // 
            this.ctTradingInfo1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ctTradingInfo1.Location = new System.Drawing.Point(8, 8);
            this.ctTradingInfo1.Name = "ctTradingInfo1";
            this.ctTradingInfo1.Size = new System.Drawing.Size(548, 224);
            this.ctTradingInfo1.TabIndex = 1;
            // 
            // ctOrderSentderSTK1
            // 
            this.ctOrderSentderSTK1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctOrderSentderSTK1.Location = new System.Drawing.Point(562, 3);
            this.ctOrderSentderSTK1.Name = "ctOrderSentderSTK1";
            this.ctOrderSentderSTK1.Size = new System.Drawing.Size(217, 192);
            this.ctOrderSentderSTK1.TabIndex = 2;
            // 
            // PageAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "PageAccount";
            this.Size = new System.Drawing.Size(782, 459);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonSplitContainer1.Panel1.ResumeLayout(false);
            this.kryptonSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).EndInit();
            this.kryptonSplitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer kryptonSplitContainer1;
        private TradingLib.TinyMGRControl.ctAccountMontier ctAccountMontier1;
        private TradingLib.TinyMGRControl.ctTradingInfo ctTradingInfo1;
        private TradingLib.TinyMGRControl.Control.ctOrderSentderSTK ctOrderSentderSTK1;
    }
}
