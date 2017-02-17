namespace TradingLib.MoniterControl
{
    partial class ctFollowItemFilter
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
            this.tbSignal = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.cbPos = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.eventType = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.tbSymbol = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.profitType = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.eventType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.profitType)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(0, 3);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(35, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "信号";
            // 
            // tbSignal
            // 
            this.tbSignal.Location = new System.Drawing.Point(33, 3);
            this.tbSignal.Name = "tbSignal";
            this.tbSignal.Size = new System.Drawing.Size(82, 20);
            this.tbSignal.TabIndex = 1;
            // 
            // cbPos
            // 
            this.cbPos.Location = new System.Drawing.Point(418, 3);
            this.cbPos.Name = "cbPos";
            this.cbPos.Size = new System.Drawing.Size(48, 20);
            this.cbPos.TabIndex = 3;
            this.cbPos.Values.Text = "持仓";
            // 
            // eventType
            // 
            this.eventType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.eventType.DropDownWidth = 86;
            this.eventType.Location = new System.Drawing.Point(226, 3);
            this.eventType.Name = "eventType";
            this.eventType.Size = new System.Drawing.Size(86, 21);
            this.eventType.TabIndex = 6;
            // 
            // tbSymbol
            // 
            this.tbSymbol.Location = new System.Drawing.Point(150, 3);
            this.tbSymbol.Name = "tbSymbol";
            this.tbSymbol.Size = new System.Drawing.Size(70, 20);
            this.tbSymbol.TabIndex = 8;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(117, 3);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(35, 20);
            this.kryptonLabel2.TabIndex = 7;
            this.kryptonLabel2.Values.Text = "合约";
            // 
            // profitType
            // 
            this.profitType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.profitType.DropDownWidth = 86;
            this.profitType.Location = new System.Drawing.Point(318, 3);
            this.profitType.Name = "profitType";
            this.profitType.Size = new System.Drawing.Size(86, 21);
            this.profitType.TabIndex = 9;
            // 
            // ctFollowItemFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.profitType);
            this.Controls.Add(this.tbSymbol);
            this.Controls.Add(this.kryptonLabel2);
            this.Controls.Add(this.eventType);
            this.Controls.Add(this.cbPos);
            this.Controls.Add(this.tbSignal);
            this.Controls.Add(this.kryptonLabel1);
            this.Name = "ctFollowItemFilter";
            this.Size = new System.Drawing.Size(732, 24);
            ((System.ComponentModel.ISupportInitialize)(this.eventType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.profitType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox tbSignal;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox cbPos;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox eventType;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox tbSymbol;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox profitType;
    }
}
