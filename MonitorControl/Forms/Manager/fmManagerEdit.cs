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
    public partial class fmManagerEdit : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmManagerEdit()
        {
            InitializeComponent();
            MoniterHelper.AdapterToIDataSource(type).BindDataSource(MoniterHelper.GetManagerTypeCBList());
            MoniterHelper.AdapterToIDataSource(agentType).BindDataSource(MoniterHelper.GetEnumValueObjects<EnumAgentType>());
            type.SelectedValueChanged += new EventHandler(type_SelectedValueChanged);
            
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
            this.agentBox.Visible = false;
            this.agentType.Visible = false;

            _manger = mgr;
            this.Text = string.Format("编辑管理员:{0}({1})-{2} 分区:{3}",_manger.Login,_manger.Name,_manger.ID,_manger.domain_id);
            this.login.Text = _manger.Login;
            this.name.Text = _manger.Name;
            this.mobile.Text = _manger.Mobile;
            this.qq.Text = _manger.QQ;

           

            if (_manger.Type == QSEnumManagerType.AGENT)
            {
                agentBox.Visible = true;
                this.acclimit.Value = _manger.AccLimit;
                this.agentlimit.Value = _manger.AgentLimit;

            }
            
            
            //this.acclimit.Enabled = false;
            // this.agentlimit.Enabled = false;

            //如果是代理商则可以修改帐户数量限制 同时设定限制为自己的限制 给代理的客户数量不能超过过自己的限制
            //if (_manger.Type == QSEnumManagerType.AGENT)
            //{
            //    this.acclimit.Enabled = true;
            //    this.acclimit.Maximum = CoreService.SiteInfo.Manager.AccLimit;
            //    this.agentlimit.Enabled = true;
            //    this.agentlimit.Maximum = CoreService.SiteInfo.Manager.AgentLimit;
            //}
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!CoreService.SiteInfo.Manager.RightAddManager())
            {
                MoniterHelper.WindowMessage("没有添加柜员的权限");
                return;
            }

            var mgr_type = (QSEnumManagerType)type.SelectedValue;
            var login = this.login.Text;

            var name = this.name.Text;
            var mobile = this.mobile.Text;
            var qq = this.qq.Text;
            var acc_limit = (int)this.acclimit.Value;
            var agent_limit = (int)this.agentlimit.Value;
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
                    name = name,
                    mobile = mobile,
                    qq = qq,
                    acc_limit = acc_limit,
                    agent_limit = agent_limit,
                    mgr_fk = mgr_fk,
                    agent_type = (EnumAgentType)agentType.SelectedValue,
                    
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

                    name = name,
                    mobile = mobile,
                    qq = qq,
                    acc_limit = acc_limit,
                    agent_limit = agent_limit,
                    mgr_fk = mgr_fk,
                    agent_type = (EnumAgentType)agentType.SelectedValue,

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
