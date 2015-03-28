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
using TradingLib.MoniterCore;


namespace TradingLib.MoniterControl
{
    public partial class fmEditFinAccount : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmEditFinAccount()
        {
            InitializeComponent();
            this.Load += new EventHandler(fmAddFinAccount_Load);
        }

        void fmAddFinAccount_Load(object sender, EventArgs e)
        {
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
            CoreService.EventCore.RegIEventHandler(this);

        }

        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback("AccountManager", "QryAccountProfile", this.OnQryAccountProfile);
            
            //如果设定了交易帐户 则表明是编辑/查询个人信息
            if (_account != null)
            {
                this.ReqQryAccountProfile(_account.Account);
            }
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("AccountManager", "QryAccountProfile", this.OnQryAccountProfile);
            
        }

        void OnQryAccountProfile(string json, bool islast)
        {
            AccountProfile profile = MoniterHelper.ParseJsonResponse<AccountProfile>(json);
            if (profile != null && profile.Account.Equals(_account.Account))
            {
                account.Text = profile.Account;
                ctBankList1.BankSelected = profile.Bank_ID;
                bankac.Text = profile.BankAC;
                branch.Text = profile.Branch;
                email.Text = profile.Email;
                idcard.Text = profile.IDCard;
                mobile.Text = profile.Mobile;
                name.Text = profile.Name;
                qq.Text = profile.QQ;

            }
        }
        AccountLite _account=null;
        public void SetAccount(AccountLite acc)
        {
            _account = acc;
            this.Text = string.Format("查看/编辑帐户[{0}]个人信息", _account.Account);
            account.Enabled = false;
        }


        void btnSubmit_Click(object sender, EventArgs e)
        {
            AccountProfile profile = new AccountProfile();
            profile.Account = account.Text;
            profile.Bank_ID = ctBankList1.BankSelected;
            profile.BankAC = bankac.Text;
            profile.Branch = branch.Text;
            profile.Email = email.Text;
            profile.IDCard = idcard.Text;
            profile.Mobile = mobile.Text;
            profile.Name = name.Text;
            profile.QQ = qq.Text;

            //这里加入数据验证和检查
            if (_account == null)
            {
                if (MoniterHelper.WindowConfirm("请确认个人信息填写准确") == System.Windows.Forms.DialogResult.Yes)
                {
                    ReqAddFinServiceAccount(profile);
                    this.Close();
                }
            }
            else
            {
                if (MoniterHelper.WindowConfirm("确认更新个人信息") == System.Windows.Forms.DialogResult.Yes)
                {
                    ReqUpdateAccountProfile(profile);
                    this.Close();
                }
            }
        }

        /// <summary>
        /// 请求添加配资客户
        /// </summary>
        /// <param name="profile"></param>
        void ReqAddFinServiceAccount(AccountProfile profile)
        {
            CoreService.TLClient.ReqContribRequest("AccountManager", "AddFinServiceAccount", profile);
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
    }
}
