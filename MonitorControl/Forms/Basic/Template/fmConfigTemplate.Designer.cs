namespace TradingLib.MoniterControl
{
    partial class fmConfigTemplate
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
            this.currentTitle = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonGroupBox1 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.kryptonLabel15 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbExStrategyTemplate = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnSubmit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonLabel13 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbMarginTemplate = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.cbCommissionTemplate = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.btnAddTemplate = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.templateTree = new ComponentFactory.Krypton.Toolkit.KryptonTreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.kryptonGroupBox2 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.btnRiskRule = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbExStrategyTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbMarginTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbCommissionTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).BeginInit();
            this.kryptonGroupBox2.Panel.SuspendLayout();
            this.kryptonGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox2);
            this.kryptonPanel1.Controls.Add(this.currentTitle);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox1);
            this.kryptonPanel1.Controls.Add(this.btnAddTemplate);
            this.kryptonPanel1.Controls.Add(this.templateTree);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(554, 581);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // currentTitle
            // 
            this.currentTitle.Location = new System.Drawing.Point(260, 13);
            this.currentTitle.Name = "currentTitle";
            this.currentTitle.Size = new System.Drawing.Size(20, 20);
            this.currentTitle.TabIndex = 29;
            this.currentTitle.Values.Text = "--";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(166, 13);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(88, 20);
            this.kryptonLabel1.TabIndex = 28;
            this.kryptonLabel1.Values.Text = "当前配置模板:";
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Location = new System.Drawing.Point(166, 50);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel15);
            this.kryptonGroupBox1.Panel.Controls.Add(this.cbExStrategyTemplate);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel4);
            this.kryptonGroupBox1.Panel.Controls.Add(this.btnSubmit);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel13);
            this.kryptonGroupBox1.Panel.Controls.Add(this.cbMarginTemplate);
            this.kryptonGroupBox1.Panel.Controls.Add(this.cbCommissionTemplate);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(376, 170);
            this.kryptonGroupBox1.TabIndex = 27;
            this.kryptonGroupBox1.Values.Heading = "手续费/保证金";
            // 
            // kryptonLabel15
            // 
            this.kryptonLabel15.Location = new System.Drawing.Point(15, 68);
            this.kryptonLabel15.Name = "kryptonLabel15";
            this.kryptonLabel15.Size = new System.Drawing.Size(88, 20);
            this.kryptonLabel15.TabIndex = 25;
            this.kryptonLabel15.Values.Text = "交易参数模板:";
            // 
            // cbExStrategyTemplate
            // 
            this.cbExStrategyTemplate.DropDownWidth = 145;
            this.cbExStrategyTemplate.Location = new System.Drawing.Point(115, 67);
            this.cbExStrategyTemplate.Name = "cbExStrategyTemplate";
            this.cbExStrategyTemplate.Size = new System.Drawing.Size(145, 21);
            this.cbExStrategyTemplate.TabIndex = 26;
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(30, 6);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(75, 20);
            this.kryptonLabel4.TabIndex = 21;
            this.kryptonLabel4.Values.Text = "手续费模板:";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(290, 107);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(70, 25);
            this.btnSubmit.TabIndex = 13;
            this.btnSubmit.Values.Text = "提 交";
            // 
            // kryptonLabel13
            // 
            this.kryptonLabel13.Location = new System.Drawing.Point(30, 35);
            this.kryptonLabel13.Name = "kryptonLabel13";
            this.kryptonLabel13.Size = new System.Drawing.Size(75, 20);
            this.kryptonLabel13.TabIndex = 22;
            this.kryptonLabel13.Values.Text = "保证金模板:";
            // 
            // cbMarginTemplate
            // 
            this.cbMarginTemplate.DropDownWidth = 145;
            this.cbMarginTemplate.Location = new System.Drawing.Point(115, 35);
            this.cbMarginTemplate.Name = "cbMarginTemplate";
            this.cbMarginTemplate.Size = new System.Drawing.Size(145, 21);
            this.cbMarginTemplate.TabIndex = 24;
            // 
            // cbCommissionTemplate
            // 
            this.cbCommissionTemplate.DropDownWidth = 145;
            this.cbCommissionTemplate.Location = new System.Drawing.Point(116, 5);
            this.cbCommissionTemplate.Name = "cbCommissionTemplate";
            this.cbCommissionTemplate.Size = new System.Drawing.Size(145, 21);
            this.cbCommissionTemplate.TabIndex = 23;
            // 
            // btnAddTemplate
            // 
            this.btnAddTemplate.Location = new System.Drawing.Point(70, 553);
            this.btnAddTemplate.Name = "btnAddTemplate";
            this.btnAddTemplate.Size = new System.Drawing.Size(90, 25);
            this.btnAddTemplate.TabIndex = 20;
            this.btnAddTemplate.Values.Text = "添加模板";
            // 
            // templateTree
            // 
            this.templateTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.templateTree.Location = new System.Drawing.Point(0, 0);
            this.templateTree.Name = "templateTree";
            this.templateTree.Size = new System.Drawing.Size(160, 547);
            this.templateTree.TabIndex = 18;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // kryptonGroupBox2
            // 
            this.kryptonGroupBox2.Location = new System.Drawing.Point(167, 238);
            this.kryptonGroupBox2.Name = "kryptonGroupBox2";
            // 
            // kryptonGroupBox2.Panel
            // 
            this.kryptonGroupBox2.Panel.Controls.Add(this.btnRiskRule);
            this.kryptonGroupBox2.Size = new System.Drawing.Size(375, 100);
            this.kryptonGroupBox2.TabIndex = 30;
            this.kryptonGroupBox2.Values.Heading = "风控规则";
            // 
            // btnRiskRule
            // 
            this.btnRiskRule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRiskRule.Location = new System.Drawing.Point(270, 21);
            this.btnRiskRule.Name = "btnRiskRule";
            this.btnRiskRule.Size = new System.Drawing.Size(89, 35);
            this.btnRiskRule.TabIndex = 27;
            this.btnRiskRule.Values.Text = "编辑风控规则";
            // 
            // fmConfigTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 581);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmConfigTemplate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配置模板设置";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbExStrategyTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbMarginTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbCommissionTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).EndInit();
            this.kryptonGroupBox2.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).EndInit();
            this.kryptonGroupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSubmit;
        private ComponentFactory.Krypton.Toolkit.KryptonTreeView templateTree;
        private System.Windows.Forms.ImageList imageList1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnAddTemplate;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel15;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbExStrategyTemplate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel13;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbMarginTemplate;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbCommissionTemplate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel currentTitle;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnRiskRule;
    }
}