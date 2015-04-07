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
            this.kryptonGroupBox2 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.creditseparate = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.poslock = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonGroupBox1 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.margin = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.avabilefund = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.sidemargin = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.btnSubmit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.templateTree = new ComponentFactory.Krypton.Toolkit.KryptonTreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).BeginInit();
            this.kryptonGroupBox2.Panel.SuspendLayout();
            this.kryptonGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.margin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.avabilefund)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
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
            // kryptonGroupBox2
            // 
            this.kryptonGroupBox2.Location = new System.Drawing.Point(177, 168);
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
            this.kryptonLabel5.Location = new System.Drawing.Point(10, 3);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(95, 18);
            this.kryptonLabel5.TabIndex = 8;
            this.kryptonLabel5.Values.Text = "信用额度显示:";
            // 
            // creditseparate
            // 
            this.creditseparate.Location = new System.Drawing.Point(129, 3);
            this.creditseparate.Name = "creditseparate";
            this.creditseparate.Size = new System.Drawing.Size(78, 18);
            this.creditseparate.TabIndex = 12;
            this.creditseparate.Values.Text = "单独显示";
            // 
            // poslock
            // 
            this.poslock.Location = new System.Drawing.Point(129, 29);
            this.poslock.Name = "poslock";
            this.poslock.Size = new System.Drawing.Size(51, 18);
            this.poslock.TabIndex = 15;
            this.poslock.Values.Text = "支持";
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(35, 29);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(95, 18);
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
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel3);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel4);
            this.kryptonGroupBox1.Panel.Controls.Add(this.margin);
            this.kryptonGroupBox1.Panel.Controls.Add(this.avabilefund);
            this.kryptonGroupBox1.Panel.Controls.Add(this.sidemargin);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(317, 150);
            this.kryptonGroupBox1.TabIndex = 16;
            this.kryptonGroupBox1.Values.Heading = "保证金与可用资金";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(23, 3);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(109, 18);
            this.kryptonLabel1.TabIndex = 5;
            this.kryptonLabel1.Values.Text = "保证金计算方式:";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(10, 30);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(123, 18);
            this.kryptonLabel3.TabIndex = 6;
            this.kryptonLabel3.Values.Text = "可用资金计算方式:";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(60, 57);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(68, 18);
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
            // avabilefund
            // 
            this.avabilefund.DropDownWidth = 146;
            this.avabilefund.Location = new System.Drawing.Point(130, 29);
            this.avabilefund.Name = "avabilefund";
            this.avabilefund.Size = new System.Drawing.Size(146, 21);
            this.avabilefund.TabIndex = 10;
            // 
            // sidemargin
            // 
            this.sidemargin.Location = new System.Drawing.Point(130, 57);
            this.sidemargin.Name = "sidemargin";
            this.sidemargin.Size = new System.Drawing.Size(51, 18);
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
            // templateTree
            // 
            this.templateTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.templateTree.Location = new System.Drawing.Point(0, 0);
            this.templateTree.Name = "templateTree";
            this.templateTree.Size = new System.Drawing.Size(160, 420);
            this.templateTree.TabIndex = 18;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
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
            this.kryptonGroupBox2.Panel.ResumeLayout(false);
            this.kryptonGroupBox2.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).EndInit();
            this.kryptonGroupBox2.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.margin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.avabilefund)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox creditseparate;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox sidemargin;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox avabilefund;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox margin;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSubmit;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox poslock;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonTreeView templateTree;
        private System.Windows.Forms.ImageList imageList1;
    }
}