using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;
using ICSharpCode.Core;

namespace TradingLib.MoniterControl
{
    /// <summary>
    /// 显示跟单策略列表
    /// 统计项等
    /// </summary>
    public partial class ctFollowStrategyMoniter : UserControl, IEventBinder
    {
        public event Action<FollowStrategyConfig> StrategySelected;

        public ctFollowStrategyMoniter()
        {
            
            InitializeComponent();
            SetPreferences();
            InitTable();
            BindToTable();

            this.Load += new EventHandler(ctFollowStrategyMoniter_Load);
        }

        void ctFollowStrategyMoniter_Load(object sender, EventArgs e)
        {
            //全局事件回调
            CoreService.EventCore.RegIEventHandler(this);
            try
            {
                strategyGrid.ContextMenuStrip = MenuService.CreateContextMenu(this, "/FollowStrategyList/ContextMenu");
                strategyGrid.DoubleClick += new EventHandler(strategyGrid_DoubleClick);
            }
            catch (Exception ex)
            { 
            
            }

            strategyGrid.MouseClick += new MouseEventHandler(strategyGrid_MouseClick);
            
        }

        DateTime _lastresumetime = DateTime.Now;
        void strategyGrid_DoubleClick(object sender, EventArgs e)
        {
            if (CoreService.TradingInfoTracker.IsInResume)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("交易记录恢复中,请稍候!");
                return;
            }

            if (DateTime.Now.Subtract(_lastresumetime).TotalSeconds <= 1)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请不要频繁请求帐户日内数据");
                return;
            }
            _lastresumetime = DateTime.Now;
            string account = CurrentStrategyConfig.Account;

            AccountItem accountlite = CoreService.BasicInfoTracker.GetAccount(account);
            if (accountlite != null)
            {
                //触发事件中继的帐户选择事件
                //CoreService.EventAccount.FireAccountSelectedEvent(accountlite);

                //对外触发跟单策略选中事件
                if (StrategySelected != null)
                {
                    StrategySelected(CurrentStrategyConfig);
                }
            }
            else
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("跟单策略绑定的交易帐户不存在");
                return;
            }
        }

        void strategyGrid_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                MoniterHelper.WindowMessage("右键单击");
            }
        }

       
        /// <summary>
        /// 获得当前选中的跟单策略
        /// </summary>
        public FollowStrategyConfig CurrentStrategyConfig
        {
            get
            {
                int row = (strategyGrid.SelectedRows.Count > 0 ? strategyGrid.SelectedRows[0].Index : -1);
                if(row == -1) return null;
                int strategy_id = int.Parse(strategyGrid[0, row].Value.ToString());

                return GetStrategyConfig(strategy_id);
            }
        }



        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback("FollowCentre", "QryFollowStrategyList", OnQryFollowStrategyList);
            CoreService.EventContrib.RegisterNotifyCallback("FollowCentre", "FollowStrategyConfigNotify", OnFollowStrategyConfigNotify);
            CoreService.EventContrib.RegisterNotifyCallback("FollowCentre", "FollowStrategyStatusNotify", OnFollowStrategyStatusNotify);
            
            CoreService.TLClient.ReqContribRequest("FollowCentre", "QryFollowStrategyList", "");
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("FollowCentre", "QryFollowStrategyList", OnQryFollowStrategyList);
            CoreService.EventContrib.UnRegisterNotifyCallback("FollowCentre", "FollowStrategyConfigNotify", OnFollowStrategyConfigNotify);
          
            CoreService.EventContrib.UnRegisterNotifyCallback("FollowCentre", "FollowStrategyStatusNotify", OnFollowStrategyStatusNotify);
           
        }


        void OnFollowStrategyConfigNotify(string json)
        {
            FollowStrategyConfig cfg = CoreService.ParseJsonResponse<FollowStrategyConfig>(json);
            if (cfg != null)
            {
                InvokeGotStrategyCfg(cfg);
            }
        }

        void OnFollowStrategyStatusNotify(string json)
        {
            FollowStrategyStatus status = CoreService.ParseJsonResponse<FollowStrategyStatus>(json);
            if (status != null)
            {
                InvokeGotStrategyStatusNotify(status);
            }
        }

        void OnQryFollowStrategyList(string json, bool islast)
        {
            FollowStrategyConfig cfg = CoreService.ParseJsonResponse<FollowStrategyConfig>(json);
            if (cfg != null)
            {
                InvokeGotStrategyCfg(cfg);
            }
        }

        ConcurrentDictionary<int, FollowStrategyConfig> cfgmap = new ConcurrentDictionary<int, FollowStrategyConfig>();
        ConcurrentDictionary<int, int> cfgrowmap = new ConcurrentDictionary<int, int>();
        int cfgID2RowIdx(int id)
        {
            int idx = -1;
            if (cfgrowmap.TryGetValue(id, out idx))
            {
                return idx;
            }
            return -1;
        }

        FollowStrategyConfig GetStrategyConfig(int strategy_id)
        {
            FollowStrategyConfig target = null;
            if (cfgmap.TryGetValue(strategy_id, out target))
            {
                return target;
            }
            return null;
        }

        void InvokeGotStrategyStatusNotify(FollowStrategyStatus status)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<FollowStrategyStatus>(InvokeGotStrategyStatusNotify), new object[] { status });
            }
            else
            {
                int r = cfgID2RowIdx(status.StrategyID);
                if (r != -1)
                {
                    gt.Rows[r][WORKSTATE] = status.WorkState;

                    gt.Rows[r][SIGNALCNT] = status.SignalCount;

                    gt.Rows[r][SIGREALIZEDPL] = status.SignalRealizedPL;
                    gt.Rows[r][SIGUNREALIZEDPL] = status.SignalUnRealizedPL;
                    gt.Rows[r][FOLLOWREALIZEDPL] = status.FollowRealizedPL;
                    gt.Rows[r][FOLLOWUNREALIZEDPL] = status.FollowUnRealizedPL;
                    gt.Rows[r][TOTALSLIP] = status.TotalSlip;
                    gt.Rows[r][ENTRYITEMTOTAL] = status.TotalEntryCount;
                    gt.Rows[r][ENTRYITEMSUCCESS] = status.TotalEntrySuccessCount;

                }
            }
        }
        void InvokeGotStrategyCfg(FollowStrategyConfig cfg)
        {
            if(InvokeRequired)
            {
                Invoke(new Action<FollowStrategyConfig>(InvokeGotStrategyCfg),new object[]{cfg});
            }
            else
            {
                int r = cfgID2RowIdx(cfg.ID);
                if (r == -1)
                {
                    gt.Rows.Add();
                    int i = gt.Rows.Count - 1;
                    gt.Rows[i][STRATEGYID] = cfg.ID;
                    gt.Rows[i][NAME] = cfg.Token;
                    gt.Rows[i][WORKSTATE] = QSEnumFollowWorkState.Shutdown;
                    gt.Rows[i][SIGNALCNT] = 0;
                    gt.Rows[i][FOLLOWACCOUNT] = cfg.Account;


                    cfgmap.TryAdd(cfg.ID,cfg);
                    cfgrowmap.TryAdd(cfg.ID, i);


                }
                else
                { 
                    
                }
            }
        }


        #region 表格

        const string STRATEGYID = "ID";
        const string NAME = "名称";
        const string WORKSTATE = "WORKSTATE";
        const string WORKSTATEIMG = "状态";

        const string SIGNALCNT = "信号数";
        const string FOLLOWACCOUNT = "跟单帐户";

        const string SIGREALIZEDPL = "平(信号)";
        const string SIGUNREALIZEDPL = "浮(信号)";


        const string FOLLOWREALIZEDPL = "平(跟单)";
        const string FOLLOWUNREALIZEDPL = "浮(跟单)";
        const string TOTALSLIP = "滑点";

        const string ENTRYITEMTOTAL = "开仓项";
        const string ENTRYITEMSUCCESS = "执行";


        DataTable gt = new DataTable();
        BindingSource datasource = new BindingSource();




        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = strategyGrid;

            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            grid.ColumnHeadersHeight = 25;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.StateCommon.Background.Color1 = Color.WhiteSmoke;
            grid.StateCommon.Background.Color2 = Color.WhiteSmoke;

        }

        //初始化Account显示空格
        private void InitTable()
        {

            gt.Columns.Add(STRATEGYID);//0
            gt.Columns.Add(NAME);//1

            gt.Columns.Add(WORKSTATE);//2
            gt.Columns.Add(WORKSTATEIMG);//3


            gt.Columns.Add(SIGNALCNT);//4
            gt.Columns.Add(FOLLOWACCOUNT);//5


            gt.Columns.Add(SIGREALIZEDPL);//6
            gt.Columns.Add(SIGUNREALIZEDPL);//7
            gt.Columns.Add(FOLLOWREALIZEDPL);//8
            gt.Columns.Add(FOLLOWUNREALIZEDPL);//9
            gt.Columns.Add(TOTALSLIP);//10

            gt.Columns.Add(ENTRYITEMTOTAL);//11

            gt.Columns.Add(ENTRYITEMSUCCESS);//18

        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            datasource.DataSource = gt;
            //datasource.Sort = ACCOUNT + " ASC";
            strategyGrid.DataSource = datasource;

            //strategyGrid.Columns[CATEGORY].Visible = false;
            //strategyGrid.Columns[AGENTMGRFK].Visible = false;

           
            for (int i = 0; i < gt.Columns.Count; i++)
            {
                strategyGrid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        #endregion
    }
}
