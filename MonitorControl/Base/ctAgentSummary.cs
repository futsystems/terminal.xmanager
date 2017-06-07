using System;
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

namespace TradingLib.MoniterControl.Base
{
    public partial class ctAgentSummary : UserControl, IEventBinder
    {
        public ctAgentSummary()
        {
            InitializeComponent();

           
            CoreService.EventCore.RegIEventHandler(this);

            WireEvent();
        }

        void WireEvent()
        {
            kryptonContextMenuItem1.Click += new EventHandler(kryptonContextMenuItem1_Click);
            kryptonContextMenuItem2.Click += new EventHandler(kryptonContextMenuItem2_Click);
        }

        /// <summary>
        /// 模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void kryptonContextMenuItem2_Click(object sender, EventArgs e)
        {
            if (_agent == null)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("代理财务账户不存在");
                return;
            }
            fmEditAgentTemplate fm = new fmEditAgentTemplate();
            fm.SetAgent(_agent);
            fm.ShowDialog();
            fm.Close();
        }

        /// <summary>
        /// 出入金
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void kryptonContextMenuItem1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        public void SetAgentAccount(string account)
        {
            this.agentAccount.Text = account;
            this.Reset();
            //查询代理财务账户
            CoreService.TLClient.ReqQryAgent(account);
        }
        public void OnInit()
        {
            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_AGENT, OnQryAgent);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_AGENT, OnNotifyAgent);
        }
        public void OnDisposed()
        { 
            
        }

        const string EMPTY = "--";
        void Reset()
        {
            _agent = null;
            lastEquity.Text = EMPTY;
            nowEquity.Text = EMPTY;
            unrealizedPL.Text = EMPTY;
            closedPL.Text = EMPTY;
            commissionCost.Text = EMPTY;
            commissionIncome.Text = EMPTY;
        }

        AgentSetting _agent;
        void OnQryAgent(string json,bool islast)
        {
            AgentSetting item = CoreService.ParseJsonResponse<AgentSetting>(json);
            
            if (item != null)
            {
                _agent = item;
                InvokeGotAgent(_agent);
            }
        }

        void OnNotifyAgent(string json)
        {
            AgentSetting item = CoreService.ParseJsonResponse<AgentSetting>(json);
            if (item != null)
            {
                _agent = item;
                InvokeGotAgent(_agent);
            }
        }

        void InvokeGotAgent(AgentSetting agent)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<AgentSetting>(InvokeGotAgent), new object[] { agent });
            }
            else
            {
                lastEquity.Text = agent.LastEquity.ToFormatStr();
            }
        }
    }
}
