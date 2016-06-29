using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TradingLib.TinyMGRControl
{
    public partial class ctSTKSymbol : UserControl
    {
        public ctSTKSymbol()
        {
            InitializeComponent();

            SetPreferences();
            InitTable();
            BindToTable();

            //MoniterHelper.AdapterToIDataSource(cbsecurity).BindDataSource(CoreService.BasicInfoTracker.GetSecurityCombListViaExchange(0, SecurityType.FUT, true));
            //MoniterHelper.AdapterToIDataSource(cbtradeable).BindDataSource(MoniterHelper.GetTradeableCBList(true));
            //MoniterHelper.AdapterToIDataSource(cbexchange).BindDataSource(CoreService.BasicInfoTracker.GetExchangeCombList(true));


        }




        #region 表格
        #region 显示字段

        const string ID = "ID";
        const string SYMBOL = "合约";

        const string ENTRYCOMMISSION = "开仓手续费";
        const string EXITCOMMISSION = "平仓手续费";
        const string MARGIN = "保证金";
        //const string EXTRAMARGIN = "额外保证金";
        //const string MAINTANCEMARGIN = "过夜保证金";
        const string SECID = "SecID";//品种编号
        const string SECCODE = "品种";
        const string SECTYPE = "SecType";
        const string EXCHANGEID = "ExchangeID";
        const string EXCHANGE = "交易所";
        //const string UNDERLAYINGID = "UnderLayingID";
        //const string UNDERLAYING = "异化底层证券";
        //const string UNDERLAYINGSYMBOLID = "UnderLayingSymbolID";
        //const string UNDERLAYINGSYMBOL = "底层证券";
        const string MONTH = "月份";
        const string EXPIREDATE = "到期日";
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
            gt.Columns.Add(ENTRYCOMMISSION);//
            gt.Columns.Add(EXITCOMMISSION);//
            gt.Columns.Add(MARGIN);//
            //gt.Columns.Add(EXTRAMARGIN);//
            //gt.Columns.Add(MAINTANCEMARGIN);//
            gt.Columns.Add(SECID);
            gt.Columns.Add(SECCODE);
            gt.Columns.Add(SECTYPE);
            gt.Columns.Add(EXCHANGEID);//
            gt.Columns.Add(EXCHANGE);//
            //gt.Columns.Add(UNDERLAYINGID);//
            //gt.Columns.Add(UNDERLAYING);//
            //gt.Columns.Add(UNDERLAYINGSYMBOLID);//
            //gt.Columns.Add(UNDERLAYINGSYMBOL);//
            gt.Columns.Add(MONTH);
            gt.Columns.Add(EXPIREDATE);
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
            grid.Columns[SECTYPE].Visible = false;
            grid.Columns[SECID].Visible = false;

            //grid.Columns[UNDERLAYINGID].Visible = false;
            //grid.Columns[UNDERLAYINGSYMBOLID].Visible = false;

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
