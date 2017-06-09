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

            ctNormalAgentSummary.Location = new Point(90, 0);
            ctNormalAgentSummary.Size = new System.Drawing.Size(120*3, 60);
            ctCustSummary.Location = new Point(90 + 120 * 3, 0);

            this.Reset();

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
            if (_agent == null)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("代理财务账户不存在");
                return;
            }
            fmAgentCashOperation fm = new fmAgentCashOperation();
            fm.SetAgent(_agent);
            fm.ShowDialog();
            fm.Close();
        }
        


        public void OnInit()
        {
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_AGENT_STATISTIC, OnNotifyAgentStatistic);
        }
        public void OnDisposed()
        { 
            
        }

        AgentSetting _agent;
        public void SetAgent(AgentSetting agent)
        {
            _agent = agent;
            agentBtnGroup.Values.Text = _agent.Account;
            kryptonContextMenuHeading1.Text = Util.GetEnumDescription(_agent.AgentType);

            if (_agent.AgentType == EnumAgentType.Normal)
            {
                ctNormalAgentSummary.Visible = true;
                ctSelfOperateAgentSummary.Visible = false;

                ctNormalAgentSummary.Location = new Point(90, 0);
                ctNormalAgentSummary.Size = new System.Drawing.Size(360, 60);
                ctCustSummary.Location = new Point(90 + 360, 0);


            }
            if (_agent.AgentType == EnumAgentType.SelfOperated)
            {
                ctNormalAgentSummary.Visible = false;
                ctSelfOperateAgentSummary.Visible = true;
                ctSelfOperateAgentSummary.Location = new Point(90, 0);
                ctSelfOperateAgentSummary.Size = new System.Drawing.Size(720, 60);
                ctCustSummary.Location = new Point(90 + 720, 0);
            }
        }

        const string EMPTY = "--";
        void Reset()
        {
            _agent = null;
            agentBtnGroup.Values.Text = EMPTY;
            ctCustSummary.Reset();
            ctNormalAgentSummary.Reset();
            ctSelfOperateAgentSummary.Reset();
        }

        

        void OnNotifyAgentStatistic(string json)
        {
            AgentStatistic st = CoreService.ParseJsonResponse<AgentStatistic>(json);
            if (st != null && _agent!= null)
            {
                if (_agent.AgentType == EnumAgentType.Normal)
                {
                    ctNormalAgentSummary.GotAgentStatic(st);
                }
                if (_agent.AgentType == EnumAgentType.SelfOperated)
                {
                    ctSelfOperateAgentSummary.GotAgentStatic(st);
                }
                ctCustSummary.GotAgentStatic(st);
            }
        }
    }
}
