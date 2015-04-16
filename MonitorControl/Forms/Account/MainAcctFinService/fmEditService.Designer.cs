namespace TradingLib.MoniterControl
{
    partial class fmEditService
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
            this.cbInteresetType = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.lbfreq = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbChargeFreq = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.cbChargeValue = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbServiceType = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lbpercent = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.description = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbChargeTime = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.cbChargeMethod = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.btnDel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSubmit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbInteresetType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbChargeFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbServiceType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbChargeTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbChargeMethod)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btnDel);
            this.kryptonPanel1.Controls.Add(this.btnSubmit);
            this.kryptonPanel1.Controls.Add(this.cbChargeMethod);
            this.kryptonPanel1.Controls.Add(this.cbChargeTime);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel1.Controls.Add(this.description);
            this.kryptonPanel1.Controls.Add(this.lbpercent);
            this.kryptonPanel1.Controls.Add(this.cbInteresetType);
            this.kryptonPanel1.Controls.Add(this.lbfreq);
            this.kryptonPanel1.Controls.Add(this.cbChargeFreq);
            this.kryptonPanel1.Controls.Add(this.cbChargeValue);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.cbServiceType);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(309, 209);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // cbInteresetType
            // 
            this.cbInteresetType.DropDownWidth = 90;
            this.cbInteresetType.Location = new System.Drawing.Point(198, 68);
            this.cbInteresetType.Name = "cbInteresetType";
            this.cbInteresetType.Size = new System.Drawing.Size(90, 21);
            this.cbInteresetType.TabIndex = 8;
            // 
            // lbfreq
            // 
            this.lbfreq.Location = new System.Drawing.Point(28, 47);
            this.lbfreq.Name = "lbfreq";
            this.lbfreq.Size = new System.Drawing.Size(68, 18);
            this.lbfreq.TabIndex = 7;
            this.lbfreq.Values.Text = "计费周期:";
            // 
            // cbChargeFreq
            // 
            this.cbChargeFreq.DropDownWidth = 90;
            this.cbChargeFreq.Location = new System.Drawing.Point(102, 44);
            this.cbChargeFreq.Name = "cbChargeFreq";
            this.cbChargeFreq.Size = new System.Drawing.Size(90, 21);
            this.cbChargeFreq.TabIndex = 6;
            // 
            // cbChargeValue
            // 
            this.cbChargeValue.Location = new System.Drawing.Point(102, 69);
            this.cbChargeValue.Name = "cbChargeValue";
            this.cbChargeValue.Size = new System.Drawing.Size(90, 20);
            this.cbChargeValue.TabIndex = 3;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(11, 71);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(85, 18);
            this.kryptonLabel2.TabIndex = 2;
            this.kryptonLabel2.Values.Text = "计费值/比例:";
            // 
            // cbServiceType
            // 
            this.cbServiceType.DropDownWidth = 90;
            this.cbServiceType.Location = new System.Drawing.Point(102, 20);
            this.cbServiceType.Name = "cbServiceType";
            this.cbServiceType.Size = new System.Drawing.Size(90, 21);
            this.cbServiceType.TabIndex = 1;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(28, 23);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(68, 18);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "计费方式:";
            // 
            // lbpercent
            // 
            this.lbpercent.Location = new System.Drawing.Point(198, 71);
            this.lbpercent.Name = "lbpercent";
            this.lbpercent.Size = new System.Drawing.Size(21, 18);
            this.lbpercent.TabIndex = 9;
            this.lbpercent.Values.Text = "%";
            // 
            // description
            // 
            this.description.Location = new System.Drawing.Point(28, 143);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(19, 18);
            this.description.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.description.TabIndex = 10;
            this.description.Values.Text = "--";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(28, 95);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(68, 18);
            this.kryptonLabel3.TabIndex = 11;
            this.kryptonLabel3.Values.Text = "收费时间:";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(28, 119);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(68, 18);
            this.kryptonLabel4.TabIndex = 12;
            this.kryptonLabel4.Values.Text = "收费方式:";
            // 
            // cbChargeTime
            // 
            this.cbChargeTime.DropDownWidth = 90;
            this.cbChargeTime.Location = new System.Drawing.Point(102, 92);
            this.cbChargeTime.Name = "cbChargeTime";
            this.cbChargeTime.Size = new System.Drawing.Size(90, 21);
            this.cbChargeTime.TabIndex = 13;
            // 
            // cbChargeMethod
            // 
            this.cbChargeMethod.DropDownWidth = 90;
            this.cbChargeMethod.Location = new System.Drawing.Point(102, 116);
            this.cbChargeMethod.Name = "cbChargeMethod";
            this.cbChargeMethod.Size = new System.Drawing.Size(90, 21);
            this.cbChargeMethod.TabIndex = 14;
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDel.Location = new System.Drawing.Point(128, 172);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(70, 25);
            this.btnDel.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDel.TabIndex = 17;
            this.btnDel.Values.Text = "删 除";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(218, 172);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(70, 25);
            this.btnSubmit.TabIndex = 16;
            this.btnSubmit.Values.Text = "提 交";
            // 
            // fmEditService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 209);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmEditService";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配资服务设置";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbInteresetType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbChargeFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbServiceType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbChargeTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbChargeMethod)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbServiceType;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown cbChargeValue;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lbfreq;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbChargeFreq;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbInteresetType;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lbpercent;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel description;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbChargeMethod;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbChargeTime;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSubmit;
    }
}