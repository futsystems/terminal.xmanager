﻿namespace TradingLib.MoniterControl
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
            this.ctPositionView1 = new TradingLib.MoniterControl.ctPositionView();
            this.kryptonPage2 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.ctOrderView1 = new TradingLib.MoniterControl.ctOrderView();
            this.kryptonPage3 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.ctTradeView1 = new TradingLib.MoniterControl.ctTradeView();
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
            this.kryptonNavigator1.Button.ButtonDisplayLogic = ComponentFactory.Krypton.Navigator.ButtonDisplayLogic.None;
            this.kryptonNavigator1.Button.CloseButtonAction = ComponentFactory.Krypton.Navigator.CloseButtonAction.None;
            this.kryptonNavigator1.Button.CloseButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonNavigator1.Button.CloseButtonShortcut = System.Windows.Forms.Keys.None;
            this.kryptonNavigator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonNavigator1.Location = new System.Drawing.Point(0, 0);
            this.kryptonNavigator1.Name = "kryptonNavigator1";
            this.kryptonNavigator1.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.kryptonPage1,
            this.kryptonPage2,
            this.kryptonPage3});
            this.kryptonNavigator1.SelectedIndex = 0;
            this.kryptonNavigator1.Size = new System.Drawing.Size(822, 384);
            this.kryptonNavigator1.TabIndex = 0;
            this.kryptonNavigator1.Text = "kryptonNavigator1";
            // 
            // kryptonPage1
            // 
            this.kryptonPage1.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage1.Controls.Add(this.ctPositionView1);
            this.kryptonPage1.Flags = 65534;
            this.kryptonPage1.LastVisibleSet = true;
            this.kryptonPage1.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage1.Name = "kryptonPage1";
            this.kryptonPage1.Size = new System.Drawing.Size(820, 357);
            this.kryptonPage1.Text = "持 仓";
            this.kryptonPage1.ToolTipTitle = "Page ToolTip";
            this.kryptonPage1.UniqueName = "B6362063223E49E221847AF2AE1AB0DB";
            // 
            // ctPositionView1
            // 
            this.ctPositionView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctPositionView1.EnableOperation = true;
            this.ctPositionView1.Location = new System.Drawing.Point(0, 0);
            this.ctPositionView1.Name = "ctPositionView1";
            this.ctPositionView1.OrderTracker = null;
            this.ctPositionView1.PositionTracker = null;
            this.ctPositionView1.Size = new System.Drawing.Size(820, 357);
            this.ctPositionView1.TabIndex = 0;
            // 
            // kryptonPage2
            // 
            this.kryptonPage2.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage2.Controls.Add(this.ctOrderView1);
            this.kryptonPage2.Flags = 65534;
            this.kryptonPage2.LastVisibleSet = true;
            this.kryptonPage2.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage2.Name = "kryptonPage2";
            this.kryptonPage2.Size = new System.Drawing.Size(820, 357);
            this.kryptonPage2.Text = "委 托";
            this.kryptonPage2.ToolTipTitle = "Page ToolTip";
            this.kryptonPage2.UniqueName = "EA05F9BF9E0C443A4C9443BDC9B6198C";
            // 
            // ctOrderView1
            // 
            this.ctOrderView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctOrderView1.EnableOperation = true;
            this.ctOrderView1.Location = new System.Drawing.Point(0, 0);
            this.ctOrderView1.Name = "ctOrderView1";
            this.ctOrderView1.OrderTracker = null;
            this.ctOrderView1.Size = new System.Drawing.Size(820, 357);
            this.ctOrderView1.TabIndex = 0;
            // 
            // kryptonPage3
            // 
            this.kryptonPage3.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage3.Controls.Add(this.ctTradeView1);
            this.kryptonPage3.Flags = 65534;
            this.kryptonPage3.LastVisibleSet = true;
            this.kryptonPage3.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage3.Name = "kryptonPage3";
            this.kryptonPage3.Size = new System.Drawing.Size(795, 382);
            this.kryptonPage3.Text = "成 交";
            this.kryptonPage3.ToolTipTitle = "Page ToolTip";
            this.kryptonPage3.UniqueName = "54AAB2FD540042632FB3A8F005373A06";
            // 
            // ctTradeView1
            // 
            this.ctTradeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctTradeView1.Location = new System.Drawing.Point(0, 0);
            this.ctTradeView1.Name = "ctTradeView1";
            this.ctTradeView1.Size = new System.Drawing.Size(795, 382);
            this.ctTradeView1.TabIndex = 0;
            // 
            // ctTradingInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonNavigator1);
            this.Name = "ctTradingInfo";
            this.Size = new System.Drawing.Size(822, 384);
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
        private ctPositionView ctPositionView1;
        private ctOrderView ctOrderView1;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage3;
        private ctTradeView ctTradeView1;
    }
}
