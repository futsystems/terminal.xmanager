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


namespace TradingLib.MoniterControl
{
    public partial class fmCleanData : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmCleanData()
        {
            InitializeComponent();
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            int date = Util.ToTLDate(settleday.Value);

            if (MoniterHelper.WindowConfirm(string.Format("确认清理结算日:{0}之前的数据?",date)) == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqCleanData(date);
            }
        }
    }
}
