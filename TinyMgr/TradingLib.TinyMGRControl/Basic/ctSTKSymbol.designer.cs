namespace TradingLib.TinyMGRControl
{
    partial class ctSTKSymbol
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
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbsecurity = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbexchange = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.symGrid = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.cbInActive = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.cbsecurity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbexchange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.symGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(454, 3);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel1.TabIndex = 21;
            this.kryptonLabel1.Values.Text = "品种:";
            this.kryptonLabel1.Visible = false;
            // 
            // cbsecurity
            // 
            this.cbsecurity.DropDownWidth = 121;
            this.cbsecurity.Location = new System.Drawing.Point(501, 0);
            this.cbsecurity.Name = "cbsecurity";
            this.cbsecurity.Size = new System.Drawing.Size(90, 21);
            this.cbsecurity.TabIndex = 20;
            this.cbsecurity.Visible = false;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(0, 3);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(51, 20);
            this.kryptonLabel2.TabIndex = 19;
            this.kryptonLabel2.Values.Text = "交易所:";
            // 
            // cbexchange
            // 
            this.cbexchange.DropDownWidth = 121;
            this.cbexchange.Location = new System.Drawing.Point(61, 0);
            this.cbexchange.Name = "cbexchange";
            this.cbexchange.Size = new System.Drawing.Size(147, 21);
            this.cbexchange.TabIndex = 18;
            // 
            // symGrid
            // 
            this.symGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.symGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.symGrid.Location = new System.Drawing.Point(0, 25);
            this.symGrid.Name = "symGrid";
            this.symGrid.RowTemplate.Height = 23;
            this.symGrid.Size = new System.Drawing.Size(652, 343);
            this.symGrid.TabIndex = 22;
            // 
            // cbInActive
            // 
            this.cbInActive.Location = new System.Drawing.Point(214, 1);
            this.cbInActive.Name = "cbInActive";
            this.cbInActive.Size = new System.Drawing.Size(73, 20);
            this.cbInActive.TabIndex = 23;
            this.cbInActive.Values.Text = "禁止交易";
            // 
            // ctSTKSymbol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbInActive);
            this.Controls.Add(this.symGrid);
            this.Controls.Add(this.kryptonLabel1);
            this.Controls.Add(this.cbsecurity);
            this.Controls.Add(this.kryptonLabel2);
            this.Controls.Add(this.cbexchange);
            this.Name = "ctSTKSymbol";
            this.Size = new System.Drawing.Size(652, 368);
            ((System.ComponentModel.ISupportInitialize)(this.cbsecurity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbexchange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.symGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbsecurity;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbexchange;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView symGrid;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox cbInActive;
    }
}
