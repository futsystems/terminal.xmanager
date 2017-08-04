using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
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
    public partial class fmEditAgentDefaultConfigTemplate : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmEditAgentDefaultConfigTemplate()
        {
            InitializeComponent();
            this.Load += new EventHandler(fmEditAccountTemplate_Load);
        }

        void fmEditAccountTemplate_Load(object sender, EventArgs e)
        {
            btnSubmit.Click += new EventHandler(btnSubmit_Click);

            CoreService.EventCore.RegIEventHandler(this);
        }

        public void OnInit()
        {
            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_CONFIG_TEMPLATE, this.OnQrConfigTemplate);
            CoreService.TLClient.ReqQryConfigTemplate();

        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_CONFIG_TEMPLATE, this.OnQrConfigTemplate);
        }

        #region 响应模板查询 生成comboxlist
        bool AnyMathItem(ComponentFactory.Krypton.Toolkit.KryptonComboBox cb, int id)
        {
            foreach (var item in cb.Items)
            {
                if (((ValueObject<int>)item).Value == id)
                    return true;
            }
            return false;
        }

        List<ConfigTemplate> configlist = new List<ConfigTemplate>();
        void OnQrConfigTemplate(string json, bool islast)
        {
            ConfigTemplate item = CoreService.ParseJsonResponse<ConfigTemplate>(json);
            if (item != null)
            {
                configlist.Add(item);
            }
            if (islast)
            {
                MoniterHelper.AdapterToIDataSource(cbConfigTemplate).BindDataSource(GetConfigTemplateCBList(configlist));
                if (_account != null)
                {
                    int commissoinid = AnyMathItem(cbConfigTemplate, _account.Default_Config_ID) ? _account.Default_Config_ID : 0;
                    cbConfigTemplate.SelectedValue = commissoinid;
                }
            }
        }

        static ArrayList GetConfigTemplateCBList(List<ConfigTemplate> items)
        {
            ArrayList list = new ArrayList();
            ValueObject<int> vo1 = new ValueObject<int>();
            vo1.Name = "系统默认";
            vo1.Value = 0;
            list.Add(vo1);

            foreach (ConfigTemplate item in items)
            {
                ValueObject<int> vo = new ValueObject<int>();
                vo.Name = string.Format("{0}{1}", item.Name, item.Manager_ID != CoreService.SiteInfo.Manager.ID ? "*" : "");
                vo.Value = item.ID;
                list.Add(vo);
            }
            return list;
        }
        #endregion



        AgentSetting _account = null;
        public void SetAccount(AgentSetting account)
        {
            _account = account;
            this.Text = string.Format("编辑代理:{0}默认配置模板项", account.Account);

        }

        string[] _accountArray = null;
        public void SetAccount(string[] array)
        {
            _accountArray = array;
            this.Text = string.Format("批量编辑配置模板项");
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm("确认更新代理默认配置模板?") == System.Windows.Forms.DialogResult.Yes)
            {
                int configid = (int)cbConfigTemplate.SelectedValue;
                if (_account != null)
                {
                    CoreService.TLClient.ReqUpdateAgentDefaultConfigTemplate(_account.Account, configid, force.Checked);
                }
               
                this.Close();
            }
            
        }
    }
}
