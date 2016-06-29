namespace TradingLib.MoniterControl
{
    partial class fmImportStk
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
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbexchange = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbsecurity = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnSubmit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbexchange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbsecurity)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btnSubmit);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel1.Controls.Add(this.cbsecurity);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel1.Controls.Add(this.cbexchange);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(372, 123);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(31, 12);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(51, 20);
            this.kryptonLabel3.TabIndex = 8;
            this.kryptonLabel3.Values.Text = "交易所:";
            // 
            // cbexchange
            // 
            this.cbexchange.DropDownWidth = 183;
            this.cbexchange.Location = new System.Drawing.Point(92, 9);
            this.cbexchange.Name = "cbexchange";
            this.cbexchange.Size = new System.Drawing.Size(137, 21);
            this.cbexchange.TabIndex = 7;
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(44, 38);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(38, 20);
            this.kryptonLabel4.TabIndex = 10;
            this.kryptonLabel4.Values.Text = "品种:";
            // 
            // cbsecurity
            // 
            this.cbsecurity.DropDownWidth = 187;
            this.cbsecurity.Location = new System.Drawing.Point(92, 36);
            this.cbsecurity.Name = "cbsecurity";
            this.cbsecurity.Size = new System.Drawing.Size(137, 21);
            this.cbsecurity.TabIndex = 9;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(270, 86);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(90, 25);
            this.btnSubmit.TabIndex = 11;
            this.btnSubmit.Values.Text = "导入";
            // 
            // fmImportStk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 123);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "fmImportStk";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导入股票列表";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbexchange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbsecurity)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbexchange;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbsecurity;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSubmit;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}