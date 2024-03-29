﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;
using TradingLib.Mixins.JsonObject;

namespace TradingLib.MoniterControl
{
    public partial class fmDomainEdit : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmDomainEdit()
        {
            InitializeComponent();
            this.Load += new EventHandler(fmDomainEdit_Load);
            this.Text = "添加域分区";
        }

        void fmDomainEdit_Load(object sender, EventArgs e)
        {
            module_agent.CheckedChanged += new EventHandler(module_agent_CheckedChanged);
            router_live.CheckedChanged += new EventHandler(router_CheckedChanged);
            router_sim.CheckedChanged += new EventHandler(router_CheckedChanged);
            CoreService.EventCore.RegIEventHandler(this);
        }

        //路由选择变动事件
        void router_CheckedChanged(object sender, EventArgs e)
        {
            //实盘通道列表只有在实盘交易权限开启时可以选
            intfaceTab.Enabled = router_live.Checked;
        }

        void module_agent_CheckedChanged(object sender, EventArgs e)
        {
            module_subagent.Enabled = module_agent.Checked;
            if (!module_agent.Checked)
            {
                module_subagent.Checked = false;
            }
        }

        public void OnInit()
        {
            CoreService.EventCore.RegisterCallback(Modules.CONN_MGR, Method_CONN_MGR.QRY_INTERFACE, this.OnQryInterface);

            //请求操作可以放在OnInit调用内，这样所有回调注册或初始化操作均已经完成
            CoreService.TLClient.ReqQryInterface();

            //超管自身模块权限决定开设分区的最大模块集
            module_follow.Visible = CoreService.SiteInfo.Domain.Module_Follow;
            kryptonLabel21.Visible = CoreService.SiteInfo.Domain.Module_Follow;
            cfg_followstrategynum.Visible = CoreService.SiteInfo.Domain.Module_Follow;

            module_agent.Visible = CoreService.SiteInfo.Domain.Module_Agent;
            module_subagent.Visible = CoreService.SiteInfo.Domain.Module_SubAgent;
            module_payonline.Visible = CoreService.SiteInfo.Domain.Module_PayOnline;
        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback(Modules.CONN_MGR, Method_CONN_MGR.QRY_INTERFACE, this.OnQryInterface);
        }


        #region 底层成交接口
        ArrayList GetInterfaceList()
        {
            ArrayList list = new System.Collections.ArrayList();
            foreach (ConnectorInterface it in interfacemap.Values)
            {
                //不选择行情接口
                if (it.Type == QSEnumConnectorType.DataFeed)
                    continue;
                ValueObject<int> vo = new ValueObject<int>();
                vo.Name = it.Name;
                vo.Value = it.ID;
                list.Add(vo);
            }
            return list;
        }

        string GetInterfaceListString()
        {
            List<string> list = new List<string>();
            foreach(object obj in interfacelist.CheckedItems)
            {
                ValueObject<int> item = (ValueObject<int>)obj;
                list.Add(item.Value.ToString());
            }
            return string.Join(",", list.ToArray());
        }

        void SetInterfaceList(string list)
        {
            IEnumerable<int> clist = list.Split(',').Select(v => int.Parse(v));
            for(int i =0;i<interfacelist.Items.Count;i++)
            { 
                ValueObject<int> item = (ValueObject<int>)interfacelist.Items[i];
                if(clist.Contains(item.Value))
                {
                    interfacelist.SetItemChecked(i, true);
                }
            }
        }
        ConcurrentDictionary<int, ConnectorInterface> interfacemap = new ConcurrentDictionary<int, ConnectorInterface>();
        bool _gotinterface = false;

        void OnQryInterface(string jsonstr ,bool islast)
        {
            ConnectorInterface[] objs = CoreService.ParseJsonResponse<ConnectorInterface[]>(jsonstr);
            if (objs !=null)
            {
                foreach (ConnectorInterface op in objs)
                {
                    if (!interfacemap.Keys.Contains(op.ID))
                    {
                        interfacemap.TryAdd(op.ID, op);
                        //interfacelist.Items.Add(op.Name);
                    }
                }
                _gotinterface = true;
                
                MoniterHelper.AdapterToIDataSource(interfacelist).BindDataSource(GetInterfaceList());
                if(_domain != null)
                {
                    SetInterfaceList(_domain.InterfaceList);
                }
            }
            else//如果没有配资服
            {

            }
        }
        #endregion


        #region 配资服务计划
        //string GetSPListString()
        //{
        //    List<string> list = new List<string>();
        //    foreach (object obj in finsplist.CheckedItems)
        //    {
        //        ValueObject<int> item = (ValueObject<int>)obj;
        //        list.Add(item.Value.ToString());
        //    }
        //    return string.Join(",", list.ToArray());
        //}

        //void SetSPList(string list)
        //{
        //    IEnumerable<int> clist = list.Split(',').Select(v => int.Parse(v));

        //    for (int i = 0; i < finsplist.Items.Count; i++)
        //    {
        //        ValueObject<int> item = (ValueObject<int>)finsplist.Items[i];
        //        if (clist.Contains(item.Value))
        //        {
        //            finsplist.SetItemChecked(i, true);
        //        }
        //    }
        //}

        //ArrayList GetSPList()
        //{
        //    ArrayList list = new System.Collections.ArrayList();
        //    foreach (JsonWrapperServicePlane it in servicePlanMap.Values)
        //    {
        //        ValueObject<int> vo = new ValueObject<int>();
        //        vo.Name = string.Format("{0}[{1}]", it.Title, it.Name);
        //        vo.Value = it.ID;
        //        list.Add(vo);
        //    }
        //    return list;
        //}

        //ConcurrentDictionary<int, JsonWrapperServicePlane> servicePlanMap = new ConcurrentDictionary<int, JsonWrapperServicePlane>();
        //bool _gotsplist = false;
        //void OnQryFinServicePlan(string jsonstr, bool islast)
        //{
        //    JsonWrapperServicePlane[] objs = CoreService.ParseJsonResponse<JsonWrapperServicePlane[]>(jsonstr);
        //    if (objs != null)
        //    {
        //        foreach (JsonWrapperServicePlane sp in objs)
        //        {
        //            if (!servicePlanMap.Keys.Contains(sp.ID))
        //            {
        //                servicePlanMap.TryAdd(sp.ID, sp);
        //            }
        //        }
        //        _gotsplist = true;

        //        MoniterHelper.AdapterToIDataSource(finsplist).BindDataSource(GetSPList());
        //        if (_domain != null)
        //        {
        //            SetSPList(_domain.FinSPList);
        //        }
        //    }
        //    else//如果没有配资服
        //    {

        //    }
        //}
        #endregion

        DomainImpl _domain = null;
        public void SetDomain(DomainImpl domain)
        {
            _domain = domain;
            domainid.Text = _domain.ID.ToString();
            name.Text = _domain.Name;
            linkman.Text = _domain.LinkMan;
            mobile.Text = _domain.Mobile;
            qq.Text = _domain.QQ;
            email.Text = _domain.Email;

            dateexpired.Value = _domain.DateExpired==0? (DateTime.Now+ new TimeSpan(1000,0,0,0,0)):Util.ToDateTime(_domain.DateExpired, 1);
            acclimit.Value = _domain.AccLimit==0?acclimit.Maximum:_domain.AccLimit;
            routergrouplimit.Value = _domain.RouterGroupLimit == 0 ? routergrouplimit.Maximum : _domain.RouterGroupLimit;
            routeritemlimit.Value = _domain.RouterItemLimit == 0 ? routeritemlimit.Maximum : _domain.RouterItemLimit;
            vendorlimit.Value = _domain.VendorLimit;

            module_agent.Checked = _domain.Module_Agent;
            module_subagent.Checked = !module_agent.Checked ? false : _domain.Module_SubAgent;

            module_payonline.Checked = _domain.Module_PayOnline;
            module_follow.Checked = _domain.Module_Follow;

            cfg_GrossPositioin.Checked = _domain.Cfg_GrossPosition;
            cfg_MaxMarginSide.Checked = _domain.Cfg_MaxMarginSide;
            cfg_followstrategynum.Value = _domain.Cfg_FollowStrategyNum;

            router_live.Checked = _domain.Router_Live;
            router_sim.Checked = _domain.Router_Sim;

            agentLimit.Value = _domain.AgentLimit;
            discountNum.Value = _domain.DiscountNum;
            isproduction.Checked = _domain.IsProduction;
            dedicated.Checked = _domain.Dedicated;
            dedicated.Enabled = false;

            cfg_device_droid.Checked = _domain.Device_Droid;
            cfg_device_ios.Checked = _domain.Device_IOS;
            cfg_md5key.Text = _domain.Cfg_MD5Key;


            this.Text = "编辑域分区:" + _domain.Name;

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!(router_live.Checked || router_sim.Checked))
            {
                MoniterHelper.WindowMessage("必须选择一种路由模式或全选");
                return;
            }
            if (router_live.Checked && string.IsNullOrEmpty(GetInterfaceListString()))
            {
                MoniterHelper.WindowMessage("实盘交易必须选择支持的接口列表");
                return;
            }
            if (_domain != null)
            {
                _domain.Name = name.Text;
                _domain.LinkMan = linkman.Text;
                _domain.Mobile = mobile.Text;
                _domain.QQ = qq.Text;
                _domain.Email = email.Text;

                _domain.DateExpired = Util.ToTLDate(dateexpired.Value);
                _domain.AccLimit = (int)acclimit.Value;
                _domain.RouterGroupLimit = (int)routergrouplimit.Value;
                _domain.RouterItemLimit = (int)routeritemlimit.Value;
                _domain.VendorLimit = (int)vendorlimit.Value;

                _domain.InterfaceList = GetInterfaceListString();
                _domain.FinSPList = string.Empty;// GetSPListString();

                _domain.Module_Agent = module_agent.Checked;
                _domain.Module_SubAgent = module_agent.Checked?module_subagent.Checked:false;

                _domain.Module_PayOnline = module_payonline.Checked;
                _domain.Module_Follow = module_follow.Checked;
                _domain.Router_Live = router_live.Checked;
                _domain.Router_Sim = router_sim.Checked;

                _domain.AgentLimit = (int)agentLimit.Value;
                _domain.DiscountNum = (int)discountNum.Value; 
                _domain.IsProduction = isproduction.Checked;
                _domain.Dedicated = dedicated.Checked;

                _domain.Cfg_MaxMarginSide = cfg_MaxMarginSide.Checked;
                _domain.Cfg_GrossPosition = cfg_GrossPositioin.Checked;
                _domain.Cfg_FollowStrategyNum = (int)cfg_followstrategynum.Value;

                _domain.Cfg_MD5Key = cfg_md5key.Text;
                _domain.Device_Droid = cfg_device_droid.Checked;
                _domain.Device_IOS = cfg_device_ios.Checked;


                if (MoniterHelper.WindowConfirm("确认更新分区设置?") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqUpdateDomain(_domain);
                    this.Close();
                }
            }
            else
            {
                _domain = new DomainImpl();
                _domain.Name = name.Text;
                _domain.LinkMan = linkman.Text;
                _domain.Mobile = mobile.Text;
                _domain.QQ = qq.Text;
                _domain.Email = email.Text;

                _domain.DateExpired = Util.ToTLDate(dateexpired.Value);
                _domain.AccLimit = (int)acclimit.Value;
                _domain.RouterGroupLimit = (int)routergrouplimit.Value;
                _domain.RouterItemLimit = (int)routeritemlimit.Value;
                _domain.VendorLimit = (int)vendorlimit.Value;


                _domain.InterfaceList = GetInterfaceListString();
                _domain.FinSPList = string.Empty;// GetSPListString();

                _domain.Module_Agent = module_agent.Checked;
                _domain.Module_SubAgent = module_agent.Checked ? module_subagent.Checked : false;

                _domain.Module_PayOnline = module_payonline.Checked;
                _domain.Module_Follow = module_follow.Checked;
                _domain.Router_Live = router_live.Checked;
                _domain.Router_Sim = router_sim.Checked;

                _domain.AgentLimit = (int)agentLimit.Value;
                _domain.DiscountNum = (int)discountNum.Value;
                _domain.IsProduction = isproduction.Checked;
                _domain.Dedicated = dedicated.Checked;

                _domain.Cfg_MaxMarginSide = cfg_MaxMarginSide.Checked;
                _domain.Cfg_GrossPosition = cfg_GrossPositioin.Checked;
                _domain.Cfg_FollowStrategyNum = (int)cfg_followstrategynum.Value;

                _domain.Cfg_MD5Key = cfg_md5key.Text;
                _domain.Device_Droid = cfg_device_droid.Checked;
                _domain.Device_IOS = cfg_device_ios.Checked;

                if ( (!_domain.Router_Live)&& (!_domain.Router_Sim))
                {
                    ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("需启用1种或全部成交方式");
                    return;
                }
                if (MoniterHelper.WindowConfirm("确认添加分区?") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqUpdateDomain(_domain);
                    this.Close();
                }
            }

        }
    }
}
