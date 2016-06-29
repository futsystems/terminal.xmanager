namespace TradingLib.TinyMGRControl
{
    partial class ctSecurity
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
            this.secGrid = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.cbexchange = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.secGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbexchange)).BeginInit();
            this.SuspendLayout();
            // 
            // secGrid
            // 
            this.secGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.secGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.secGrid.Location = new System.Drawing.Point(0, 25);
            this.secGrid.Name = "secGrid";
            this.secGrid.RowTemplate.Height = 23;
            this.secGrid.Size = new System.Drawing.Size(739, 356);
            this.secGrid.TabIndex = 0;
            // 
            // cbexchange
            // 
            this.cbexchange.DropDownWidth = 121;
            this.cbexchange.Location = new System.Drawing.Point(61, 0);
            this.cbexchange.Name = "cbexchange";
            this.cbexchange.Size = new System.Drawing.Size(129, 21);
            this.cbexchange.TabIndex = 8;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(0, 3);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(51, 22);
            this.kryptonLabel2.TabIndex = 7;
            this.kryptonLabel2.Values.Text = "交易所:";
            // 
            // ctSecurity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbexchange);
            this.Controls.Add(this.kryptonLabel2);
            this.Controls.Add(this.secGrid);
            this.Name = "ctSecurity";
            this.Size = new System.Drawing.Size(739, 384);
            ((System.ComponentModel.ISupportInitialize)(this.secGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbexchange)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView secGrid;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbexchange;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
    }
}
