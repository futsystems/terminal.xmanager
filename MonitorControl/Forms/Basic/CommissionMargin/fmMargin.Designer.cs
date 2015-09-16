namespace TradingLib.MoniterControl
{
    partial class fmMargin
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
            this.templateTree = new ComponentFactory.Krypton.Toolkit.KryptonTreeView();
            this.marginGrid = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnAddTemplate = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marginGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btnAddTemplate);
            this.kryptonPanel1.Controls.Add(this.templateTree);
            this.kryptonPanel1.Controls.Add(this.marginGrid);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(844, 476);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // templateTree
            // 
            this.templateTree.Location = new System.Drawing.Point(0, 0);
            this.templateTree.Name = "templateTree";
            this.templateTree.Size = new System.Drawing.Size(160, 443);
            this.templateTree.TabIndex = 6;
            // 
            // marginGrid
            // 
            this.marginGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.marginGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.marginGrid.Location = new System.Drawing.Point(160, 0);
            this.marginGrid.Name = "marginGrid";
            this.marginGrid.RowTemplate.Height = 23;
            this.marginGrid.Size = new System.Drawing.Size(684, 476);
            this.marginGrid.TabIndex = 1;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnAddTemplate
            // 
            this.btnAddTemplate.Location = new System.Drawing.Point(64, 448);
            this.btnAddTemplate.Name = "btnAddTemplate";
            this.btnAddTemplate.Size = new System.Drawing.Size(90, 25);
            this.btnAddTemplate.TabIndex = 7;
            this.btnAddTemplate.Values.Text = "添加模板";
            // 
            // fmMargin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 476);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmMargin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "保证金模板设置";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.marginGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView marginGrid;
        private ComponentFactory.Krypton.Toolkit.KryptonTreeView templateTree;
        private System.Windows.Forms.ImageList imageList1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnAddTemplate;
    }
}