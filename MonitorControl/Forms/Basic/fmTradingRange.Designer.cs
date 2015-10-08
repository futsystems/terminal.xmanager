namespace TradingLib.MoniterControl
{
    partial class fmTradingRange
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
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.settleflag = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnOk = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.endtime = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
            this.starttime = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
            this.endday = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.startday = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.marketclose = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.settleflag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endday)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startday)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.marketclose);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel5);
            this.kryptonPanel1.Controls.Add(this.settleflag);
            this.kryptonPanel1.Controls.Add(this.btnCancel);
            this.kryptonPanel1.Controls.Add(this.btnOk);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.endtime);
            this.kryptonPanel1.Controls.Add(this.starttime);
            this.kryptonPanel1.Controls.Add(this.endday);
            this.kryptonPanel1.Controls.Add(this.startday);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(411, 150);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(23, 57);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(60, 20);
            this.kryptonLabel5.TabIndex = 12;
            this.kryptonLabel5.Values.Text = "结算标识";
            // 
            // settleflag
            // 
            this.settleflag.DropDownWidth = 80;
            this.settleflag.Location = new System.Drawing.Point(23, 83);
            this.settleflag.Name = "settleflag";
            this.settleflag.Size = new System.Drawing.Size(79, 21);
            this.settleflag.TabIndex = 11;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(313, 113);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 25);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Values.Text = "取 消";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(226, 113);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(70, 25);
            this.btnOk.TabIndex = 9;
            this.btnOk.Values.Text = "确 定";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(291, 4);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(60, 20);
            this.kryptonLabel4.TabIndex = 8;
            this.kryptonLabel4.Values.Text = "结束时间";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(108, 4);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(60, 20);
            this.kryptonLabel3.TabIndex = 7;
            this.kryptonLabel3.Values.Text = "开始时间";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(206, 4);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(60, 20);
            this.kryptonLabel2.TabIndex = 6;
            this.kryptonLabel2.Values.Text = "结束日期";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(23, 4);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(60, 20);
            this.kryptonLabel1.TabIndex = 5;
            this.kryptonLabel1.Values.Text = "开始日期";
            // 
            // endtime
            // 
            this.endtime.CalendarShowToday = false;
            this.endtime.CalendarShowTodayCircle = false;
            this.endtime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.endtime.Location = new System.Drawing.Point(291, 30);
            this.endtime.Name = "endtime";
            this.endtime.ShowUpDown = true;
            this.endtime.Size = new System.Drawing.Size(92, 21);
            this.endtime.TabIndex = 4;
            // 
            // starttime
            // 
            this.starttime.CalendarShowToday = false;
            this.starttime.CalendarShowTodayCircle = false;
            this.starttime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.starttime.Location = new System.Drawing.Point(108, 30);
            this.starttime.Name = "starttime";
            this.starttime.ShowUpDown = true;
            this.starttime.Size = new System.Drawing.Size(92, 21);
            this.starttime.TabIndex = 3;
            // 
            // endday
            // 
            this.endday.DropDownWidth = 80;
            this.endday.Location = new System.Drawing.Point(206, 30);
            this.endday.Name = "endday";
            this.endday.Size = new System.Drawing.Size(79, 21);
            this.endday.TabIndex = 1;
            // 
            // startday
            // 
            this.startday.DropDownWidth = 80;
            this.startday.Location = new System.Drawing.Point(23, 30);
            this.startday.Name = "startday";
            this.startday.Size = new System.Drawing.Size(79, 21);
            this.startday.TabIndex = 0;
            // 
            // marketclose
            // 
            this.marketclose.Location = new System.Drawing.Point(310, 83);
            this.marketclose.Name = "marketclose";
            this.marketclose.Size = new System.Drawing.Size(73, 20);
            this.marketclose.TabIndex = 13;
            this.marketclose.Values.Text = "收盘小节";
            // 
            // fmTradingRange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 150);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "fmTradingRange";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加交易小节";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.settleflag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endday)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startday)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox startday;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox endday;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker endtime;
        private ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker starttime;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnOk;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox settleflag;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox marketclose;
    }
}