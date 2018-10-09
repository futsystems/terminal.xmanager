namespace TradingLib.MoniterBase
{
    partial class fmHelp
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
            this.stopMsg = new System.Windows.Forms.Button();
            this.debugControl1 = new TradingLib.MoniterBase.DebugControl();
            this.stop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // stopMsg
            // 
            this.stopMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stopMsg.Location = new System.Drawing.Point(756, 12);
            this.stopMsg.Name = "stopMsg";
            this.stopMsg.Size = new System.Drawing.Size(112, 27);
            this.stopMsg.TabIndex = 3;
            this.stopMsg.Text = "StockMessage";
            this.stopMsg.UseVisualStyleBackColor = true;
            this.stopMsg.Click += new System.EventHandler(this.stopMsg_Click);
            // 
            // debugControl1
            // 
            this.debugControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.debugControl1.EnableSearching = true;
            this.debugControl1.ExternalTimeStamp = 0;
            this.debugControl1.Location = new System.Drawing.Point(11, 44);
            this.debugControl1.Margin = new System.Windows.Forms.Padding(2);
            this.debugControl1.Name = "debugControl1";
            this.debugControl1.Size = new System.Drawing.Size(932, 415);
            this.debugControl1.TabIndex = 2;
            this.debugControl1.TimeStamps = true;
            this.debugControl1.UseExternalTimeStamp = false;
            // 
            // stop
            // 
            this.stop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stop.Location = new System.Drawing.Point(875, 12);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(87, 27);
            this.stop.TabIndex = 1;
            this.stop.Text = "StopTick";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // fmHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 470);
            this.Controls.Add(this.stopMsg);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.debugControl1);
            this.Name = "fmHelp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "帮助";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button stop;
        private DebugControl debugControl1;
        private System.Windows.Forms.Button stopMsg;
    }
}