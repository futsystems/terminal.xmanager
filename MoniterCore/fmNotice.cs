using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TradingLib.MoniterCore
{
    public partial class fmNotice : DevExpress.XtraEditors.XtraForm
    {
        public fmNotice()
        {
            InitializeComponent();
            this.btnAccept.Click += new EventHandler(btnAccept_Click);
            this.btnReject.Click += new EventHandler(btnReject_Click);
        }

        void btnReject_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            return;
        }

        void btnAccept_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            return;
        }
    }
}
