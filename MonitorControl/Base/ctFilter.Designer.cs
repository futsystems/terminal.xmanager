namespace TradingLib.MoniterControl
{
    partial class ctFilter
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
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.tbAccount = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.cbLogin = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.cbPos = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.cbLock = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.SuspendLayout();
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(0, 3);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(35, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "帐户";
            // 
            // tbAccount
            // 
            this.tbAccount.Location = new System.Drawing.Point(38, 3);
            this.tbAccount.Name = "tbAccount";
            this.tbAccount.Size = new System.Drawing.Size(100, 20);
            this.tbAccount.TabIndex = 1;
            // 
            // cbLogin
            // 
            this.cbLogin.Location = new System.Drawing.Point(145, 3);
            this.cbLogin.Name = "cbLogin";
            this.cbLogin.Size = new System.Drawing.Size(48, 20);
            this.cbLogin.TabIndex = 2;
            this.cbLogin.Values.Text = "登入";
            // 
            // cbPos
            // 
            this.cbPos.Location = new System.Drawing.Point(199, 3);
            this.cbPos.Name = "cbPos";
            this.cbPos.Size = new System.Drawing.Size(48, 20);
            this.cbPos.TabIndex = 3;
            this.cbPos.Values.Text = "持仓";
            // 
            // cbLock
            // 
            this.cbLock.Location = new System.Drawing.Point(253, 3);
            this.cbLock.Name = "cbLock";
            this.cbLock.Size = new System.Drawing.Size(48, 20);
            this.cbLock.TabIndex = 4;
            this.cbLock.Values.Text = "冻结";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(307, 3);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel2.TabIndex = 5;
            this.kryptonLabel2.Values.Text = "账户总数:";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(374, 3);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(20, 20);
            this.kryptonLabel3.TabIndex = 6;
            this.kryptonLabel3.Values.Text = "--";
            // 
            // ctFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.kryptonLabel3);
            this.Controls.Add(this.kryptonLabel2);
            this.Controls.Add(this.cbLock);
            this.Controls.Add(this.cbPos);
            this.Controls.Add(this.cbLogin);
            this.Controls.Add(this.tbAccount);
            this.Controls.Add(this.kryptonLabel1);
            this.Name = "ctFilter";
            this.Size = new System.Drawing.Size(443, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox tbAccount;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox cbLogin;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox cbPos;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox cbLock;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
    }
}
