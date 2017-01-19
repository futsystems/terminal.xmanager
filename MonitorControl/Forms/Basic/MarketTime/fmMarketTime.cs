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
    public partial class fmMarketTime : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmMarketTime()
        {
            InitializeComponent();
            

            this.Load += new EventHandler(fmMarketTime_Load);
            
        }

        void fmMarketTime_Load(object sender, EventArgs e)
        {
            SetPreferences();
            InitTable();
            BindToTable();
            WireEvent();

            foreach (MarketTimeImpl mt in CoreService.BasicInfoTracker.MarketTimes)
            {
                InvokeGotMarketTime(mt);
            }
        }




        public bool AnyMarketTime
        {
            get
            {
                return markettimemap.Count > 0;
            }
        }
        Dictionary<int, MarketTimeImpl> markettimemap = new Dictionary<int, MarketTimeImpl>();
        Dictionary<int, int> markettimeidxmap = new Dictionary<int, int>();

        int MarketTimeIdx(int id)
        {
            int rowid = -1;
            if (markettimeidxmap.TryGetValue(id, out rowid))
            {
                return rowid;
            }
            else
            {
                return -1;
            }
        }


        delegate void MarketTimeDel(MarketTimeImpl mt);

        void InvokeGotMarketTime(MarketTimeImpl mt)
        {
            if (InvokeRequired)
            {
                Invoke(new MarketTimeDel(InvokeGotMarketTime), new object[] { mt });
            }
            else
            {
                int r = MarketTimeIdx(mt.ID);
                if (r == -1)
                {
                    gt.Rows.Add(mt.ID);
                    int i = gt.Rows.Count - 1;
                    gt.Rows[i][MTNAME] = mt.Name;
                    gt.Rows[i][MTDESC] = mt.Description;
                    gt.Rows[i][CLOSETIME] = Util.ToDateTime(Util.ToTLDate(), mt.CloseTime).ToString("HH:mm:ss");

                    markettimemap.Add(mt.ID, mt);
                    markettimeidxmap.Add(mt.ID, i);

                }
                else
                {
                    int i = r;
                    gt.Rows[i][MTNAME] = mt.Name;
                    gt.Rows[i][MTDESC] = mt.Description;
                    gt.Rows[i][CLOSETIME] = Util.ToDateTime(Util.ToTLDate(), mt.CloseTime).ToString("HH:mm:ss");

                }
            }
        }

        void WireEvent()
        { 

            mtgrid.DoubleClick +=new EventHandler(mtgrid_DoubleClick);
            mtgrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(mtgrid_RowPrePaint);
        }

       


        #region 表格
        #region 显示字段

        const string MTID = "全局ID";
        const string MTNAME = "名称";
        const string MTDESC = "描述";
        const string CLOSETIME = "收盘时间";
        #endregion

        DataTable gt = new DataTable();
        BindingSource datasource = new BindingSource();

        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = mtgrid;

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
            gt.Columns.Add(MTID);//
            gt.Columns.Add(MTNAME);//
            gt.Columns.Add(MTDESC);//
            gt.Columns.Add(CLOSETIME);
        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = mtgrid;
            datasource.DataSource = gt;
            grid.DataSource = datasource;

            grid.Columns[MTID].Width = 60;
            //grid.Columns[MTNAME].Width = 200;
            grid.Columns[CLOSETIME].Width = 60;

        }





        #endregion


        void mtgrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts = e.PaintParts ^ DataGridViewPaintParts.Focus;
        }



        private void mtgrid_DoubleClick(object sender, EventArgs e)
        {
            fmMarketTimeInfo fm = new fmMarketTimeInfo();
            MarketTimeImpl mt = GetVisibleMarketTime(CurrentMarketTimeID);
            if (mt != null)
            {
                fm.SetMarketTime(mt);
                fm.ShowDialog();
            }
        }

        private int CurrentRow { get { return mtgrid.SelectedRows.Count > 0 ? mtgrid.SelectedRows[0].Index : -1; } }
        
        //得到当前选择的行号
        private int CurrentMarketTimeID
        {
            get
            {
                int row = CurrentRow;
                if (row >= 0)
                {
                    return int.Parse(mtgrid[0,row].Value.ToString());
                }
                else
                {
                    return 0;
                }
            }
        }

        //通过行号得该行的Security
        MarketTimeImpl GetVisibleMarketTime(int id)
        {
            MarketTimeImpl mt = null;
            if (markettimemap.TryGetValue(id, out mt))
            {
                return mt;
            }
            else
            {
                return null;
            }

        }

    }
}
