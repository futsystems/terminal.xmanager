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
    public partial class fmDataManager : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmDataManager()
        {
            InitializeComponent();
        }

        private void btnStoreAll_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm("确认转储所有交易记录?") == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqContribRequest("SettleCentre", "StoreSettledData", "");
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm("确认删除已结算交易记录?") == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqContribRequest("SettleCentre", "DeleteSettledData", "");
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {

        }
    }
}
