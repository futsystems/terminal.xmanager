using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;
using TradingLib.MoniterControl;
using ICSharpCode.Core;

namespace TradingLib.MoniterBase.Command
{
    /// <summary>
    /// 结算管理
    /// </summary>
    public class SettleManagerCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmSettleManager fm = new fmSettleManager();
            fm.ShowDialog();
            fm.Close();
        }
    }

    public class DataStoreCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            //if (MoniterHelper.WindowConfirm("确认转储所有已结算交易记录?") == System.Windows.Forms.DialogResult.Yes)
            //{
            //    CoreService.TLClient.ReqContribRequest("SettleCentre", "ReqDumpSettledData", "");
            //}
            fmDataManager fm = new fmDataManager();
            fm.ShowDialog();
            fm.Close();
        }
    }

    public class FeeManagerCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmFeeManagement fm = new fmFeeManagement();
            fm.ShowDialog();
            fm.Close();
        }
    }
}
