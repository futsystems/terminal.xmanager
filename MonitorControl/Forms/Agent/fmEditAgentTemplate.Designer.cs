namespace TradingLib.MoniterControl
{
    partial class fmEditAgentTemplate
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
            this.btnSubmit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.cbExStrategyTemplate = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel15 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbMarginTemplate = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.cbCommissionTemplate = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel13 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnQryCommissionItem = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnQryMarinItem = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnQryExStrategy = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbExStrategyTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbMarginTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbCommissionTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btnQryExStrategy);
            this.kryptonPanel1.Controls.Add(this.btnQryMarinItem);
            this.kryptonPanel1.Controls.Add(this.btnQryCommissionItem);
            this.kryptonPanel1.Controls.Add(this.btnSubmit);
            this.kryptonPanel1.Controls.Add(this.cbExStrategyTemplate);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel15);
            this.kryptonPanel1.Controls.Add(this.cbMarginTemplate);
            this.kryptonPanel1.Controls.Add(this.cbCommissionTemplate);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel13);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(310, 167);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(227, 129);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(69, 25);
            this.btnSubmit.TabIndex = 13;
            this.btnSubmit.Values.Text = "提 交";
            // 
            // cbExStrategyTemplate
            // 
            this.cbExStrategyTemplate.DropDownWidth = 145;
            this.cbExStrategyTemplate.Location = new System.Drawing.Point(112, 82);
            this.cbExStrategyTemplate.Name = "cbExStrategyTemplate";
            this.cbExStrategyTemplate.Size = new System.Drawing.Size(116, 21);
            this.cbExStrategyTemplate.TabIndex = 12;
            // 
            // kryptonLabel15
            // 
            this.kryptonLabel15.Location = new System.Drawing.Point(12, 83);
            this.kryptonLabel15.Name = "kryptonLabel15";
            this.kryptonLabel15.Size = new System.Drawing.Size(88, 20);
            this.kryptonLabel15.TabIndex = 11;
            this.kryptonLabel15.Values.Text = "交易参数模板:";
            // 
            // cbMarginTemplate
            // 
            this.cbMarginTemplate.DropDownWidth = 145;
            this.cbMarginTemplate.Location = new System.Drawing.Point(112, 50);
            this.cbMarginTemplate.Name = "cbMarginTemplate";
            this.cbMarginTemplate.Size = new System.Drawing.Size(116, 21);
            this.cbMarginTemplate.TabIndex = 10;
            // 
            // cbCommissionTemplate
            // 
            this.cbCommissionTemplate.DropDownWidth = 145;
            this.cbCommissionTemplate.Location = new System.Drawing.Point(113, 20);
            this.cbCommissionTemplate.Name = "cbCommissionTemplate";
            this.cbCommissionTemplate.Size = new System.Drawing.Size(115, 21);
            this.cbCommissionTemplate.TabIndex = 9;
            // 
            // kryptonLabel13
            // 
            this.kryptonLabel13.Location = new System.Drawing.Point(27, 50);
            this.kryptonLabel13.Name = "kryptonLabel13";
            this.kryptonLabel13.Size = new System.Drawing.Size(75, 20);
            this.kryptonLabel13.TabIndex = 8;
            this.kryptonLabel13.Values.Text = "保证金模板:";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(27, 21);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(75, 20);
            this.kryptonLabel4.TabIndex = 7;
            this.kryptonLabel4.Values.Text = "手续费模板:";
            // 
            // btnQryCommissionItem
            // 
            this.btnQryCommissionItem.Location = new System.Drawing.Point(235, 16);
            this.btnQryCommissionItem.Name = "btnQryCommissionItem";
            this.btnQryCommissionItem.Size = new System.Drawing.Size(61, 25);
            this.btnQryCommissionItem.TabIndex = 14;
            this.btnQryCommissionItem.Values.Text = "查 询";
            // 
            // btnQryMarinItem
            // 
            this.btnQryMarinItem.Location = new System.Drawing.Point(235, 50);
            this.btnQryMarinItem.Name = "btnQryMarinItem";
            this.btnQryMarinItem.Size = new System.Drawing.Size(61, 25);
            this.btnQryMarinItem.TabIndex = 15;
            this.btnQryMarinItem.Values.Text = "查 询";
            // 
            // btnQryExStrategy
            // 
            this.btnQryExStrategy.Location = new System.Drawing.Point(235, 83);
            this.btnQryExStrategy.Name = "btnQryExStrategy";
            this.btnQryExStrategy.Size = new System.Drawing.Size(61, 25);
            this.btnQryExStrategy.TabIndex = 16;
            this.btnQryExStrategy.Values.Text = "查 询";
            // 
            // fmEditAgentTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 167);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmEditAgentTemplate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "帐户模板设置";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbExStrategyTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbMarginTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbCommissionTemplate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSubmit;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbExStrategyTemplate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel15;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbMarginTemplate;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbCommissionTemplate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel13;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnQryCommissionItem;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnQryExStrategy;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnQryMarinItem;
    }
}