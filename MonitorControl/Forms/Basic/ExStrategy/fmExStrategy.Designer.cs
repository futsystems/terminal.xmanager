﻿namespace TradingLib.MoniterControl
{
    partial class fmExStrategy
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
            this.components = new System.ComponentModel.Container();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonGroupBox3 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.exitSlip = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.entrySlip = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.templateTree = new ComponentFactory.Krypton.Toolkit.KryptonTreeView();
            this.kryptonGroupBox2 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.creditseparate = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.poslock = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonGroupBox1 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.includecloseprofit = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.margin = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.sidemargin = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.btnSubmit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.includepositionprofit = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.kryptonLabel9 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.limitcheck = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.probability = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3.Panel)).BeginInit();
            this.kryptonGroupBox3.Panel.SuspendLayout();
            this.kryptonGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).BeginInit();
            this.kryptonGroupBox2.Panel.SuspendLayout();
            this.kryptonGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.margin)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox3);
            this.kryptonPanel1.Controls.Add(this.templateTree);
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox2);
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox1);
            this.kryptonPanel1.Controls.Add(this.btnSubmit);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(519, 420);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonGroupBox3
            // 
            this.kryptonGroupBox3.Location = new System.Drawing.Point(177, 243);
            this.kryptonGroupBox3.Name = "kryptonGroupBox3";
            // 
            // kryptonGroupBox3.Panel
            // 
            this.kryptonGroupBox3.Panel.Controls.Add(this.kryptonLabel8);
            this.kryptonGroupBox3.Panel.Controls.Add(this.probability);
            this.kryptonGroupBox3.Panel.Controls.Add(this.kryptonLabel3);
            this.kryptonGroupBox3.Panel.Controls.Add(this.limitcheck);
            this.kryptonGroupBox3.Panel.Controls.Add(this.exitSlip);
            this.kryptonGroupBox3.Panel.Controls.Add(this.kryptonLabel7);
            this.kryptonGroupBox3.Panel.Controls.Add(this.entrySlip);
            this.kryptonGroupBox3.Panel.Controls.Add(this.kryptonLabel2);
            this.kryptonGroupBox3.Size = new System.Drawing.Size(316, 134);
            this.kryptonGroupBox3.TabIndex = 19;
            this.kryptonGroupBox3.Values.Heading = "风险度设置";
            // 
            // exitSlip
            // 
            this.exitSlip.Location = new System.Drawing.Point(114, 30);
            this.exitSlip.Name = "exitSlip";
            this.exitSlip.Size = new System.Drawing.Size(88, 22);
            this.exitSlip.TabIndex = 3;
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(70, 32);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel7.TabIndex = 2;
            this.kryptonLabel7.Values.Text = "平仓:";
            // 
            // entrySlip
            // 
            this.entrySlip.Location = new System.Drawing.Point(114, 2);
            this.entrySlip.Name = "entrySlip";
            this.entrySlip.Size = new System.Drawing.Size(88, 22);
            this.entrySlip.TabIndex = 1;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(70, 4);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel2.TabIndex = 0;
            this.kryptonLabel2.Values.Text = "开仓:";
            // 
            // templateTree
            // 
            this.templateTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.templateTree.Location = new System.Drawing.Point(0, 0);
            this.templateTree.Name = "templateTree";
            this.templateTree.Size = new System.Drawing.Size(160, 420);
            this.templateTree.TabIndex = 18;
            // 
            // kryptonGroupBox2
            // 
            this.kryptonGroupBox2.Location = new System.Drawing.Point(177, 137);
            this.kryptonGroupBox2.Name = "kryptonGroupBox2";
            // 
            // kryptonGroupBox2.Panel
            // 
            this.kryptonGroupBox2.Panel.Controls.Add(this.kryptonLabel5);
            this.kryptonGroupBox2.Panel.Controls.Add(this.creditseparate);
            this.kryptonGroupBox2.Panel.Controls.Add(this.poslock);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kryptonLabel6);
            this.kryptonGroupBox2.Size = new System.Drawing.Size(316, 91);
            this.kryptonGroupBox2.TabIndex = 17;
            this.kryptonGroupBox2.Values.Heading = "其他";
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(34, 3);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(88, 20);
            this.kryptonLabel5.TabIndex = 8;
            this.kryptonLabel5.Values.Text = "信用额度显示:";
            // 
            // creditseparate
            // 
            this.creditseparate.Location = new System.Drawing.Point(129, 3);
            this.creditseparate.Name = "creditseparate";
            this.creditseparate.Size = new System.Drawing.Size(73, 20);
            this.creditseparate.TabIndex = 12;
            this.creditseparate.Values.Text = "单独显示";
            // 
            // poslock
            // 
            this.poslock.Location = new System.Drawing.Point(129, 29);
            this.poslock.Name = "poslock";
            this.poslock.Size = new System.Drawing.Size(48, 20);
            this.poslock.TabIndex = 15;
            this.poslock.Values.Text = "支持";
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(35, 29);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(88, 20);
            this.kryptonLabel6.TabIndex = 14;
            this.kryptonLabel6.Values.Text = "是否支持锁仓:";
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Location = new System.Drawing.Point(176, 12);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel9);
            this.kryptonGroupBox1.Panel.Controls.Add(this.includepositionprofit);
            this.kryptonGroupBox1.Panel.Controls.Add(this.includecloseprofit);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel4);
            this.kryptonGroupBox1.Panel.Controls.Add(this.margin);
            this.kryptonGroupBox1.Panel.Controls.Add(this.sidemargin);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(317, 119);
            this.kryptonGroupBox1.TabIndex = 16;
            this.kryptonGroupBox1.Values.Heading = "保证金与可用资金";
            // 
            // includecloseprofit
            // 
            this.includecloseprofit.Location = new System.Drawing.Point(133, 29);
            this.includecloseprofit.Name = "includecloseprofit";
            this.includecloseprofit.Size = new System.Drawing.Size(73, 20);
            this.includecloseprofit.TabIndex = 12;
            this.includecloseprofit.Values.Text = "平仓盈亏";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(23, 3);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(100, 20);
            this.kryptonLabel1.TabIndex = 5;
            this.kryptonLabel1.Values.Text = "保证金计算方式:";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(60, 64);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel4.TabIndex = 7;
            this.kryptonLabel4.Values.Text = "单向大边:";
            // 
            // margin
            // 
            this.margin.DropDownWidth = 146;
            this.margin.Location = new System.Drawing.Point(130, 2);
            this.margin.Name = "margin";
            this.margin.Size = new System.Drawing.Size(146, 21);
            this.margin.TabIndex = 9;
            // 
            // sidemargin
            // 
            this.sidemargin.Location = new System.Drawing.Point(130, 64);
            this.sidemargin.Name = "sidemargin";
            this.sidemargin.Size = new System.Drawing.Size(48, 20);
            this.sidemargin.TabIndex = 11;
            this.sidemargin.Values.Text = "支持";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(437, 383);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(70, 25);
            this.btnSubmit.TabIndex = 13;
            this.btnSubmit.Values.Text = "提 交";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // includepositionprofit
            // 
            this.includepositionprofit.Location = new System.Drawing.Point(212, 29);
            this.includepositionprofit.Name = "includepositionprofit";
            this.includepositionprofit.Size = new System.Drawing.Size(73, 20);
            this.includepositionprofit.TabIndex = 14;
            this.includepositionprofit.Values.Text = "浮动盈亏";
            // 
            // kryptonLabel9
            // 
            this.kryptonLabel9.Location = new System.Drawing.Point(35, 29);
            this.kryptonLabel9.Name = "kryptonLabel9";
            this.kryptonLabel9.Size = new System.Drawing.Size(88, 20);
            this.kryptonLabel9.TabIndex = 15;
            this.kryptonLabel9.Values.Text = "可用资金包含:";
            // 
            // limitcheck
            // 
            this.limitcheck.Location = new System.Drawing.Point(111, 59);
            this.limitcheck.Name = "limitcheck";
            this.limitcheck.Size = new System.Drawing.Size(48, 20);
            this.limitcheck.TabIndex = 16;
            this.limitcheck.Values.Text = "支持";
            // 
            // probability
            // 
            this.probability.Location = new System.Drawing.Point(114, 85);
            this.probability.Name = "probability";
            this.probability.Size = new System.Drawing.Size(45, 22);
            this.probability.TabIndex = 18;
            this.probability.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(70, 87);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel3.TabIndex = 17;
            this.kryptonLabel3.Values.Text = "权重:";
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(45, 61);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel8.TabIndex = 19;
            this.kryptonLabel8.Values.Text = "限价判断:";
            // 
            // fmExStrategy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 420);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmExStrategy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "交易参数模板设置";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3.Panel)).EndInit();
            this.kryptonGroupBox3.Panel.ResumeLayout(false);
            this.kryptonGroupBox3.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3)).EndInit();
            this.kryptonGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).EndInit();
            this.kryptonGroupBox2.Panel.ResumeLayout(false);
            this.kryptonGroupBox2.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).EndInit();
            this.kryptonGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.margin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox creditseparate;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox sidemargin;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox margin;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSubmit;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox poslock;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonTreeView templateTree;
        private System.Windows.Forms.ImageList imageList1;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox includecloseprofit;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox3;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown exitSlip;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown entrySlip;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel9;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox includepositionprofit;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown probability;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox limitcheck;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
    }
}