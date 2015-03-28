namespace TradingLib.MoniterControl
{
    partial class ctTradeView
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
            this.tradeGrid = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.kryptonRadioButton1 = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.kryptonRadioButton2 = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tradeGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonRadioButton2);
            this.kryptonPanel1.Controls.Add(this.kryptonRadioButton1);
            this.kryptonPanel1.Controls.Add(this.tradeGrid);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(848, 181);
            this.kryptonPanel1.TabIndex = 2;
            // 
            // tradeGrid
            // 
            this.tradeGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tradeGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tradeGrid.Location = new System.Drawing.Point(0, 0);
            this.tradeGrid.Name = "tradeGrid";
            this.tradeGrid.RowTemplate.Height = 23;
            this.tradeGrid.Size = new System.Drawing.Size(848, 154);
            this.tradeGrid.TabIndex = 0;
            // 
            // kryptonRadioButton1
            // 
            this.kryptonRadioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.kryptonRadioButton1.Location = new System.Drawing.Point(4, 160);
            this.kryptonRadioButton1.Name = "kryptonRadioButton1";
            this.kryptonRadioButton1.Size = new System.Drawing.Size(50, 18);
            this.kryptonRadioButton1.TabIndex = 11;
            this.kryptonRadioButton1.Values.Text = "明细";
            // 
            // kryptonRadioButton2
            // 
            this.kryptonRadioButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.kryptonRadioButton2.Location = new System.Drawing.Point(60, 160);
            this.kryptonRadioButton2.Name = "kryptonRadioButton2";
            this.kryptonRadioButton2.Size = new System.Drawing.Size(50, 18);
            this.kryptonRadioButton2.TabIndex = 12;
            this.kryptonRadioButton2.Values.Text = "合计";
            // 
            // ctTradeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel1);
            this.DoubleBuffered = true;
            this.Name = "ctTradeView";
            this.Size = new System.Drawing.Size(848, 181);
            this.Load += new System.EventHandler(this.ctTradeView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tradeGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView tradeGrid;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton kryptonRadioButton2;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton kryptonRadioButton1;
    }
}
