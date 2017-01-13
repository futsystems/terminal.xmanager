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
    public partial class fmExchangeRate : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmExchangeRate()
        {
            InitializeComponent();
            SetPreferences();
            InitTable();
            BindToTable();

            CoreService.EventCore.RegIEventHandler(this);

            this.Load += new EventHandler(fmExchangeRate_Load);
        }

        void fmExchangeRate_Load(object sender, EventArgs e)
        {

            CoreService.TLClient.ReqQryExchangeRateContrib();
            rategrid.DoubleClick += new EventHandler(rategrid_DoubleClick);
        }


        //得到当前选择的行号
        private int CurrentExchangeRateID
        {
            get
            {
                int row = rategrid.SelectedRows.Count > 0 ? rategrid.SelectedRows[0].Index : -1;
                if (row >= 0)
                {
                    return int.Parse(rategrid[0, row].Value.ToString());
                }
                else
                {
                    return 0;
                }
            }
        }

        ExchangeRate GetVisibleExchangeRate(int id)
        {
            ExchangeRate rate = null;
            if (exchangeratemap.TryGetValue(id, out rate))
            {
                return rate;
            }
            else
            {
                return null;
            }

        }


        void rategrid_DoubleClick(object sender, EventArgs e)
        {
            ExchangeRate rate = GetVisibleExchangeRate(CurrentExchangeRateID);
            if (rate != null)
            {
                fmExchangeRateEdit fm = new fmExchangeRateEdit();
                fm.SetExchangeRate(rate);
                fm.ShowDialog();

            }
        }

        public void OnInit()
        {

            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_EXCHANGERATES, this.OnQryExchangeRates);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_EXCHANGERATES, this.OnNotifyExchangeRate);
        }

        public void OnDisposed()
        {
            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_EXCHANGERATES, this.OnQryExchangeRates);
            CoreService.EventCore.UnRegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_EXCHANGERATES, this.OnNotifyExchangeRate);
            
        }

        void OnNotifyExchangeRate(string json)
        {
            ExchangeRate obj = CoreService.ParseJsonResponse<ExchangeRate>(json);
            if (obj != null)
            {
                InvokeGotExchangeRate(obj);   
            }
        }
        void OnQryExchangeRates(string json,bool islast)
        {

            ExchangeRate[] obj = CoreService.ParseJsonResponse<ExchangeRate[]>(json);
            if (obj != null)
            {
                foreach (var rate in obj)
                {
                    InvokeGotExchangeRate(rate);
                }
            }
        }

        

        Dictionary<int, ExchangeRate> exchangeratemap = new Dictionary<int, ExchangeRate>();
        Dictionary<int, int> exchangeraterowmap = new Dictionary<int, int>();

        int ExchangeIdx(int id)
        {
            int rowid = -1;
            if (!exchangeraterowmap.TryGetValue(id, out rowid))
            {
                return -1;
            }
            else
            {
                return rowid;
            }
        }


        void InvokeGotExchangeRate(ExchangeRate rate)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ExchangeRate>(InvokeGotExchangeRate), new object[] { rate });
            }
            else
            {
                int r = ExchangeIdx(rate.ID);
                if (r == -1)
                {
                    gt.Rows.Add(rate.ID);
                    int i = gt.Rows.Count - 1;

                    gt.Rows[i][CURRENCY] = rate.Currency;
                    gt.Rows[i][INTER] = rate.IntermediateRate;
                    gt.Rows[i][ASK] = rate.AskRate;
                    gt.Rows[i][BID] = rate.BidRate;
                    gt.Rows[i][UPDATETIME] = Util.ToDateTime(rate.UpdateTime).ToString();

                    exchangeraterowmap.Add(rate.ID,i);
                    exchangeratemap.Add(rate.ID, rate);
                }
                else
                {
                    //更新状态
                    int i = r;
                    gt.Rows[i][INTER] = rate.IntermediateRate;
                    gt.Rows[i][ASK] = rate.AskRate;
                    gt.Rows[i][BID] = rate.BidRate;
                    gt.Rows[i][UPDATETIME] = Util.ToDateTime(rate.UpdateTime).ToString();
                    exchangeratemap[rate.ID] = rate;
                }
                
            }
        }

        #region 表格
        #region 显示字段

        const string ID = "全局ID";
        const string CURRENCY = "货币";
        const string INTER = "中间价";
        const string ASK = "卖价";
        const string BID = "买价";
        const string UPDATETIME = "更新时间";

        #endregion

        DataTable gt = new DataTable();
        BindingSource datasource = new BindingSource();

        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = rategrid;

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
            gt.Columns.Add(ID);//0
            gt.Columns.Add(CURRENCY);//1
            gt.Columns.Add(INTER);//1
            gt.Columns.Add(ASK);//1
            gt.Columns.Add(BID);//1
            gt.Columns.Add(UPDATETIME);//1



        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = rategrid;

            datasource.DataSource = gt;
            grid.DataSource = datasource;

            grid.Columns[ID].Width = 50;
            grid.Columns[UPDATETIME].Width = 120;
        }






        #endregion
    }
}
