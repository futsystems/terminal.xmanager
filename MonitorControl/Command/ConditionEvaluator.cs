using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;
using ICSharpCode.Core;


//默认权限设置 然后加入自定义|| 任何一个为true就可以显示。这样可以增加自定义控制的灵活性
namespace TradingLib.MoniterControl
{




    /// <summary>
    /// 判断是否满足角色条件
    /// 比如某个角色才可以看到的菜单
    /// </summary>
    public class RoleStateConditionEvaluator : IConditionEvaluator
    {
        public bool IsValid(object caller, Condition condition)
        {
            string role = condition.Properties["role"];//root,agent

            if (String.Compare(role, "root", true) == 0 || String.Compare(role, "r", true) == 0)
            {
                return CoreService.SiteInfo.Manager.Type == QSEnumManagerType.ROOT;
            }
            if (String.Compare(role, "agent", true) == 0 || String.Compare(role, "a", true) == 0)
            {
                return CoreService.SiteInfo.Manager.Type == QSEnumManagerType.AGENT;
            }
            if (string.Compare(role, "inroot", true) == 0)
            {
                //管理域在Root中 
                if (CoreService.SiteInfo.Manager.Type == QSEnumManagerType.ROOT)
                    return true;
                if (CoreService.SiteInfo.Manager.Type == QSEnumManagerType.STAFF)
                {
                    var mgr = CoreService.BasicInfoTracker.GetManager(CoreService.SiteInfo.Manager.GetBaseMGR());
                    if (mgr != null && mgr.Type == QSEnumManagerType.ROOT)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    /// <summary>
    /// 判断是否满足产品条件
    /// 比如某个产品才可以显示的菜单
    /// </summary>
    public class ProductStateConditionEvaluator : IConditionEvaluator
    {
        public bool IsValid(object caller, Condition condition)
        {
            string product = condition.Properties["product"];//对应的产品
            //所有产品
            if (String.Compare(product, "all", true) == 0 || product.Equals("*"))
            {
                return true;
            }
            //主帐户监控
            if (String.Compare(product, "vendor", true) == 0 || String.Compare(product, "v", true) == 0)
            {
                return CoreService.SiteInfo.ProductType == QSEnumProductType.VendorMoniter;
            }
            //分帐户柜台
            if (String.Compare(product, "counter", true) == 0 || String.Compare(product, "c", true) == 0)
            {
                return CoreService.SiteInfo.ProductType == QSEnumProductType.CounterSystem;
            }
            return false;
        }
    }

    /// <summary>
    /// 判定系统模块条件
    /// 代理模块,跟单模块等
    /// 根据模块开启显示的菜单或内容
    /// </summary>
    public class ModuleStateConditionEvaluator : IConditionEvaluator
    {
        public bool IsValid(object caller, Condition condition)
        {
            //管理员默认拥有所有权限
            if (CoreService.SiteInfo.Manager.IsRoot() && CoreService.SiteInfo.Domain.Super) return true;

            string module = condition.Properties["module"];//对应的产品
            module = module.ToUpper();
            switch (module)
            { 
                case "AGENT":
                    return CoreService.SiteInfo.Domain.Module_Agent;
                case "FOLLOW": 
                    return CoreService.SiteInfo.Domain.Module_Follow;
                case "PAYONLINE":
                    return CoreService.SiteInfo.Domain.Module_PayOnline;
                default:
                    return false;
            }

        }
    }




    /// <summary>
    /// 权限检查
    /// </summary>
    public class PermissionStateConditionEvaluator : IConditionEvaluator
    {
        public bool IsValid(object caller, Condition condition)
        {
            //管理员默认拥有所有权限
            if (CoreService.SiteInfo.Manager.IsRoot()) return true;

            string permission = condition.Properties["permission"].ToLower();

            //排除特例
            if (permission == "r_account_cashop")
            {
                //分区管理员可执行出入金
                if (CoreService.SiteInfo.Manager.IsRoot())
                {
                    return true;
                }

                //自营代理可以出入金
                if (CoreService.SiteInfo.Manager.IsAgent())
                {
                    //自盈代理有出入金权限
                    if (CoreService.SiteInfo.Agent != null && CoreService.SiteInfo.Agent.AgentType == EnumAgentType.SelfOperated)
                    {
                        return true;
                    }
                    return false;
                }
                if (CoreService.SiteInfo.Manager.IsStaff())
                {
                    var basemgr = CoreService.BasicInfoTracker.Managers.FirstOrDefault(m => m.mgr_fk == CoreService.SiteInfo.Manager.GetBaseMGR());
                    if (basemgr.IsRoot())
                    {
                        return CoreService.SiteInfo.Permission.r_account_cashop;
                    }
                    if (basemgr.IsAgent())
                    {
                        if (CoreService.SiteInfo.Agent != null && CoreService.SiteInfo.Agent.AgentType == EnumAgentType.SelfOperated)
                        {
                            return CoreService.SiteInfo.Permission.r_account_cashop;
                        }
                    }
                    return false;
                }
                return false;
            }

            return CoreService.SiteInfo.Permission.GetPermission(permission);

        }
    }



    /// <summary>
    /// 判断是单独部署还是托管分区
    /// 
    /// </summary>
    public class DeployStateConditionEvaluator : IConditionEvaluator
    {
        public bool IsValid(object caller, Condition condition)
        {
            string product = condition.Properties["deploy"];
            //单独部署和托管分区都可见
            if (String.Compare(product, "all", true) == 0 || product.Equals("*"))
            {
                return true;
            }
            //if (CoreService.SiteInfo == null)
            //{
            //    MoniterHelper.WindowMessage("系统初始化异常,请重新登入");
            //    return false;
            //}
            //托管分区
            if (String.Compare(product, "hosted", true) == 0 || String.Compare(product, "h", true) == 0)
            {
                return (!CoreService.SiteInfo.Domain.Super) && (!CoreService.SiteInfo.Domain.Dedicated);//非超高级管理域 并且不是独立部署
            }
            //独立部署
            if (String.Compare(product, "dedicated", true) == 0 || String.Compare(product, "d", true) == 0)
            {
                return (!CoreService.SiteInfo.Domain.Super) && CoreService.SiteInfo.Domain.Dedicated;//非超高级管理域 并且是独立部署
            }

            //超级管理域
            if (String.Compare(product, "super", true) == 0 || String.Compare(product, "s", true) == 0)
            {
                return CoreService.SiteInfo.Domain.Super;//超级管理域
            }

            return false;
        }
    }


    /// <summary>
    /// 帐户是否绑定主帐户
    /// </summary>
    public class AccountBindedStateConditionEvaluator : IConditionEvaluator
    {
        public bool IsValid(object caller, Condition condition)
        {
            //AccountItem account = null;
            //if (!AccountMoniterHelper.GetCurrentAccount(caller, out account))
            //{
            //    return false;
            //}

            //if (string.IsNullOrEmpty(account.ConnectorToken))
            //{
            //    return false;
            //}
            //return true;
            return false;
        }
    }


    /// <summary>
    /// 判断帐户是否处于激活状态
    /// </summary>
    public class AccountActiveStateConditionEvaluator : IConditionEvaluator
    {
        public bool IsValid(object caller, Condition condition)
        {
            AccountItem account = null;
            if (!AccountMoniterHelper.GetCurrentAccount(caller, out account))
            {
                return false;
            }

            if (account.Execute)
            {
                return true;
            }
            return false;
        }
    }




}
