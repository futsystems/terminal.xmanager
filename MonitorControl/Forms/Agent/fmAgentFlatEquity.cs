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
    public partial class fmAgentFlatEquity : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmAgentFlatEquity()
        {
            InitializeComponent();
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
            CoreService.EventCore.RegIEventHandler(this);
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm(string.Format("确认修改强平权益为:{0}?",currentFlatEquity.Value.ToFormatStr())) == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqUpdateAgentFlatEquity(_agent.Account, currentFlatEquity.Value);
            }
        }

        public void OnInit()
        {
            CoreService.EventCore.RegisterNotifyCallback(Modules.AgentManager, Method_AGENT_MGR.NOTIFY_AGENT, OnNotifAgent);
        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterNotifyCallback(Modules.AgentManager, Method_AGENT_MGR.NOTIFY_AGENT, OnNotifAgent);
        }

        void OnNotifAgent(string json)
        { 
            AgentSetting item = CoreService.ParseJsonResponse<AgentSetting>(json);
            if (item != null)
            {
                flatEquity.Text = item.FlatEquity.ToFormatStr();
            }
        }

        AgentSetting _agent;
        public void SetAgent(AgentSetting agent)
        {
            _agent = agent;
            flatEquity.Text = agent.FlatEquity.ToFormatStr();
            currentFlatEquity.Value = _agent.FlatEquity;

            if (_agent.Account == CoreService.SiteInfo.Manager.Login)
            {
                btnSubmit.Visible = false;
                currentFlatEquity.Enabled = false;
            }
        }
    }
}
