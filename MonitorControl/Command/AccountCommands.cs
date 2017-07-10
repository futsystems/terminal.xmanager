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

    public class AccountMoniterHelper
    {
        /// <summary>
        /// 获得当前选中交易帐户
        /// 在邮件菜单有预帐户状态相关的状态检查
        /// 1、如果没有帐户，则不需要弹出选择交易帐户的警告窗口
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="acct"></param>
        /// <returns></returns>
        public static bool GetCurrentAccount(object obj, out AccountItem acct)
        {
            acct = null;
            ctAccountMontier accountMoniter = obj as ctAccountMontier;
            if (accountMoniter == null) return false;
            if (accountMoniter.VisualAccountCount == 0) return false;
            acct = accountMoniter.CurrentAccount;
            if (acct == null)
            {
                MoniterHelper.WindowMessage("请选择交易帐户");
                return false;
            }
            return true;
        }
    }

    public class AccountMoniterStateConditionEvaluator : IConditionEvaluator
    {
        public bool IsValid(object caller, Condition condition)
        {
            string product = condition.Properties["product"];
            if (product.Equals("All"))
            {
                return true;
            }
            if (product.Equals("Vendor"))
            {
                return CoreService.SiteInfo.ProductType == QSEnumProductType.VendorMoniter;
            }
            if (product == "Counter")
            {
                return CoreService.SiteInfo.ProductType == QSEnumProductType.CounterSystem;
            }
            return false;
        }
    }




    


    /// <summary>
    /// 出入金操作
    /// </summary>
    public class CashOperationCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            AccountItem account = null;
            if (!AccountMoniterHelper.GetCurrentAccount(this.Owner, out account))
            {
                return;
            }

            if (CoreService.SiteInfo.ProductType == QSEnumProductType.CounterSystem)
            {
                bool access = false;
                //管理员可出入金
                if (CoreService.SiteInfo.Manager.IsRoot())
                {
                    access =  true;
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
                        access = CoreService.SiteInfo.Permission.r_account_cashop;
                    }
                    //自营代理下属员工按权限模板
                    if (basemgr.IsAgent())
                    {
                        if (CoreService.SiteInfo.Agent != null && CoreService.SiteInfo.Agent.AgentType == EnumAgentType.SelfOperated)
                        {
                            access = CoreService.SiteInfo.Permission.r_account_cashop;
                        }
                    }
                }

                fmCashOperationCounter fm = new fmCashOperationCounter();
                fm.SetAccount(account);
                fm.ShowDialog();
                fm.Close();
            }
        }

    }

    /// <summary>
    /// 激活交易帐户
    /// </summary>
    public class ActiveAccountCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            AccountItem account = null;
            if (!AccountMoniterHelper.GetCurrentAccount(this.Owner, out account))
            {
                return;
            }

            if (MoniterHelper.WindowConfirm(string.Format("确认激活交易帐户:{0}?", account.Account)) == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqUpdateAccountExecute(account.Account,true);

            }
        }

    }

    /// <summary>
    /// 冻结交易帐户
    /// </summary>
    public class InActiveAccountCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            AccountItem account = null;
            if (!AccountMoniterHelper.GetCurrentAccount(this.Owner, out account))
            {
                return;
            }


            if (MoniterHelper.WindowConfirm(string.Format("确认冻结交易帐户:{0}?", account.Account)) == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqUpdateAccountExecute(account.Account,false);
            }
        }

    }


    /// <summary>
    /// 更新交易帐户模板设置
    /// </summary>
    public class UpdateTemplateCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            AccountItem account = null;
            if (!AccountMoniterHelper.GetCurrentAccount(this.Owner, out account))
            {
                return;
            }
            fmEditAccountTemplate fm = new fmEditAccountTemplate();
            fm.SetAccount(account);
            fm.ShowDialog();
            fm.Close();
        }
    }


    public class EditRiskRuleCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            AccountItem account = null;
            if (!AccountMoniterHelper.GetCurrentAccount(this.Owner, out account))
            {
                return;
            }

            fmEditRiskRule fm = new fmEditRiskRule();
            fm.SetAccount(account);
            fm.ShowDialog();
            fm.Close();
        }
    }


    /// <summary>
    /// 查询交易密码
    /// </summary>
    public class QryAccountPasswordCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            AccountItem account = null;
            if (!AccountMoniterHelper.GetCurrentAccount(this.Owner, out account))
            {
                return;
            }
            fmLoginInfo fm = new fmLoginInfo();
            fm.SetAccount(account);
            fm.ShowDialog();
            fm.Close();
        }
    }


    /// <summary>
    /// 修改交易帐户密码
    /// </summary>
    public class ChangeAccountPasswordCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            AccountItem account = null;
            if (!AccountMoniterHelper.GetCurrentAccount(this.Owner,out account))
            {
                return;
            }

            fmChangePassword fm = new fmChangePassword();
            fm.SetAccount(account.Account);
            fm.ShowDialog();
            fm.Close();
        }

    }



    /// <summary>
    /// 更新交易帐户个人信息
    /// </summary>
    public class UpdateAccountProfileCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            AccountItem account = null;
            if (!AccountMoniterHelper.GetCurrentAccount(this.Owner, out account))
            {
                return;
            }

            fmEditAccount fm = new fmEditAccount();
            fm.SetAccount(account);
            fm.ShowDialog();
            fm.Close();
        }
    }


   
    /// <summary>
    /// 更新交易帐户路由组设定
    /// </summary>
    public class UpdateAccountRouterGroup : AbstractMenuCommand
    {
        public override void Run()
        {
            AccountItem account = null;
            if (!AccountMoniterHelper.GetCurrentAccount(this.Owner, out account))
            {
                return;
            }

            fmChangeRouter fm = new fmChangeRouter();
            fm.SetAccount(account);
            fm.ShowDialog();
            fm.Close();
        }
    }

    /// <summary>
    /// 删除交易帐户
    /// </summary>
    public class DelAccountCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            ctAccountMontier accountMoniter = (ctAccountMontier)this.Owner;
            AccountItem acct = accountMoniter.CurrentAccount;
            if (acct == null)
            {
                MoniterHelper.WindowMessage("请选择交易帐户");
                return;
            }

            if (!CoreService.SiteInfo.Manager.IsRoot() && !CoreService.SiteInfo.Permission.r_account_del)
            {
                MoniterHelper.WindowMessage("无权限");
                return;
            }


            if (MoniterHelper.WindowConfirm("确认删除交易帐户?") == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqDelAccount(acct.Account);
            }



        }

    }

    /// <summary>
    /// 注销交易客户端
    /// </summary>
    public class ClearAccountTerminal : AbstractCommand
    {
        public override void Run()
        {
            AccountItem account = null;
            if (!AccountMoniterHelper.GetCurrentAccount(this.Owner, out account))
            {
                return;
            }

            if (MoniterHelper.WindowConfirm(string.Format("确认注销交易帐户[{0}]的所有登入交易终端?", account.Account)) == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqClearTerminals(account.Account);
            }
        }
    }


}
