using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;


namespace TradingLib.MoniterControl
{
    public partial class ctFollowItemList : UserControl, IEventBinder
    {
        public ctFollowItemList()
        {
            InitializeComponent();
            SetPreferences();
            InitTable();
            BindToTable();

            this.Load += new EventHandler(ctFollowItemList_Load);
        }

        void ctFollowItemList_Load(object sender, EventArgs e)
        {
            //全局事件回调
            CoreService.EventCore.RegIEventHandler(this);
            itemGrid.SizeChanged += new EventHandler(itemGrid_SizeChanged);
            itemGrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(itemGrid_RowPrePaint);
            itemGrid.CellFormatting += new DataGridViewCellFormattingEventHandler(itemGrid_CellFormatting);
            
            GridWidth();
        }

        void itemGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
            string evtype = itemGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            //if (e.ColumnIndex == 1)
            {
                QSEnumPositionEventType type = (QSEnumPositionEventType)Enum.Parse(typeof(QSEnumPositionEventType), evtype);
                if (type == QSEnumPositionEventType.EntryPosition)
                {
                    e.CellStyle.BackColor = Color.WhiteSmoke;
                    //itemGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.WhiteSmoke; 

                    if (e.ColumnIndex == 9)
                    {
                        bool side = (bool)itemGrid.Rows[e.RowIndex].Cells[8].Value;
                        if (side)
                        {
                            e.CellStyle.ForeColor = UIConstant.LongSideColor;
                        }
                        else
                        {
                            e.CellStyle.ForeColor = UIConstant.ShortSideColor;
                        }
                    }
                }
                else
                {
                    if (e.ColumnIndex == 19)
                    {
                        bool side = (bool)itemGrid.Rows[e.RowIndex].Cells[18].Value;
                        if (side)
                        {
                            e.CellStyle.ForeColor = UIConstant.LongSideColor;
                        }
                        else
                        {
                            e.CellStyle.ForeColor = UIConstant.ShortSideColor;
                        }
                    }
                }
            }
            

            //if (e.ColumnIndex == 8 || e.ColumnIndex == 9 || e.ColumnIndex == 11)
            //{
            //    e.CellStyle.Font = UIConstant.BoldFont;
            //    decimal v = 0;
            //    decimal.TryParse(e.Value.ToString(), out v);
            //    if (v > 0)
            //    {
            //        e.CellStyle.ForeColor = UIConstant.LongSideColor;
            //    }
            //    else if (v < 0)
            //    {
            //        e.CellStyle.ForeColor = UIConstant.ShortSideColor;
            //    }
            //    else if (v == 0)
            //    {
            //        e.CellStyle.ForeColor = System.Drawing.Color.Black;
            //    }

            //}
        }

        void itemGrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts = e.PaintParts ^ DataGridViewPaintParts.Focus;
        }


        void itemGrid_SizeChanged(object sender, EventArgs e)
        {
            GridWidth();
        }


        ConcurrentDictionary<string, EntryFollowItemStruct> entrymap = new ConcurrentDictionary<string, EntryFollowItemStruct>();
        ConcurrentDictionary<string, int> entryrowmap = new ConcurrentDictionary<string, int>();
        ConcurrentDictionary<string, ExitFollowItemStruct> exitmap = new ConcurrentDictionary<string, ExitFollowItemStruct>();
        ConcurrentDictionary<string, int> exitrowmp = new ConcurrentDictionary<string, int>();

        int EntryRowIdx(EntryFollowItemStruct entry)
        { 
            int idx=-1;
            if (entryrowmap.TryGetValue(entry.FollowKey, out idx))
            {
                return idx;
            }
            return -1;
        }

        int ExitRowIdx(ExitFollowItemStruct exit)
        {
            int idx = -1;
            if (exitrowmp.TryGetValue(exit.FollowKey, out idx))
            {
                return idx;
            }
            return -1;
        }

        FollowStrategyConfig cfgSelected = null;

        /// <summary>
        /// 响应策略列表中双击某个策略 用于查询并显示该策略的跟单项目
        /// </summary>
        /// <param name="cfg"></param>
        public void OnStrategySelected(FollowStrategyConfig cfg)
        {
            //MessageBox.Show("strategy id:" + cfg.ID.ToString());
            if (cfg == null) return;
            Clear();

            cfgSelected = cfg;
            CoreService.TLClient.ReqContribRequest("FollowCentre", "QryEntryFollowItemList", cfg.ID.ToString());
        }

        void Clear()
        {
            entrymap.Clear();
            entryrowmap.Clear();
            exitmap.Clear();
            exitrowmp.Clear();

            //清空缓存
            itemGrid.DataSource = null;
            gt.Rows.Clear();
            BindToTable();

        }
        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback("FollowCentre", "QryEntryFollowItemList", OnQryEntryItemList);
            CoreService.EventContrib.RegisterCallback("FollowCentre", "QryExitFollowItemList", OnQryExitItemList);
            CoreService.EventContrib.RegisterNotifyCallback("FollowCentre", "EntryFollowItemNotify", OnNotifyEntryItem);
            CoreService.EventContrib.RegisterNotifyCallback("FollowCentre", "ExitFollowItemNotify", OnNotifyExitItem);

            //CoreService.TLClient.ReqContribRequest("FollowCentre", "QryEntryFollowItemList", "1");
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("FollowCentre", "QryEntryFollowItemList", OnQryEntryItemList);
            CoreService.EventContrib.UnRegisterCallback("FollowCentre", "QryExitFollowItemList", OnQryExitItemList);
            CoreService.EventContrib.UnRegisterNotifyCallback("FollowCentre", "EntryFollowItemNotify", OnNotifyEntryItem);
            CoreService.EventContrib.UnRegisterNotifyCallback("FollowCentre", "ExitFollowItemNotify", OnNotifyExitItem);

        }


        void OnNotifyEntryItem(string json)
        {
            EntryFollowItemStruct item = MoniterHelper.ParseJsonResponse<EntryFollowItemStruct>(json);
            if (item != null)
            {
                if (cfgSelected == null)
                    return;
                if (cfgSelected.ID != item.StrategyID)
                    return;

                InvokeGotFollowItem(item);
            }

        }

        void OnNotifyExitItem(string json)
        {
            ExitFollowItemStruct item = MoniterHelper.ParseJsonResponse<ExitFollowItemStruct>(json);
            if (item != null)
            {
                if (cfgSelected == null)
                    return;
                if (cfgSelected.ID != item.StrategyID)
                    return;

                InvokeGotFollowItem(item);
            }
        }

        void OnQryEntryItemList(string json, bool islast)
        {
            EntryFollowItemStruct item = MoniterHelper.ParseJsonResponse<EntryFollowItemStruct>(json);
            if (item != null)
            {
                if (cfgSelected == null)
                    return;
                if (cfgSelected.ID != item.StrategyID)
                    return;

                InvokeGotFollowItem(item);
            }
            if (islast)
            {
                if (cfgSelected != null)
                {
                    CoreService.TLClient.ReqContribRequest("FollowCentre", "QryExitFollowItemList", cfgSelected.ID.ToString());
                }
            }
        }

        void OnQryExitItemList(string json, bool islast)
        {
            ExitFollowItemStruct item = MoniterHelper.ParseJsonResponse<ExitFollowItemStruct>(json);
            if (item != null)
            {
                if (cfgSelected == null)
                    return;
                if (cfgSelected.ID != item.StrategyID)
                    return;

                InvokeGotFollowItem(item);
            }
        }

        void InvokeGotFollowItem(ExitFollowItemStruct item)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ExitFollowItemStruct>(InvokeGotFollowItem), new object[] { item });
            }
            else
            {
                int i = ExitRowIdx(item);
                if (i == -1)
                {
                    gt.Rows.Add();
                    i = gt.Rows.Count - 1;

                    gt.Rows[i][ITEMID] = 0;
                    gt.Rows[i][POSEVENTTYPE] = QSEnumPositionEventType.ExitPosition;
                    gt.Rows[i][FOLLOWKEY] = item.FollowKey;
                    gt.Rows[i][ENTRYFOLLOWKEY] = item.EntryFollowKey;
                    //gt.Rows[i][SIGNAL] = string.Format("{0}[1]", item.SignalToken, item.SignalID);

                    gt.Rows[i][EXITCLOSETRADEID] = item.CloseTradeID;
                    gt.Rows[i][EXITSIGPRICE] = item.SigPrice;
                    gt.Rows[i][EXITSIGSIZE] = item.SigSize;
                    gt.Rows[i][EXITSIDE] = item.Side;
                    gt.Rows[i][EXITSIDESTR] = item.Side ? "买平" : "卖平";
                    gt.Rows[i][EXITFOLLOWSIZE] = item.FollowSentSize;
                    gt.Rows[i][EXITFOLLOWFILLSIZE] = item.FollowFillSize;
                    gt.Rows[i][EXITFOLLOWAVGPRICE] = item.FollowAvgPrice;
                    gt.Rows[i][EXITFOLLOWSLIP] = item.FollowSlip;
                    gt.Rows[i][EXITFOLLOWPROFIT] = item.FollowProfit;

                    gt.Rows[i][EXITFOLLOWSTAGE] = Util.GetEnumDescription(item.Stage);

                    exitmap.TryAdd(item.FollowKey, item);
                    exitrowmp.TryAdd(item.FollowKey, i);
                }
                else //更新
                {
                    gt.Rows[i][EXITFOLLOWSIZE] = item.FollowSentSize;
                    gt.Rows[i][EXITFOLLOWFILLSIZE] = item.FollowFillSize;
                    gt.Rows[i][EXITFOLLOWAVGPRICE] = item.FollowAvgPrice;
                    gt.Rows[i][EXITFOLLOWSLIP] = item.FollowSlip;
                    gt.Rows[i][EXITFOLLOWPROFIT] = item.FollowProfit;

                    gt.Rows[i][EXITFOLLOWSTAGE] = Util.GetEnumDescription(item.Stage);
                    
                }

                //cfgmap.TryAdd(cfg.ID, cfg);
                //cfgrowmap.TryAdd(cfg.ID, i);
            }
        }



        void InvokeGotFollowItem(EntryFollowItemStruct item)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<EntryFollowItemStruct>(InvokeGotFollowItem), new object[] { item });
            }
            else
            {
                int i = EntryRowIdx(item);
                if (i == -1)
                {
                    gt.Rows.Add();
                    i = gt.Rows.Count - 1;

                    gt.Rows[i][ITEMID] = 0;
                    gt.Rows[i][POSEVENTTYPE] = QSEnumPositionEventType.EntryPosition;
                    gt.Rows[i][FOLLOWKEY] = item.FollowKey;

                    gt.Rows[i][SIGNAL] = string.Format("{0}[1]", item.SignalToken, item.SignalID);
                    gt.Rows[i][ENTRYOPENTRADEID] = item.OpenTradeID;
                    gt.Rows[i][ENTRYSIGPRICE] = item.SigPrice;
                    gt.Rows[i][ENTRYSIGSIZE] = item.SigSize;
                    gt.Rows[i][ENTRYSIDE] = item.Side;
                    gt.Rows[i][ENTRYSIDESTR] = item.Side ? "买开" : "卖开";

                    gt.Rows[i][ENTRYFOLLOWSIZE] = item.FollowSentSize;
                    gt.Rows[i][ENTRYFOLLOWFILLSIZE] = item.FollowFillSize;
                    gt.Rows[i][ENTRYFOLLOWAVGPRICE] = item.FollowAvgPrice;
                    gt.Rows[i][ENTRYFOLLOWSLIP] = item.FollowSlip;
                    gt.Rows[i][ENTRYFOLLOWSTAGE] = Util.GetEnumDescription(item.Stage);

                    gt.Rows[i][TOTALSLIP] = item.TotalSlip;
                    gt.Rows[i][TOTALPROFIT] = item.TotalRealizedPL;

                    entrymap.TryAdd(item.FollowKey, item);
                    entryrowmap.TryAdd(item.FollowKey, i);
                }
                else
                {
                    gt.Rows[i][ENTRYFOLLOWSIZE] = item.FollowSentSize;
                    gt.Rows[i][ENTRYFOLLOWFILLSIZE] = item.FollowFillSize;
                    gt.Rows[i][ENTRYFOLLOWAVGPRICE] = item.FollowAvgPrice;
                    gt.Rows[i][ENTRYFOLLOWSLIP] = item.FollowSlip;
                    gt.Rows[i][ENTRYFOLLOWSTAGE] = Util.GetEnumDescription(item.Stage);

                    gt.Rows[i][TOTALSLIP] = item.TotalSlip;
                    gt.Rows[i][TOTALPROFIT] = item.TotalRealizedPL;
                }

                //cfgmap.TryAdd(cfg.ID, cfg);
                //cfgrowmap.TryAdd(cfg.ID, i);
            }
        }




        #region 表格

        const string ITEMID = "ID";
        const string POSEVENTTYPE = "持仓事件";
        const string FOLLOWKEY = "FollowKey";
        const string ENTRYFOLLOWKEY = "EntryKey";
        const string SIGNAL = "信号";


        const string ENTRYOPENTRADEID = "开仓成交";
        const string ENTRYSIGPRICE = "价格(O)";
        const string ENTRYSIGSIZE = "数量(O)";
        const string ENTRYSIDE = "Side(O)";
        const string ENTRYSIDESTR = "方向(O)";
        const string ENTRYFOLLOWSIZE = "跟单(O)";
        const string ENTRYFOLLOWFILLSIZE = "成交(O)";
        const string ENTRYFOLLOWAVGPRICE = "均价(O)";
        const string ENTRYFOLLOWSLIP = "滑点(O)";
        const string ENTRYFOLLOWSTAGE = "状态(O)";

        const string EXITCLOSETRADEID = "平仓成交";
        const string EXITSIGPRICE = "价格(C)";
        const string EXITSIGSIZE = "数量(C)";
        const string EXITSIDE = "Side(C)";
        const string EXITSIDESTR = "方向(C)";
        const string EXITFOLLOWSIZE = "跟单(C)";
        const string EXITFOLLOWFILLSIZE = "成交(C)";
        const string EXITFOLLOWAVGPRICE = "均价(C)";
        const string EXITFOLLOWSLIP = "滑点(C)";
        const string EXITFOLLOWPROFIT = "盈亏(C)";
        const string EXITFOLLOWSTAGE = "状态(C)";

        const string TOTALSLIP = "滑点(T)";
        const string TOTALPROFIT = "平仓盈亏(T)";





        DataTable gt = new DataTable();
        BindingSource datasource = new BindingSource();




        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = itemGrid;

            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            grid.ColumnHeadersHeight = 25;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.StateCommon.Background.Color1 = Color.WhiteSmoke;
            grid.StateCommon.Background.Color2 = Color.WhiteSmoke;

        }

        //初始化Account显示空格
        private void InitTable()
        {

            gt.Columns.Add(ITEMID);//0
            gt.Columns.Add(POSEVENTTYPE);//1
            gt.Columns.Add(FOLLOWKEY);//2
            gt.Columns.Add(ENTRYFOLLOWKEY);//3
            gt.Columns.Add(SIGNAL);//4

            gt.Columns.Add(ENTRYOPENTRADEID);//5
            gt.Columns.Add(ENTRYSIGPRICE);//6
            gt.Columns.Add(ENTRYSIGSIZE);//7
            gt.Columns.Add(ENTRYSIDE,typeof(bool));//8
            gt.Columns.Add(ENTRYSIDESTR);//9
            gt.Columns.Add(ENTRYFOLLOWSIZE);//10
            gt.Columns.Add(ENTRYFOLLOWFILLSIZE);//11
            gt.Columns.Add(ENTRYFOLLOWAVGPRICE);//12
            gt.Columns.Add(ENTRYFOLLOWSLIP);//13
            gt.Columns.Add(ENTRYFOLLOWSTAGE);//14


            gt.Columns.Add(EXITCLOSETRADEID);//15
            gt.Columns.Add(EXITSIGPRICE);//16
            gt.Columns.Add(EXITSIGSIZE);//17
            gt.Columns.Add(EXITSIDE,typeof(bool));//18
            gt.Columns.Add(EXITSIDESTR);//19
            gt.Columns.Add(EXITFOLLOWSIZE);//20
            gt.Columns.Add(EXITFOLLOWFILLSIZE);//21
            gt.Columns.Add(EXITFOLLOWAVGPRICE);//22
            gt.Columns.Add(EXITFOLLOWSLIP);//23
            gt.Columns.Add(EXITFOLLOWPROFIT);//24
            gt.Columns.Add(EXITFOLLOWSTAGE);//25

            gt.Columns.Add(TOTALSLIP);//26
            gt.Columns.Add(TOTALPROFIT);//27



        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            datasource.DataSource = gt;
            datasource.Sort = FOLLOWKEY + " ASC";
            itemGrid.DataSource = datasource;

            itemGrid.Columns[ITEMID].Visible = false;
            itemGrid.Columns[POSEVENTTYPE].Visible = false;
            itemGrid.Columns[FOLLOWKEY].Visible = false;
            itemGrid.Columns[ENTRYFOLLOWKEY].Visible = false;

            itemGrid.Columns[ENTRYSIDE].Visible = false;
            itemGrid.Columns[EXITSIDE].Visible = false;

            for (int i = 0; i < gt.Columns.Count; i++)
            {
                itemGrid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

           
        }

        private void GridWidth()
        {

            itemGrid.Columns[ITEMID].Width = 100;
            itemGrid.Columns[SIGNAL].Width = 80;

            itemGrid.Columns[ENTRYOPENTRADEID].Width = 80;
            itemGrid.Columns[ENTRYSIGPRICE].Width = 60;
            itemGrid.Columns[ENTRYSIGSIZE].Width = 60;
            itemGrid.Columns[ENTRYFOLLOWFILLSIZE].Width = 60;
            itemGrid.Columns[ENTRYFOLLOWAVGPRICE].Width = 60;
            itemGrid.Columns[ENTRYFOLLOWSTAGE].Width = 60;

            itemGrid.Columns[EXITCLOSETRADEID].Width = 80;
            itemGrid.Columns[EXITSIGPRICE].Width = 60;
            itemGrid.Columns[EXITSIGSIZE].Width = 60;
            itemGrid.Columns[EXITFOLLOWFILLSIZE].Width = 60;
            itemGrid.Columns[EXITFOLLOWAVGPRICE].Width = 60;
            //itemGrid.Columns[EXITFOLLOWSTAGE].Width = 50;


        }

        #endregion
    }
}
