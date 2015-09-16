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
            this.kryptonPage2 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.ctOrderPanel1 = new TradingLib.MoniterControl.ctOrderPanel();
            this.kryptonPage3 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.quoteList = new TradingLib.MoniterControl.ViewQuoteList();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).BeginInit();
            this.kryptonNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).BeginInit();
            this.kryptonPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage2)).BeginInit();
            this.kryptonPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage3)).BeginInit();
            this.kryptonPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonNavigator1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(435, 263);
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
            this.kryptonPage2,
            this.kryptonPage3});
            this.kryptonNavigator1.SelectedIndex = 1;
            this.kryptonNavigator1.Size = new System.Drawing.Size(435, 263);
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
            this.kryptonPage1.Size = new System.Drawing.Size(433, 236);
            this.kryptonPage1.Text = "帐户信息";
            this.kryptonPage1.ToolTipTitle = "Page ToolTip";
            this.kryptonPage1.UniqueName = "4F5C53DB656B4BFD9ABE0F998C4FAAEB";
            // 
            // ctAccountInfo1
            // 
            this.ctAccountInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctAccountInfo1.Location = new System.Drawing.Point(0, 0);
            this.ctAccountInfo1.Name = "ctAccountInfo1";
            this.ctAccountInfo1.Size = new System.Drawing.Size(433, 236);
            this.ctAccountInfo1.TabIndex = 0;
            // 
            // kryptonPage2
            // 
            this.kryptonPage2.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage2.Controls.Add(this.ctOrderPanel1);
            this.kryptonPage2.Flags = 65534;
            this.kryptonPage2.LastVisibleSet = true;
            this.kryptonPage2.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage2.Name = "kryptonPage2";
            this.kryptonPage2.Size = new System.Drawing.Size(433, 236);
            this.kryptonPage2.Text = "交易面板";
            this.kryptonPage2.ToolTipTitle = "Page ToolTip";
            this.kryptonPage2.UniqueName = "F95FF00A1AA74CFDEF923FD31568BCB5";
            // 
            // ctOrderPanel1
            // 
            this.ctOrderPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctOrderPanel1.Location = new System.Drawing.Point(0, 0);
            this.ctOrderPanel1.Name = "ctOrderPanel1";
            this.ctOrderPanel1.Size = new System.Drawing.Size(433, 236);
            this.ctOrderPanel1.TabIndex = 0;
            // 
            // kryptonPage3
            // 
            this.kryptonPage3.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage3.Controls.Add(this.quoteList);
            this.kryptonPage3.Flags = 65534;
            this.kryptonPage3.LastVisibleSet = true;
            this.kryptonPage3.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage3.Name = "kryptonPage3";
            this.kryptonPage3.Size = new System.Drawing.Size(433, 236);
            this.kryptonPage3.Text = "行情列表";
            this.kryptonPage3.ToolTipTitle = "Page ToolTip";
            this.kryptonPage3.UniqueName = "3E83B9F032594BD04C9CEAEE33D42AF6";
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
            this.quoteList.QuoteViewWidth = 1030;
            this.quoteList.SelectedQuoteRow = -1;
            this.quoteList.Size = new System.Drawing.Size(433, 236);
            this.quoteList.SymbolFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.quoteList.SymbolFontColor = System.Drawing.Color.Green;
            this.quoteList.TabIndex = 3;
            this.quoteList.TableLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.quoteList.Text = "viewQuoteList4";
            this.quoteList.UPColor = System.Drawing.Color.Red;
            // 
            // ctFunctionPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "ctFunctionPanel";
            this.Size = new System.Drawing.Size(435, 263);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).EndInit();
            this.kryptonNavigator1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).EndInit();
            this.kryptonPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage2)).EndInit();
            this.kryptonPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage3)).EndInit();
            this.kryptonPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Navigator.KryptonNavigator kryptonNavigator1;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage1;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage2;
        private ctOrderPanel ctOrderPanel1;
        private ctAccountInfo ctAccountInfo1;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage3;
        private ViewQuoteList quoteList;
    }
}
