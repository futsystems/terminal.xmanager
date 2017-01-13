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

            //if (CoreService.SiteInfo.ProductType == QSEnumProductType.VendorMoniter)
            //{
            //    fmCashOperation fm = new fmCashOperation();
            //    fm.SetAccount(account);
            //    fm.ShowDialog();
            //    fm.Close();
            //}
            if (CoreService.SiteInfo.ProductType == QSEnumProductType.CounterSystem)
            {
                if (!CoreService.SiteInfo.Manager.IsRoot() && !CoreService.SiteInfo.UIAccess.r_cashop)
                {
                    MoniterHelper.WindowMessage("无权限");
                    return;
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

            if (!CoreService.SiteInfo.Manager.IsRoot() && !CoreService.SiteInfo.UIAccess.r_block)
            {
                MoniterHelper.WindowMessage("无权限");
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

            if (!CoreService.SiteInfo.Manager.IsRoot() && !CoreService.SiteInfo.UIAccess.r_block)
            {
                MoniterHelper.WindowMessage("无权限");
                return;
            }

            if (MoniterHelper.WindowConfirm(string.Format("确认冻结交易帐户:{0}?", account.Account)) == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqUpdateAccountExecute(account.Account,false);
            }
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
            if (!CoreService.SiteInfo.Manager.IsRoot() && !CoreService.SiteInfo.UIAccess.r_account_info)
            {
                MoniterHelper.WindowMessage("无权限");
                return;
            }

            fmChangePassword fm = new fmChangePassword();
            fm.SetAccount(account.Account);
            fm.ShowDialog();
            fm.Close();
        }

    }

    /// <summary>
    /// 修改投资者信息
    /// </summary>
    public class ChangeInvestorInfoCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            //AccountItem account = null;
            //if (!AccountMoniterHelper.GetCurrentAccount(this.Owner, out account))
            //{
            //    return;
            //}

            //if (!CoreService.SiteInfo.Manager.IsRoot() && !CoreService.SiteInfo.UIAccess.r_account_info)
            //{
            //    MoniterHelper.WindowMessage("无权限");
            //    return;
            //}

            //fmChangeInvestor fm = new fmChangeInvestor();
            //fm.SetAccount(account);
            //fm.ShowDialog();
            //fm.Close();
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

            if (!CoreService.SiteInfo.Manager.IsRoot() && !CoreService.SiteInfo.UIAccess.r_account_info)
            {
                MoniterHelper.WindowMessage("无权限");
                return;
            }


            fmEditAccount fm = new fmEditAccount();
            fm.SetAccount(account);
            fm.ShowDialog();
            fm.Close();
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

            if (!CoreService.SiteInfo.Manager.IsRoot() && !CoreService.SiteInfo.UIAccess.r_riskrule)
            {
                MoniterHelper.WindowMessage("无权限");
                return;
            }


            fmEditRiskRule fm = new fmEditRiskRule();
            fm.SetAccount(account);
            fm.ShowDialog();
            fm.Close();
        }
    }
    /// <summary>
    /// 更新主帐户强平规则
    /// </summary>
    public class UpdateFlatRuleCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            AccountItem account = null;
            if (!AccountMoniterHelper.GetCurrentAccount(this.Owner, out account))
            {
                return;
            }
            fmEditVendorFlatRule fm = new fmEditVendorFlatRule();
            fm.SetAccount(account);
            fm.ShowDialog();
            fm.Close();
        }
    }

    /// <summary>
    /// 更新配资服务
    /// </summary>
    public class UpdateFinServiceCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            AccountItem account = null;
            if (!AccountMoniterHelper.GetCurrentAccount(this.Owner, out account))
            {
                return;
            }
            fmEditService fm = new fmEditService();
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

            if (!CoreService.SiteInfo.Manager.IsRoot() && !CoreService.SiteInfo.UIAccess.r_account_del)
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
    /// 删除交易帐户与主帐户的绑定关系
    /// </summary>
    public class DelAccountConnectorCommand : AbstractCommand
    {
        public override void Run()
        {
            //AccountItem account = null;
            //if (!AccountMoniterHelper.GetCurrentAccount(this.Owner, out account))
            //{
            //    return;
            //}
            //if (MoniterHelper.WindowConfirm("确认从帐户:" + account.Account + "解绑主帐户?") == System.Windows.Forms.DialogResult.Yes)
            //{
            //    CoreService.TLClient.ReqDelAccountConnector(account.Account);
            //}
        }
    }

    /// <summary>
    /// 更新交易帐户与主帐户的绑定关系
    /// </summary>
    public class UpdateAccountConnectorCommand : AbstractCommand
    {
        public override void Run()
        {
            //AccountItem account = null;
            //if (!AccountMoniterHelper.GetCurrentAccount(this.Owner, out account))
            //{
            //    return;
            //}

            //fmBindConnector fm = new fmBindConnector();
            //fm.SetAccount(account);
            //fm.ShowDialog();
            //fm.Close();
        }
    }

    /// <summary>
    /// 重新从CTP接口同步主帐户交易数据
    /// </summary>
    public class SyncAccountDataCommand : AbstractCommand
    {
        public override void Run()
        {

            //AccountItem account = null;
            //if (!AccountMoniterHelper.GetCurrentAccount(this.Owner, out account))
            //{
            //    return;
            //}

            //CoreService.EventAccount.FireSyncAccountEvent(account);//触发同步事件，

            //CoreService.TLClient.ReqSyncData(account.Account);
        }
    }

    /// <summary>
    /// 对应主帐户管理界面
    /// </summary>
    public class MotherAccountManagerCommand : AbstractCommand
    {
        public override void Run()
        {
            //AccountItem account = null;
            //if (!AccountMoniterHelper.GetCurrentAccount(this.Owner, out account))
            //{
            //    return;
            //}

            //fmMainAccountManager fm = new fmMainAccountManager();
            //fm.SetAccount(account);
            //fm.ShowDialog();
            //fm.Close();
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

    /// <summary>
    /// 过滤交易帐户
    /// </summary>
    public class FilterAccountCommand : AbstractMenuCommand
    {
        public override void Run()
        {
        //    fmAcctFilter fm = 
        //    fm.FilterArgsChangedEvent += new VoidDelegate(fm_FilterArgsChangedEvent);
        //    fm.Show();
            //fmAcctFilter.Instance.Show();
        }

        void fm_FilterArgsChangedEvent()
        {
            LogService.Debug("filter args changed");
        }
    }

}
