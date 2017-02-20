namespace TradingLib.MoniterControl
{
    partial class fmOrderRule_TimeFilter
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
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.seclist = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lbDesp = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnSubmit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnRemoveTimeSpan = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnAddTimeSpan = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.end = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
            this.start = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
            this.timefilter = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.kryptonLabel17 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btnRemoveTimeSpan);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel1.Controls.Add(this.btnAddTimeSpan);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.end);
            this.kryptonPanel1.Controls.Add(this.seclist);
            this.kryptonPanel1.Controls.Add(this.start);
            this.kryptonPanel1.Controls.Add(this.lbDesp);
            this.kryptonPanel1.Controls.Add(this.timefilter);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel17);
            this.kryptonPanel1.Controls.Add(this.btnSubmit);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(436, 293);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(31, 199);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(228, 20);
            this.kryptonLabel4.TabIndex = 19;
            this.kryptonLabel4.Values.Text = "品种列表,用逗号分隔,注意大小写与逗号";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(12, 124);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(60, 20);
            this.kryptonLabel2.TabIndex = 18;
            this.kryptonLabel2.Values.Text = "品种列表";
            // 
            // seclist
            // 
            this.seclist.Location = new System.Drawing.Point(76, 124);
            this.seclist.Multiline = true;
            this.seclist.Name = "seclist";
            this.seclist.Size = new System.Drawing.Size(181, 69);
            this.seclist.TabIndex = 17;
            // 
            // lbDesp
            // 
            this.lbDesp.Location = new System.Drawing.Point(12, 228);
            this.lbDesp.Name = "lbDesp";
            this.lbDesp.Size = new System.Drawing.Size(20, 20);
            this.lbDesp.TabIndex = 9;
            this.lbDesp.Values.Text = "--";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(357, 256);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(67, 25);
            this.btnSubmit.TabIndex = 8;
            this.btnSubmit.Values.Text = "提 交";
            // 
            // btnRemoveTimeSpan
            // 
            this.btnRemoveTimeSpan.Location = new System.Drawing.Point(269, 62);
            this.btnRemoveTimeSpan.Name = "btnRemoveTimeSpan";
            this.btnRemoveTimeSpan.Size = new System.Drawing.Size(70, 25);
            this.btnRemoveTimeSpan.TabIndex = 41;
            this.btnRemoveTimeSpan.Values.Text = "删 除";
            // 
            // btnAddTimeSpan
            // 
            this.btnAddTimeSpan.Location = new System.Drawing.Point(350, 62);
            this.btnAddTimeSpan.Name = "btnAddTimeSpan";
            this.btnAddTimeSpan.Size = new System.Drawing.Size(70, 25);
            this.btnAddTimeSpan.TabIndex = 36;
            this.btnAddTimeSpan.Values.Text = "添 加";
            // 
            // end
            // 
            this.end.CustomFormat = "HH:mm:ss";
            this.end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.end.Location = new System.Drawing.Point(334, 35);
            this.end.Name = "end";
            this.end.ShowUpDown = true;
            this.end.Size = new System.Drawing.Size(86, 21);
            this.end.TabIndex = 40;
            // 
            // start
            // 
            this.start.CustomFormat = "HH:mm:ss";
            this.start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.start.Location = new System.Drawing.Point(334, 8);
            this.start.Name = "start";
            this.start.ShowUpDown = true;
            this.start.Size = new System.Drawing.Size(86, 21);
            this.start.TabIndex = 39;
            // 
            // timefilter
            // 
            this.timefilter.Location = new System.Drawing.Point(76, 12);
            this.timefilter.Name = "timefilter";
            this.timefilter.Size = new System.Drawing.Size(181, 75);
            this.timefilter.TabIndex = 38;
            // 
            // kryptonLabel17
            // 
            this.kryptonLabel17.Location = new System.Drawing.Point(0, 12);
            this.kryptonLabel17.Name = "kryptonLabel17";
            this.kryptonLabel17.Size = new System.Drawing.Size(73, 20);
            this.kryptonLabel17.TabIndex = 37;
            this.kryptonLabel17.Values.Text = "时间段过滤";
            // 
            // fmOrderRule_TimeFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 293);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmOrderRule_TimeFilter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "品种过滤";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSubmit;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lbDesp;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox seclist;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnRemoveTimeSpan;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnAddTimeSpan;
        private ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker end;
        private ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker start;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox timefilter;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel17;
    }
}