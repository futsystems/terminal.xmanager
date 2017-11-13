namespace TradingLib.MoniterControl
{
    partial class fmHistSecurityStatistic
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
            this.direct = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.btn_export = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.inputAccount = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lbaccount = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnQry = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.end = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.start = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.settlementGrid = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.settlementGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.direct);
            this.kryptonPanel1.Controls.Add(this.btn_export);
            this.kryptonPanel1.Controls.Add(this.inputAccount);
            this.kryptonPanel1.Controls.Add(this.lbaccount);
            this.kryptonPanel1.Controls.Add(this.btnQry);
            this.kryptonPanel1.Controls.Add(this.end);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.start);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.settlementGrid);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(884, 326);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // direct
            // 
            this.direct.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.direct.Checked = true;
            this.direct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.direct.Location = new System.Drawing.Point(287, 300);
            this.direct.Name = "direct";
            this.direct.Size = new System.Drawing.Size(73, 20);
            this.direct.TabIndex = 31;
            this.direct.Values.Text = "直接客户";
            // 
            // btn_export
            // 
            this.btn_export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_export.Location = new System.Drawing.Point(810, 295);
            this.btn_export.Name = "btn_export";
            this.btn_export.Size = new System.Drawing.Size(70, 25);
            this.btn_export.TabIndex = 16;
            this.btn_export.Values.Text = "导 出";
            // 
            // inputAccount
            // 
            this.inputAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.inputAccount.Location = new System.Drawing.Point(160, 300);
            this.inputAccount.Name = "inputAccount";
            this.inputAccount.Size = new System.Drawing.Size(106, 20);
            this.inputAccount.TabIndex = 15;
            // 
            // lbaccount
            // 
            this.lbaccount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbaccount.Location = new System.Drawing.Point(103, 301);
            this.lbaccount.Name = "lbaccount";
            this.lbaccount.Size = new System.Drawing.Size(51, 20);
            this.lbaccount.TabIndex = 14;
            this.lbaccount.Values.Text = "管理员:";
            // 
            // btnQry
            // 
            this.btnQry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQry.Location = new System.Drawing.Point(734, 295);
            this.btnQry.Name = "btnQry";
            this.btnQry.Size = new System.Drawing.Size(70, 25);
            this.btnQry.TabIndex = 13;
            this.btnQry.Values.Text = "查 询";
            // 
            // end
            // 
            this.end.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.end.Location = new System.Drawing.Point(593, 297);
            this.end.Name = "end";
            this.end.Size = new System.Drawing.Size(128, 21);
            this.end.TabIndex = 12;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonLabel2.Location = new System.Drawing.Point(549, 298);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel2.TabIndex = 11;
            this.kryptonLabel2.Values.Text = "结束:";
            // 
            // start
            // 
            this.start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.start.Location = new System.Drawing.Point(411, 297);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(128, 21);
            this.start.TabIndex = 10;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonLabel1.Location = new System.Drawing.Point(366, 298);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel1.TabIndex = 9;
            this.kryptonLabel1.Values.Text = "开始:";
            // 
            // settlementGrid
            // 
            this.settlementGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.settlementGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.settlementGrid.Location = new System.Drawing.Point(0, 0);
            this.settlementGrid.Name = "settlementGrid";
            this.settlementGrid.RowTemplate.Height = 23;
            this.settlementGrid.Size = new System.Drawing.Size(884, 283);
            this.settlementGrid.TabIndex = 0;
            // 
            // fmHistSecurityStatistic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 326);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmHistSecurityStatistic";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.settlementGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView settlementGrid;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox inputAccount;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lbaccount;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnQry;
        private ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker end;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker start;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_export;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox direct;
    }
}