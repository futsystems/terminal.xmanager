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
            this.pageExecution = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonPage3 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.PageQuoteOrder = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonPanel2 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.ctAccountInfo1 = new TradingLib.MoniterControl.ctAccountInfo();
            this.ctOrderPanel1 = new TradingLib.MoniterControl.ctOrderPanel();
            this.quoteList = new TradingLib.MoniterControl.ViewQuoteList();
            this.ctQuoteMoniterS1 = new TradingLib.MoniterControl.ctQuoteMoniterS();
            this.ctOrderSenderM1 = new TradingLib.MoniterControl.ctOrderSenderM();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).BeginInit();
            this.kryptonNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).BeginInit();
            this.kryptonPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pageExecution)).BeginInit();
            this.pageExecution.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage3)).BeginInit();
            this.kryptonPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PageQuoteOrder)).BeginInit();
            this.PageQuoteOrder.SuspendLayout();
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
            this.kryptonPanel1.Size = new System.Drawing.Size(581, 321);
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
            this.pageExecution,
            this.kryptonPage3,
            this.PageQuoteOrder});
            this.kryptonNavigator1.SelectedIndex = 3;
            this.kryptonNavigator1.Size = new System.Drawing.Size(581, 321);
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
            // pageExecution
            // 
            this.pageExecution.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.pageExecution.Controls.Add(this.ctOrderPanel1);
            this.pageExecution.Flags = 65534;
            this.pageExecution.LastVisibleSet = true;
            this.pageExecution.MinimumSize = new System.Drawing.Size(50, 50);
            this.pageExecution.Name = "pageExecution";
            this.pageExecution.Size = new System.Drawing.Size(579, 294);
            this.pageExecution.Text = "交易面板";
            this.pageExecution.ToolTipTitle = "Page ToolTip";
            this.pageExecution.UniqueName = "F95FF00A1AA74CFDEF923FD31568BCB5";
            this.pageExecution.Visible = false;
            // 
            // kryptonPage3
            // 
            this.kryptonPage3.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage3.Controls.Add(this.quoteList);
            this.kryptonPage3.Flags = 65534;
            this.kryptonPage3.LastVisibleSet = true;
            this.kryptonPage3.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage3.Name = "kryptonPage3";
            this.kryptonPage3.Size = new System.Drawing.Size(579, 294);
            this.kryptonPage3.Text = "行情列表";
            this.kryptonPage3.ToolTipTitle = "Page ToolTip";
            this.kryptonPage3.UniqueName = "3E83B9F032594BD04C9CEAEE33D42AF6";
            this.kryptonPage3.Visible = false;
            // 
            // PageQuoteOrder
            // 
            this.PageQuoteOrder.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.PageQuoteOrder.Controls.Add(this.kryptonPanel2);
            this.PageQuoteOrder.Flags = 65534;
            this.PageQuoteOrder.LastVisibleSet = true;
            this.PageQuoteOrder.MinimumSize = new System.Drawing.Size(50, 50);
            this.PageQuoteOrder.Name = "PageQuoteOrder";
            this.PageQuoteOrder.Size = new System.Drawing.Size(579, 294);
            this.PageQuoteOrder.Text = "下单面板";
            this.PageQuoteOrder.ToolTipTitle = "Page ToolTip";
            this.PageQuoteOrder.UniqueName = "A1CBFCB68EC54A84EAA8D1DD93BC1A7B";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.ctQuoteMoniterS1);
            this.kryptonPanel2.Controls.Add(this.ctOrderSenderM1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(579, 294);
            this.kryptonPanel2.TabIndex = 0;
            // 
            // ctAccountInfo1
            // 
            this.ctAccountInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctAccountInfo1.Location = new System.Drawing.Point(0, 0);
            this.ctAccountInfo1.Name = "ctAccountInfo1";
            this.ctAccountInfo1.Size = new System.Drawing.Size(433, 265);
            this.ctAccountInfo1.TabIndex = 0;
            // 
            // ctOrderPanel1
            // 
            this.ctOrderPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctOrderPanel1.Location = new System.Drawing.Point(0, 0);
            this.ctOrderPanel1.Name = "ctOrderPanel1";
            this.ctOrderPanel1.Size = new System.Drawing.Size(579, 294);
            this.ctOrderPanel1.TabIndex = 0;
            // 
            // quoteList
            // 
            this.quoteList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.quoteList.DNColor = System.Drawing.Color.Green;
            this.quoteList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.quoteList.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.quoteList.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.quoteList.HeaderFontColor = System.Drawing.Color.Turquoise;
            this.quoteList.Location = new System.Drawing.Point(0, 0);
            this.quoteList.MenuEnable = false;
            this.quoteList.Name = "quoteList";
            this.quoteList.QuoteBackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.quoteList.QuoteBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.quoteList.QuoteFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.quoteList.QuoteType = TradingLib.MoniterControl.EnumQuoteType.CNQUOTE;
            this.quoteList.QuoteViewWidth = 1030;
            this.quoteList.SelectedColor = System.Drawing.Color.Blue;
            this.quoteList.SelectedQuoteRow = -1;
            this.quoteList.Size = new System.Drawing.Size(579, 294);
            this.quoteList.SymbolFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.quoteList.SymbolFontColor = System.Drawing.Color.Green;
            this.quoteList.TabIndex = 3;
            this.quoteList.TableLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.quoteList.Text = "viewQuoteList4";
            this.quoteList.UPColor = System.Drawing.Color.Red;
            // 
            // ctQuoteMoniterS1
            // 
            this.ctQuoteMoniterS1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctQuoteMoniterS1.Location = new System.Drawing.Point(0, 0);
            this.ctQuoteMoniterS1.Name = "ctQuoteMoniterS1";
            this.ctQuoteMoniterS1.Size = new System.Drawing.Size(580, 240);
            this.ctQuoteMoniterS1.TabIndex = 2;
            // 
            // ctOrderSenderM1
            // 
            this.ctOrderSenderM1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctOrderSenderM1.Location = new System.Drawing.Point(0, 237);
            this.ctOrderSenderM1.Name = "ctOrderSenderM1";
            this.ctOrderSenderM1.Size = new System.Drawing.Size(579, 57);
            this.ctOrderSenderM1.TabIndex = 1;
            // 
            // ctFunctionPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "ctFunctionPanel";
            this.Size = new System.Drawing.Size(581, 321);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).EndInit();
            this.kryptonNavigator1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).EndInit();
            this.kryptonPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pageExecution)).EndInit();
            this.pageExecution.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage3)).EndInit();
            this.kryptonPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PageQuoteOrder)).EndInit();
            this.PageQuoteOrder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Navigator.KryptonNavigator kryptonNavigator1;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage1;
        private ComponentFactory.Krypton.Navigator.KryptonPage pageExecution;
        private ctOrderPanel ctOrderPanel1;
        private ctAccountInfo ctAccountInfo1;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage3;
        private ViewQuoteList quoteList;
        private ComponentFactory.Krypton.Navigator.KryptonPage PageQuoteOrder;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private ctOrderSenderM ctOrderSenderM1;
        private ctQuoteMoniterS ctQuoteMoniterS1;
    }
}
