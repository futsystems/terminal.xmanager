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
    /// 开启清算中心
    /// </summary>
    public class OpenClearCentreCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            throw new NotImplementedException();
        }
    }

    public class CloseClearCentreCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            throw new NotImplementedException();
        }
    }



    /// <summary>
    /// 底层交易接口管理
    /// </summary>
    public class InterfaceManagerCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmInterface fm = new fmInterface();
            fm.ShowDialog();
        }
    }

    /// <summary>
    /// 默认通道管理
    /// </summary>
    public class DefaultConnectorManagerCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmDefaultConnector fm = new fmDefaultConnector();
            fm.ShowDialog();
        }
    }

    /// <summary>
    /// 系统状态
    /// </summary>
    public class SystemStatusCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmCoreStatus fm = new fmCoreStatus();
            fm.ShowDialog();
        }
    }

    /// <summary>
    /// 任务日志管理
    /// </summary>
    public class TaskLogManagerCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmTaskMoniter fm = new fmTaskMoniter();
            fm.ShowDialog();
        }
    }

    /// <summary>
    /// 结算管理
    /// </summary>
    public class SettleManagerCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmSettleManager fm = new fmSettleManager();
            fm.ShowDialog();
        }
    }
}
