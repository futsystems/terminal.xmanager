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
    public partial class fmExchange : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmExchange()
        {
            InitializeComponent();

            SetPreferences();
            InitTable();
            BindToTable();


            this.Load += new EventHandler(fmExchange_Load);
           
        }

        void fmExchange_Load(object sender, EventArgs e)
        {
            
            exchangegrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(exchangegrid_RowPrePaint);
            exchangegrid.DoubleClick += new EventHandler(exchangegrid_DoubleClick);
            btnAddExchange.Click += new EventHandler(btnAddExchange_Click);
            foreach (Exchange ex in CoreService.BasicInfoTracker.Exchanges)
            {
                this.InvokeGotExchange(ex);
            }

            CoreService.EventBasicInfo.OnExchangeEvent += new Action<Exchange>(EventBasicInfo_OnExchangeEvent);
        }

        void btnAddExchange_Click(object sender, EventArgs e)
        {
            fmExchangeEdit fm = new fmExchangeEdit();
            fm.ShowDialog();
            fm.Close();
        }

        void EventBasicInfo_OnExchangeEvent(Exchange obj)
        {
            InvokeGotExchange(obj);
        }

        private int CurrentRow { get { return exchangegrid.SelectedRows.Count > 0 ? exchangegrid.SelectedRows[0].Index : -1; } }

        
        private int CurrentExchangeID
        {
            get
            {
                int row = CurrentRow;
                if (row >= 0)
                {
                    return int.Parse(exchangegrid[0, row].Value.ToString());
                }
                else
                {
                    return 0;
                }
            }
        }

        void exchangegrid_DoubleClick(object sender, EventArgs e)
        {
            Exchange current = GetExchangeSelected(CurrentExchangeID);
            if (current == null)
            {
                MoniterHelper.WindowMessage("请选择要编辑的交易所");
                return;
            }
            fmExchangeEdit fm = new fmExchangeEdit();
            fm.SetExchange(current);
            fm.ShowDialog();
            fm.Close();
        }



        void exchangegrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts = e.PaintParts ^ DataGridViewPaintParts.Focus;
        }

        //通过行号得该行的Security
        Exchange GetExchangeSelected(int id)
        {
            Exchange ex = null;
            if (exchangemap.TryGetValue(id, out ex))
            {
                return ex;
            }
            else
            {
                return null;
            }

        }

        Dictionary<int, int> exchangeidmap = new Dictionary<int, int>();
        Dictionary<int, Exchange> exchangemap = new Dictionary<int, Exchange>();

        int ExchangeIdx(int id)
        {
            int rowid = -1;
            if (exchangeidmap.TryGetValue(id, out rowid))
            {
                return rowid;
            }
            else
            {
                return -1;
            }
        }


        delegate void ExchangeDel(Exchange ex);
        void InvokeGotExchange(Exchange ex)
        {
            if (InvokeRequired)
            {
                Invoke(new ExchangeDel(InvokeGotExchange), new object[] { ex });
            }
            else
            {
                int r = ExchangeIdx(ex.ID);
                if (r == -1)
                {
                    gt.Rows.Add(ex.ID);
                    int i = gt.Rows.Count - 1;
                    gt.Rows[i][EXNAME] = ex.Name;
                    gt.Rows[i][EXCODE] = ex.EXCode;
                    gt.Rows[i][EXCOUNTRY] = Util.GetEnumDescription(ex.Country);
                    gt.Rows[i][TITLE] = ex.Title;

                    gt.Rows[i][TIMEZONE] = ex.TimeZoneID;//ex.TimeZoneInfo != null ? ex.TimeZoneInfo.DisplayName : "";
                    gt.Rows[i][CALENDAR] = ex.Calendar;
                    gt.Rows[i][SETTLETIME] = Util.ToDateTime(Util.ToTLDate(), ex.CloseTime).ToString("HH:mm:ss");
                    exchangemap.Add(ex.ID, ex);
                    exchangeidmap.Add(ex.ID, i);

                }
                else
                {
                    int i = r;
                    gt.Rows[i][EXNAME] = ex.Name;
                    gt.Rows[i][EXCOUNTRY] = Util.GetEnumDescription(ex.Country);
                    gt.Rows[i][TITLE] = ex.Title;
                    gt.Rows[i][TIMEZONE] = ex.TimeZoneID;// ex.TimeZoneInfo != null ? ex.TimeZoneInfo.DisplayName : "";
                    gt.Rows[i][CALENDAR] = ex.Calendar;
                    gt.Rows[i][SETTLETIME] = Util.ToDateTime(Util.ToTLDate(), ex.CloseTime).ToString("HH:mm:ss");

                }
            }
        }


        #region 表格
        const string EXID = "全局ID";
        
        const string EXCODE = "编号";
        const string EXCOUNTRY = "国家";
        const string EXNAME = "名称";
        const string TITLE = "简称";
        const string TIMEZONE = "时区";
        const string CALENDAR = "交易日历";
        const string SETTLETIME = "结算时间";

        DataTable gt = new DataTable();
        BindingSource datasource = new BindingSource();

        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = exchangegrid;

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
            gt.Columns.Add(EXID);//
            
            gt.Columns.Add(EXCODE);//
            gt.Columns.Add(EXCOUNTRY);//
            gt.Columns.Add(EXNAME);//
            gt.Columns.Add(TITLE);
            gt.Columns.Add(TIMEZONE);
            gt.Columns.Add(CALENDAR);
            gt.Columns.Add(SETTLETIME);
        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = exchangegrid;

            datasource.DataSource = gt;
            grid.DataSource = datasource;

            grid.Columns[EXID].Width = 40;
            
            grid.Columns[EXCODE].Width = 60;
            grid.Columns[EXCOUNTRY].Width = 60;
            grid.Columns[EXNAME].Width = 150;
            grid.Columns[CALENDAR].Width = 60;
            grid.Columns[SETTLETIME].Width = 60;


        }





        #endregion



    }
}
