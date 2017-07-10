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
            //root可查看
            if (!CoreService.SiteInfo.Manager.IsRoot())
            {
                MoniterHelper.WindowMessage("无权限");
                return;
            }

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
        static fmCashOperationManager mgr = null;
        public override void Run()
        {
            bool access = false;
            //管理员可出入金
            if (CoreService.SiteInfo.Manager.IsRoot())
            {
                access = true;
            }

            //自营代理可以出入金
            if (CoreService.SiteInfo.Manager.IsAgent())
            {
                //自盈代理有出入金权限
                if (CoreService.SiteInfo.Agent != null && CoreService.SiteInfo.Agent.AgentType == EnumAgentType.SelfOperated)
                {
                    access = true;
                }
            }

            if (CoreService.SiteInfo.Manager.IsStaff())
            {
                var basemgr = CoreService.BasicInfoTracker.Managers.FirstOrDefault(m => m.mgr_fk == CoreService.SiteInfo.Manager.GetBaseMGR());
                //管理员下属员工按权限模板
                if (basemgr.IsRoot())
                {
                    access = CoreService.SiteInfo.Permission.r_cashop;
                }
                //自营代理下属员工按权限模板
                if (basemgr.IsAgent())
                {
                    if (CoreService.SiteInfo.Agent != null && CoreService.SiteInfo.Agent.AgentType == EnumAgentType.SelfOperated)
                    {
                        access = CoreService.SiteInfo.Permission.r_cashop;
                    }
                }
            }

            if (!access)
            {
                MoniterHelper.WindowMessage("无权限");
                return;
            }
            if (mgr == null)
                mgr = new fmCashOperationManager();
            mgr.Show();
        }
    }
}
