namespace TradingLib.TinyMGRControl
{
    partial class ctTradingInfo
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
            this.kryptonNavigator1 = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            this.kryptonPage1 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.ctPositionViewSTK1 = new TradingLib.TinyMGRControl.ctPositionViewSTK();
            this.kryptonPage2 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.ctOrderViewSTK1 = new TradingLib.TinyMGRControl.ctOrderViewSTK();
            this.kryptonPage3 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.ctTradeViewSTK1 = new TradingLib.TinyMGRControl.ctTradeViewSTK();
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
            // kryptonNavigator1
            // 
            this.kryptonNavigator1.Bar.TabStyle = ComponentFactory.Krypton.Toolkit.TabStyle.LowProfile;
            this.kryptonNavigator1.Button.CloseButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonNavigator1.Button.ContextButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonNavigator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonNavigator1.Location = new System.Drawing.Point(0, 0);
            this.kryptonNavigator1.Name = "kryptonNavigator1";
            this.kryptonNavigator1.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.kryptonPage1,
            this.kryptonPage2,
            this.kryptonPage3});
            this.kryptonNavigator1.SelectedIndex = 0;
            this.kryptonNavigator1.Size = new System.Drawing.Size(645, 339);
            this.kryptonNavigator1.TabIndex = 0;
            this.kryptonNavigator1.Text = "kryptonNavigator1";
            // 
            // kryptonPage1
            // 
            this.kryptonPage1.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage1.Controls.Add(this.ctPositionViewSTK1);
            this.kryptonPage1.Flags = 65534;
            this.kryptonPage1.LastVisibleSet = true;
            this.kryptonPage1.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage1.Name = "kryptonPage1";
            this.kryptonPage1.Size = new System.Drawing.Size(643, 312);
            this.kryptonPage1.Text = "持 仓";
            this.kryptonPage1.ToolTipTitle = "Page ToolTip";
            this.kryptonPage1.UniqueName = "A9CB1A7E1B7D4F275D927E6CAF3B99FC";
            // 
            // ctPositionViewSTK1
            // 
            this.ctPositionViewSTK1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctPositionViewSTK1.Location = new System.Drawing.Point(0, 0);
            this.ctPositionViewSTK1.Name = "ctPositionViewSTK1";
            this.ctPositionViewSTK1.RealView = true;
            this.ctPositionViewSTK1.Size = new System.Drawing.Size(643, 312);
            this.ctPositionViewSTK1.TabIndex = 0;
            // 
            // kryptonPage2
            // 
            this.kryptonPage2.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage2.Controls.Add(this.ctOrderViewSTK1);
            this.kryptonPage2.Flags = 65534;
            this.kryptonPage2.LastVisibleSet = true;
            this.kryptonPage2.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage2.Name = "kryptonPage2";
            this.kryptonPage2.Size = new System.Drawing.Size(643, 312);
            this.kryptonPage2.Text = "委 托";
            this.kryptonPage2.ToolTipTitle = "Page ToolTip";
            this.kryptonPage2.UniqueName = "6FCEE9EC2C894807AAB29DFE43DE5172";
            // 
            // ctOrderViewSTK1
            // 
            this.ctOrderViewSTK1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctOrderViewSTK1.Location = new System.Drawing.Point(0, 0);
            this.ctOrderViewSTK1.Name = "ctOrderViewSTK1";
            this.ctOrderViewSTK1.RealView = true;
            this.ctOrderViewSTK1.Size = new System.Drawing.Size(643, 312);
            this.ctOrderViewSTK1.TabIndex = 0;
            // 
            // kryptonPage3
            // 
            this.kryptonPage3.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage3.Controls.Add(this.ctTradeViewSTK1);
            this.kryptonPage3.Flags = 65534;
            this.kryptonPage3.LastVisibleSet = true;
            this.kryptonPage3.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage3.Name = "kryptonPage3";
            this.kryptonPage3.Size = new System.Drawing.Size(643, 312);
            this.kryptonPage3.Text = "成 交";
            this.kryptonPage3.ToolTipTitle = "Page ToolTip";
            this.kryptonPage3.UniqueName = "85DD81BEFA41415786AEEBA9F37055B2";
            // 
            // ctTradeViewSTK1
            // 
            this.ctTradeViewSTK1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctTradeViewSTK1.Location = new System.Drawing.Point(0, 0);
            this.ctTradeViewSTK1.Name = "ctTradeViewSTK1";
            this.ctTradeViewSTK1.RealView = true;
            this.ctTradeViewSTK1.Size = new System.Drawing.Size(643, 312);
            this.ctTradeViewSTK1.TabIndex = 0;
            // 
            // ctTradingInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonNavigator1);
            this.Name = "ctTradingInfo";
            this.Size = new System.Drawing.Size(645, 339);
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

        private ComponentFactory.Krypton.Navigator.KryptonNavigator kryptonNavigator1;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage1;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage2;
        private ctPositionViewSTK ctPositionViewSTK1;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage3;
        private ctOrderViewSTK ctOrderViewSTK1;
        private ctTradeViewSTK ctTradeViewSTK1;
    }
}
