﻿using System;
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
using Common.Logging;


namespace TradingLib.MoniterControl
{
    public partial class ctTradeView : UserControl
    {

        ILog logger = LogManager.GetLogger("ctTradeView");

        string _defaultformat = "{0:F2}";
        


        public ctTradeView()
        {
            InitializeComponent();

            SetPreferences();
            InitTable();
            BindToTable();

            WireEvent();
            
        }
        void WireEvent()
        {
            tradeGrid.CellFormatting += new DataGridViewCellFormattingEventHandler(tradeGrid_CellFormatting);
            tradeGrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(tradeGrid_RowPrePaint);
        }
        void tradeGrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts = e.PaintParts ^ DataGridViewPaintParts.Focus;
        }

        

       
        public void GotFill(Trade t)
        {
            if (InvokeRequired)
                Invoke(new FillDelegate(GotFill), new object[] { t });
            else
            {
                DataRow r = tb.Rows.Add(t.id);
                AccountItem account = CoreService.BasicInfoTracker.GetAccount(t.Account);
                int i = tb.Rows.Count - 1;
                tb.Rows[i][ID] = t.id;
                tb.Rows[i][DATETIME] = Util.ToDateTime(t.xDate, t.xTime).ToString("HH:mm:ss");
                tb.Rows[i][RAWDATETIME] = Util.ToTLDateTime(t.xDate, t.xTime);
                tb.Rows[i][SYMBOL] = t.Symbol;
                tb.Rows[i][SIDE] = (t.Side ? "买入" : "   卖出");
                tb.Rows[i][SIZE] = Math.Abs(t.xSize);
                tb.Rows[i][PRICE] = string.Format(t.oSymbol.SecurityFamily.GetPriceFormat(), t.xPrice);
                tb.Rows[i][COMMISSION] = string.Format(_defaultformat, t.GetCommission());
                tb.Rows[i][OPERATION] = Util.GetEnumDescription(t.OffsetFlag);
                tb.Rows[i][ACCOUNT] = t.Account;
                tb.Rows[i][PROFIT] = string.Format(_defaultformat,t.Profit);
                if (CoreService.SiteInfo.ProductType == QSEnumProductType.VendorMoniter)
                {
                    tb.Rows[i][FILLID] = t.BrokerTradeID;
                }
                if (CoreService.SiteInfo.ProductType == QSEnumProductType.CounterSystem)
                {
                    tb.Rows[i][FILLID] = t.TradeID;
                }

                if (account != null)
                {
                    tb.Rows[i][COMMISSIONACCTCURRENCY] = string.Format(_defaultformat, t.GetCommission() * account.GetExchangeRate(t.oSymbol.SecurityFamily.Currency));
                    tb.Rows[i][PROFITACCTCURRENCY] = string.Format(_defaultformat, t.Profit * account.GetExchangeRate(t.oSymbol.SecurityFamily.Currency));
                }

                tb.Rows[i][EXCHANGE] = CoreService.BasicInfoTracker.GetExchangeName(t.Exchange);
                tb.Rows[i][ORDERSYSID] = t.OrderSysID;
               
            }
        }

        public void Clear()
        {
            tradeGrid.DataSource = null;
            tb.Rows.Clear();
            BindToTable();
        }


        #region 表格
        const string ID = "委托编号";
        const string DATETIME = "成交时间";
        const string SYMBOL = "合约";
        const string SIDE = "买卖";
        const string SIZE = "成交手数";
        const string PRICE = "成交价格";
        const string COMMISSION = "手续费";
        const string COMMISSIONACCTCURRENCY = "C(基币)";
        const string OPERATION = "开平";
        const string ACCOUNT = "交易帐户";
        const string PROFIT = "盈亏";
        const string PROFITACCTCURRENCY = "P(基币)";
        const string FILLID = "成交编号";
        const string EXCHANGE = "交易所";
        const string ORDERSYSID = "报单编号";
        const string RAWDATETIME = "DATETIME";

        DataTable tb = new DataTable();

        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences() 
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = tradeGrid;
;

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
        /// <summary>
        /// 初始化数据表格
        /// </summary>
        private void InitTable()
        {
            tb.Columns.Add(FILLID);
            tb.Columns.Add(SYMBOL);
            tb.Columns.Add(SIDE);
            tb.Columns.Add(OPERATION);
            tb.Columns.Add(PRICE);
            tb.Columns.Add(SIZE);
            tb.Columns.Add(DATETIME);
            tb.Columns.Add(ORDERSYSID);
            tb.Columns.Add(EXCHANGE);
            tb.Columns.Add(COMMISSION);
            tb.Columns.Add(COMMISSIONACCTCURRENCY);
            tb.Columns.Add(PROFIT);
            tb.Columns.Add(PROFITACCTCURRENCY);
            
            tb.Columns.Add(ACCOUNT);
            tb.Columns.Add(ID);
            tb.Columns.Add(RAWDATETIME);
        }

        BindingSource datasource = new BindingSource();
        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = tradeGrid;

            datasource.DataSource = tb;
            datasource.Sort = RAWDATETIME + " DESC";
            grid.DataSource = datasource;

            grid.Columns[ID].Visible = false;
            grid.Columns[RAWDATETIME].Visible = false;

            for (int i = 0; i < tb.Columns.Count; i++)
            {
                grid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            
        }

        #endregion



        void ResetColumSize()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = tradeGrid;

        }
        private void ctTradeView_Load(object sender, EventArgs e)
        {
           
        }

        void tradeGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {

                if (e.ColumnIndex == 2)
                {
                    e.CellStyle.Font = UIConstant.BoldFont;
                    if (e.Value.ToString() == "买入")
                    {
                        e.CellStyle.ForeColor = UIConstant.LongSideColor;

                    }
                    else
                    {
                        e.CellStyle.ForeColor = UIConstant.ShortSideColor;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Cellformating error:" + ex.ToString());
            }
        }




    }
}
