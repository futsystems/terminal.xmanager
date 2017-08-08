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
    /// 品种管理
    /// 权限
    /// 分区管理员可见
    /// </summary>
    public class SecurityManagerCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            if (!CoreService.SiteInfo.Manager.IsRoot())
            {
                MoniterHelper.WindowMessage("无权限");
                return;
            }
            fmSecurity fm = new fmSecurity();
            fm.ShowDialog();
            fm.Close();
        }
    }


    /// <summary>
    /// 合约管理
    /// 权限
    /// 分区管理员可见
    /// </summary>
    public class SymbolManagerCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            if (!CoreService.SiteInfo.Manager.IsRoot())
            {
                MoniterHelper.WindowMessage("无权限");
                return;
            }

            fmSymbol fm = new fmSymbol();
            fm.ShowDialog();
            fm.Close();
        }
    }

    /// <summary>
    /// 合约同步设置
    /// </summary>
    //public class SynSymbolManagerCommand : AbstractMenuCommand
    //{
    //    public override void Run()
    //    {
    //        fmSyncSymbol fm = new fmSyncSymbol();
    //        fm.ShowDialog();
    //        fm.Close();
    //    }
    //}
    /// <summary>
    /// 手续费模板设置
    /// </summary>
    public class CommissionTemplateCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            //if (!CoreService.SiteInfo.UIAccess.r_commission)
            //{
            //    MoniterHelper.WindowMessage("无权限");
            //    return;
            //}
            fmCommission fm = new fmCommission();
            fm.ShowDialog();
            fm.Close();
        }
    }

    /// <summary>
    /// 保证金模板设置
    /// </summary>
    public class MarginTemplateCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            //if (!CoreService.SiteInfo.UIAccess.r_margin)
            //{
            //    MoniterHelper.WindowMessage("无权限");
            //    return;
            //}
            fmMargin fm = new fmMargin();
            fm.ShowDialog();
            fm.Close();
        }
    }

    /// <summary>
    /// 交易参数模板设置
    /// </summary>
    public class ExStrategyTemplateCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmExStrategy fm = new fmExStrategy();
            fm.ShowDialog();
            fm.Close();
        }
    }

    /// <summary>
    /// 配置模板
    /// </summary>
    public class ConfigTemplateCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmConfigTemplate fm = new fmConfigTemplate();
            fm.ShowDialog();
            fm.Close();
        }
    }

    /// <summary>
    /// 设定默认配置模板
    /// </summary>
    public class DefaultConfigTemplateCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            var _agent = CoreService.BasicInfoTracker.GetAgent(CoreService.SiteInfo.Manager.Login);
            if (_agent == null)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("代理财务账户不存在");
                return;
            }

            fmEditAgentDefaultConfigTemplate fm = new fmEditAgentDefaultConfigTemplate();
            fm.SetAccount(_agent);
            fm.ShowDialog();
            fm.Close();
        }
    }




    /// <summary>
    /// 交易时间段管理
    /// 权限
    /// 超级分区可管理
    /// </summary>
    public class MarketTimeManagerCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            if (!CoreService.SiteInfo.Domain.Super)
            {
                MoniterHelper.WindowMessage("无权限");
                return;
            }
            fmMarketTime fm = new fmMarketTime();
            fm.ShowDialog();
            fm.Close();
        }
    }

    /// <summary>
    /// 交易所管理
    /// 权限
    /// 超级分区可管理
    /// </summary>
    public class ExchangeManagerCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            if (!CoreService.SiteInfo.Domain.Super)
            {
                MoniterHelper.WindowMessage("无权限");
                return;
            }

            fmExchange fm = new fmExchange();
            fm.ShowDialog();
            fm.Close();
        }
    }
}
