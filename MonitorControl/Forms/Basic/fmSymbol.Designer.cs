namespace TradingLib.MoniterControl
{
    partial class fmSymbol
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmSymbol));
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbsecurity = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.btnDisableAll = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnAddSymbol = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSyncSymbols = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbtradeable = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.cbexchange = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.symgrid = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.btnImportStk = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbsecurity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbtradeable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbexchange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.symgrid)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btnImportStk);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.cbsecurity);
            this.kryptonPanel1.Controls.Add(this.btnDisableAll);
            this.kryptonPanel1.Controls.Add(this.btnAddSymbol);
            this.kryptonPanel1.Controls.Add(this.btnSyncSymbols);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.cbtradeable);
            this.kryptonPanel1.Controls.Add(this.cbexchange);
            this.kryptonPanel1.Controls.Add(this.symgrid);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(844, 416);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(213, 8);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel1.TabIndex = 17;
            this.kryptonLabel1.Values.Text = "品种:";
            // 
            // cbsecurity
            // 
            this.cbsecurity.DropDownWidth = 121;
            this.cbsecurity.Location = new System.Drawing.Point(260, 5);
            this.cbsecurity.Name = "cbsecurity";
            this.cbsecurity.Size = new System.Drawing.Size(90, 21);
            this.cbsecurity.TabIndex = 16;
            // 
            // btnDisableAll
            // 
            this.btnDisableAll.Location = new System.Drawing.Point(511, 388);
            this.btnDisableAll.Name = "btnDisableAll";
            this.btnDisableAll.Size = new System.Drawing.Size(108, 25);
            this.btnDisableAll.TabIndex = 15;
            this.btnDisableAll.Values.Text = "禁止所有合约";
            // 
            // btnAddSymbol
            // 
            this.btnAddSymbol.Location = new System.Drawing.Point(748, 388);
            this.btnAddSymbol.Name = "btnAddSymbol";
            this.btnAddSymbol.Size = new System.Drawing.Size(90, 25);
            this.btnAddSymbol.TabIndex = 14;
            this.btnAddSymbol.Values.Text = "添加合约";
            // 
            // btnSyncSymbols
            // 
            this.btnSyncSymbols.Location = new System.Drawing.Point(625, 388);
            this.btnSyncSymbols.Name = "btnSyncSymbols";
            this.btnSyncSymbols.Size = new System.Drawing.Size(117, 25);
            this.btnSyncSymbols.TabIndex = 13;
            this.btnSyncSymbols.Values.Text = "初始化合约数据";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(370, 8);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(75, 20);
            this.kryptonLabel3.TabIndex = 12;
            this.kryptonLabel3.Values.Text = "是否可交易:";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(5, 8);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(51, 20);
            this.kryptonLabel2.TabIndex = 11;
            this.kryptonLabel2.Values.Text = "交易所:";
            // 
            // cbtradeable
            // 
            this.cbtradeable.DropDownWidth = 121;
            this.cbtradeable.Location = new System.Drawing.Point(458, 5);
            this.cbtradeable.Name = "cbtradeable";
            this.cbtradeable.Size = new System.Drawing.Size(96, 21);
            this.cbtradeable.TabIndex = 10;
            // 
            // cbexchange
            // 
            this.cbexchange.DropDownWidth = 121;
            this.cbexchange.Location = new System.Drawing.Point(66, 5);
            this.cbexchange.Name = "cbexchange";
            this.cbexchange.Size = new System.Drawing.Size(147, 21);
            this.cbexchange.TabIndex = 9;
            // 
            // symgrid
            // 
            this.symgrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.symgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.symgrid.Location = new System.Drawing.Point(0, 35);
            this.symgrid.Name = "symgrid";
            this.symgrid.RowTemplate.Height = 23;
            this.symgrid.Size = new System.Drawing.Size(844, 350);
            this.symgrid.TabIndex = 0;
            // 
            // btnImportStk
            // 
            this.btnImportStk.Location = new System.Drawing.Point(415, 388);
            this.btnImportStk.Name = "btnImportStk";
            this.btnImportStk.Size = new System.Drawing.Size(90, 25);
            this.btnImportStk.TabIndex = 18;
            this.btnImportStk.Values.Text = "导入股票列表";
            // 
            // fmSymbol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 416);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmSymbol";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "合约管理";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbsecurity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbtradeable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbexchange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.symgrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView symgrid;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbtradeable;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbexchange;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnAddSymbol;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSyncSymbols;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDisableAll;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbsecurity;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnImportStk;
    }
}