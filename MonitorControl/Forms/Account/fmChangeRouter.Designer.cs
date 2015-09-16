namespace TradingLib.MoniterControl
{
    partial class fmChangeRouter
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
            this.btnSubmit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.cutrgname = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.ctRouterGroupList1 = new TradingLib.MoniterControl.ctRouterGroupList();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btnSubmit);
            this.kryptonPanel1.Controls.Add(this.cutrgname);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.ctRouterGroupList1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(265, 123);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(180, 86);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(70, 25);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Values.Text = "提 交";
            // 
            // cutrgname
            // 
            this.cutrgname.Location = new System.Drawing.Point(121, 12);
            this.cutrgname.Name = "cutrgname";
            this.cutrgname.Size = new System.Drawing.Size(20, 20);
            this.cutrgname.TabIndex = 2;
            this.cutrgname.Values.Text = "--";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(27, 13);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(88, 20);
            this.kryptonLabel1.TabIndex = 1;
            this.kryptonLabel1.Values.Text = "当前主帐户组:";
            // 
            // ctRouterGroupList1
            // 
            this.ctRouterGroupList1.EnableAny = false;
            this.ctRouterGroupList1.Location = new System.Drawing.Point(48, 39);
            this.ctRouterGroupList1.Name = "ctRouterGroupList1";
            this.ctRouterGroupList1.RouterGroupID = 0;
            this.ctRouterGroupList1.Size = new System.Drawing.Size(190, 21);
            this.ctRouterGroupList1.TabIndex = 0;
            // 
            // fmChangeRouter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 123);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmChangeRouter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改主帐户组";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ctRouterGroupList ctRouterGroupList1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel cutrgname;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSubmit;
    }
}