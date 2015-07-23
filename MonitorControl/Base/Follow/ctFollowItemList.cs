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
            GridWidth();
        }

        void itemGrid_SizeChanged(object sender, EventArgs e)
        {
            GridWidth();
        }

        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback("FollowCentre", "QryEntryFollowItemList", OnQryEntryItemList);
            CoreService.EventContrib.RegisterCallback("FollowCentre", "QryExitFollowItemList", OnQryExitItemList);

            CoreService.TLClient.ReqContribRequest("FollowCentre", "QryEntryFollowItemList", "1");
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("FollowCentre", "QryEntryFollowItemList", OnQryEntryItemList);
            CoreService.EventContrib.UnRegisterCallback("FollowCentre", "QryExitFollowItemList", OnQryExitItemList);
        }


        void OnQryEntryItemList(string json, bool islast)
        {
            EntryFollowItemStruct item = MoniterHelper.ParseJsonResponse<EntryFollowItemStruct>(json);
            if (item != null)
            {
                InvokeGotFollowItem(item);
            }
            if (islast)
            {
                CoreService.TLClient.ReqContribRequest("FollowCentre", "QryExitFollowItemList", "1");
            }
        }

        void OnQryExitItemList(string json, bool islast)
        {
            ExitFollowItemStruct item = MoniterHelper.ParseJsonResponse<ExitFollowItemStruct>(json);
            if (item != null)
            {
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
                gt.Rows.Add();
                int i = gt.Rows.Count - 1;

                gt.Rows[i][ITEMID] = 0;
                gt.Rows[i][FOLLOWKEY] = item.FollowKey;
                gt.Rows[i][ENTRYFOLLOWKEY] = item.EntryFollowKey;
                //gt.Rows[i][SIGNAL] = string.Format("{0}[1]", item.SignalToken, item.SignalID);
                
                gt.Rows[i][EXITCLOSETRADEID] = item.CloseTradeID;
                gt.Rows[i][EXITSIGPRICE] = item.SigPrice;
                gt.Rows[i][EXITSIGSIZE] = item.SigSize;
                gt.Rows[i][EXITSIDE] = item.Side ? "买平" : "卖平";
                gt.Rows[i][EXITFOLLOWSIZE] = item.FollowSentSize;
                gt.Rows[i][EXITFOLLOWFILLSIZE] = item.FollowFillSize;
                gt.Rows[i][EXITFOLLOWAVGPRICE] = item.FollowAvgPrice;
                gt.Rows[i][EXITFOLLOWSLIP] = item.FollowSlip;
                gt.Rows[i][EXITFOLLOWPROFIT] = item.FollowProfit;

                //gt.Rows[i][EXITFOLLOWSTAGE] = item.Stage;


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
                gt.Rows.Add();
                int i = gt.Rows.Count - 1;

                gt.Rows[i][ITEMID] = 0;
                gt.Rows[i][FOLLOWKEY] = item.FollowKey;

                gt.Rows[i][SIGNAL] = string.Format("{0}[1]",item.SignalToken,item.SignalID);
                gt.Rows[i][ENTRYOPENTRADEID] = item.OpenTradeID;
                gt.Rows[i][ENTRYSIGPRICE] = item.SigPrice;
                gt.Rows[i][ENTRYSIGSIZE] = item.SigSize;
                gt.Rows[i][ENTRYSIDE] = item.Side?"买开":"卖开";
                gt.Rows[i][ENTRYFOLLOWSIZE] = item.FollowSentSize;
                gt.Rows[i][ENTRYFOLLOWFILLSIZE] = item.FollowFillSize;
                gt.Rows[i][ENTRYFOLLOWAVGPRICE] = item.FollowAvgPrice;
                gt.Rows[i][ENTRYFOLLOWSLIP] = item.FollowSlip;


                gt.Rows[i][TOTALSLIP] = item.TotalSlip;
                gt.Rows[i][TOTALPROFIT] = item.TotalRealizedPL;



                //cfgmap.TryAdd(cfg.ID, cfg);
                //cfgrowmap.TryAdd(cfg.ID, i);
            }
        }




        #region 表格

        const string ITEMID = "ID";
        const string FOLLOWKEY = "FollowKey";
        const string ENTRYFOLLOWKEY = "EntryKey";
        const string SIGNAL = "信号";


        const string ENTRYOPENTRADEID = "开仓成交";
        const string ENTRYSIGPRICE = "价格(O)";
        const string ENTRYSIGSIZE = "数量(O)";
        const string ENTRYSIDE = "方向(O)";
        const string ENTRYFOLLOWSIZE = "跟单(O)";
        const string ENTRYFOLLOWFILLSIZE = "成交(O)";
        const string ENTRYFOLLOWAVGPRICE = "均价(O)";
        const string ENTRYFOLLOWSLIP = "滑点(O)";
        const string ENTRYFOLLOWSTAGE = "状态(O)";

        const string EXITCLOSETRADEID = "平仓成交";
        const string EXITSIGPRICE = "价格(C)";
        const string EXITSIGSIZE = "数量(C)";
        const string EXITSIDE = "方向(C)";
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
            gt.Columns.Add(FOLLOWKEY);
            gt.Columns.Add(ENTRYFOLLOWKEY);
            gt.Columns.Add(SIGNAL);//1

            gt.Columns.Add(ENTRYOPENTRADEID);//2
            gt.Columns.Add(ENTRYSIGPRICE);//3
            gt.Columns.Add(ENTRYSIGSIZE);//4
            gt.Columns.Add(ENTRYSIDE);
            gt.Columns.Add(ENTRYFOLLOWSIZE);//5
            gt.Columns.Add(ENTRYFOLLOWFILLSIZE);//6
            gt.Columns.Add(ENTRYFOLLOWAVGPRICE);//7
            gt.Columns.Add(ENTRYFOLLOWSLIP);//7
            gt.Columns.Add(ENTRYFOLLOWSTAGE);//8


            gt.Columns.Add(EXITCLOSETRADEID);//2
            gt.Columns.Add(EXITSIGPRICE);//3
            gt.Columns.Add(EXITSIGSIZE);//4
            gt.Columns.Add(EXITSIDE);
            gt.Columns.Add(EXITFOLLOWSIZE);//5
            gt.Columns.Add(EXITFOLLOWFILLSIZE);//6
            gt.Columns.Add(EXITFOLLOWAVGPRICE);//7
            gt.Columns.Add(EXITFOLLOWSLIP);//7
            gt.Columns.Add(EXITFOLLOWPROFIT);//7
            gt.Columns.Add(EXITFOLLOWSTAGE);//8

            gt.Columns.Add(TOTALSLIP);//8
            gt.Columns.Add(TOTALPROFIT);//8



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
            itemGrid.Columns[FOLLOWKEY].Visible = false;
            itemGrid.Columns[ENTRYFOLLOWKEY].Visible = false;


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
