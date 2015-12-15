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
            this.type.Enabled = false;
        }


        System.Text.RegularExpressions.Regex reglogin = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");

        ManagerSetting _manger = null;
        public void SetManger(ManagerSetting mgr)
        {
            _manger = mgr;
            this.Text = string.Format("编辑管理员:{0}({1})-{2} 分区:{3}",_manger.Login,_manger.Name,_manger.ID,_manger.domain_id);
            this.login.Text = _manger.Login;
            this.name.Text = _manger.Name;
            this.mobile.Text = _manger.Mobile;
            this.qq.Text = _manger.QQ;
            this.acclimit.Value = _manger.AccLimit;
            this.agentlimit.Value = _manger.AgentLimit;
            
            this.login.Enabled = false;
            this.acclimit.Enabled = false;
            this.agentlimit.Enabled = false;

            //如果是代理商则可以修改帐户数量限制 同时设定限制为自己的限制 给代理的客户数量不能超过过自己的限制
            if (_manger.Type == QSEnumManagerType.AGENT)
            {
                this.acclimit.Enabled = true;
                this.acclimit.Maximum = CoreService.SiteInfo.Manager.AccLimit;
                this.agentlimit.Enabled = true;
                this.agentlimit.Maximum = CoreService.SiteInfo.Manager.AgentLimit;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!CoreService.SiteInfo.Manager.RightAddManager())
            {
                MoniterHelper.WindowMessage("没有添加柜员的权限");
                return;
            }
            if (_manger == null)
            {
                Manager m = new Manager();
                m.Type = (QSEnumManagerType)type.SelectedValue;
                m.Login = this.login.Text;
                if (string.IsNullOrEmpty(m.Login))
                {
                    ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请输入登入名");
                    return;
                }
                if (!InputReg.Login.IsMatch(m.Login))
                {
                    ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("登入ID只能包含数字和字母,-");
                    return;
                }
                if (m.Login.Length > 12)
                {
                    ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("登入名长度不能大于12位");
                    return;
                }
                m.Name = this.name.Text;
                m.Mobile = this.mobile.Text;
                m.QQ = this.qq.Text;
                m.AccLimit = (int)this.acclimit.Value;
                m.AgentLimit = (int)this.agentlimit.Value;

                if (CoreService.SiteInfo.Manager.Type == QSEnumManagerType.ROOT)
                {
                    if (m.Type == QSEnumManagerType.AGENT)//如果添加代理则mgr_fk=0
                    {
                        m.mgr_fk = 0;
                    }
                }
                if (CoreService.SiteInfo.Manager.Type == QSEnumManagerType.AGENT)
                {
                    m.mgr_fk = CoreService.SiteInfo.Manager.mgr_fk;
                }
                if (MoniterHelper.WindowConfirm("确认添加管理员信息?") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqUpdateManager(m);
                }
            }
            else
            {
                //更新
                _manger.Login = this.login.Text;
                _manger.Name = this.name.Text;
                _manger.Mobile = this.mobile.Text;
                _manger.QQ = this.qq.Text;

                if (_manger.Type == QSEnumManagerType.AGENT)
                {
                    _manger.AccLimit = (int)this.acclimit.Value;
                    _manger.AgentLimit = (int)this.agentlimit.Value;
                }
                if (MoniterHelper.WindowConfirm("确认更新管理员信息?") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqUpdateManager(_manger);
                }

            }
            this.Close();
        }
    }
}
