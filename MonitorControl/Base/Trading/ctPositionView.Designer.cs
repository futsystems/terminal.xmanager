namespace TradingLib.MoniterControl
{
    partial class ctPositionView
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
            this.positiongrid = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.btnFlatAll = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnFlat = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnShowAll = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.btnShowHold = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.positiongrid)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.positiongrid);
            this.kryptonPanel1.Controls.Add(this.btnFlatAll);
            this.kryptonPanel1.Controls.Add(this.btnFlat);
            this.kryptonPanel1.Controls.Add(this.btnShowAll);
            this.kryptonPanel1.Controls.Add(this.btnShowHold);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(872, 261);
            this.kryptonPanel1.TabIndex = 2;
            // 
            // positiongrid
            // 
            this.positiongrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.positiongrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.positiongrid.Location = new System.Drawing.Point(0, 0);
            this.positiongrid.Name = "positiongrid";
            this.positiongrid.RowTemplate.Height = 23;
            this.positiongrid.Size = new System.Drawing.Size(872, 230);
            this.positiongrid.TabIndex = 12;
            // 
            // btnFlatAll
            // 
            this.btnFlatAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFlatAll.Location = new System.Drawing.Point(221, 233);
            this.btnFlatAll.Name = "btnFlatAll";
            this.btnFlatAll.Size = new System.Drawing.Size(71, 25);
            this.btnFlatAll.TabIndex = 3;
            this.btnFlatAll.Values.Text = "市价平仓";
            // 
            // btnFlat
            // 
            this.btnFlat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFlat.Location = new System.Drawing.Point(144, 233);
            this.btnFlat.Name = "btnFlat";
            this.btnFlat.Size = new System.Drawing.Size(71, 25);
            this.btnFlat.TabIndex = 2;
            this.btnFlat.Values.Text = "对价平仓";
            // 
            // btnShowAll
            // 
            this.btnShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShowAll.Location = new System.Drawing.Point(60, 236);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(77, 18);
            this.btnShowAll.TabIndex = 1;
            this.btnShowAll.Values.Text = "当日明细";
            // 
            // btnShowHold
            // 
            this.btnShowHold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShowHold.Location = new System.Drawing.Point(4, 236);
            this.btnShowHold.Name = "btnShowHold";
            this.btnShowHold.Size = new System.Drawing.Size(50, 18);
            this.btnShowHold.TabIndex = 0;
            this.btnShowHold.Values.Text = "持仓";
            // 
            // ctPositionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel1);
            this.DoubleBuffered = true;
            this.Name = "ctPositionView";
            this.Size = new System.Drawing.Size(872, 261);
            this.Load += new System.EventHandler(this.ctPositionView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.positiongrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnFlatAll;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnFlat;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton btnShowAll;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton btnShowHold;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView positiongrid;
    }
}
