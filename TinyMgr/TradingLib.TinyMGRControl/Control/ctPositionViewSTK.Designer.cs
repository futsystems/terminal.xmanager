﻿namespace TradingLib.TinyMGRControl
{
    partial class ctPositionViewSTK
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
            this.positionGrid = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.positionGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // positionGrid
            // 
            this.positionGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.positionGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.positionGrid.Location = new System.Drawing.Point(0, 0);
            this.positionGrid.Name = "positionGrid";
            this.positionGrid.RowTemplate.Height = 23;
            this.positionGrid.Size = new System.Drawing.Size(707, 316);
            this.positionGrid.TabIndex = 0;
            // 
            // ctPositionViewSTK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.positionGrid);
            this.Name = "ctPositionViewSTK";
            this.Size = new System.Drawing.Size(707, 316);
            ((System.ComponentModel.ISupportInitialize)(this.positionGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView positionGrid;

    }
}
