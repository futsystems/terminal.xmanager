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

namespace TradingLib.MoniterControl
{
    public partial class fmLoginInfo : ComponentFactory.Krypton.Toolkit.KryptonForm, IEventBinder
    {
        public fmLoginInfo()
        {
            InitializeComponent();
            this.Load += new EventHandler(fmDomainRootLoginInfo_Load);
        }

        void fmDomainRootLoginInfo_Load(object sender, EventArgs e)
        {
            CoreService.EventCore.RegIEventHandler(this);
        }

        public void OnInit()
        {
            if (_domain != null)
            {
                CoreService.EventContrib.RegisterCallback(Modules.MGR_EXCH,Method_MGR_EXCH.QRY_DOMAIN_LOGININFO, this.OnLoginInfo);
                CoreService.TLClient.ReqQryDomainRootLoginInfo(_domain.ID);
            }

            if (_manager != null)
            {
                CoreService.EventContrib.RegisterCallback(Modules.MGR_EXCH, "QryManagerLoginInfo", this.OnLoginInfo);
                CoreService.TLClient.ReqQryManagerLoginInfo(_manager.ID);
            }

            if (_account != null)
            {
                CoreService.EventContrib.RegisterCallback(Modules.ACC_MGR,Method_ACC_MGR.QRY_LOGIN_INFO, this.OnLoginInfo);
                CoreService.TLClient.ReqQryAccountLoginInfo(_account.Account);
            }

        }

        public void OnDisposed()
        {
            if (_domain != null)
            {
                CoreService.EventContrib.UnRegisterCallback("MgrExchServer", "QryDomainRootLoginInfo", this.OnLoginInfo);
            }
            if (_manager != null)
            {
                CoreService.EventContrib.UnRegisterCallback("MgrExchServer", "QryManagerLoginInfo", this.OnLoginInfo);
            }

            if (_account != null)
            {
                CoreService.EventContrib.UnRegisterCallback(Modules.ACC_MGR, Method_ACC_MGR.QRY_LOGIN_INFO, this.OnLoginInfo);
            }
        }

        void OnLoginInfo(string json, bool islast)
        {
            LoginInfo info = CoreService.ParseJsonResponse<LoginInfo>(json);
            if (json != null)
            {
                loginname.Text = info.LoginID;
                password.Text = info.Pass;
            }
        }
        DomainImpl _domain = null;
        public void SetDomain(DomainImpl domain)
        {
            _domain = domain;
        }

        ManagerSetting _manager = null;

        public void SetManager(ManagerSetting manger)
        {
            _manager = manger;
        }

        AccountItem _account = null;
        public void SetAccount(AccountItem account)
        {
            _account = account;
        }
    }
}
