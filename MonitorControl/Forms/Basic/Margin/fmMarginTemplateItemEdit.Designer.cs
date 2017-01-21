﻿namespace TradingLib.MoniterControl
{
    partial class fmMarginTemplateItemEdit
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
            this.setAllCodeMonth = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.gbPercent = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.percent = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.gbConfig = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.marginbymoney = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.byvolume = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.marginbyvolume = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.bymoney = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
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
            this.kryptonPanel1.Size = new System.Drawing.Size(368, 338);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // setAllCodeMonth
            // 
            this.setAllCodeMonth.Location = new System.Drawing.Point(90, 277);
            this.setAllCodeMonth.Name = "setAllCodeMonth";
            this.setAllCodeMonth.Size = new System.Drawing.Size(98, 20);
            this.setAllCodeMonth.TabIndex = 31;
            this.setAllCodeMonth.Values.Text = "更新所有项目";
            // 
            // gbPercent
            // 
            this.gbPercent.Location = new System.Drawing.Point(12, 153);
            this.gbPercent.Name = "gbPercent";
            // 
            // gbPercent.Panel
            // 
            this.gbPercent.Panel.Controls.Add(this.kryptonLabel7);
            this.gbPercent.Panel.Controls.Add(this.percent);
            this.gbPercent.Size = new System.Drawing.Size(344, 57);
            this.gbPercent.TabIndex = 30;
            this.gbPercent.Values.Heading = "按比例上浮";
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(3, 6);
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
            this.percent.Location = new System.Drawing.Point(181, 6);
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
            this.gbConfig.Panel.Controls.Add(this.kryptonLabel1);
            this.gbConfig.Panel.Controls.Add(this.kryptonLabel4);
            this.gbConfig.Panel.Controls.Add(this.marginbymoney);
            this.gbConfig.Panel.Controls.Add(this.byvolume);
            this.gbConfig.Panel.Controls.Add(this.marginbyvolume);
            this.gbConfig.Panel.Controls.Add(this.bymoney);
            this.gbConfig.Size = new System.Drawing.Size(344, 111);
            this.gbConfig.TabIndex = 27;
            this.gbConfig.Values.Heading = "费率设置";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(4, 55);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(108, 20);
            this.kryptonLabel1.TabIndex = 27;
            this.kryptonLabel1.Values.Text = "保证金率(按数量):";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(3, 31);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(135, 20);
            this.kryptonLabel4.TabIndex = 4;
            this.kryptonLabel4.Values.Text = "保证金率(按金额,比例):";
            // 
            // marginbymoney
            // 
            this.marginbymoney.DecimalPlaces = 6;
            this.marginbymoney.Location = new System.Drawing.Point(183, 29);
            this.marginbymoney.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.marginbymoney.Name = "marginbymoney";
            this.marginbymoney.Size = new System.Drawing.Size(131, 22);
            this.marginbymoney.TabIndex = 5;
            // 
            // byvolume
            // 
            this.byvolume.Location = new System.Drawing.Point(250, 7);
            this.byvolume.Name = "byvolume";
            this.byvolume.Size = new System.Drawing.Size(60, 20);
            this.byvolume.TabIndex = 26;
            this.byvolume.Values.Text = "按手数";
            // 
            // marginbyvolume
            // 
            this.marginbyvolume.DecimalPlaces = 6;
            this.marginbyvolume.Location = new System.Drawing.Point(183, 55);
            this.marginbyvolume.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.marginbyvolume.Name = "marginbyvolume";
            this.marginbyvolume.Size = new System.Drawing.Size(131, 22);
            this.marginbyvolume.TabIndex = 6;
            // 
            // bymoney
            // 
            this.bymoney.Location = new System.Drawing.Point(183, 7);
            this.bymoney.Name = "bymoney";
            this.bymoney.Size = new System.Drawing.Size(60, 20);
            this.bymoney.TabIndex = 25;
            this.bymoney.Values.Text = "按金额";
            // 
            // setAllMonth
            // 
            this.setAllMonth.Location = new System.Drawing.Point(91, 255);
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
            this.chargetype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chargetype.DropDownWidth = 132;
            this.chargetype.Location = new System.Drawing.Point(90, 227);
            this.chargetype.Name = "chargetype";
            this.chargetype.Size = new System.Drawing.Size(266, 21);
            this.chargetype.TabIndex = 23;
            // 
            // kryptonLabel13
            // 
            this.kryptonLabel13.Location = new System.Drawing.Point(17, 230);
            this.kryptonLabel13.Name = "kryptonLabel13";
            this.kryptonLabel13.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel13.TabIndex = 22;
            this.kryptonLabel13.Values.Text = "收取方式:";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(286, 301);
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
            // fmMarginTemplateItemEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 338);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmMarginTemplateItemEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "保证金项目";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
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
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown marginbyvolume;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown marginbymoney;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
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
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
    }
}