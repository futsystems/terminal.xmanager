namespace TradingLib.MoniterControl
{
    partial class ctFunctionPanel
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
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonNavigator1 = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            this.kryptonPage1 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.ctAccountInfo1 = new TradingLib.MoniterControl.ctAccountInfo();
            this.pageExecution = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonPanel2 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.ctOrderSenderM1 = new TradingLib.MoniterControl.ctOrderSenderM();
            this.ctQuoteMoniterS1 = new TradingLib.MoniterControl.ctQuoteMoniterS();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).BeginInit();
            this.kryptonNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).BeginInit();
            this.kryptonPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pageExecution)).BeginInit();
            this.pageExecution.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonNavigator1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(589, 362);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonNavigator1
            // 
            this.kryptonNavigator1.Bar.TabStyle = ComponentFactory.Krypton.Toolkit.TabStyle.LowProfile;
            this.kryptonNavigator1.Button.ButtonDisplayLogic = ComponentFactory.Krypton.Navigator.ButtonDisplayLogic.None;
            this.kryptonNavigator1.Button.CloseButtonAction = ComponentFactory.Krypton.Navigator.CloseButtonAction.None;
            this.kryptonNavigator1.Button.CloseButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonNavigator1.Button.NextButtonAction = ComponentFactory.Krypton.Navigator.DirectionButtonAction.None;
            this.kryptonNavigator1.Button.NextButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonNavigator1.Button.PreviousButtonAction = ComponentFactory.Krypton.Navigator.DirectionButtonAction.None;
            this.kryptonNavigator1.Button.PreviousButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonNavigator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonNavigator1.Location = new System.Drawing.Point(0, 0);
            this.kryptonNavigator1.Name = "kryptonNavigator1";
            this.kryptonNavigator1.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.kryptonPage1,
            this.pageExecution});
            this.kryptonNavigator1.SelectedIndex = 1;
            this.kryptonNavigator1.Size = new System.Drawing.Size(589, 362);
            this.kryptonNavigator1.TabIndex = 0;
            this.kryptonNavigator1.Text = "kryptonNavigator1";
            // 
            // kryptonPage1
            // 
            this.kryptonPage1.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage1.Controls.Add(this.ctAccountInfo1);
            this.kryptonPage1.Flags = 65534;
            this.kryptonPage1.LastVisibleSet = true;
            this.kryptonPage1.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage1.Name = "kryptonPage1";
            this.kryptonPage1.Size = new System.Drawing.Size(433, 265);
            this.kryptonPage1.Text = "帐户信息";
            this.kryptonPage1.ToolTipTitle = "Page ToolTip";
            this.kryptonPage1.UniqueName = "4F5C53DB656B4BFD9ABE0F998C4FAAEB";
            // 
            // ctAccountInfo1
            // 
            this.ctAccountInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctAccountInfo1.Location = new System.Drawing.Point(0, 0);
            this.ctAccountInfo1.Name = "ctAccountInfo1";
            this.ctAccountInfo1.Size = new System.Drawing.Size(433, 265);
            this.ctAccountInfo1.TabIndex = 0;
            // 
            // pageExecution
            // 
            this.pageExecution.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.pageExecution.Controls.Add(this.kryptonPanel2);
            this.pageExecution.Flags = 65534;
            this.pageExecution.LastVisibleSet = true;
            this.pageExecution.MinimumSize = new System.Drawing.Size(50, 50);
            this.pageExecution.Name = "pageExecution";
            this.pageExecution.Size = new System.Drawing.Size(587, 335);
            this.pageExecution.Text = "交易面板";
            this.pageExecution.ToolTipTitle = "Page ToolTip";
            this.pageExecution.UniqueName = "F95FF00A1AA74CFDEF923FD31568BCB5";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.ctQuoteMoniterS1);
            this.kryptonPanel2.Controls.Add(this.ctOrderSenderM1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(587, 335);
            this.kryptonPanel2.TabIndex = 0;
            // 
            // ctOrderSenderM1
            // 
            this.ctOrderSenderM1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctOrderSenderM1.Location = new System.Drawing.Point(0, 271);
            this.ctOrderSenderM1.Name = "ctOrderSenderM1";
            this.ctOrderSenderM1.Size = new System.Drawing.Size(588, 65);
            this.ctOrderSenderM1.TabIndex = 0;
            // 
            // ctQuoteMoniterS1
            // 
            this.ctQuoteMoniterS1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctQuoteMoniterS1.Location = new System.Drawing.Point(0, 0);
            this.ctQuoteMoniterS1.Name = "ctQuoteMoniterS1";
            this.ctQuoteMoniterS1.Size = new System.Drawing.Size(588, 265);
            this.ctQuoteMoniterS1.TabIndex = 1;
            // 
            // ctFunctionPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "ctFunctionPanel";
            this.Size = new System.Drawing.Size(589, 362);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).EndInit();
            this.kryptonNavigator1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).EndInit();
            this.kryptonPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pageExecution)).EndInit();
            this.pageExecution.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Navigator.KryptonNavigator kryptonNavigator1;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage1;
        private ComponentFactory.Krypton.Navigator.KryptonPage pageExecution;
        private ctAccountInfo ctAccountInfo1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private ctQuoteMoniterS ctQuoteMoniterS1;
        private ctOrderSenderM ctOrderSenderM1;
    }
}
