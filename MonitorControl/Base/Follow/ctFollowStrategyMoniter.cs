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


namespace TradingLib.MoniterControl.Base.Follow
{
    /// <summary>
    /// 显示跟单策略列表
    /// 统计项等
    /// </summary>
    public partial class ctFollowStrategyMoniter : UserControl, IEventBinder
    {
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
        }


        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback("FollowCentre", "QryFollowStrategyList", OnQryFollowStrategyList);
            CoreService.TLClient.ReqContribRequest("FollowCentre", "QryFollowStrategyList", "");
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("FollowCentre", "QryFollowStrategyList", OnQryFollowStrategyList);
     
        }


        void OnQryFollowStrategyList(string json, bool islast)
        {
            FollowStrategyConfig cfg = MoniterHelper.ParseJsonResponse<FollowStrategyConfig>(json);
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
                    gt.Rows.Add(cfg.ID);
                    int i = gt.Rows.Count - 1;

                    gt.Rows[i][NAME] = cfg.Token;
                    gt.Rows[i][RUNNING] = true;
                    gt.Rows[i][SIGNALCNT] = 10;
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
        const string RUNNING = "RunningStatus";
        const string RUNNINGIMG = "状态";

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

            gt.Columns.Add(RUNNING);//2
            gt.Columns.Add(RUNNINGIMG);//3


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
