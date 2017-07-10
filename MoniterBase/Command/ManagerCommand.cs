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
    /// 管理员管理
    /// 权限
    /// 有代理模块 才可以进行柜员管理
    /// </summary>
    public class ManagerCommand:AbstractMenuCommand
    {
        public override void Run()
        {
            //if (!CoreService.SiteInfo.Domain.Module_Agent)
            //{
            //    MoniterHelper.WindowMessage("无权限");
            //    return;
            //}
            
            ////如果是代理且可开下级代理数量为0则无权查看
            //if (CoreService.SiteInfo.Manager.IsAgent() && (CoreService.SiteInfo.Manager.AgentLimit == 0))
            //{
            //    MoniterHelper.WindowMessage("无权限");
            //    return;
            //}
            ////多级代理检查
            //if (CoreService.SiteInfo.Manager.IsAgent() && (!CoreService.SiteInfo.Domain.Module_SubAgent))
            //{
            //    MoniterHelper.WindowMessage("无权限");
            //    return;
            //}

            fmManagerCentre fm = new fmManagerCentre();
            fm.ShowDialog();
            fm.Close();
        }
    }

    public class AgentPermissionTemplateCommand : AbstractCommand
    {
        public override void Run()
        {
            //只有Root/Agent可以添加权限模板
            if (CoreService.SiteInfo.Manager.IsStaff())
            {
                MoniterHelper.WindowMessage("无权限");
                return;
            }

            fmPermissionTemplate fm = new fmPermissionTemplate();
            fm.ShowDialog();
            fm.Close();

            //fmAgentPermission fm = new fmAgentPermission();
            //fm.ShowDialog();
            //fm.Close();
        }
    }
}
