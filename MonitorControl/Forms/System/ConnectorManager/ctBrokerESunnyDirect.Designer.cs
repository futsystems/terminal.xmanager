﻿namespace TradingLib.MoniterControl
{
    partial class ctBrokerESunnyDirect
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
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.uf1 = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.username = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.port = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.pass = new ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox();
            this.address = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.uf1);
            this.kryptonPanel1.Controls.Add(this.username);
            this.kryptonPanel1.Controls.Add(this.port);
            this.kryptonPanel1.Controls.Add(this.pass);
            this.kryptonPanel1.Controls.Add(this.address);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel5);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(252, 150);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(10, 3);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "交易前置:";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(35, 29);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel2.TabIndex = 1;
            this.kryptonLabel2.Values.Text = "端口:";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(14, 55);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(61, 20);
            this.kryptonLabel3.TabIndex = 2;
            this.kryptonLabel3.Values.Text = "BrokerID:";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(35, 107);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel4.TabIndex = 4;
            this.kryptonLabel4.Values.Text = "密码:";
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(10, 81);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(63, 20);
            this.kryptonLabel5.TabIndex = 3;
            this.kryptonLabel5.Values.Text = "交易账号:";
            // 
            // uf1
            // 
            this.uf1.Location = new System.Drawing.Point(79, 55);
            this.uf1.Name = "uf1";
            this.uf1.Size = new System.Drawing.Size(62, 20);
            this.uf1.TabIndex = 18;
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(79, 81);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(118, 20);
            this.username.TabIndex = 16;
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(79, 29);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(62, 20);
            this.port.TabIndex = 19;
            // 
            // pass
            // 
            this.pass.Location = new System.Drawing.Point(79, 107);
            this.pass.Name = "pass";
            this.pass.PasswordChar = '*';
            this.pass.Size = new System.Drawing.Size(118, 20);
            this.pass.TabIndex = 20;
            // 
            // address
            // 
            this.address.Location = new System.Drawing.Point(79, 3);
            this.address.Name = "address";
            this.address.Size = new System.Drawing.Size(118, 20);
            this.address.TabIndex = 17;
            // 
            // ctBrokerCTPDirect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "ctBrokerCTPDirect";
            this.Size = new System.Drawing.Size(252, 150);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox uf1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox username;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox port;
        private ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox pass;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox address;
    }
}
