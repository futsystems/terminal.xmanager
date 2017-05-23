namespace TradingLib.MoniterControl
{
    partial class ctGateWayTfbPay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctGateWayTfbPay));
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.tfbPublicKey = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.merPrivateKey = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.spid = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.payurl = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.spuserid = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.md5key = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.test = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.test);
            this.kryptonPanel1.Controls.Add(this.md5key);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel5);
            this.kryptonPanel1.Controls.Add(this.spuserid);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel1.Controls.Add(this.tfbPublicKey);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel1.Controls.Add(this.merPrivateKey);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel7);
            this.kryptonPanel1.Controls.Add(this.spid);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.payurl);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(321, 318);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // tfbPublicKey
            // 
            this.tfbPublicKey.Location = new System.Drawing.Point(27, 185);
            this.tfbPublicKey.Name = "tfbPublicKey";
            this.tfbPublicKey.Size = new System.Drawing.Size(269, 20);
            this.tfbPublicKey.TabIndex = 14;
            this.tfbPublicKey.Text = resources.GetString("tfbPublicKey.Text");
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(7, 159);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(73, 20);
            this.kryptonLabel3.TabIndex = 13;
            this.kryptonLabel3.Values.Text = "天下付公钥";
            // 
            // merPrivateKey
            // 
            this.merPrivateKey.Location = new System.Drawing.Point(27, 133);
            this.merPrivateKey.Name = "merPrivateKey";
            this.merPrivateKey.Size = new System.Drawing.Size(269, 20);
            this.merPrivateKey.TabIndex = 12;
            this.merPrivateKey.Text = resources.GetString("merPrivateKey.Text");
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(7, 107);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(60, 20);
            this.kryptonLabel7.TabIndex = 11;
            this.kryptonLabel7.Values.Text = "商户私钥";
            // 
            // spid
            // 
            this.spid.Location = new System.Drawing.Point(27, 81);
            this.spid.Name = "spid";
            this.spid.Size = new System.Drawing.Size(148, 20);
            this.spid.TabIndex = 3;
            this.spid.Text = "1800071515";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(4, 55);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(36, 20);
            this.kryptonLabel2.TabIndex = 2;
            this.kryptonLabel2.Values.Text = "SPID";
            // 
            // payurl
            // 
            this.payurl.Location = new System.Drawing.Point(27, 29);
            this.payurl.Name = "payurl";
            this.payurl.Size = new System.Drawing.Size(259, 20);
            this.payurl.TabIndex = 1;
            this.payurl.Text = "http://apitest.tfb8.com/cgi-bin/v2.0/api_cardpay_apply.cgi";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(4, 4);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(98, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "天下付网关地址";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(7, 210);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(60, 20);
            this.kryptonLabel4.TabIndex = 15;
            this.kryptonLabel4.Values.Text = "SPUserID";
            // 
            // spuserid
            // 
            this.spuserid.Location = new System.Drawing.Point(27, 236);
            this.spuserid.Name = "spuserid";
            this.spuserid.Size = new System.Drawing.Size(134, 20);
            this.spuserid.TabIndex = 16;
            this.spuserid.Text = "101347613";
            // 
            // md5key
            // 
            this.md5key.Location = new System.Drawing.Point(27, 286);
            this.md5key.Name = "md5key";
            this.md5key.Size = new System.Drawing.Size(134, 20);
            this.md5key.TabIndex = 18;
            this.md5key.Text = "12345";
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(7, 260);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(57, 20);
            this.kryptonLabel5.TabIndex = 17;
            this.kryptonLabel5.Values.Text = "MD5Key";
            // 
            // test
            // 
            this.test.Location = new System.Drawing.Point(223, 286);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(73, 20);
            this.test.TabIndex = 19;
            this.test.Values.Text = "测试建行";
            // 
            // ctGateWayTfbPay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "ctGateWayTfbPay";
            this.Size = new System.Drawing.Size(321, 318);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox payurl;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox spid;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox merPrivateKey;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox tfbPublicKey;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox md5key;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox spuserid;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox test;
    }
}
