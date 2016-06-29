using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Logging;

namespace TradingLib.TinyMGRControl
{
    public partial class ctSecurity : UserControl
    {
        ILog logger = LogManager.GetLogger("ctSecurity");
        public ctSecurity()
        {
            InitializeComponent();

            SetPreferences();
            InitTable();
            BindToTable();
        }


        #region 表格
        #region 显示字段

        const string ID = "ID";
        const string CCODE = "编码";
        const string NAME = "名称";
        //const string CURRENCY = "货币";
        const string TYPE = "品种";
        const string MULTIPLE = "乘数";
        const string PRICETICK = "价格变动";

        const string ENTRYCOMMISSION = "开仓手续费";
        const string EXITCOMMISSION = "平仓手续费";
        const string MARGIN = "保证金比例";
        //const string EXTRAMARGIN = "额外保证金";
        //const string MAINTANCEMARGIN = "过夜保证金";
        const string EXCHANGEID = "ExchangeID";
        const string EXCHANGE = "交易所";

        //const string UNDERLAYINGID = "UnderLayingID";
        //const string UNDERLAYING = "底层证券";
        //const string MARKETTIMEID = "MarketTimeID";
        //const string MARKETTIME = "交易时间段";
        const string TRADEABLE = "TRADEABLE";
        const string TRADEABLETITLE = "允许交易";
        //const string DATAFEED = "行情源";
        #endregion

        DataTable gt = new DataTable();
        BindingSource datasource = new BindingSource();

        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = secGrid;

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
            gt.Columns.Add(CCODE);//
            gt.Columns.Add(NAME);//
            //gt.Columns.Add(CURRENCY);//
            gt.Columns.Add(TYPE);//
            gt.Columns.Add(EXCHANGEID);//
            gt.Columns.Add(EXCHANGE);//
            //gt.Columns.Add(MARKETTIMEID);//
            //gt.Columns.Add(MARKETTIME);//

            gt.Columns.Add(MULTIPLE);//
            gt.Columns.Add(PRICETICK);//
            //gt.Columns.Add(TRADABLE);//
            gt.Columns.Add(ENTRYCOMMISSION);//
            gt.Columns.Add(EXITCOMMISSION);//
            gt.Columns.Add(MARGIN);//
            //gt.Columns.Add(EXTRAMARGIN);//
            //gt.Columns.Add(MAINTANCEMARGIN);//

            //gt.Columns.Add(UNDERLAYINGID);//
            //gt.Columns.Add(UNDERLAYING);//

            gt.Columns.Add(TRADEABLE);
            gt.Columns.Add(TRADEABLETITLE);
            //gt.Columns.Add(DATAFEED);
        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = secGrid;

            datasource.DataSource = gt;
            grid.DataSource = datasource;

            //需要在绑定数据源后设定具体的可见性
            grid.Columns[ID].Visible = false;
            //grid.Columns[CURRENCY].Visible = false;
            //grid.Columns[UNDERLAYING].Visible = false;
            grid.Columns[TYPE].Visible = false;

            grid.Columns[EXCHANGEID].Visible = false;
            //grid.Columns[UNDERLAYINGID].Visible = false;
            //grid.Columns[MARKETTIMEID].Visible = false;
            grid.Columns[TRADEABLE].Visible = false;

            //grid.Columns[EXTRAMARGIN].Visible = false;
            //grid.Columns[MAINTANCEMARGIN].Visible = false;
        }





        #endregion

    }
}
