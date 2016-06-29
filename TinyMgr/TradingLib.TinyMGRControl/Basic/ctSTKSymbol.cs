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

namespace TradingLib.TinyMGRControl
{
    public partial class ctSTKSymbol : UserControl,IEventBinder
    {
        public ctSTKSymbol()
        {
            InitializeComponent();

            SetPreferences();
            InitTable();
            BindToTable();

            WireEvent();

            

            this.Load += new EventHandler(ctSTKSymbol_Load);
        }

        void ctSTKSymbol_Load(object sender, EventArgs e)
        {
            foreach (SymbolImpl sym in CoreService.BasicInfoTracker.Symbols)
            {
                InvokeGotSymbol(sym);
            }
        }

        public void OnInit()
        {
            MoniterHelper.AdapterToIDataSource(cbsecurity).BindDataSource(CoreService.BasicInfoTracker.GetSecurityCombListViaExchange(0, SecurityType.FUT, true));
            //MoniterHelper.AdapterToIDataSource(cbtradeable).BindDataSource(MoniterHelper.GetTradeableCBList(true));
            MoniterHelper.AdapterToIDataSource(cbexchange).BindDataSource(CoreService.BasicInfoTracker.GetExchangeCombList(true));

            CoreService.EventBasicInfo.OnSymbolEvent += new Action<SymbolImpl>(InvokeGotSymbol);
        }
        public void OnDisposed()
        {
            CoreService.EventBasicInfo.OnSymbolEvent -= new Action<SymbolImpl>(InvokeGotSymbol);
        }

        void WireEvent()
        {
            CoreService.EventCore.RegIEventHandler(this);

            cbsecurity.SelectedIndexChanged += new EventHandler(cbsecurity_SelectedIndexChanged);
            cbexchange.SelectedIndexChanged += new EventHandler(cbexchange_SelectedIndexChanged);
            //cbtradeable.SelectedIndexChanged += new EventHandler(cbtradeable_SelectedIndexChanged);

        }
        private void cbsecurity_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshSecurityQuery();
        }

        private void cbexchange_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshSecurityQuery();

            //通过交易所ID获得该交易所所有合约
            if (CoreService.BasicInfoTracker.Initialized)
            {
                int exid = (int)cbexchange.SelectedValue;
                MoniterHelper.AdapterToIDataSource(cbsecurity).BindDataSource(CoreService.BasicInfoTracker.GetSecurityCombListViaExchange(exid, SecurityType.FUT, true));
            }
        }

        void RefreshSecurityQuery()
        {
            //if (!_load) return;
            //string sectype = string.Empty;

            //if (cbsecurity.SelectedIndex == 0)
            //{
            //    sectype = "*";
            //}
            //else
            //{
            //    sectype = cbsecurity.SelectedValue.ToString();
            //}

            string strFilter = string.Format(ID + " > '{0}'", "*");


            //if (cbsecurity.SelectedIndex == 0)
            //{
            //    strFilter = string.Format(SECTYPE + " > '{0}'", sectype);
            //}
            //else
            //{
            //    strFilter = string.Format(SECTYPE + " = '{0}'", sectype);
            //}

            //通过交易所过滤
            if (cbexchange.SelectedIndex != 0)
            {
                strFilter = string.Format(strFilter + " and " + EXCHANGEID + " = '{0}'", cbexchange.SelectedValue);
            }

            //通过交易所过滤
            //if (cbsecurity.SelectedIndex != 0)
            //{
            //    strFilter = string.Format(strFilter + " and " + SECID + " = '{0}'", cbsecurity.SelectedValue);
            //}


            //通过交易标识过滤
            //if (cbtradeable.SelectedIndex != 0)
            //{
            //    int sv = int.Parse(cbtradeable.SelectedValue.ToString());
            //    if (sv == 1)
            //    {
            //        strFilter = string.Format(strFilter + " and " + TRADEABLE + " = '{0}'", true);
            //    }
            //    else if (sv == -1)
            //    {
            //        strFilter = string.Format(strFilter + " and " + TRADEABLE + " = '{0}'", false);
            //    }
            //}
            //Globals.Debug("strFilter:" + strFilter);
            datasource.Filter = strFilter;
        }



        Dictionary<int, SymbolImpl> symbolmap = new Dictionary<int, SymbolImpl>();
        Dictionary<int, int> symbolidxmap = new Dictionary<int, int>();

        int SymbolIdx(int id)
        {
            int rowid = -1;
            if (symbolidxmap.TryGetValue(id, out rowid))
            {
                return rowid;
            }
            else
            {
                return -1;
            }
        }

        string GetTradeableTitle(bool tradeable)
        {
            return tradeable ? "是" : "否";
        }


        void InvokeGotSymbol(SymbolImpl sym)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<SymbolImpl>(InvokeGotSymbol), new object[] { sym });
            }
            else
            {
                if (string.IsNullOrEmpty(sym.Symbol))
                    return;
                int r = SymbolIdx(sym.ID);
                if (r == -1)
                {
                    gt.Rows.Add(sym.ID);
                    int i = gt.Rows.Count - 1;

                    symbolmap.Add(sym.ID, sym);
                    symbolidxmap.Add(sym.ID, i);

                    gt.Rows[i][SYMBOL] = sym.Symbol;
                    gt.Rows[i][NAME] = sym.GetName();
                    gt.Rows[i][ENTRYCOMMISSION] = sym.EntryCommission;
                    gt.Rows[i][EXITCOMMISSION] = sym.ExitCommission;


                    //gt.Rows[i][SECID] = sym.SecurityFamily != null ? (sym.SecurityFamily as SecurityFamilyImpl).ID : 0;
                    //gt.Rows[i][SECCODE] = sym.SecurityFamily != null ? sym.SecurityFamily.Code : "未设置";
                    //gt.Rows[i][SECTYPE] = sym.SecurityType;
                    gt.Rows[i][EXCHANGEID] = sym.SecurityFamily != null ? (sym.SecurityFamily.Exchange as Exchange).ID : 0;
                    gt.Rows[i][EXCHANGE] = sym.SecurityFamily != null ? sym.SecurityFamily.Exchange.Title : "未设置";

                    gt.Rows[i][TRADEABLE] = sym.Tradeable;
                    gt.Rows[i][TRADEABLETITLE] = GetTradeableTitle(sym.Tradeable);
                    
                }
                else
                {
                    int i = r;
                    gt.Rows[i][SYMBOL] = sym.Symbol;
                    gt.Rows[i][NAME] = sym.GetName();
                    gt.Rows[i][ENTRYCOMMISSION] = sym.EntryCommission;
                    gt.Rows[i][EXITCOMMISSION] = sym.ExitCommission;
                   

                    //gt.Rows[i][SECID] = sym.SecurityFamily != null ? (sym.SecurityFamily as SecurityFamilyImpl).ID : 0;
                    //gt.Rows[i][SECCODE] = sym.SecurityFamily != null ? sym.SecurityFamily.Code : "未设置";
                    //gt.Rows[i][SECTYPE] = sym.SecurityType;
                    gt.Rows[i][EXCHANGEID] = sym.SecurityFamily != null ? (sym.SecurityFamily.Exchange as Exchange).ID : 0;
                    gt.Rows[i][EXCHANGE] = sym.SecurityFamily != null ? sym.SecurityFamily.Exchange.Title : "未设置";

               
                    gt.Rows[i][TRADEABLE] = sym.Tradeable;
                    gt.Rows[i][TRADEABLETITLE] = GetTradeableTitle(sym.Tradeable);
                   
                }
            }
        }


        #region 表格

        #region 显示字段

        const string ID = "ID";
        const string SYMBOL = "证券代码";
        const string NAME = "证券名称";

        const string ENTRYCOMMISSION = "开仓手续费";
        const string EXITCOMMISSION = "平仓手续费";
        const string MARGIN = "保证金";
        //const string EXTRAMARGIN = "额外保证金";
        //const string MAINTANCEMARGIN = "过夜保证金";

        const string EXCHANGEID = "ExchangeID";
        const string EXCHANGE = "交易所";

        const string TRADEABLE = "TRADEABLE";
        const string TRADEABLETITLE = "允许交易";
        //const string SYMBOLTYPE = "类别";



        #endregion

        DataTable gt = new DataTable();
        BindingSource datasource = new BindingSource();

        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = symGrid;

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
            gt.Columns.Add(ID);//
            gt.Columns.Add(SYMBOL);//
            gt.Columns.Add(NAME);
            gt.Columns.Add(ENTRYCOMMISSION);//
            gt.Columns.Add(EXITCOMMISSION);//
            gt.Columns.Add(MARGIN);//

            gt.Columns.Add(EXCHANGEID);//
            gt.Columns.Add(EXCHANGE);//


            gt.Columns.Add(TRADEABLE);
            gt.Columns.Add(TRADEABLETITLE);
            //gt.Columns.Add(SYMBOLTYPE);

        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = symGrid;

            datasource.DataSource = gt;
            grid.DataSource = datasource;

            //需要在绑定数据源后设定具体的可见性
            //grid.Columns[EXCHANGEID].IsVisible = false;
            grid.Columns[ID].Visible = false;
            

            grid.Columns[EXCHANGEID].Visible = false;

            grid.Columns[TRADEABLE].Visible = false;

            //grid.Columns[EXTRAMARGIN].Visible = false;
            //grid.Columns[MAINTANCEMARGIN].Visible = false;

            //grid.Columns[UNDERLAYINGSYMBOL].Visible = false;
            //grid.Columns[UNDERLAYING].Visible = false;


        }





        #endregion

    }
}
