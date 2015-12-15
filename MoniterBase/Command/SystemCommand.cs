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
    /// 默认通道管理
    /// </summary>
    public class DefaultConnectorManagerCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            //超级分区或者独立分区的root可执行
            if ((CoreService.SiteInfo.Domain.Super || CoreService.SiteInfo.Domain.Dedicated) && CoreService.SiteInfo.Manager.IsRoot())
            {
                fmDefaultConnector fm = new fmDefaultConnector();
                fm.ShowDialog();
                fm.Close();
            }
            else
            {
                MoniterHelper.WindowMessage("无权限");
                return;
            }
            
        }
    }

    /// <summary>
    /// 系统状态
    /// </summary>
    public class SystemStatusCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            //root可查看
            if (!CoreService.SiteInfo.Manager.IsRoot())
            {
                MoniterHelper.WindowMessage("无权限");
                return;
            }
            fmCoreStatus fm = new fmCoreStatus();
            fm.ShowDialog();
            fm.Close();
        }
    }

    /// <summary>
    /// 任务日志管理
    /// </summary>
    public class TaskLogManagerCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            //超级分区可查看
            if (!CoreService.SiteInfo.Domain.Super)
            {
                MoniterHelper.WindowMessage("无权限");
                return;
            }

            fmTaskMoniter fm = new fmTaskMoniter();
            fm.ShowDialog();
            fm.Close();
        }
    }

    public class DomainInfoCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            //root可查看
            if (!CoreService.SiteInfo.Manager.IsRoot())
            {
                MoniterHelper.WindowMessage("无权限");
                return;
            }

            fmDomainInfo fm = new fmDomainInfo();
            fm.SetDomain(CoreService.SiteInfo.Domain);
            fm.ShowDialog();
            fm.Close();
        }
    }



    /// <summary>
    /// 修改管理员密码
    /// </summary>
    public class ChangePasswordCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmChangePasswordAgent fm = new fmChangePasswordAgent();
            fm.ShowDialog();
            fm.Close();
        }
    }

    public class ExitCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            if (MoniterHelper.WindowConfirm("确认退出系统") == System.Windows.Forms.DialogResult.Yes)
            {
                Workbench workbench = (Workbench)this.Owner;
                workbench.Close();
            }
        }
    }


    /// <summary>
    /// 底层交易接口管理
    /// </summary>
    public class InterfaceManagerCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            if (!CoreService.SiteInfo.Domain.Super)
            {
                MoniterHelper.WindowMessage("无权限");
                return;
            }

            fmInterface fm = new fmInterface();
            fm.ShowDialog();
            fm.Close();
        }
    }

    /// <summary>
    /// 托管分区管理
    /// </summary>
    public class HostedManagerCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            if (!CoreService.SiteInfo.Domain.Super)
            {
                MoniterHelper.WindowMessage("无权限");
                return;
            }

            fmDomain fm = new fmDomain();
            fm.ShowDialog();
            fm.Close();
        }
    }




    
}
