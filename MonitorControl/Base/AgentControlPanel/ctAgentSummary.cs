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

            MoniterHelper.AdapterToIDataSource(cbCurrency).BindDataSource(MoniterHelper.GetEnumValueObjects<CurrencyType>());
            ctNormalAgentSummary.Location = new Point(90, 0);
            ctNormalAgentSummary.Size = new System.Drawing.Size(120*3, 60);
            ctCustSummary.Location = new Point(90 + 120 * 3, 0);

            this.Reset();

            WireEvent();
        }

        decimal GetRatio(CurrencyType targetCurrency)
        {
            if (targetCurrency == CurrencyType.RMB) return 1;
            //获得品种货币对应的汇率 返回中间汇率
            ExchangeRate secRate = CoreService.BasicInfoTracker.GetExchangeRate(targetCurrency);// BasicTracker.ExchangeRateTracker[TLCtxHelper.ModuleSettleCentre.Tradingday, sec.Currency];
            if (secRate == null) return 1;//没有找到品种汇率 则默认返回1
            return 1/secRate.IntermediateRate;
          
        }

        void WireEvent()
        {
            kryptonContextMenuItem1.Click += new EventHandler(kryptonContextMenuItem1_Click);
            kryptonContextMenuItem2.Click += new EventHandler(kryptonContextMenuItem2_Click);
            kryptonContextMenuItem7.Click += new EventHandler(kryptonContextMenuItem7_Click);
            kryptonContextMenuItem8.Click += new EventHandler(kryptonContextMenuItem8_Click);
            kryptonContextMenuItem9.Click += new EventHandler(kryptonContextMenuItem9_Click);

           
            kryptonContextMenuItem12.Click += new EventHandler(kryptonContextMenuItem12_Click);//下级账户结算统计
            kryptonContextMenuItem13.Click += new EventHandler(kryptonContextMenuItem13_Click);//默认配置模板
            cbCurrency.SelectedValue = CurrencyType.RMB;
            cbCurrency.SelectedIndexChanged += new EventHandler(cbCurrency_SelectedIndexChanged);
        }

        void cbCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrencyType currency = (CurrencyType)cbCurrency.SelectedValue;

            decimal ratio = this.GetRatio(currency);
            ctNormalAgentSummary.Ratio = ratio;
            ctSelfOperateAgentSummary.Ratio = ratio;
            ctCustSummary.Ratio = ratio;
        }

        void kryptonContextMenuItem13_Click(object sender, EventArgs e)
        {
            if (_agent == null)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("代理财务账户不存在");
                return;
            }

            fmEditAgentDefaultConfigTemplate fm = new fmEditAgentDefaultConfigTemplate();
            fm.SetAccount(_agent);
            fm.ShowDialog();
            fm.Close();
        }

        void kryptonContextMenuItem12_Click(object sender, EventArgs e)
        {
            if (_agent == null)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("代理财务账户不存在");
                return;
            }

            fmHistSettlementStatistic fm = new fmHistSettlementStatistic();
            fm.SetAccount(_agent.Account);
            fm.ShowDialog();
            fm.Close();
        }

        void kryptonContextMenuItem9_Click(object sender, EventArgs e)
        {
            if (_agent == null)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("代理财务账户不存在");
                return;
            }
            fmAgentFlatEquity fm= new fmAgentFlatEquity();
            fm.SetAgent(_agent);
            fm.ShowDialog();
            fm.Close();
        }

        /// <summary>
        /// 结算记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void kryptonContextMenuItem8_Click(object sender, EventArgs e)
        {
            if (_agent == null)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("代理财务账户不存在");
                return;
            }
            fmHistSettlement fm = new fmHistSettlement();
            fm.HistReportType = EnumHistReportType.Agent;
            fm.SetAccount(_agent.Account);
            fm.ShowDialog();
            fm.Close();
        }

        /// <summary>
        /// 出入金
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void kryptonContextMenuItem7_Click(object sender, EventArgs e)
        {
            if (_agent == null)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("代理财务账户不存在");
                return;
            }

            fmHistQueryCashTrans fm = new fmHistQueryCashTrans();
            fm.HistReportType = EnumHistReportType.Agent;
            fm.SetAccount(_agent.Account);
            fm.ShowDialog();
            fm.Close();
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
        ManagerSetting _manager;
        public void SetAgent(ManagerSetting mgr,AgentSetting agent)
        {
            _agent = agent;
            _manager = mgr;
            agentBtnGroup.Values.Text = _agent.Account;

            agentBtnGroup.StateCommon.Content.ShortText.Color1 = Color.Black;
            //管理员与代理显示Heading
            if (_manager.Type == QSEnumManagerType.ROOT)
            {
                kryptonContextMenuHeading1.Text = "管理员";
            }
            else
            {
                kryptonContextMenuHeading1.Text = Util.GetEnumDescription(_agent.AgentType);
            }

            

            //普通代理
            if (_agent.AgentType == EnumAgentType.Normal)
            {
                ctNormalAgentSummary.Visible = true;
                ctSelfOperateAgentSummary.Visible = false;

                ctNormalAgentSummary.Location = new Point(90, 0);
                ctNormalAgentSummary.Size = new System.Drawing.Size(360, 60);
                ctCustSummary.Location = new Point(90 + 360, 0);

                kryptonContextMenuItem9.Visible = false;//普通代理不显示强平设置
            }

            if (_agent.AgentType == EnumAgentType.SelfOperated)
            {
                ctNormalAgentSummary.Visible = false;
                ctSelfOperateAgentSummary.Visible = true;
                ctSelfOperateAgentSummary.Location = new Point(90, 0);

                kryptonContextMenuItem9.Visible = true;


                if (_manager.Type == QSEnumManagerType.ROOT)
                {
                    ctSelfOperateAgentSummary.IsRootView = true;
                    ctCustSummary.Location = new Point(90 + 240, 0);
                    kryptonContextMenuItem9.Visible = false;
                }
                else
                {
                    ctSelfOperateAgentSummary.IsRootView = false;
                    ctCustSummary.Location = new Point(90 + 720, 0);
                }
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
                if (st.Freezed)
                {
                    agentBtnGroup.StateCommon.Content.ShortText.Color1 = Color.Red;
                }

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
