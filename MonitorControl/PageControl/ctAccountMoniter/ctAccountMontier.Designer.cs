namespace TradingLib.MoniterControl
{
    partial class ctAccountMontier
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.accountgrid = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.kryptonPanel2 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.splitContainer = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.agentTree = new ComponentFactory.Krypton.Toolkit.KryptonTreeView();
            this.ctAgentSummary = new TradingLib.MoniterControl.Base.ctAgentSummary();
            this.kcmFilter = new ComponentFactory.Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuItems1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuItem1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem2 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.accountgrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer.Panel1)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer.Panel2)).BeginInit();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // accountgrid
            // 
            this.accountgrid.AllowUserToAddRows = false;
            this.accountgrid.AllowUserToDeleteRows = false;
            this.accountgrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.accountgrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.accountgrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.accountgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.accountgrid.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.accountgrid.Location = new System.Drawing.Point(0, 60);
            this.accountgrid.Name = "accountgrid";
            this.accountgrid.ReadOnly = true;
            this.accountgrid.RowHeadersVisible = false;
            this.accountgrid.RowTemplate.Height = 23;
            this.accountgrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.accountgrid.Size = new System.Drawing.Size(1255, 578);
            this.accountgrid.TabIndex = 30;
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.splitContainer);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(1380, 638);
            this.kryptonPanel2.TabIndex = 2;
            // 
            // splitContainer
            // 
            this.splitContainer.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.agentTree);
            this.splitContainer.Panel1MinSize = 0;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.ctAgentSummary);
            this.splitContainer.Panel2.Controls.Add(this.accountgrid);
            this.splitContainer.Panel2MinSize = 100;
            this.splitContainer.Size = new System.Drawing.Size(1380, 638);
            this.splitContainer.SplitterDistance = 120;
            this.splitContainer.TabIndex = 31;
            // 
            // agentTree
            // 
            this.agentTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.agentTree.Location = new System.Drawing.Point(0, 0);
            this.agentTree.Name = "agentTree";
            this.agentTree.Size = new System.Drawing.Size(120, 638);
            this.agentTree.TabIndex = 0;
            // 
            // ctAgentSummary
            // 
            this.ctAgentSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctAgentSummary.Location = new System.Drawing.Point(0, 0);
            this.ctAgentSummary.Name = "ctAgentSummary";
            this.ctAgentSummary.Size = new System.Drawing.Size(1255, 60);
            this.ctAgentSummary.TabIndex = 31;
            // 
            // kcmFilter
            // 
            this.kcmFilter.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItems1});
            // 
            // kryptonContextMenuItems1
            // 
            this.kryptonContextMenuItems1.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItem1,
            this.kryptonContextMenuItem2});
            // 
            // kryptonContextMenuItem1
            // 
            this.kryptonContextMenuItem1.Text = "Menu Item";
            // 
            // kryptonContextMenuItem2
            // 
            this.kryptonContextMenuItem2.Text = "Menu Item";
            // 
            // ctAccountMontier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel2);
            this.DoubleBuffered = true;
            this.Name = "ctAccountMontier";
            this.Size = new System.Drawing.Size(1380, 638);
            ((System.ComponentModel.ISupportInitialize)(this.accountgrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer.Panel1)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer.Panel2)).EndInit();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView accountgrid;
        private ctRouterGroupList ctRouterGroupList1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenu kcmFilter;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem2;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer splitContainer;
        private ComponentFactory.Krypton.Toolkit.KryptonTreeView agentTree;
        private Base.ctAgentSummary ctAgentSummary;

        //private Telerik.WinControls.UI.RadPageViewPage LottoServicePage;

    }
}
