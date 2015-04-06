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
    /// 交易时间段管理
    /// </summary>
    public class MarketTimeManagerCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmMarketTime fm = new fmMarketTime();
            fm.ShowDialog();
            fm.Close();
        }
    }

    /// <summary>
    /// 交易所管理
    /// </summary>
    public class ExchangeManagerCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmExchange fm = new fmExchange();
            fm.ShowDialog();
            fm.Close();
        }
    }

    /// <summary>
    /// 品种管理
    /// </summary>
    public class SecurityManagerCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmSecurity fm = new fmSecurity();
            fm.ShowDialog();
            fm.Close();
        }
    }


    /// <summary>
    /// 合约管理
    /// </summary>
    public class SymbolManagerCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmSymbol fm = new fmSymbol();
            fm.ShowDialog();
            fm.Close();
        }
    }

    /// <summary>
    /// 合约同步设置
    /// </summary>
    public class SynSymbolManagerCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmSyncSymbol fm = new fmSyncSymbol();
            fm.ShowDialog();
            fm.Close();
        }
    }
    /// <summary>
    /// 手续费模板设置
    /// </summary>
    public class CommissionTemplateCommand : AbstractMenuCommand
    {
        public override void Run()
        {
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
}
