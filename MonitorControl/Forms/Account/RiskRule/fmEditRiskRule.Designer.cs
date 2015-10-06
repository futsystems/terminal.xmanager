namespace TradingLib.MoniterControl
{
    partial class fmEditRiskRule
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
            this.kryptonNavigator1 = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            this.kryptonPage1 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonPanel2 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.orderRuleClassList = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.orderRuleItemList = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.kryptonPage2 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonPanel3 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel12 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.accountRuleClassList = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.accountRuleItemList = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.btnAddAccountRule = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnDelAccountRule = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnDelOrderRule = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnAddOrderRule = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).BeginInit();
            this.kryptonNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).BeginInit();
            this.kryptonPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage2)).BeginInit();
            this.kryptonPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).BeginInit();
            this.kryptonPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonNavigator1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(407, 391);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonNavigator1
            // 
            this.kryptonNavigator1.Bar.TabStyle = ComponentFactory.Krypton.Toolkit.TabStyle.LowProfile;
            this.kryptonNavigator1.Button.CloseButtonAction = ComponentFactory.Krypton.Navigator.CloseButtonAction.None;
            this.kryptonNavigator1.Button.CloseButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonNavigator1.Button.ContextButtonAction = ComponentFactory.Krypton.Navigator.ContextButtonAction.None;
            this.kryptonNavigator1.Button.ContextButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonNavigator1.Button.NextButtonAction = ComponentFactory.Krypton.Navigator.DirectionButtonAction.None;
            this.kryptonNavigator1.Button.NextButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonNavigator1.Button.PreviousButtonAction = ComponentFactory.Krypton.Navigator.DirectionButtonAction.None;
            this.kryptonNavigator1.Button.PreviousButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonNavigator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonNavigator1.Location = new System.Drawing.Point(0, 0);
            this.kryptonNavigator1.Name = "kryptonNavigator1";
            this.kryptonNavigator1.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.kryptonPage1,
            this.kryptonPage2});
            this.kryptonNavigator1.SelectedIndex = 1;
            this.kryptonNavigator1.Size = new System.Drawing.Size(407, 391);
            this.kryptonNavigator1.TabIndex = 0;
            this.kryptonNavigator1.Text = "kryptonNavigator1";
            // 
            // kryptonPage1
            // 
            this.kryptonPage1.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage1.Controls.Add(this.kryptonPanel2);
            this.kryptonPage1.Flags = 65534;
            this.kryptonPage1.LastVisibleSet = true;
            this.kryptonPage1.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage1.Name = "kryptonPage1";
            this.kryptonPage1.Size = new System.Drawing.Size(405, 364);
            this.kryptonPage1.Text = "交易规则";
            this.kryptonPage1.ToolTipTitle = "Page ToolTip";
            this.kryptonPage1.UniqueName = "5E167244D4D64B87BA8633ED5EBD2643";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.btnDelOrderRule);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel2.Controls.Add(this.btnAddOrderRule);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel2.Controls.Add(this.orderRuleClassList);
            this.kryptonPanel2.Controls.Add(this.orderRuleItemList);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(405, 364);
            this.kryptonPanel2.TabIndex = 0;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(5, 183);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel3.TabIndex = 9;
            this.kryptonLabel3.Values.Text = "当前规则:";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(3, 3);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(88, 20);
            this.kryptonLabel2.TabIndex = 10;
            this.kryptonLabel2.Values.Text = "交易规则列表:";
            // 
            // orderRuleClassList
            // 
            this.orderRuleClassList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.orderRuleClassList.Location = new System.Drawing.Point(5, 27);
            this.orderRuleClassList.Name = "orderRuleClassList";
            this.orderRuleClassList.Size = new System.Drawing.Size(392, 120);
            this.orderRuleClassList.TabIndex = 7;
            // 
            // orderRuleItemList
            // 
            this.orderRuleItemList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.orderRuleItemList.Location = new System.Drawing.Point(5, 207);
            this.orderRuleItemList.Name = "orderRuleItemList";
            this.orderRuleItemList.Size = new System.Drawing.Size(392, 120);
            this.orderRuleItemList.TabIndex = 6;
            // 
            // kryptonPage2
            // 
            this.kryptonPage2.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage2.Controls.Add(this.kryptonPanel3);
            this.kryptonPage2.Flags = 65534;
            this.kryptonPage2.LastVisibleSet = true;
            this.kryptonPage2.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage2.Name = "kryptonPage2";
            this.kryptonPage2.Size = new System.Drawing.Size(405, 364);
            this.kryptonPage2.Text = "强平规则";
            this.kryptonPage2.ToolTipTitle = "Page ToolTip";
            this.kryptonPage2.UniqueName = "F1CECBAB5D7F41FD198ACD1AE45AA574";
            // 
            // kryptonPanel3
            // 
            this.kryptonPanel3.Controls.Add(this.btnDelAccountRule);
            this.kryptonPanel3.Controls.Add(this.btnAddAccountRule);
            this.kryptonPanel3.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel3.Controls.Add(this.kryptonLabel12);
            this.kryptonPanel3.Controls.Add(this.accountRuleClassList);
            this.kryptonPanel3.Controls.Add(this.accountRuleItemList);
            this.kryptonPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel3.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel3.Name = "kryptonPanel3";
            this.kryptonPanel3.Size = new System.Drawing.Size(405, 364);
            this.kryptonPanel3.TabIndex = 1;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(3, 3);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(88, 20);
            this.kryptonLabel1.TabIndex = 9;
            this.kryptonLabel1.Values.Text = "强平规则列表:";
            // 
            // kryptonLabel12
            // 
            this.kryptonLabel12.Location = new System.Drawing.Point(5, 183);
            this.kryptonLabel12.Name = "kryptonLabel12";
            this.kryptonLabel12.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel12.TabIndex = 8;
            this.kryptonLabel12.Values.Text = "当前规则:";
            // 
            // accountRuleClassList
            // 
            this.accountRuleClassList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.accountRuleClassList.Location = new System.Drawing.Point(5, 27);
            this.accountRuleClassList.Name = "accountRuleClassList";
            this.accountRuleClassList.Size = new System.Drawing.Size(392, 120);
            this.accountRuleClassList.TabIndex = 7;
            // 
            // accountRuleItemList
            // 
            this.accountRuleItemList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.accountRuleItemList.Location = new System.Drawing.Point(5, 207);
            this.accountRuleItemList.Name = "accountRuleItemList";
            this.accountRuleItemList.Size = new System.Drawing.Size(392, 120);
            this.accountRuleItemList.TabIndex = 6;
            // 
            // btnAddAccountRule
            // 
            this.btnAddAccountRule.Location = new System.Drawing.Point(327, 153);
            this.btnAddAccountRule.Name = "btnAddAccountRule";
            this.btnAddAccountRule.Size = new System.Drawing.Size(70, 25);
            this.btnAddAccountRule.TabIndex = 10;
            this.btnAddAccountRule.Values.Text = "添 加";
            // 
            // btnDelAccountRule
            // 
            this.btnDelAccountRule.Location = new System.Drawing.Point(327, 332);
            this.btnDelAccountRule.Name = "btnDelAccountRule";
            this.btnDelAccountRule.Size = new System.Drawing.Size(70, 25);
            this.btnDelAccountRule.TabIndex = 11;
            this.btnDelAccountRule.Values.Text = "删 除";
            // 
            // btnDelOrderRule
            // 
            this.btnDelOrderRule.Location = new System.Drawing.Point(327, 332);
            this.btnDelOrderRule.Name = "btnDelOrderRule";
            this.btnDelOrderRule.Size = new System.Drawing.Size(70, 25);
            this.btnDelOrderRule.TabIndex = 13;
            this.btnDelOrderRule.Values.Text = "删 除";
            // 
            // btnAddOrderRule
            // 
            this.btnAddOrderRule.Location = new System.Drawing.Point(327, 153);
            this.btnAddOrderRule.Name = "btnAddOrderRule";
            this.btnAddOrderRule.Size = new System.Drawing.Size(70, 25);
            this.btnAddOrderRule.TabIndex = 12;
            this.btnAddOrderRule.Values.Text = "添 加";
            // 
            // fmEditRiskRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 391);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmEditRiskRule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "风控规则设置";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).EndInit();
            this.kryptonNavigator1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).EndInit();
            this.kryptonPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage2)).EndInit();
            this.kryptonPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).EndInit();
            this.kryptonPanel3.ResumeLayout(false);
            this.kryptonPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Navigator.KryptonNavigator kryptonNavigator1;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage1;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage2;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel3;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox orderRuleClassList;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox orderRuleItemList;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel12;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox accountRuleClassList;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox accountRuleItemList;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnAddAccountRule;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDelAccountRule;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDelOrderRule;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnAddOrderRule;
    }
}