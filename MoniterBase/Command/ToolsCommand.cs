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
            if (!CoreService.SiteInfo.Domain.Super)
            {
                MoniterHelper.WindowMessage("无权限");
                return;
            }

            fmSettleManager fm = new fmSettleManager();
            fm.ShowDialog();
            fm.Close();
        }
    }

    public class DataStoreCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            if (!CoreService.SiteInfo.Domain.Super)
            {
                MoniterHelper.WindowMessage("无权限");
                return;
            }

            fmDataManager fm = new fmDataManager();
            fm.ShowDialog();
            fm.Close();
        }
    }

    public class ExchangeRateCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            if (!CoreService.SiteInfo.Manager.IsRoot())
            {
                MoniterHelper.WindowMessage("无权限");
                return;
            }
            fmExchangeRate fm = new fmExchangeRate();
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
    /// <summary>
    /// 支付通道设置 
    /// </summary>
    public class PayGateWayCommand: AbstractMenuCommand
    {
        public override void Run()
        {
            fmPaymentGateway fm = new fmPaymentGateway();
            fm.ShowDialog();
            fm.Close();
        }
    }

    /// <summary>
    /// 在线出入金管理
    /// </summary>
    public class CashOperationCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmCashOperationManager fm = new fmCashOperationManager();
            fm.ShowDialog();
            fm.Close();
        }
    }
}
