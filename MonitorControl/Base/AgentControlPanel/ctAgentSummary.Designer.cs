﻿namespace TradingLib.MoniterControl.Base
{
    partial class ctAgentSummary
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
            this.agentBtnGroup = new ComponentFactory.Krypton.Toolkit.KryptonDropButton();
            this.agentMenu = new ComponentFactory.Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuHeading1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading();
            this.kryptonContextMenuItems1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuItem1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem2 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem9 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuSeparator1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuSeparator();
            this.kryptonContextMenuItem13 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuSeparator3 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuSeparator();
            this.kryptonContextMenuItem6 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItems4 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuItem7 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem8 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuSeparator2 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuSeparator();
            this.kryptonContextMenuItem10 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItems5 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuItem12 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem5 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItems2 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuItems3 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuItem3 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem4 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem11 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.cbCurrency = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.ctSelfOperateAgentSummary = new TradingLib.MoniterControl.ctSelfOperateAgentSummary();
            this.ctCustSummary = new TradingLib.MoniterControl.ctCustSummary();
            this.ctNormalAgentSummary = new TradingLib.MoniterControl.ctNormalAgentSummary();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbCurrency)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.cbCurrency);
            this.kryptonPanel1.Controls.Add(this.ctSelfOperateAgentSummary);
            this.kryptonPanel1.Controls.Add(this.ctCustSummary);
            this.kryptonPanel1.Controls.Add(this.ctNormalAgentSummary);
            this.kryptonPanel1.Controls.Add(this.agentBtnGroup);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(1228, 60);
            this.kryptonPanel1.StateCommon.Color1 = System.Drawing.Color.DimGray;
            this.kryptonPanel1.TabIndex = 0;
            // 
            // agentBtnGroup
            // 
            this.agentBtnGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.agentBtnGroup.DropDownPosition = ComponentFactory.Krypton.Toolkit.VisualOrientation.Bottom;
            this.agentBtnGroup.KryptonContextMenu = this.agentMenu;
            this.agentBtnGroup.Location = new System.Drawing.Point(4, 5);
            this.agentBtnGroup.Name = "agentBtnGroup";
            this.agentBtnGroup.Size = new System.Drawing.Size(81, 50);
            this.agentBtnGroup.StateCommon.Content.ShortText.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.agentBtnGroup.TabIndex = 14;
            this.agentBtnGroup.Values.Text = "代理管理";
            // 
            // agentMenu
            // 
            this.agentMenu.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuHeading1,
            this.kryptonContextMenuItems1});
            // 
            // kryptonContextMenuHeading1
            // 
            this.kryptonContextMenuHeading1.ExtraText = "";
            this.kryptonContextMenuHeading1.Text = "--";
            // 
            // kryptonContextMenuItems1
            // 
            this.kryptonContextMenuItems1.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItem1,
            this.kryptonContextMenuItem2,
            this.kryptonContextMenuItem9,
            this.kryptonContextMenuSeparator1,
            this.kryptonContextMenuItem13,
            this.kryptonContextMenuSeparator3,
            this.kryptonContextMenuItem6,
            this.kryptonContextMenuSeparator2,
            this.kryptonContextMenuItem10});
            // 
            // kryptonContextMenuItem1
            // 
            this.kryptonContextMenuItem1.Text = "出入金/财务信息";
            // 
            // kryptonContextMenuItem2
            // 
            this.kryptonContextMenuItem2.Text = "手续费/保证金";
            // 
            // kryptonContextMenuItem9
            // 
            this.kryptonContextMenuItem9.Text = "强平设置";
            // 
            // kryptonContextMenuItem13
            // 
            this.kryptonContextMenuItem13.Text = "设定默认配置模板";
            // 
            // kryptonContextMenuItem6
            // 
            this.kryptonContextMenuItem6.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItems4});
            this.kryptonContextMenuItem6.Text = "记录查询";
            // 
            // kryptonContextMenuItems4
            // 
            this.kryptonContextMenuItems4.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItem7,
            this.kryptonContextMenuItem8});
            // 
            // kryptonContextMenuItem7
            // 
            this.kryptonContextMenuItem7.Text = "出入金";
            // 
            // kryptonContextMenuItem8
            // 
            this.kryptonContextMenuItem8.Text = "结算记录";
            // 
            // kryptonContextMenuItem10
            // 
            this.kryptonContextMenuItem10.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItems5});
            this.kryptonContextMenuItem10.Text = "统计";
            // 
            // kryptonContextMenuItems5
            // 
            this.kryptonContextMenuItems5.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItem12});
            // 
            // kryptonContextMenuItem12
            // 
            this.kryptonContextMenuItem12.Text = "客户结算统计";
            // 
            // kryptonContextMenuItem5
            // 
            this.kryptonContextMenuItem5.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItems2});
            this.kryptonContextMenuItem5.Text = "Menu Item";
            // 
            // kryptonContextMenuItem3
            // 
            this.kryptonContextMenuItem3.Text = "Menu Item";
            // 
            // kryptonContextMenuItem4
            // 
            this.kryptonContextMenuItem4.Text = "Menu Item";
            // 
            // kryptonContextMenuItem11
            // 
            this.kryptonContextMenuItem11.Text = "Menu Item";
            // 
            // cbCurrency
            // 
            this.cbCurrency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCurrency.DropDownWidth = 69;
            this.cbCurrency.Location = new System.Drawing.Point(1156, 34);
            this.cbCurrency.Name = "cbCurrency";
            this.cbCurrency.Size = new System.Drawing.Size(69, 21);
            this.cbCurrency.TabIndex = 18;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonLabel1.Location = new System.Drawing.Point(1156, 3);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(69, 23);
            this.kryptonLabel1.StateCommon.ShortText.Color1 = System.Drawing.Color.Gainsboro;
            this.kryptonLabel1.StateCommon.ShortText.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.kryptonLabel1.TabIndex = 19;
            this.kryptonLabel1.Values.Text = "折算货币";
            // 
            // ctSelfOperateAgentSummary
            // 
            this.ctSelfOperateAgentSummary.BackColor = System.Drawing.SystemColors.Control;
            this.ctSelfOperateAgentSummary.IsRootView = false;
            this.ctSelfOperateAgentSummary.Location = new System.Drawing.Point(301, 0);
            this.ctSelfOperateAgentSummary.Name = "ctSelfOperateAgentSummary";
            this.ctSelfOperateAgentSummary.Ratio = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ctSelfOperateAgentSummary.Size = new System.Drawing.Size(173, 60);
            this.ctSelfOperateAgentSummary.TabIndex = 17;
            // 
            // ctCustSummary
            // 
            this.ctCustSummary.BackColor = System.Drawing.Color.DimGray;
            this.ctCustSummary.Location = new System.Drawing.Point(550, 0);
            this.ctCustSummary.Name = "ctCustSummary";
            this.ctCustSummary.Ratio = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ctCustSummary.Size = new System.Drawing.Size(606, 60);
            this.ctCustSummary.TabIndex = 16;
            // 
            // ctNormalAgentSummary
            // 
            this.ctNormalAgentSummary.BackColor = System.Drawing.Color.DimGray;
            this.ctNormalAgentSummary.Location = new System.Drawing.Point(91, 0);
            this.ctNormalAgentSummary.Name = "ctNormalAgentSummary";
            this.ctNormalAgentSummary.Ratio = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ctNormalAgentSummary.Size = new System.Drawing.Size(149, 60);
            this.ctNormalAgentSummary.TabIndex = 15;
            // 
            // ctAgentSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "ctAgentSummary";
            this.Size = new System.Drawing.Size(1228, 60);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbCurrency)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonDropButton agentBtnGroup;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenu agentMenu;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem2;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuSeparator kryptonContextMenuSeparator1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem6;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems4;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem7;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem8;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem5;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems2;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems3;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem3;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem4;
        private ctNormalAgentSummary ctNormalAgentSummary;
        private ctCustSummary ctCustSummary;
        private ctSelfOperateAgentSummary ctSelfOperateAgentSummary;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading kryptonContextMenuHeading1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem9;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuSeparator kryptonContextMenuSeparator2;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem10;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems5;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem12;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem11;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem13;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuSeparator kryptonContextMenuSeparator3;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbCurrency;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
    }
}
