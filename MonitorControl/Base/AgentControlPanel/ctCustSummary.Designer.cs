namespace TradingLib.MoniterControl
{
    partial class ctCustSummary
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
            this.margin = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.marginfrzoen = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cashout = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel10 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cashin = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel12 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.unrealizedpl = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.realizedpl = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.shortpos = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.longpos = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(37, 4);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(65, 26);
            this.kryptonLabel1.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.kryptonLabel1.StateCommon.ShortText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "保证金:";
            // 
            // margin
            // 
            this.margin.Location = new System.Drawing.Point(101, 4);
            this.margin.Name = "margin";
            this.margin.Size = new System.Drawing.Size(103, 26);
            this.margin.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.margin.StateCommon.ShortText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.margin.TabIndex = 1;
            this.margin.Values.Text = "1250000.00";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(4, 30);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(98, 26);
            this.kryptonLabel3.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.kryptonLabel3.StateCommon.ShortText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.kryptonLabel3.TabIndex = 2;
            this.kryptonLabel3.Values.Text = "冻结保证金:";
            // 
            // marginfrzoen
            // 
            this.marginfrzoen.Location = new System.Drawing.Point(101, 30);
            this.marginfrzoen.Name = "marginfrzoen";
            this.marginfrzoen.Size = new System.Drawing.Size(103, 26);
            this.marginfrzoen.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.marginfrzoen.StateCommon.ShortText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.marginfrzoen.TabIndex = 3;
            this.marginfrzoen.Values.Text = "1250000.00";
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.shortpos);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel1.Controls.Add(this.longpos);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel7);
            this.kryptonPanel1.Controls.Add(this.label1);
            this.kryptonPanel1.Controls.Add(this.cashout);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel10);
            this.kryptonPanel1.Controls.Add(this.cashin);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel12);
            this.kryptonPanel1.Controls.Add(this.unrealizedpl);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel8);
            this.kryptonPanel1.Controls.Add(this.realizedpl);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel6);
            this.kryptonPanel1.Controls.Add(this.marginfrzoen);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel1.Controls.Add(this.margin);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(1027, 60);
            this.kryptonPanel1.StateCommon.Color1 = System.Drawing.Color.DimGray;
            this.kryptonPanel1.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 50);
            this.label1.TabIndex = 12;
            this.label1.Text = "label1";
            // 
            // cashout
            // 
            this.cashout.Location = new System.Drawing.Point(420, 30);
            this.cashout.Name = "cashout";
            this.cashout.Size = new System.Drawing.Size(103, 26);
            this.cashout.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.cashout.StateCommon.ShortText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cashout.TabIndex = 11;
            this.cashout.Values.Text = "1250000.00";
            // 
            // kryptonLabel10
            // 
            this.kryptonLabel10.Location = new System.Drawing.Point(372, 30);
            this.kryptonLabel10.Name = "kryptonLabel10";
            this.kryptonLabel10.Size = new System.Drawing.Size(49, 26);
            this.kryptonLabel10.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.kryptonLabel10.StateCommon.ShortText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.kryptonLabel10.TabIndex = 10;
            this.kryptonLabel10.Values.Text = "出金:";
            // 
            // cashin
            // 
            this.cashin.Location = new System.Drawing.Point(419, 4);
            this.cashin.Name = "cashin";
            this.cashin.Size = new System.Drawing.Size(103, 26);
            this.cashin.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.cashin.StateCommon.ShortText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cashin.TabIndex = 9;
            this.cashin.Values.Text = "1250000.00";
            // 
            // kryptonLabel12
            // 
            this.kryptonLabel12.Location = new System.Drawing.Point(372, 4);
            this.kryptonLabel12.Name = "kryptonLabel12";
            this.kryptonLabel12.Size = new System.Drawing.Size(49, 26);
            this.kryptonLabel12.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.kryptonLabel12.StateCommon.ShortText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.kryptonLabel12.TabIndex = 8;
            this.kryptonLabel12.Values.Text = "入金:";
            // 
            // unrealizedpl
            // 
            this.unrealizedpl.Location = new System.Drawing.Point(279, 30);
            this.unrealizedpl.Name = "unrealizedpl";
            this.unrealizedpl.Size = new System.Drawing.Size(103, 26);
            this.unrealizedpl.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.unrealizedpl.StateCommon.ShortText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.unrealizedpl.TabIndex = 7;
            this.unrealizedpl.Values.Text = "1250000.00";
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(197, 30);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(82, 26);
            this.kryptonLabel8.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.kryptonLabel8.StateCommon.ShortText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.kryptonLabel8.TabIndex = 6;
            this.kryptonLabel8.Values.Text = "浮动盈亏:";
            // 
            // realizedpl
            // 
            this.realizedpl.Location = new System.Drawing.Point(279, 4);
            this.realizedpl.Name = "realizedpl";
            this.realizedpl.Size = new System.Drawing.Size(103, 26);
            this.realizedpl.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.realizedpl.StateCommon.ShortText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.realizedpl.TabIndex = 5;
            this.realizedpl.Values.Text = "1250000.00";
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(197, 4);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(82, 26);
            this.kryptonLabel6.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.kryptonLabel6.StateCommon.ShortText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.kryptonLabel6.TabIndex = 4;
            this.kryptonLabel6.Values.Text = "平仓盈亏:";
            // 
            // shortpos
            // 
            this.shortpos.Location = new System.Drawing.Point(559, 30);
            this.shortpos.Name = "shortpos";
            this.shortpos.Size = new System.Drawing.Size(31, 26);
            this.shortpos.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.shortpos.StateCommon.ShortText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.shortpos.TabIndex = 16;
            this.shortpos.Values.Text = "22";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(511, 30);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(49, 26);
            this.kryptonLabel4.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.kryptonLabel4.StateCommon.ShortText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.kryptonLabel4.TabIndex = 15;
            this.kryptonLabel4.Values.Text = "空头:";
            // 
            // longpos
            // 
            this.longpos.Location = new System.Drawing.Point(558, 4);
            this.longpos.Name = "longpos";
            this.longpos.Size = new System.Drawing.Size(31, 26);
            this.longpos.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.longpos.StateCommon.ShortText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.longpos.TabIndex = 14;
            this.longpos.Values.Text = "11";
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(511, 4);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(49, 26);
            this.kryptonLabel7.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.kryptonLabel7.StateCommon.ShortText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.kryptonLabel7.TabIndex = 13;
            this.kryptonLabel7.Values.Text = "多头:";
            // 
            // ctCustSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "ctCustSummary";
            this.Size = new System.Drawing.Size(1027, 60);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel margin;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel marginfrzoen;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel unrealizedpl;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel realizedpl;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel cashout;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel10;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel cashin;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel12;
        private System.Windows.Forms.Label label1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel shortpos;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel longpos;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;

    }
}
