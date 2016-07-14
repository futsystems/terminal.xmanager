using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;

namespace TradingLib.TinyMGRControl
{
    public partial class fmAddAccount : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmAddAccount()
        {
            InitializeComponent();

            btnSubmit.Click += new EventHandler(btnSubmit_Click);
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm("确认添加交易帐号?") == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqAddAccount(QSEnumAccountCategory.SUBACCOUNT, account.Text,password.Text,CoreService.SiteInfo.MGRID,0,0);
                this.Close();
            }
        }
    }
}
