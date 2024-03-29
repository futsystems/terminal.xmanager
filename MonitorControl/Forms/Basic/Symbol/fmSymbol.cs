﻿using System;
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
    public partial class fmSymbol : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmSymbol()
        {
            InitializeComponent();
            SetPreferences();
            InitTable();
            BindToTable();

            MoniterHelper.AdapterToIDataSource(cbsecurity).BindDataSource(MoniterHelper.GetSecurityComboxArrayViaExchange(0, SecurityType.FUT, true));
            MoniterHelper.AdapterToIDataSource(cbtradeable).BindDataSource(MoniterHelper.GetTradeableCBList(true));
            MoniterHelper.AdapterToIDataSource(cbexchange).BindDataSource(MoniterHelper.GetExchangeComboxArray(true));

            this.Load += new EventHandler(fmSymbol_Load);
        }

        void fmSymbol_Load(object sender, EventArgs e)
        {
            WireEvent();
            foreach (SymbolImpl sym in CoreService.BasicInfoTracker.Symbols)
            {
                InvokeGotSymbol(sym);
            }
        }


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


        string GetMonth(SymbolImpl sym)
        {
            switch (sym.SecurityFamily.Type)
            { 
                case SecurityType.FUT:
                    return sym.Month;
                case SecurityType.STK:
                    return "";
                default:
                    return "";
            }
        }

        string GetExpireDate(SymbolImpl sym)
        {
            switch (sym.SecurityFamily.Type)
            {
                case SecurityType.FUT:
                    return sym.ExpireDate.ToString();
                case SecurityType.STK:
                    return "";
                default:
                    return "";
            }
        }
        string GetMargin(SymbolImpl sym)
        {
            switch (sym.SecurityFamily.Type)
            {
                case SecurityType.FUT:
                    return sym.Margin.ToString();
                case SecurityType.STK:
                    return "";
                default:
                    return "";
            }
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


                    symbolidxmap.Add(sym.ID, i);

                    gt.Rows[i][SYMBOL] = sym.Symbol;
                    gt.Rows[i][NAME] = sym.GetTitleName(true);
                    gt.Rows[i][ENTRYCOMMISSION] = sym.EntryCommission;
                    gt.Rows[i][EXITCOMMISSION] = sym.ExitCommission;
                    gt.Rows[i][EXITCOMMISSIONTODAY] = sym.ExitCommissionToday;
                    gt.Rows[i][MARGIN] = GetMargin(sym);
                    gt.Rows[i][EXTRAMARGIN] = sym.ExtraMargin;
                    gt.Rows[i][MAINTANCEMARGIN] = sym.MaintanceMargin;

                    gt.Rows[i][SECID] = sym.SecurityFamily != null ?(sym.SecurityFamily as SecurityFamilyImpl).ID:0;
                    gt.Rows[i][SECCODE] = sym.SecurityFamily != null ? sym.SecurityFamily.Code : "未设置";
                    gt.Rows[i][SECTYPE] = sym.SecurityType;
                    gt.Rows[i][EXCHANGEID] = sym.SecurityFamily != null ? (sym.SecurityFamily.Exchange as ExchangeImpl).ID : 0;
                    gt.Rows[i][EXCHANGE] = sym.SecurityFamily != null ? sym.SecurityFamily.Exchange.Title : "未设置";

                    gt.Rows[i][UNDERLAYINGID] = sym.underlaying_fk;
                    gt.Rows[i][UNDERLAYING] = sym.ULSymbol != null ? sym.ULSymbol.Symbol : "无";

                    gt.Rows[i][UNDERLAYINGSYMBOLID] = sym.underlayingsymbol_fk;
                    gt.Rows[i][UNDERLAYINGSYMBOL] = sym.UnderlayingSymbol != null ? sym.UnderlayingSymbol.Symbol : "无";
                    gt.Rows[i][MONTH] = GetMonth(sym);
                    gt.Rows[i][EXPIREDATE] = GetExpireDate(sym);
                    gt.Rows[i][TRADEABLE] = sym.Tradeable;
                    gt.Rows[i][TRADEABLETITLE] = GetTradeableTitle(sym.Tradeable);
                    gt.Rows[i][SYMBOLTYPE] = Util.GetEnumDescription(sym.SymbolType);
                    gt.Rows[i][TAG] = sym;
                }
                else
                {
                    int i = r;
                    gt.Rows[i][SYMBOL] = sym.Symbol;
                    gt.Rows[i][NAME] = sym.GetTitleName(true);
                    gt.Rows[i][ENTRYCOMMISSION] = sym.EntryCommission;
                    gt.Rows[i][EXITCOMMISSION] = sym.ExitCommission;
                    gt.Rows[i][EXITCOMMISSIONTODAY] = sym.ExitCommissionToday;
                    gt.Rows[i][MARGIN] = GetMargin(sym);
                    gt.Rows[i][EXTRAMARGIN] = sym.ExtraMargin;
                    gt.Rows[i][MAINTANCEMARGIN] = sym.MaintanceMargin;

                    gt.Rows[i][SECID] = sym.SecurityFamily != null ? (sym.SecurityFamily as SecurityFamilyImpl).ID : 0;
                    gt.Rows[i][SECCODE] = sym.SecurityFamily != null ? sym.SecurityFamily.Code : "未设置";
                    gt.Rows[i][SECTYPE] = sym.SecurityType;
                    gt.Rows[i][EXCHANGEID] = sym.SecurityFamily != null ? (sym.SecurityFamily.Exchange as ExchangeImpl).ID : 0;
                    gt.Rows[i][EXCHANGE] = sym.SecurityFamily != null ? sym.SecurityFamily.Exchange.Title : "未设置";

                    gt.Rows[i][UNDERLAYINGID] = sym.underlaying_fk;
                    gt.Rows[i][UNDERLAYING] = sym.ULSymbol != null ? sym.ULSymbol.Symbol : "无";

                    gt.Rows[i][UNDERLAYINGSYMBOLID] = sym.underlayingsymbol_fk;
                    gt.Rows[i][UNDERLAYINGSYMBOL] = sym.UnderlayingSymbol != null ? sym.UnderlayingSymbol.Symbol : "无";
                    gt.Rows[i][MONTH] = GetMonth(sym);//sym.Month;
                    gt.Rows[i][EXPIREDATE] = GetExpireDate(sym);//sym.ExpireDate;
                    gt.Rows[i][TRADEABLE] = sym.Tradeable;
                    gt.Rows[i][TRADEABLETITLE] = GetTradeableTitle(sym.Tradeable);
                    gt.Rows[i][SYMBOLTYPE] = Util.GetEnumDescription(sym.SymbolType);
                }
            }
        }



        #region 表格
        #region 显示字段

        const string ID = "全局ID";
        const string SYMBOL = "合约";
        const string NAME = "名称";
        const string ENTRYCOMMISSION = "开仓手续费";
        const string EXITCOMMISSION = "平仓手续费";
        const string EXITCOMMISSIONTODAY = "平今手续费";
        const string MARGIN = "保证金";
        const string EXTRAMARGIN = "额外保证金";
        const string MAINTANCEMARGIN = "过夜保证金";
        const string SECID = "SecID";//品种编号
        const string SECCODE = "品种";
        const string SECTYPE = "SecType";
        const string EXCHANGEID = "ExchangeID";
        const string EXCHANGE = "交易所";
        const string UNDERLAYINGID = "UnderLayingID";
        const string UNDERLAYING = "异化底层证券";
        const string UNDERLAYINGSYMBOLID = "UnderLayingSymbolID";
        const string UNDERLAYINGSYMBOL = "底层证券";
        const string MONTH = "月份";
        const string EXPIREDATE = "到期日";
        const string TRADEABLE = "TRADEABLE";
        const string TRADEABLETITLE = "允许交易";
        const string SYMBOLTYPE = "类别";
        const string TAG = "TAG";


        #endregion

        DataTable gt = new DataTable();
        BindingSource datasource = new BindingSource();

        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = symgrid;

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
            gt.Columns.Add(EXITCOMMISSIONTODAY);
            gt.Columns.Add(MARGIN);//
            gt.Columns.Add(EXTRAMARGIN);//
            gt.Columns.Add(MAINTANCEMARGIN);//
            gt.Columns.Add(SECID);
            gt.Columns.Add(SECCODE);
            gt.Columns.Add(SECTYPE);
            gt.Columns.Add(EXCHANGEID);//
            gt.Columns.Add(EXCHANGE);//
            gt.Columns.Add(UNDERLAYINGID);//
            gt.Columns.Add(UNDERLAYING);//
            gt.Columns.Add(UNDERLAYINGSYMBOLID);//
            gt.Columns.Add(UNDERLAYINGSYMBOL);//
            gt.Columns.Add(MONTH);
            gt.Columns.Add(EXPIREDATE);
            gt.Columns.Add(TRADEABLE);
            gt.Columns.Add(TRADEABLETITLE);
            gt.Columns.Add(SYMBOLTYPE);
            gt.Columns.Add(TAG, typeof(SymbolImpl));

        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = symgrid;

            datasource.DataSource = gt;
            grid.DataSource = datasource;

            //需要在绑定数据源后设定具体的可见性
            //grid.Columns[EXCHANGEID].IsVisible = false;
            grid.Columns[ID].Visible = false;
            grid.Columns[SECTYPE].Visible = false;
            grid.Columns[SECID].Visible = false;

            grid.Columns[UNDERLAYINGID].Visible = false;
            grid.Columns[UNDERLAYINGSYMBOLID].Visible = false;

            grid.Columns[EXCHANGEID].Visible = false;
            grid.Columns[TRADEABLE].Visible = false;

            grid.Columns[EXTRAMARGIN].Visible = false;
            grid.Columns[MAINTANCEMARGIN].Visible = false;

            grid.Columns[UNDERLAYINGSYMBOL].Visible = false;
            grid.Columns[UNDERLAYING].Visible = false;
            grid.Columns[TAG].Visible = false;

            
        }





        #endregion

        void FilterSymbol()
        {
            string strFilter = string.Format(SECTYPE + " > '{0}'", "*");

            //通过交易所过滤
            if (cbexchange.SelectedIndex != 0)
            {
                strFilter = string.Format(strFilter + " and " + EXCHANGEID + " = '{0}'", cbexchange.SelectedValue);
            }

            //通过交易所过滤
            if (cbsecurity.SelectedIndex != 0)
            {
                strFilter = string.Format(strFilter + " and " + SECID + " = '{0}'", cbsecurity.SelectedValue);
            }


            //通过交易标识过滤
            if (cbtradeable.SelectedIndex != 0)
            {
                int sv = int.Parse(cbtradeable.SelectedValue.ToString());
                if (sv == 1)
                {
                    strFilter = string.Format(strFilter + " and " + TRADEABLE + " = '{0}'", true);
                }
                else if (sv == -1)
                {
                    strFilter = string.Format(strFilter + " and " + TRADEABLE + " = '{0}'", false);
                }
            }
            datasource.Filter = strFilter;
        }


        //得到当前选择的行号
        private SymbolImpl CurrentSymbol
        {
            get
            {
                int row = symgrid.SelectedRows.Count > 0 ? symgrid.SelectedRows[0].Index : -1;
                if (row >= 0)
                {
                    return symgrid[TAG, row].Value as SymbolImpl;
                }
                else
                {
                    return null;
                }
            }
        }


        public void OnInit()
        {
            btnAddSymbol.Visible = CoreService.SiteInfo.Manager.IsRoot();
            btnSyncSymbols.Visible = CoreService.SiteInfo.Manager.IsRoot();

            if (!CoreService.SiteInfo.Domain.Super)
            {
                btnDisableAll.Visible = false;
                btnAddSymbol.Visible = false;
                btnSyncSymbols.Visible = false;
                btnImportStk.Visible = false;
                if (CoreService.SiteInfo.Manager.IsRoot())
                {
                    btnSyncSymbols.Visible = CoreService.BasicInfoTracker.Symbols.Count() == 0;
                }
            }
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_INFO_SYM, OnNotifySymbol);
        }
        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_INFO_SYM, OnNotifySymbol);
        }

        void OnNotifySymbol(string json)
        {
            string content = json.DeserializeObject<string>();
            SymbolImpl sym = SymbolImpl.Deserialize(content);
            var localSym = CoreService.BasicInfoTracker.GetSymbol(sym.ID);
            InvokeGotSymbol(localSym);
        }

        void WireEvent()
        {
            CoreService.EventCore.RegIEventHandler(this);

            cbsecurity.SelectedIndexChanged += new EventHandler(cbsecurity_SelectedIndexChanged);
            cbexchange.SelectedIndexChanged += new EventHandler(cbexchange_SelectedIndexChanged);
            cbtradeable.SelectedIndexChanged += new EventHandler(cbtradeable_SelectedIndexChanged);

            symgrid.DoubleClick +=new EventHandler(symgrid_DoubleClick);
            symgrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(symgrid_RowPrePaint);

            btnAddSymbol.Click +=new EventHandler(btnAddSymbol_Click);
            btnSyncSymbols.Click +=new EventHandler(btnSyncSymbols_Click);
            btnDisableAll.Click += new EventHandler(btnDisableAll_Click);
            btnImportStk.Click += new EventHandler(btnImportStk_Click);
        }

        void btnImportStk_Click(object sender, EventArgs e)
        {
            fmImportStk fm = new fmImportStk();
            fm.ShowDialog();
            fm.Close();
        }
       

        void symgrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts = e.PaintParts ^ DataGridViewPaintParts.Focus;
        }

  




        private void cbsecurity_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FilterSymbol();
        }

        private void cbexchange_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FilterSymbol();

            //通过交易所ID获得该交易所所有合约
            if (CoreService.BasicInfoTracker.Initialized)
            {
                int exid = (int)cbexchange.SelectedValue;
                MoniterHelper.AdapterToIDataSource(cbsecurity).BindDataSource(MoniterHelper.GetSecurityComboxArrayViaExchange(exid, SecurityType.FUT, true));
            }
        }

        private void symgrid_DoubleClick(object sender, EventArgs e)
        {
            if (!CoreService.SiteInfo.Manager.IsRoot()) return;
            SymbolImpl symbol = CurrentSymbol;
            if (symbol != null)
            {
                fmSymbolEdit fm = new fmSymbolEdit();
                fm.Symbol = symbol;
                fm.ShowDialog();
            }
        }

        private void cbtradeable_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FilterSymbol();
        }

        private void btnAddSymbol_Click(object sender, EventArgs e)
        {
            fmSymbolEdit fm = new fmSymbolEdit();
            fm.ShowDialog();
        }

        bool _first = true;
        DateTime _lastack = DateTime.Now;

        void btnDisableAll_Click(object sender, EventArgs e)
        {
            if (!_first)
            {
                if ((DateTime.Now - _lastack).TotalSeconds < 10)
                {
                    MoniterHelper.WindowMessage("请不要频繁同步或禁止合约");
                    return;
                }
            }

            if (MoniterHelper.WindowConfirm("确认禁止所有合约?") == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqDisableAllSymbols();
                _first = false;
            }
        }


        private void btnSyncSymbols_Click(object sender, EventArgs e)
        {
            if (!_first)
            {
                if ((DateTime.Now - _lastack).TotalSeconds < 10)
                {
                    MoniterHelper.WindowMessage("请不要频繁同步或禁止合约");
                    return;
                }
            }
            if (MoniterHelper.WindowConfirm("确认同步合约数据?") == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqSyncSymbol();
                _first = false;
            }
        }

        void fm_GotSymbolImplEvent(SymbolImpl sym, bool islast)
        {
            //如果不存在合约则新增
            if (sym.security_fk == 0)
            {
                return;
            }
            if (CoreService.BasicInfoTracker.GetSymbol(sym.Symbol) == null)
            {

                CoreService.TLClient.ReqUpdateSymbol(sym);
            }
            else
            {
                CoreService.TLClient.ReqUpdateSymbol(sym);
            }
        }

    }
}
