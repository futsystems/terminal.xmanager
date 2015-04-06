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

    internal class AccountMoniterHelper
    {
        public static bool GetCurrentAccount(object obj, out AccountLite acct)
        {
            acct = null;
            ctAccountMontier accountMoniter = obj as ctAccountMontier;
            if (accountMoniter == null) return false;

            string account = accountMoniter.CurrentAccount;
            acct = CoreService.BasicInfoTracker.GetAccount(account);
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
    /// 修改交易帐户密码
    /// </summary>
    public class ChangeAccountPasswordCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            AccountLite account = null;
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
    /// 修改投资者信息
    /// </summary>
    public class ChangeInvestorInfoCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            AccountLite account = null;
            if (!AccountMoniterHelper.GetCurrentAccount(this.Owner, out account))
            {
                return;
            }
            fmChangeInvestor fm = new fmChangeInvestor();
            fm.SetAccount(account);
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
            AccountLite account = null;
            if (!AccountMoniterHelper.GetCurrentAccount(this.Owner, out account))
            {
                return;
            }
            fmEditFinAccount fm = new fmEditFinAccount();
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
            AccountLite account = null;
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

    /// <summary>
    /// 更新主帐户强平规则
    /// </summary>
    public class UpdateFlatRuleCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            AccountLite account = null;
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
    /// 查询交易密码
    /// </summary>
    public class QryAccountPasswordCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            AccountLite account = null;
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
            AccountLite account = null;
            if (!AccountMoniterHelper.GetCurrentAccount(this.Owner, out account))
            {
                return;
            }

            fmChangeRouter fm = new fmChangeRouter();
            fm.SetAccount(account);
            fm.Show();
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
            string account = accountMoniter.CurrentAccount;
            AccountLite acct = CoreService.BasicInfoTracker.GetAccount(account);
            if (acct == null)
            {
                MoniterHelper.WindowMessage("请选择交易帐户");
                return;
            }
            if (MoniterHelper.WindowConfirm("确认删除交易帐户?") == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqDelAccount(acct.Account);
                acct.Deleted = true;//修改删除标识
                accountMoniter.GotAccountChanged(acct);
                
                //RefreshAccountQuery();//刷新表格

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
            AccountLite account = null;
            if (!AccountMoniterHelper.GetCurrentAccount(this.Owner, out account))
            {
                return;
            }
            if (MoniterHelper.WindowConfirm("确认从帐户:" + account.Account + "解绑主帐户?") == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqDelAccountConnector(account.Account);
            }
        }
    }

    /// <summary>
    /// 更新交易帐户与主帐户的绑定关系
    /// </summary>
    public class UpdateAccountConnectorCommand : AbstractCommand
    {
        public override void Run()
        {
            AccountLite account = null;
            if (!AccountMoniterHelper.GetCurrentAccount(this.Owner, out account))
            {
                return;
            }

            fmBindConnector fm = new fmBindConnector();
            fm.SetAccount(account);
            fm.ShowDialog();
            fm.Close();
        }
    }

    /// <summary>
    /// 重新从CTP接口同步主帐户交易数据
    /// </summary>
    public class SyncAccountDataCommand : AbstractCommand
    {
        public override void Run()
        {

            AccountLite account = null;
            if (!AccountMoniterHelper.GetCurrentAccount(this.Owner, out account))
            {
                return;
            }

            CoreService.EventAccount.FireSyncAccountEvent(account);//触发同步事件，

            CoreService.TLClient.ReqSyncData(account.Account);
        }
    }

    /// <summary>
    /// 对应主帐户管理界面
    /// </summary>
    public class MotherAccountManagerCommand : AbstractCommand
    {
        public override void Run()
        {
            AccountLite account = null;
            if (!AccountMoniterHelper.GetCurrentAccount(this.Owner, out account))
            {
                return;
            }

            fmMainAccountManager fm = new fmMainAccountManager();
            fm.SetAccount(account);
            fm.ShowDialog();
            fm.Close();
        }
    }


    /// <summary>
    /// 注销交易客户端
    /// </summary>
    public class ClearAccountTerminal : AbstractCommand
    {
        public override void Run()
        {
            AccountLite account = null;
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
    /// 过来交易帐户
    /// </summary>
    public class FilterAccountCommand : AbstractMenuCommand
    {
        public override void Run()
        {
        //    fmAcctFilter fm = 
        //    fm.FilterArgsChangedEvent += new VoidDelegate(fm_FilterArgsChangedEvent);
        //    fm.Show();
            fmAcctFilter.Instance.Show();
        }

        void fm_FilterArgsChangedEvent()
        {
            LogService.Debug("filter args changed");
        }
    }

}
