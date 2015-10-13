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
    public partial class fmMarketTimeInfo : ComponentFactory.Krypton.Toolkit.KryptonForm//,IEventBinder
    {
        public fmMarketTimeInfo()
        {
            InitializeComponent();

            SetPreferences();
            InitTable();
            BindToTable();

            //MoniterHelper.AdapterToIDataSource(timezone).BindDataSource(MoniterHelper.GetTimeZoneList());
            btnAdd.Click += new EventHandler(btnAdd_Click);
            btnDel.Click += new EventHandler(btnDel_Click);
            //btnEdit.Click += new EventHandler(btnEdit_Click);
            btnSubmit.Click += new EventHandler(btnSubmit_Click);

            CoreService.EventBasicInfo.OnMarketTimeEvent += new Action<MarketTime>(EventBasicInfo_OnMarketTimeEvent);

            //timezone.SelectedIndex = 1;
        }

        void EventBasicInfo_OnMarketTimeEvent(MarketTime obj)
        {
            if (obj.ID == _mt.ID)
            {
                LogService.Info("got markettime obj..");
                SetMarketTime(obj);
            }
        }


        //public void OnInit()
        //{ 
        
        //}

        //public void OnDisposed()
        //{ 
            
        //}

        string GetRangesStr()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var range in rangemap.Values)
            {
                sb.Append('#');
                sb.Append(TradingRangeImpl.Serialize(range));
            }
            return sb.ToString();
        }
        void btnSubmit_Click(object sender, EventArgs e)
        {
            //更新MarketTime
            if (_mt != null)
            {
                
                string rangestr = GetRangesStr();
                LogService.Debug("RangeStr:" + rangestr);
                //_mt.TimeZone = timezone.SelectedValue.ToString();
                _mt.CloseTime = Util.ToTLTime(closetime.Value);
                _mt.DeserializeTradingRange(rangestr);

                CoreService.TLClient.ReqUpdateMarketTime(_mt);
            }
            else
            {

            }
        }

        //void btnEdit_Click(object sender, EventArgs e)
        //{
        //    string key = CurrentRangeKey;
        //    TradingRange range = null;
        //    //MessageBox.Show("currentkey:" + key);
        //    if (rangemap.TryGetValue(key, out range))
        //    {
        //        fmTradingRange fm = new fmTradingRange();
        //        fm.SetTradingRange(range);
        //        if (fm.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
        //        {
        //            TradingRange outrange = fm.CurrentRange;
        //            GotTradingRange(outrange);
        //        }

        //    }
        //    else
        //    {
        //        MoniterHelper.WindowMessage("请选择要编辑的交易小节");
        //        return;
        //    }
        //}

        void btnDel_Click(object sender, EventArgs e)
        {

            string key = CurrentRangeKey;
            if (string.IsNullOrEmpty(key))
            {
                MoniterHelper.WindowMessage("请选择需要删除的交易小节");
                return;
            }
            else
            {
                RemoveTrangeRange(key);
            }
            
        }

        void RemoveTrangeRange(string rangekey)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(RemoveTrangeRange), new object[] { rangekey });
            }
            else
            {
                int idx = -1;
                for (int i = 0; i < gt.Rows.Count; i++)
                {
                    if (gt.Rows[i][KEY].ToString() == rangekey)
                    {
                        idx = i;
                    }
                }
                if (idx != -1)
                {
                    gt.Rows.RemoveAt(idx);

                    rangemap.Remove(rangekey);
                    //rangeidxmap.Remove(rangekey);
                }
            }
        }

        void btnAdd_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            fmTradingRange fm = new fmTradingRange();
            if (fm.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                TradingRangeImpl range = fm.CurrentRange;
                GotTradingRange(range);
            }
        }

        MarketTime _mt = null;

        public void SetMarketTime(MarketTime mt)
        {
            //清空原有数据
            rangemap.Clear();
            gt.Clear();
            
            _mt = mt;
            lbMTName.Text = mt.Name;
            lbDesp.Text = mt.Description;
            this.closetime.Value = Util.ToDateTime(Util.ToTLDate(), _mt.CloseTime);
            //timezone.SelectedValue = string.IsNullOrEmpty(mt.TimeZone) ? "" : mt.TimeZone;
            foreach (var m in mt.RangeList.Values)
            {
                GotTradingRange(m as TradingRangeImpl);
            }
        }


        //得到当前选择的行号
        private string CurrentRangeKey
        {
            get
            {
                int row = rangeGrid.SelectedRows.Count > 0 ? rangeGrid.SelectedRows[0].Index : -1;
                if (row >= 0)
                {
                    return rangeGrid[0, row].Value.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }


        //Dictionary<string,int> rangeidxmap = new Dictionary<string,int>();
        Dictionary<string, TradingRange> rangemap = new Dictionary<string, TradingRange>();
        //int RangeKey2Idx(string rangekey)
        //{
        //    int idx = -1;
        //    if (rangeidxmap.TryGetValue(rangekey, out idx))
        //    {
        //        return idx;
        //    }
        //    return -1;
        //}

        void GotTradingRange(TradingRangeImpl range)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<TradingRangeImpl>(GotTradingRange), new object[] { range });
            }
            else
            {
                bool contains = rangemap.Keys.Contains(range.RangeKey);
                if (!contains)
                {
                    gt.Rows.Add();
                    int i = gt.Rows.Count - 1;
                    gt.Rows[i][KEY] = range.RangeKey;
                    gt.Rows[i][STARTDAY] = GetWeekDayTitle(range.StartDay);
                    gt.Rows[i][STARTTIME] = GetTimeStr(range.StartTime);
                    gt.Rows[i][ENDDAY] = GetWeekDayTitle(range.EndDay);

                    gt.Rows[i][ENDTIME] = GetTimeStr(range.EndTime);
                    gt.Rows[i][SETTLEFLAG] = Util.GetEnumDescription(range.SettleFlag);
                    gt.Rows[i][CLOSEMARKET] = range.MarketClose ? "Y" : "";
                    //rangeidxmap.Add(range.RangeKey, i);
                    rangemap.Add(range.RangeKey, range);

                }
                //else
                //{
                //    int i = r;
                //    gt.Rows[i][STARTDAY] = GetWeekDayTitle(range.StartDay);
                //    gt.Rows[i][STARTTIME] = GetTimeStr(range.StartTime);
                //    gt.Rows[i][ENDDAY] = GetWeekDayTitle(range.EndDay);

                //    gt.Rows[i][ENDTIME] = GetTimeStr(range.EndTime);
                //    gt.Rows[i][SETTLEFLAG] = Util.GetEnumDescription(range.SettleFlag);

                //    rangemap.Add(range.RangeKey, range);
                //}
            }
        }

        string GetTimeStr(int time)
        {
            return Util.ToDateTime(Util.ToTLDate(), time).ToString("HH:mm:ss");
        }
        string GetWeekDayTitle(DayOfWeek day)
        {
            switch (day)
            { 
                case DayOfWeek.Monday: return "星期一";
                case DayOfWeek.Tuesday: return "星期二";
                case DayOfWeek.Wednesday: return "星期三";
                case DayOfWeek.Thursday: return "星期四";
                case DayOfWeek.Friday: return "星期五";
                case DayOfWeek.Saturday: return "星期六";
                case DayOfWeek.Sunday: return "星期日";
                default:
                    return "异常";
            }
        }
        #region 表格
        #region 显示字段

        const string KEY = "KEY";
        const string STARTDAY = "开始日期";
        const string STARTTIME = "开始时间";
        const string ENDDAY = "结束日期";
        const string ENDTIME = "结束时间";
        const string SETTLEFLAG = "结算日";
        const string CLOSEMARKET = "收盘";


        #endregion

        DataTable gt = new DataTable();
        BindingSource datasource = new BindingSource();

        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = rangeGrid;

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
            gt.Columns.Add(KEY);
            gt.Columns.Add(STARTDAY);//
            gt.Columns.Add(STARTTIME);//
            gt.Columns.Add(ENDDAY);//
            gt.Columns.Add(ENDTIME);//
            gt.Columns.Add(SETTLEFLAG);
            gt.Columns.Add(CLOSEMARKET);
        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = rangeGrid;
            datasource.DataSource = gt;
            datasource.Sort = KEY + " ASC";
            grid.DataSource = datasource;

            grid.Columns[KEY].Visible = false;
            //grid.Columns[MTID].Width = 80;
            //grid.Columns[MTNAME].Width = 200;

        }





        #endregion


    }
}
