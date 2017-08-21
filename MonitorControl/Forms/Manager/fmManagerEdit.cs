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
    public partial class fmManagerEdit : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmManagerEdit()
        {
            InitializeComponent();
            MoniterHelper.AdapterToIDataSource(type).BindDataSource(MoniterHelper.GetManagerTypeCBList());
            MoniterHelper.AdapterToIDataSource(agentType).BindDataSource(MoniterHelper.GetEnumValueObjects<EnumAgentType>());
            type.SelectedValueChanged += new EventHandler(type_SelectedValueChanged);
            CoreService.EventCore.RegIEventHandler(this);

            //this.type.Enabled = false;
            agentBox.Visible = false;
        }

        public void OnInit()
        {
            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_MANAGER_PROFILE, OnQryManagerProfile);

        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_MANAGER, OnQryManagerProfile);
        }

        void type_SelectedValueChanged(object sender, EventArgs e)
        {
            QSEnumManagerType t = (QSEnumManagerType)type.SelectedValue;
            agentBox.Visible = t == QSEnumManagerType.AGENT;
        }


        System.Text.RegularExpressions.Regex reglogin = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");

        ManagerSetting _manger = null;
        public void SetManger(ManagerSetting mgr)
        {
            this.login.Enabled = false;//登入用户名不可编辑
            this.type.Enabled = false;//管理员类别不可修改
            this.agentBox.Visible = false;
            this.agentType.Enabled = false;
            
            _manger = mgr;
            this.Text = string.Format("编辑管理员:{0} 分区:{1}",_manger.Login,_manger.domain_id);
            this.login.Text = _manger.Login;
            this.type.SelectedValue = _manger.Type;
            if (_manger.Type == QSEnumManagerType.AGENT)
            {
                agentBox.Visible = true;
                this.acclimit.Value = _manger.AccLimit;
                this.agentlimit.Value = _manger.AgentLimit;

            }

            CoreService.TLClient.ReqQryManagerProfile(mgr.ID);
        }

        void OnQryManagerProfile(string json, bool islast)
        {
            ManagerProfile profile = CoreService.ParseJsonResponse<ManagerProfile>(json);
            if (profile != null)
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<string, bool>(OnQryManagerProfile), new object[] { json, islast });
                }
                else
                {
                    name.Text = profile.Name;
                    mobile.Text = profile.Mobile;
                    qq.Text = profile.QQ;
                    email.Text = profile.Email;
                    idcard.Text = profile.IDCard;
                    memo.Text = profile.Memo;

                    ctBankList.BankSelected = profile.Bank_ID;
                    bankac.Text = profile.BankAC;
                    branch.Text = profile.Branch;
                }
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!CoreService.SiteInfo.Manager.RightAddManager())
            {
                MoniterHelper.WindowMessage("没有添加柜员的权限");
                return;
            }

            var login = this.login.Text;
            var mgr_type = (QSEnumManagerType)type.SelectedValue;

            var mgr_fk = 0;

            if (_manger == null)
            {
                if (string.IsNullOrEmpty(login))
                {
                    ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请输入登入名");
                    return;
                }
                if (!InputReg.Login.IsMatch(login))
                {
                    ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("登入ID只能包含数字和字母,-");
                    return;
                }
                if (login.Length > 12)
                {
                    ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("登入名长度不能大于12位");
                    return;
                }

                //如果添加代理则mgr_fk=0
                if (CoreService.SiteInfo.Manager.Type == QSEnumManagerType.ROOT)
                {
                    if (mgr_type == QSEnumManagerType.AGENT)
                    {
                        mgr_fk = 0;
                    }
                }

                //如果当前操作员是代理 添加员工账户 则制定mgr_fk
                if (CoreService.SiteInfo.Manager.Type == QSEnumManagerType.AGENT)
                {
                    mgr_fk = CoreService.SiteInfo.Manager.mgr_fk;
                }


                var obj = new 
                {
                    id=0,
                    login = login,
                    mgr_type = mgr_type,
                    acc_limit = (int)this.acclimit.Value,
                    agent_limit = (int)this.agentlimit.Value,
                    mgr_fk = mgr_fk,
                    agent_type = (EnumAgentType)agentType.SelectedValue,

                    name = name.Text,
                    mobile = mobile.Text,
                    qq = qq.Text,
                    email = email.Text,
                    memo=memo.Text,
                    idcard = idcard.Text,
                    bank = ctBankList.BankSelected,
                    branch = branch.Text,
                    bankac = bankac.Text,
                    
                };

                if (MoniterHelper.WindowConfirm("确认添加管理员信息?") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqUpdateManager(obj);
                }
            }
            else
            {
                var obj = new
                {
                    id = _manger.ID,
                    login = _manger.Login,
                    mgr_type = _manger.Type,
                    acc_limit = (int)this.acclimit.Value,
                    agent_limit = (int)this.agentlimit.Value,
                    mgr_fk = mgr_fk,
                    agent_type = (EnumAgentType)agentType.SelectedValue,

                    name = name.Text,
                    mobile = mobile.Text,
                    qq = qq.Text,
                    email = email.Text,
                    memo = memo.Text,
                    idcard = idcard.Text,
                    bank = ctBankList.BankSelected,
                    branch = branch.Text,
                    bankac = bankac.Text

                };


                if (MoniterHelper.WindowConfirm("确认更新管理员信息?") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqUpdateManager(obj);
                }

            }
            this.Close();
        }

    }
}
