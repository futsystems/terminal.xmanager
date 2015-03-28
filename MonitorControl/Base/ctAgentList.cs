﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;



namespace TradingLib.MoniterControl
{
    public partial class ctAgentList : UserControl,IEventBinder
    {
        public event VoidDelegate AgentSelectedChangedEvent;
        bool _gotdata = false;

        //属性获得和设置
        [DefaultValue(true)]
        bool _enableself = true;
        public bool EnableSelf
        {
            get
            {
                return _enableself;
            }
            set
            {
                _enableself = value;
            }
        }


        //属性获得和设置
        [DefaultValue(true)]
        bool _enableselected = true;
        public bool EnableSelected
        {
            get
            {
                return _enableselected;
            }
            set
            {
                _enableselected = value;
                agent.Enabled = _enableselected;
            }
        }

        /// <summary>
        /// 是否允许显示Any 即所有代理商
        /// 比如查询时查询所有统计 则选择Any 帐户列表过滤时候选择Any则表示不用代理上过滤
        /// </summary>
        [DefaultValue(false)]
        bool _enableany = false;
        public bool EnableAny
        {
            get
            {
                return _enableany;
            }
            set
            {
                _enableany = value;

                if (CoreService.BasicInfoTracker.Initialized)
                {
                    ReloadList();
                }
                //agent.Enabled = _enableselected;
            }
        }

        [DefaultValue(true)]
        bool _defaultbasemgr = true;
        public bool EnableDefaultBaseMGR
        {
            get
            {
                return _defaultbasemgr;
            }
            set
            {
                _defaultbasemgr = value;
                if (CoreService.BasicInfoTracker.Initialized)
                {
                    ReloadList();
                }
            }
        }


        public ctAgentList()
        {
            InitializeComponent();

            
            this.Load += new EventHandler(ctAgentList_Load);
        }



        void ctAgentList_Load(object sender, EventArgs e)
        {

            CoreService.EventCore.RegIEventHandler(this);
            if (!_enableselected)
            {
                agent.Enabled = false;
            }
            else
            {
                agent.Enabled = true;
            }
        }

        void OnManagerNotify(string jsonstr)
        {
            Manager obj = MoniterHelper.ParseJsonResponse<Manager>(jsonstr);
            if (obj != null)
            {
                ReloadList();
            }
        }



        public void OnInit()
        {
            ReloadList();

            if (CoreService.SiteInfo.Manager.Type != QSEnumManagerType.ROOT)
            {
                if (_defaultbasemgr)//如果默认选择当前域 则设置selectedvalue
                {
                    agent.SelectedValue = CoreService.SiteInfo.Manager.mgr_fk;
                }
            }

            CoreService.EventContrib.RegisterNotifyCallback("MgrExchServer", "NotifyManagerUpdate", OnManagerNotify);
            _gotdata = true;
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterNotifyCallback("MgrExchServer", "NotifyManagerUpdate", OnManagerNotify);
        }



        void ReloadList()
        {

            MoniterHelper.AdapterToIDataSource(agent).BindDataSource(CoreService.BasicInfoTracker.GetBaseManagerCombList(_enableany, _enableself));
        }

        /// <summary>
        /// 获得当前选中的代理ID
        /// </summary>
        public int CurrentAgentFK
        {
            get
            {
                try
                {
                    return int.Parse(agent.SelectedValue.ToString());
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
            set
            {
                try
                {
                    agent.SelectedValue = value;
                }
                catch (Exception ex)
                { 
                
                }
            }
        }

        public int SelectedIndex
        {
            get
            {
                try
                {
                    return agent.SelectedIndex;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }
        private void agent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AgentSelectedChangedEvent != null && _gotdata)
                AgentSelectedChangedEvent();
        }

    }
}
