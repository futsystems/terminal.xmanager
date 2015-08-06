using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;
using ICSharpCode.Core;

namespace TradingLib.MoniterControl
{
    /// <summary>
    /// 编辑跟单策略参数
    /// </summary>
    public class EditFollowStrategyCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            FollowStrategyConfig cfg  = null;
            ctFollowStrategyMoniter moniter = this.Owner as ctFollowStrategyMoniter;
            if (moniter == null) return;
            cfg = moniter.CurrentStrategyConfig;
            if (cfg == null)
            {
                MoniterHelper.WindowMessage("请选择跟单策略");
                return;
            }
            fmFollowStrategyCfg fm = new fmFollowStrategyCfg();
            fm.SetFollowStrategyConfig(cfg);
            fm.ShowDialog();
            fm.Close();
        }
    }

    /// <summary>
    /// 编辑跟单策略信号源
    /// </summary>
    public class EditFollowStrategySignalCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            FollowStrategyConfig cfg = null;
            ctFollowStrategyMoniter moniter = this.Owner as ctFollowStrategyMoniter;
            if (moniter == null) return;
            cfg = moniter.CurrentStrategyConfig;
            if (cfg == null)
            {
                MoniterHelper.WindowMessage("请选择跟单策略");
                return;
            }

            fmFollowStrategySignals fm = new fmFollowStrategySignals();
            fm.SetFollowStrategyConfig(cfg);
            fm.ShowDialog();
            fm.Close();
           
        }
    }
}
