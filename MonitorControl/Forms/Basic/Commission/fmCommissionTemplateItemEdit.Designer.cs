namespace TradingLib.MoniterControl
{
    partial class fmCommissionTemplateItemEdit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.gbConfig2 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.closetodaybymoney = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.closetodaybyvolume = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel9 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.setAllCodeMonth = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.gbPercent = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.percent = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.gbConfig = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.kryptonLabel10 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.openbymoney = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.byvolume = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.openbyvolume = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.bymoney = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.closebymoney = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.closebyvolume = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.setAllMonth = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.month = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.chargetype = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel13 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnSubmit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.code = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbConfig2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbConfig2.Panel)).BeginInit();
            this.gbConfig2.Panel.SuspendLayout();
            this.gbConfig2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbPercent.Panel)).BeginInit();
            this.gbPercent.Panel.SuspendLayout();
            this.gbPercent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbConfig.Panel)).BeginInit();
            this.gbConfig.Panel.SuspendLayout();
            this.gbConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.month)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chargetype)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.gbConfig2);
            this.kryptonPanel1.Controls.Add(this.setAllCodeMonth);
            this.kryptonPanel1.Controls.Add(this.gbPercent);
            this.kryptonPanel1.Controls.Add(this.gbConfig);
            this.kryptonPanel1.Controls.Add(this.setAllMonth);
            this.kryptonPanel1.Controls.Add(this.month);
            this.kryptonPanel1.Controls.Add(this.chargetype);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel13);
            this.kryptonPanel1.Controls.Add(this.btnSubmit);
            this.kryptonPanel1.Controls.Add(this.code);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(390, 456);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // gbConfig2
            // 
            this.gbConfig2.Location = new System.Drawing.Point(12, 209);
            this.gbConfig2.Name = "gbConfig2";
            // 
            // gbConfig2.Panel
            // 
            this.gbConfig2.Panel.Controls.Add(this.closetodaybymoney);
            this.gbConfig2.Panel.Controls.Add(this.closetodaybyvolume);
            this.gbConfig2.Panel.Controls.Add(this.kryptonLabel5);
            this.gbConfig2.Panel.Controls.Add(this.kryptonLabel9);
            this.gbConfig2.Size = new System.Drawing.Size(363, 72);
            this.gbConfig2.TabIndex = 32;
            this.gbConfig2.Values.Heading = "部分交易所有效";
            // 
            // closetodaybymoney
            // 
            this.closetodaybymoney.DecimalPlaces = 6;
            this.closetodaybymoney.Location = new System.Drawing.Point(213, 3);
            this.closetodaybymoney.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.closetodaybymoney.Name = "closetodaybymoney";
            this.closetodaybymoney.Size = new System.Drawing.Size(131, 22);
            this.closetodaybymoney.TabIndex = 8;
            // 
            // closetodaybyvolume
            // 
            this.closetodaybyvolume.DecimalPlaces = 6;
            this.closetodaybyvolume.Location = new System.Drawing.Point(213, 29);
            this.closetodaybyvolume.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.closetodaybyvolume.Name = "closetodaybyvolume";
            this.closetodaybyvolume.Size = new System.Drawing.Size(131, 22);
            this.closetodaybyvolume.TabIndex = 9;
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(3, 3);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(75, 20);
            this.kryptonLabel5.TabIndex = 7;
            this.kryptonLabel5.Values.Text = "平今手续费:";
            // 
            // kryptonLabel9
            // 
            this.kryptonLabel9.Location = new System.Drawing.Point(3, 27);
            this.kryptonLabel9.Name = "kryptonLabel9";
            this.kryptonLabel9.Size = new System.Drawing.Size(120, 20);
            this.kryptonLabel9.TabIndex = 28;
            this.kryptonLabel9.Values.Text = "平今手续费(按数量):";
            // 
            // setAllCodeMonth
            // 
            this.setAllCodeMonth.Location = new System.Drawing.Point(100, 404);
            this.setAllCodeMonth.Name = "setAllCodeMonth";
            this.setAllCodeMonth.Size = new System.Drawing.Size(98, 20);
            this.setAllCodeMonth.TabIndex = 31;
            this.setAllCodeMonth.Values.Text = "更新所有项目";
            // 
            // gbPercent
            // 
            this.gbPercent.Location = new System.Drawing.Point(12, 286);
            this.gbPercent.Name = "gbPercent";
            // 
            // gbPercent.Panel
            // 
            this.gbPercent.Panel.Controls.Add(this.kryptonLabel7);
            this.gbPercent.Panel.Controls.Add(this.percent);
            this.gbPercent.Size = new System.Drawing.Size(363, 57);
            this.gbPercent.TabIndex = 30;
            this.gbPercent.Values.Heading = "按比例上浮";
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(3, 3);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel7.TabIndex = 29;
            this.kryptonLabel7.Values.Text = "上浮比例:";
            // 
            // percent
            // 
            this.percent.DecimalPlaces = 2;
            this.percent.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.percent.Location = new System.Drawing.Point(213, 3);
            this.percent.Name = "percent";
            this.percent.Size = new System.Drawing.Size(132, 22);
            this.percent.TabIndex = 28;
            // 
            // gbConfig
            // 
            this.gbConfig.Location = new System.Drawing.Point(12, 36);
            this.gbConfig.Name = "gbConfig";
            // 
            // gbConfig.Panel
            // 
            this.gbConfig.Panel.Controls.Add(this.kryptonLabel10);
            this.gbConfig.Panel.Controls.Add(this.kryptonLabel8);
            this.gbConfig.Panel.Controls.Add(this.kryptonLabel4);
            this.gbConfig.Panel.Controls.Add(this.openbymoney);
            this.gbConfig.Panel.Controls.Add(this.byvolume);
            this.gbConfig.Panel.Controls.Add(this.openbyvolume);
            this.gbConfig.Panel.Controls.Add(this.bymoney);
            this.gbConfig.Panel.Controls.Add(this.closebymoney);
            this.gbConfig.Panel.Controls.Add(this.closebyvolume);
            this.gbConfig.Panel.Controls.Add(this.kryptonLabel6);
            this.gbConfig.Size = new System.Drawing.Size(363, 167);
            this.gbConfig.TabIndex = 27;
            this.gbConfig.Values.Heading = "费率设置";
            // 
            // kryptonLabel10
            // 
            this.kryptonLabel10.Location = new System.Drawing.Point(3, 111);
            this.kryptonLabel10.Name = "kryptonLabel10";
            this.kryptonLabel10.Size = new System.Drawing.Size(120, 20);
            this.kryptonLabel10.TabIndex = 29;
            this.kryptonLabel10.Values.Text = "平仓手续费(按数量):";
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(3, 59);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(120, 20);
            this.kryptonLabel8.TabIndex = 27;
            this.kryptonLabel8.Values.Text = "开仓手续费(按数量):";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(3, 31);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(148, 20);
            this.kryptonLabel4.TabIndex = 4;
            this.kryptonLabel4.Values.Text = "开仓手续费(按金额,比例):";
            // 
            // openbymoney
            // 
            this.openbymoney.DecimalPlaces = 6;
            this.openbymoney.Location = new System.Drawing.Point(214, 31);
            this.openbymoney.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.openbymoney.Name = "openbymoney";
            this.openbymoney.Size = new System.Drawing.Size(131, 22);
            this.openbymoney.TabIndex = 5;
            // 
            // byvolume
            // 
            this.byvolume.Location = new System.Drawing.Point(282, 7);
            this.byvolume.Name = "byvolume";
            this.byvolume.Size = new System.Drawing.Size(60, 20);
            this.byvolume.TabIndex = 26;
            this.byvolume.Values.Text = "按手数";
            // 
            // openbyvolume
            // 
            this.openbyvolume.DecimalPlaces = 6;
            this.openbyvolume.Location = new System.Drawing.Point(214, 57);
            this.openbyvolume.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.openbyvolume.Name = "openbyvolume";
            this.openbyvolume.Size = new System.Drawing.Size(131, 22);
            this.openbyvolume.TabIndex = 6;
            // 
            // bymoney
            // 
            this.bymoney.Location = new System.Drawing.Point(215, 7);
            this.bymoney.Name = "bymoney";
            this.bymoney.Size = new System.Drawing.Size(60, 20);
            this.bymoney.TabIndex = 25;
            this.bymoney.Values.Text = "按金额";
            // 
            // closebymoney
            // 
            this.closebymoney.DecimalPlaces = 6;
            this.closebymoney.Location = new System.Drawing.Point(213, 83);
            this.closebymoney.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.closebymoney.Name = "closebymoney";
            this.closebymoney.Size = new System.Drawing.Size(131, 22);
            this.closebymoney.TabIndex = 10;
            // 
            // closebyvolume
            // 
            this.closebyvolume.DecimalPlaces = 6;
            this.closebyvolume.Location = new System.Drawing.Point(213, 109);
            this.closebyvolume.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.closebyvolume.Name = "closebyvolume";
            this.closebyvolume.Size = new System.Drawing.Size(131, 22);
            this.closebyvolume.TabIndex = 11;
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(3, 83);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(148, 20);
            this.kryptonLabel6.TabIndex = 12;
            this.kryptonLabel6.Values.Text = "平仓手续费(按金额,比例):";
            // 
            // setAllMonth
            // 
            this.setAllMonth.Location = new System.Drawing.Point(101, 382);
            this.setAllMonth.Name = "setAllMonth";
            this.setAllMonth.Size = new System.Drawing.Size(136, 20);
            this.setAllMonth.TabIndex = 1;
            this.setAllMonth.Values.Text = "更新该品种所有项目";
            // 
            // month
            // 
            this.month.DropDownWidth = 132;
            this.month.Location = new System.Drawing.Point(228, 8);
            this.month.Name = "month";
            this.month.Size = new System.Drawing.Size(86, 21);
            this.month.TabIndex = 24;
            // 
            // chargetype
            // 
            this.chargetype.DropDownWidth = 132;
            this.chargetype.Location = new System.Drawing.Point(92, 355);
            this.chargetype.Name = "chargetype";
            this.chargetype.Size = new System.Drawing.Size(283, 21);
            this.chargetype.TabIndex = 23;
            // 
            // kryptonLabel13
            // 
            this.kryptonLabel13.Location = new System.Drawing.Point(17, 358);
            this.kryptonLabel13.Name = "kryptonLabel13";
            this.kryptonLabel13.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel13.TabIndex = 22;
            this.kryptonLabel13.Values.Text = "收取方式:";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(308, 419);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(70, 25);
            this.btnSubmit.TabIndex = 21;
            this.btnSubmit.Values.Text = "提 交";
            // 
            // code
            // 
            this.code.Location = new System.Drawing.Point(92, 8);
            this.code.Name = "code";
            this.code.Size = new System.Drawing.Size(74, 20);
            this.code.TabIndex = 20;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(181, 11);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel3.TabIndex = 3;
            this.kryptonLabel3.Values.Text = "月份:";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(17, 12);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel2.TabIndex = 2;
            this.kryptonLabel2.Values.Text = "品种代码:";
            // 
            // fmCommissionTemplateItemEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 456);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmCommissionTemplateItemEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "手续费项目";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbConfig2.Panel)).EndInit();
            this.gbConfig2.Panel.ResumeLayout(false);
            this.gbConfig2.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbConfig2)).EndInit();
            this.gbConfig2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbPercent.Panel)).EndInit();
            this.gbPercent.Panel.ResumeLayout(false);
            this.gbPercent.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbPercent)).EndInit();
            this.gbPercent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbConfig.Panel)).EndInit();
            this.gbConfig.Panel.ResumeLayout(false);
            this.gbConfig.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbConfig)).EndInit();
            this.gbConfig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.month)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chargetype)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown closetodaybyvolume;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown closetodaybymoney;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown openbyvolume;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown openbymoney;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown closebyvolume;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown closebymoney;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox code;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSubmit;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel13;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox chargetype;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox month;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton bymoney;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton byvolume;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox setAllMonth;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox gbConfig;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox gbPercent;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown percent;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox setAllCodeMonth;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox gbConfig2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel9;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel10;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
    }
}