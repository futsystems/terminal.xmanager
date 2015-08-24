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
using TradingLib.MoniterCore;

namespace TradingLib.MoniterControl
{
    public partial class fmRouterItemEdit : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmRouterItemEdit()
        {
            InitializeComponent();

            MoniterHelper.AdapterToIDataSource(cbPriority).BindDataSource(GetPriorityCBList());
            this.Load += new EventHandler(fmRouterItemEdit_Load);
        }

        void fmRouterItemEdit_Load(object sender, EventArgs e)
        {
            this.btnPlace.Click += new EventHandler(btnPlace_Click);
            CoreService.EventCore.RegIEventHandler(this);
        }

        void btnPlace_Click(object sender, EventArgs e)
        {
            if (_item == null)
            {
                //int vendorid = int.Parse(cbvendor.SelectedValue.ToString());
                RouterItemSetting item = new RouterItemSetting();
                item.Active = acceptEntry.Checked;

                item.priority = (int)cbPriority.SelectedValue;
                item.Connector_ID = ((ConnectorConfig)cbConnector.SelectedValue).ID;
                item.MarginLimit = marginlimit.Value;
                item.routegroup_id = _rg.ID;
                
                CoreService.TLClient.ReqUpdateRouterItem(item);
                this.Close();
            }
            else
            {
                //int vendorid = int.Parse(cbvendor.SelectedValue.ToString());
                //_routeritem.vendor_id = vendorid;
                //_routeritem.routegroup_id = _group.ID;
                //_routeritem.rule = rule.Text;
                //_routeritem.Active = active.Checked;
                //_routeritem.priority = (int)cbpriority.SelectedValue;
                _item.priority = (int)cbPriority.SelectedValue;
                _item.Active = acceptEntry.Checked;
                _item.MarginLimit = marginlimit.Value;


                CoreService.TLClient.ReqUpdateRouterItem(_item);
                this.Close();
            }
        }

        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback("ConnectorManager", "QryConnectorNotInGroup", OnQryConnectorNotInGroup);
            if (_item == null)
            {
                CoreService.TLClient.ReqContribRequest("ConnectorManager", "QryConnectorNotInGroup", "");
            }
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("ConnectorManager", "QryConnectorNotInGroup", OnQryConnectorNotInGroup);
      
        }

        Dictionary<int, ConnectorConfig> cfgmap = new Dictionary<int, ConnectorConfig>();
        void OnQryConnectorNotInGroup(string json, bool islast)
        {
            ConnectorConfig[] cfgs = MoniterHelper.ParseJsonResponse<ConnectorConfig[]>(json);
            if (cfgs != null)
            {
                foreach(var cfg in cfgs)
                {
                    cfgmap.Add(cfg.ID, cfg);
                }
            }
            if (islast)
            {
                if (cfgmap.Values.Count == 0)
                {
                    //this.btnPlace.Enabled = false;
                    MoniterHelper.WindowMessage("没有可用主帐户");
                    this.Close();
                    return;
                }
                ArrayList list = new ArrayList();
                foreach (var cfg in cfgmap.Values)
                {
                    ValueObject<ConnectorConfig> vo = new ValueObject<ConnectorConfig>();
                    vo.Name = cfg.Token;
                    vo.Value = cfg;
                    list.Add(vo);
                }
                MoniterHelper.AdapterToIDataSource(cbConnector).BindDataSource(list);

                
            }
        }



        ArrayList GetPriorityCBList()
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i <= 5; i++)
            {
                ValueObject<int> vo = new ValueObject<int>();
                vo.Name = string.Format("{0}级", i);
                vo.Value = i;
                list.Add(vo);
            }
            return list;
        }


        RouterGroupSetting _rg = null;
        public void SetRouterGroup(RouterGroupSetting rg)
        {
            _rg = rg;
        }

        RouterItemSetting _item = null;
        ConnectorConfig _cfg = null;
        public void SetRouterItem(RouterItemSetting item,ConnectorConfig cfg)
        {
            _item = item;
            _cfg = cfg;

            this.Text = string.Format("编辑主帐户:{0}规则",cfg.Token);
            marginlimit.Value = _item.MarginLimit;
            cbPriority.SelectedValue = _item.priority;
            acceptEntry.Checked = _item.Active;
            cbConnector.Text = _cfg.Token;
            cbConnector.Enabled = false;
        }
    }
}
