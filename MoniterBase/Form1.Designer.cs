namespace TradingLib.MoniterBase
{
    partial class Form1
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbDeployID = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbExpireMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.imgLink = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbSpring = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lbDeployID,
            this.toolStripStatusLabel2,
            this.lbExpireMessage,
            this.lbSpring,
            this.imgLink});
            this.statusStrip1.Location = new System.Drawing.Point(0, 294);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(737, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(59, 17);
            this.toolStripStatusLabel1.Text = "部署编号:";
            // 
            // lbDeployID
            // 
            this.lbDeployID.Name = "lbDeployID";
            this.lbDeployID.Size = new System.Drawing.Size(18, 17);
            this.lbDeployID.Text = "--";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(18, 17);
            this.toolStripStatusLabel2.Text = "--";
            this.toolStripStatusLabel2.Visible = false;
            // 
            // lbExpireMessage
            // 
            this.lbExpireMessage.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lbExpireMessage.Name = "lbExpireMessage";
            this.lbExpireMessage.Size = new System.Drawing.Size(162, 17);
            this.lbExpireMessage.Text = "柜台还有7天到期,请及时续费";
            // 
            // imgLink
            // 
            this.imgLink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.imgLink.Image = global::TradingLib.MoniterBase.Properties.Resources.offline;
            this.imgLink.Name = "imgLink";
            this.imgLink.Size = new System.Drawing.Size(16, 17);
            this.imgLink.Text = "--";
            // 
            // lbSpring
            // 
            this.lbSpring.Name = "lbSpring";
            this.lbSpring.Size = new System.Drawing.Size(418, 17);
            this.lbSpring.Spring = true;
            this.lbSpring.Text = "spring";
            this.lbSpring.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 316);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lbDeployID;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel lbExpireMessage;
        private System.Windows.Forms.ToolStripStatusLabel lbSpring;
        private System.Windows.Forms.ToolStripStatusLabel imgLink;
    }
}