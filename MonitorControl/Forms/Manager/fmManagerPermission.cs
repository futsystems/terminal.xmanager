using System;
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
using TradingLib.Mixins.JsonObject;
using TradingLib.MoniterCore;


namespace TradingLib.MoniterControl
{
    public partial class fmManagerPermission : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmManagerPermission()
        {
            InitializeComponent();

            this.Load += new EventHandler(fmAgentPermission_Load);
            
        }

        ManagerSetting _manager = null;
        public void SetManager(ManagerSetting manager)
        {
            _manager = manager;
            lbManager.Text = string.Format("{0}-管理域:{1}",_manager.Login, _manager.mgr_fk);
            
        }
        void fmAgentPermission_Load(object sender, EventArgs e)
        {
            
            pmlist.SelectedIndexChanged += new EventHandler(pmlist_SelectedIndexChanged);
            btnSubmit.Click += new EventHandler(btnSubmit_Click);

            CoreService.EventCore.RegIEventHandler(this);
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm("确认更新的代理的权限模板?") == System.Windows.Forms.DialogResult.Yes)
            {
                int idx = int.Parse(pmlist.SelectedValue.ToString());
                CoreService.TLClient.ReqUpdateAgentPermission(_manager.ID, idx);
            }
        }

        void pmlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loaded) return;
            int idx = int.Parse(pmlist.SelectedValue.ToString());
        }

        //void ctAgentList1_AgentSelectedChangedEvent()
        //{
        //    if (!_loaded) return;
        //    CoreService.TLClient.ReqQryAgentPermission(ctAgentList3.CurrentAgentFK);
        //}

        public void OnInit()
        {

            CoreService.EventCore.RegisterCallback("MgrExchServer", "QueryPermmissionTemplate", OnPermissionTemplate);
            CoreService.EventCore.RegisterCallback("MgrExchServer", "QueryAgentPermission", OnAgentPermission);
            CoreService.EventCore.RegisterNotifyCallback("MgrExchServer", "NotifyAgentPermission", OnNotifyAgentPermission);

            CoreService.TLClient.ReqQryPermmissionTemplateList();
            CoreService.TLClient.ReqQryAgentPermission(_manager.ID);
        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback("MgrExchServer", "QueryPermmissionTemplate", OnPermissionTemplate);
            CoreService.EventCore.UnRegisterCallback("MgrExchServer", "QueryAgentPermission", OnAgentPermission);
            CoreService.EventCore.UnRegisterNotifyCallback("MgrExchServer", "NotifyAgentPermission", OnNotifyAgentPermission);
        }


        void OnNotifyAgentPermission(string jsonstr)
        {
            UIAccess obj = CoreService.ParseJsonResponse<UIAccess>(jsonstr);
            if (obj != null)
            {
                pmcurrent.Text = obj.name;
            }
            else
            {
                pmcurrent.Text = "未设置";
            }
        }
        void OnAgentPermission(string jsonstr, bool islast)
        {

            UIAccess obj = CoreService.ParseJsonResponse<UIAccess>(jsonstr);
            if (obj != null)
            {   
                pmcurrent.Text = obj.name;
            }
            else
            {
                pmcurrent.Text = "未设置";
            }
        }

        bool _loaded = false;
        void OnPermissionTemplate(string jsonstr, bool islast)
        {
            UIAccess[] objs = CoreService.ParseJsonResponse<UIAccess[]>(jsonstr);
            if (objs!= null)
            {
                foreach (UIAccess access in objs)
                {
                    GotUIAccess(access);
                }
                MoniterHelper.AdapterToIDataSource(pmlist).BindDataSource(GetPermissionTemplateListCB());
                _loaded = true;
            }
        }

        Dictionary<int, UIAccess> accessmap = new Dictionary<int, UIAccess>();
        void GotUIAccess(UIAccess access)
        {
            if (!accessmap.Keys.Contains(access.id))
            {
                accessmap.Add(access.id, access);
            }
            else
            {
                accessmap[access.id] = access;
            }
            
        }


        ArrayList GetPermissionTemplateListCB()
        {
            ArrayList list = new ArrayList();

            foreach (UIAccess access in accessmap.Values)
            {
                ValueObject<int> vo = new ValueObject<int>();
                vo.Name = access.id.ToString() + "-" + access.name;

                vo.Value = access.id;
                list.Add(vo);
            }
            return list;
        }
    }
}
