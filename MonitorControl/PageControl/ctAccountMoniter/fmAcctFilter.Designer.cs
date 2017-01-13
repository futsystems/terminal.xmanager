//namespace TradingLib.MoniterControl
//{
//    partial class fmAcctFilter
//    {
//        /// <summary>
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
//            this.accexecute = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
//            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
//            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
//            this.account = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
//            this.closeimg = new System.Windows.Forms.PictureBox();
//            this.ctAgentList1 = new TradingLib.MoniterControl.ctAgentList();
//            this.ctAccountType1 = new TradingLib.MoniterControl.ctAccountType();
//            this.ctRouterGroupList1 = new TradingLib.MoniterControl.ctRouterGroupList();
//            this.ctRouterType1 = new TradingLib.MoniterControl.ctRouterType();
//            this.macctconnected = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
//            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
//            this.macctbinded = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
//            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
//            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
//            this.kryptonPanel1.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.accexecute)).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)(this.closeimg)).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)(this.macctconnected)).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)(this.macctbinded)).BeginInit();
//            this.SuspendLayout();
//            // 
//            // kryptonPanel1
//            // 
//            this.kryptonPanel1.Controls.Add(this.macctbinded);
//            this.kryptonPanel1.Controls.Add(this.kryptonLabel4);
//            this.kryptonPanel1.Controls.Add(this.macctconnected);
//            this.kryptonPanel1.Controls.Add(this.kryptonLabel3);
//            this.kryptonPanel1.Controls.Add(this.closeimg);
//            this.kryptonPanel1.Controls.Add(this.account);
//            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
//            this.kryptonPanel1.Controls.Add(this.ctAgentList1);
//            this.kryptonPanel1.Controls.Add(this.accexecute);
//            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
//            this.kryptonPanel1.Controls.Add(this.ctAccountType1);
//            this.kryptonPanel1.Controls.Add(this.ctRouterGroupList1);
//            this.kryptonPanel1.Controls.Add(this.ctRouterType1);
//            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
//            this.kryptonPanel1.Name = "kryptonPanel1";
//            this.kryptonPanel1.Size = new System.Drawing.Size(258, 148);
//            this.kryptonPanel1.TabIndex = 0;
//            // 
//            // accexecute
//            // 
//            this.accexecute.DropDownWidth = 128;
//            this.accexecute.Location = new System.Drawing.Point(93, 45);
//            this.accexecute.Name = "accexecute";
//            this.accexecute.Size = new System.Drawing.Size(124, 21);
//            this.accexecute.TabIndex = 12;
//            // 
//            // kryptonLabel1
//            // 
//            this.kryptonLabel1.Location = new System.Drawing.Point(15, 48);
//            this.kryptonLabel1.Name = "kryptonLabel1";
//            this.kryptonLabel1.Size = new System.Drawing.Size(68, 18);
//            this.kryptonLabel1.TabIndex = 11;
//            this.kryptonLabel1.Values.Text = "交易权限:";
//            // 
//            // kryptonLabel2
//            // 
//            this.kryptonLabel2.Location = new System.Drawing.Point(15, 23);
//            this.kryptonLabel2.Name = "kryptonLabel2";
//            this.kryptonLabel2.Size = new System.Drawing.Size(68, 18);
//            this.kryptonLabel2.TabIndex = 14;
//            this.kryptonLabel2.Values.Text = "交易帐号:";
//            // 
//            // account
//            // 
//            this.account.Location = new System.Drawing.Point(93, 20);
//            this.account.Name = "account";
//            this.account.Size = new System.Drawing.Size(124, 21);
//            this.account.TabIndex = 15;
//            // 
//            // closeimg
//            // 
//            this.closeimg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
//            this.closeimg.BackColor = System.Drawing.Color.Transparent;
//            this.closeimg.Image = global::TradingLib.MoniterControl.Properties.Resources.close_16;
//            this.closeimg.InitialImage = global::TradingLib.MoniterControl.Properties.Resources.close_16;
//            this.closeimg.Location = new System.Drawing.Point(239, 3);
//            this.closeimg.Name = "closeimg";
//            this.closeimg.Size = new System.Drawing.Size(16, 16);
//            this.closeimg.TabIndex = 16;
//            this.closeimg.TabStop = false;
//            // 
//            // ctAgentList1
//            // 
//            this.ctAgentList1.CurrentAgentFK = 0;
//            this.ctAgentList1.EnableAny = true;
//            this.ctAgentList1.EnableDefaultBaseMGR = true;
//            this.ctAgentList1.EnableSelected = true;
//            this.ctAgentList1.EnableSelf = true;
//            this.ctAgentList1.Location = new System.Drawing.Point(27, 122);
//            this.ctAgentList1.Name = "ctAgentList1";
//            this.ctAgentList1.Size = new System.Drawing.Size(190, 21);
//            this.ctAgentList1.TabIndex = 13;
//            this.ctAgentList1.Visible = false;
//            // 
//            // ctAccountType1
//            // 
//            this.ctAccountType1.AccountType = TradingLib.API.QSEnumAccountCategory.SUBACCOUNT;
//            this.ctAccountType1.EnableAny = true;
//            this.ctAccountType1.Location = new System.Drawing.Point(15, 122);
//            this.ctAccountType1.Name = "ctAccountType1";
//            this.ctAccountType1.Size = new System.Drawing.Size(202, 21);
//            this.ctAccountType1.TabIndex = 9;
//            this.ctAccountType1.Visible = false;
//            // 
//            // ctRouterGroupList1
//            // 
//            this.ctRouterGroupList1.EnableAny = true;
//            this.ctRouterGroupList1.Location = new System.Drawing.Point(27, 122);
//            this.ctRouterGroupList1.Name = "ctRouterGroupList1";
//            this.ctRouterGroupList1.RouterGroupID = 0;
//            this.ctRouterGroupList1.Size = new System.Drawing.Size(190, 21);
//            this.ctRouterGroupList1.TabIndex = 8;
//            this.ctRouterGroupList1.Visible = false;
//            // 
//            // ctRouterType1
//            // 
//            this.ctRouterType1.EnableAny = true;
//            this.ctRouterType1.Location = new System.Drawing.Point(15, 122);
//            this.ctRouterType1.Name = "ctRouterType1";
//            this.ctRouterType1.RouterType = TradingLib.API.QSEnumOrderTransferType.LIVE;
//            this.ctRouterType1.Size = new System.Drawing.Size(202, 21);
//            this.ctRouterType1.TabIndex = 7;
//            this.ctRouterType1.Visible = false;
//            // 
//            // macctconnected
//            // 
//            this.macctconnected.DropDownWidth = 128;
//            this.macctconnected.Location = new System.Drawing.Point(93, 70);
//            this.macctconnected.Name = "macctconnected";
//            this.macctconnected.Size = new System.Drawing.Size(124, 21);
//            this.macctconnected.TabIndex = 18;
//            // 
//            // kryptonLabel3
//            // 
//            this.kryptonLabel3.Location = new System.Drawing.Point(42, 73);
//            this.kryptonLabel3.Name = "kryptonLabel3";
//            this.kryptonLabel3.Size = new System.Drawing.Size(41, 18);
//            this.kryptonLabel3.TabIndex = 17;
//            this.kryptonLabel3.Values.Text = "连接:";
//            // 
//            // macctbinded
//            // 
//            this.macctbinded.DropDownWidth = 128;
//            this.macctbinded.Location = new System.Drawing.Point(93, 95);
//            this.macctbinded.Name = "macctbinded";
//            this.macctbinded.Size = new System.Drawing.Size(124, 21);
//            this.macctbinded.TabIndex = 20;
//            // 
//            // kryptonLabel4
//            // 
//            this.kryptonLabel4.Location = new System.Drawing.Point(3, 97);
//            this.kryptonLabel4.Name = "kryptonLabel4";
//            this.kryptonLabel4.Size = new System.Drawing.Size(82, 18);
//            this.kryptonLabel4.TabIndex = 19;
//            this.kryptonLabel4.Values.Text = "主帐户分配:";
//            // 
//            // fmAcctFilter
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(258, 148);
//            this.Controls.Add(this.kryptonPanel1);
//            this.DoubleBuffered = true;
//            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
//            this.MaximizeBox = false;
//            this.MinimizeBox = false;
//            this.Name = "fmAcctFilter";
//            this.Opacity = 0.95D;
//            this.ShowIcon = false;
//            this.ShowInTaskbar = false;
//            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
//            this.TopMost = true;
//            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
//            this.kryptonPanel1.ResumeLayout(false);
//            this.kryptonPanel1.PerformLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.accexecute)).EndInit();
//            ((System.ComponentModel.ISupportInitialize)(this.closeimg)).EndInit();
//            ((System.ComponentModel.ISupportInitialize)(this.macctconnected)).EndInit();
//            ((System.ComponentModel.ISupportInitialize)(this.macctbinded)).EndInit();
//            this.ResumeLayout(false);

//        }

//        #endregion

//        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
//        private ComponentFactory.Krypton.Toolkit.KryptonComboBox accexecute;
//        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
//        private ctAccountType ctAccountType1;
//        private ctRouterGroupList ctRouterGroupList1;
//        private ctRouterType ctRouterType1;
//        private ctAgentList ctAgentList1;
//        private ComponentFactory.Krypton.Toolkit.KryptonTextBox account;
//        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
//        private System.Windows.Forms.PictureBox closeimg;
//        private ComponentFactory.Krypton.Toolkit.KryptonComboBox macctconnected;
//        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
//        private ComponentFactory.Krypton.Toolkit.KryptonComboBox macctbinded;
//        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
//    }
//}