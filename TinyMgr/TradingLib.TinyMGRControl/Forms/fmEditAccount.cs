using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.Protocol;
using TradingLib.MoniterCore;


namespace TradingLib.TinyMGRControl
{
    public partial class fmEditAccount : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmEditAccount()
        {
            InitializeComponent();

            this.Load += new EventHandler(fmEditAccount_Load);
            btnChangePassword.Click += new EventHandler(btnChangePassword_Click);
            btnUpdateProfile.Click += new EventHandler(btnUpdateProfile_Click);
        }

        void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            AccountProfile profile = new AccountProfile();
            profile.Account = account.Account;
            profile.IDCard = idcard.Text;
            profile.Mobile = mobile.Text;
            profile.Name = name.Text;
            if (MoniterHelper.WindowConfirm("确认更新个人信息") == System.Windows.Forms.DialogResult.Yes)
            {
                ReqUpdateAccountProfile(profile);
            }
        }

        /// <summary>
        /// 查询个人信息
        /// </summary>
        /// <param name="account"></param>
        void ReqQryAccountProfile(string account)
        {
            CoreService.TLClient.ReqContribRequest("AccountManager", "QryAccountProfile", account);
        }

        /// <summary>
        /// 更新个人信息
        /// </summary>
        /// <param name="account"></param>
        void ReqUpdateAccountProfile(AccountProfile profile)
        {
            CoreService.TLClient.ReqContribRequest("AccountManager", "UpdateAccountProfile", profile);
        }

        void fmEditAccount_Load(object sender, EventArgs e)
        {
            CoreService.EventCore.RegIEventHandler(this);
        }

        AccountLite account = null;
        public void SetAccount(AccountLite acc)
        {
            account = acc;
        }

        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback("AccountManager", "QryAccountLoginInfo", this.OnLoginInfo);

            CoreService.EventContrib.RegisterCallback("AccountManager", "QryAccountProfile", this.OnQryAccountProfile);
            

            CoreService.TLClient.ReqQryAccountLoginInfo(account.Account);
            this.ReqQryAccountProfile(account.Account);
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("AccountManager", "QryAccountLoginInfo", this.OnLoginInfo);
            CoreService.EventContrib.UnRegisterCallback("AccountManager", "QryAccountProfile", this.OnQryAccountProfile);
            
        }

        void OnLoginInfo(string json, bool islast)
        {
            LoginInfo info = CoreService.ParseJsonResponse<LoginInfo>(json);
            if (json != null)
            {
                lbCurrentPass.Text = info.Pass;
            }
        }

        void OnQryAccountProfile(string json, bool islast)
        {
            AccountProfile profile = CoreService.ParseJsonResponse<AccountProfile>(json);
            if (profile != null && profile.Account.Equals(account.Account))
            {
                idcard.Text = profile.IDCard;
                mobile.Text = profile.Mobile;
                name.Text = profile.Name;
            }
        }


        void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (account == null)
            {
                MoniterHelper.WindowMessage("请选中账号");
            }


            if (newpass.Text.Contains(",") || newpass.Text.Contains("|") || newpass.Text.Contains("^"))
            {

                MoniterHelper.WindowMessage("密码不能含有系统保留字符 | , ^");
                return;
            }
            if (newpass.Text.Length < 4)
            {
                MoniterHelper.WindowMessage("密码长度不能小于4位");
                return;
            }
            if (newpass.Text.Length > 10)
            {
                MoniterHelper.WindowMessage("密码不能大于10位");
                return;
            }

            CoreService.TLClient.ReqChangeAccountPass(account.Account, newpass.Text);
        }


    }
}
