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
    public partial class fmQryFollowExecution : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmQryFollowExecution()
        {
            InitializeComponent();
            SetPreferences();
            InitTable();
            BindToTable();

            this.Load += new EventHandler(fmQryFollowExecution_Load);
        }

        void fmQryFollowExecution_Load(object sender, EventArgs e)
        {
           
            btnQry.Click += new EventHandler(btnQry_Click);
            btnExport.Click += new EventHandler(btnExport_Click);

            CoreService.EventCore.RegIEventHandler(this);
        }

        void btnExport_Click(object sender, EventArgs e)
        {
            
        }

        void btnQry_Click(object sender, EventArgs e)
        {
            this.Clear();
            int strategyid = (int)cbStrategy.SelectedValue;
            int s = TradingLib.Common.Util.ToTLDate(settleday.Value);

            CoreService.TLClient.ReqQryFollowExecution(strategyid, s);
        }


        public void OnInit()
        {
            CoreService.EventCore.RegisterCallback(Modules.Follow, Method_Follow.QRY_STRATEGY_CFG_LIST, OnQryStrategyConfig);
            CoreService.EventCore.RegisterCallback(Modules.Follow, Method_Follow.QRY_FOLLOW_EXECUTION, OnQryFollowExecution);


            CoreService.TLClient.ReqQryFollowStrategyList();
        }

        public void OnDisposed()
        { 
            
        }

        void OnQryFollowExecution(string json, bool islast)
        {
            FollowExecution ex = CoreService.ParseJsonResponse<FollowExecution>(json);
            if (ex != null)
            {
                InvokeGotFollowExecution(ex);
            }
        }

        void InvokeGotFollowExecution(FollowExecution ex)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<FollowExecution>(InvokeGotFollowExecution), new object[] { ex });
            }
            else
            { 
                DataRow r = gt.Rows.Add(ex.StrategyID);
                int i = gt.Rows.Count - 1;
                gt.Rows[i][FOLLOWKEY] = ex.FollowKey;
                gt.Rows[i][SOURCESIGNAL] = ex.SourceSignal;
                gt.Rows[i][SIGNALINFO] = ex.SignalInfo;
                gt.Rows[i][EXCHANGE] = ex.Exchange;
                gt.Rows[i][SYMBOL] = ex.Symbol;
                gt.Rows[i][SIDE] = ex.Side;
                gt.Rows[i][SIZE] = ex.Size;
                gt.Rows[i][OPENTIME] = ex.OpenTime;
                gt.Rows[i][OPENAVGPRICE] = ex.OpenAvgPrice;
                gt.Rows[i][OPENSLIP] = ex.OpenSlip;
                gt.Rows[i][CLOSETIME] = ex.CloseTime;
                gt.Rows[i][CLOSEAVGPRICE] = ex.CloseAvgPrice;
                gt.Rows[i][CLOSESLIP] = ex.CloseSlip;
                gt.Rows[i][REALIZEDPL] = ex.RealizedPL;
                gt.Rows[i][COMMISSION] = ex.Commission;
                gt.Rows[i][PROFIT] = ex.Profit;

            }
        }

        Dictionary<int, FollowStrategyConfig> configMap = new Dictionary<int, FollowStrategyConfig>();
        void OnQryStrategyConfig(string json, bool islast)
        {
            FollowStrategyConfig cfg = CoreService.ParseJsonResponse<FollowStrategyConfig>(json);
            if (cfg != null)
            {
                configMap.Add(cfg.ID, cfg);
            }
            if (islast)
            {
                MoniterHelper.AdapterToIDataSource(cbStrategy).BindDataSource(GetStrategyList());
            }
        }

        public ArrayList GetStrategyList()
        {
            ArrayList list = new ArrayList();
            foreach (var item in configMap.Values)
            {
                ValueObject<int> vo = new ValueObject<int>();
                vo.Name = item.Token;
                vo.Value = item.ID;
                list.Add(vo);
            }
            return list;
        }


        #region 表格
        #region 显示字段

        const string STRATEGYID = "跟单策略ID";
        const string FOLLOWKEY = "跟单键";
        const string SOURCESIGNAL = "初始信号";
        const string SIGNALINFO = "信号信息";
        const string EXCHANGE = "交易所";
        const string SYMBOL = "合约";
        const string SIDE = "方向";
        const string SIZE = "数量";
        const string OPENTIME = "开仓时间";
        const string OPENAVGPRICE = "开仓均价";
        const string OPENSLIP = "开仓滑点";
        const string CLOSETIME = "平仓时间";
        const string CLOSEAVGPRICE = "平仓均价";
        const string CLOSESLIP = "平仓滑点";
        const string REALIZEDPL = "平仓盈亏";
        const string COMMISSION = "手续费";
        const string PROFIT = "净利润";


        #endregion

        DataTable gt = new DataTable();
        BindingSource datasource = new BindingSource();

        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = fegrid;

            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.ColumnHeadersHeight = 25;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;

            grid.StateCommon.Background.Color1 = Color.WhiteSmoke;
            grid.StateCommon.Background.Color2 = Color.WhiteSmoke;

        }
        //初始化Account显示空格
        private void InitTable()
        {
            gt.Columns.Add(STRATEGYID);//0
            gt.Columns.Add(FOLLOWKEY);//1
            gt.Columns.Add(SOURCESIGNAL);//1
            gt.Columns.Add(SIGNALINFO);//1
            gt.Columns.Add(EXCHANGE);//1
            gt.Columns.Add(SYMBOL);//1

            gt.Columns.Add(SIDE);
            gt.Columns.Add(SIZE);//1
            gt.Columns.Add(OPENTIME);//1
            gt.Columns.Add(OPENAVGPRICE);//1
            gt.Columns.Add(OPENSLIP);//1
            gt.Columns.Add(CLOSETIME);//1
            gt.Columns.Add(CLOSEAVGPRICE);//1
            gt.Columns.Add(CLOSESLIP);//1
            gt.Columns.Add(REALIZEDPL);//1
            gt.Columns.Add(COMMISSION);//1
            gt.Columns.Add(PROFIT);//

        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = fegrid;

            datasource.DataSource = gt;
            grid.DataSource = datasource;

            grid.Columns[STRATEGYID].Visible = false;
            grid.Columns[FOLLOWKEY].Width = 120;
        }

        public void Clear()
        {
            fegrid.DataSource = null;
            gt.Rows.Clear();
            BindToTable();
        }




        #endregion
    }
}
