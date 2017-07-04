namespace TradingLib.MoniterControl
{
    partial class ctGateWayZhiHPay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctGateWayZhiHPay));
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.domain = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.dinPublicKey = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.merPrivateKey = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.mercode = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.payurl = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.domain);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel1.Controls.Add(this.dinPublicKey);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel1.Controls.Add(this.merPrivateKey);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel7);
            this.kryptonPanel1.Controls.Add(this.mercode);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.payurl);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(321, 281);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // domain
            // 
            this.domain.Location = new System.Drawing.Point(27, 236);
            this.domain.Name = "domain";
            this.domain.Size = new System.Drawing.Size(134, 20);
            this.domain.TabIndex = 16;
            this.domain.Text = "abc.com";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(7, 210);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(35, 20);
            this.kryptonLabel4.TabIndex = 15;
            this.kryptonLabel4.Values.Text = "域名";
            // 
            // dinPublicKey
            // 
            this.dinPublicKey.Location = new System.Drawing.Point(27, 185);
            this.dinPublicKey.Name = "dinPublicKey";
            this.dinPublicKey.Size = new System.Drawing.Size(269, 20);
            this.dinPublicKey.TabIndex = 14;
            this.dinPublicKey.Text = resources.GetString("dinPublicKey.Text");
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(7, 159);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(60, 20);
            this.kryptonLabel3.TabIndex = 13;
            this.kryptonLabel3.Values.Text = "智付公钥";
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
            // mercode
            // 
            this.mercode.Location = new System.Drawing.Point(27, 81);
            this.mercode.Name = "mercode";
            this.mercode.Size = new System.Drawing.Size(148, 20);
            this.mercode.TabIndex = 3;
            this.mercode.Text = "1111110166";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(4, 55);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(48, 20);
            this.kryptonLabel2.TabIndex = 2;
            this.kryptonLabel2.Values.Text = "商户号";
            // 
            // payurl
            // 
            this.payurl.Location = new System.Drawing.Point(27, 29);
            this.payurl.Name = "payurl";
            this.payurl.Size = new System.Drawing.Size(259, 20);
            this.payurl.TabIndex = 1;
            this.payurl.Text = "https://pay.zhihpay.com/gateway?input_charset=UTF-8";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(4, 4);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(101, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "智汇付i网关地址";
            // 
            // ctGateWayZhiHPay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "ctGateWayZhiHPay";
            this.Size = new System.Drawing.Size(321, 281);
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
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox mercode;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox merPrivateKey;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox dinPublicKey;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox domain;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
    }
}
