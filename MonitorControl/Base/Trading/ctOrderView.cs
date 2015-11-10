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
using System.Threading;
using Common.Logging;

namespace TradingLib.MoniterControl
{
    public partial class ctOrderView : UserControl,IEventBinder
    {
        ILog logger = LogManager.GetLogger("OrderVeiw");

        public event DebugDelegate SendDebugEvent;
        void debug(string msg)
        {
            if (SendDebugEvent != null)
                SendDebugEvent(msg);
        }

        //属性获得和设置
        [DefaultValue(true)]
        bool _enableoperation = true;
        public bool EnableOperation
        {
            get
            {
                return _enableoperation;
            }
            set
            {
                _enableoperation = value;
                btnCancelAll.Visible = _enableoperation;
                btnCancelOrder.Visible = _enableoperation;
            }
        }

        public event LongDelegate SendOrderCancel;
        string _defaultformat = "{0:F1}";
        /// <summary>
        /// 查询某个合约用于
        /// </summary>
        public event FindSymbolDel FindSecurityEvent;
        Symbol findSecurity(string symbol)
        {
            if (FindSecurityEvent != null)
                return FindSecurityEvent(symbol);
            else
                return null;
        }

        string getDisplayFormat(string symbol)
        {
            Symbol sec = findSecurity(symbol);
            if (sec == null)
                return _defaultformat;
            else
                return MoniterHelper.GetPriceTickFormat(sec.SecurityFamily.PriceTick);
        }

        public ctOrderView()
        {
            InitializeComponent();

            SetPreferences();
            InitTable();
            BindToTable();

            WireEvent();//绑定事件
            
        }

        public void OnInit()
        {
            //logger.Info("OrderView Init root:" + CoreService.SiteInfo.Manager.IsRoot().ToString() + " insert:" + CoreService.SiteInfo.Domain.Misc_InsertTrade.ToString());
            btnReserve.Visible = false;
            if (!CoreService.SiteInfo.Domain.Super)
            {
                if (CoreService.SiteInfo.Manager.IsRoot() && CoreService.SiteInfo.Domain.Misc_InsertTrade)
                {
                    btnReserve.Visible = true;
                }
            }
            else
            {
                btnReserve.Visible = true;
            }
        }

        public void OnDisposed()
        { 
        
        }

        OrderTracker ord;
        /// <summary>
        /// 将OrderTracker传递给orderview,orderview本身不维护order信息,ordertracker由clearcentre或者tradingtracker来提供
        /// </summary>
        public OrderTracker OrderTracker { get { return ord; } set { ord = value; } }



        const string ID = "委托编号";
        const string DATETIME = "报单时间";
        const string SYMBOL = "合约";
        const string DIRECTION = "方向";
        const string OPERATION = "买卖";
        const string OFFSETFLAG = "开平";
        const string SIZE = "未成交";
        const string TOTALSIZE = "报单量";
        const string PRICE = "报单价格";
        const string FILLED = "已成交";
        const string STATUS = "挂单状态";
        const string STATUSSTR = "报单状态";
        const string COMMENT = "详细状态";
        const string ORDERREF = "本地编号";
        const string EXCHORDERID = "报单编号";
        const string EXCHANGE = "交易所";
        const string ACCOUNT = "交易帐户";
        const string FORCECLOSE = "强平";
        const string FORCEREASON = "风控信息";

        DataTable tb = new DataTable();
        ConcurrentDictionary<long, int> orderidxmap = new ConcurrentDictionary<long, int>();
        int OrderID2Idx(long id)
        {
            int idx = -1;
            if (orderidxmap.TryGetValue(id, out idx))
            {
                return idx;
            }
            return -1;
        }
        string GetOrderPrice(Order o)
        {
            if (o.isLimit)
            {
                return "限价:" + Util.FormatDecimal(o.LimitPrice, Util.GetPriceTickFormat(o.oSymbol.SecurityFamily.PriceTick));
            }
            else
            {
                return "市价";
            }
           // return o.oSymbol.FormatPrice(o.LimitPrice);
        }

        public void GotOrder(Order o)
        {
            //debug("order view got order:" + o.ToString());
            if (InvokeRequired)
                Invoke(new OrderDelegate(GotOrder), new object[] { o });
            else
            {
                try
                {
                    int i = OrderID2Idx(o.id);
                    if (i == -1)
                    {
                        DataRow r = tb.Rows.Add(o.id);
                        i = tb.Rows.Count - 1;//得到新建的Row号
                        orderidxmap.TryAdd(o.id, i);

                        tb.Rows[i][ID] = o.id;
                        tb.Rows[i][DATETIME] = Util.ToDateTime(o.Date, o.Time).ToString("HH:mm:ss");
                        tb.Rows[i][SYMBOL] = o.Symbol;
                        tb.Rows[i][DIRECTION] = o.Side ? "1" : "-1";
                        tb.Rows[i][OPERATION] = o.Side ? "买入" : "   卖出";
                        tb.Rows[i][OFFSETFLAG] = o.OffsetFlag == QSEnumOffsetFlag.OPEN ? "开仓" : "平仓";
                        tb.Rows[i][SIZE] = Math.Abs(o.Size);
                        tb.Rows[i][TOTALSIZE] = Math.Abs(o.TotalSize);
                        tb.Rows[i][PRICE] = GetOrderPrice(o);
                        tb.Rows[i][FILLED] = Math.Abs(o.FilledSize);
                        tb.Rows[i][STATUS] = o.Status;
                        tb.Rows[i][STATUSSTR] = Util.GetEnumDescription(o.Status);
                        tb.Rows[i][ORDERREF] = o.OrderRef;
                        tb.Rows[i][EXCHANGE] = CoreService.BasicInfoTracker.GetExchangeName(o.Exchange);
                        tb.Rows[i][EXCHORDERID] = o.OrderSysID;
                        tb.Rows[i][ACCOUNT] = o.Account;
                        tb.Rows[i][COMMENT] = o.Comment;
                        tb.Rows[i][FORCECLOSE] = o.ForceClose?"强平":"";
                        tb.Rows[i][FORCEREASON] = o.ForceCloseReason;
                        //num.Text = orderGrid.RowCount.ToString();
                    }
                    else
                    {
                        tb.Rows[i][SIZE] = Math.Abs(o.Size);
                        tb.Rows[i][FILLED] = o.FilledSize;
                        tb.Rows[i][STATUS] = o.Status;
                        tb.Rows[i][STATUSSTR] = Util.GetEnumDescription(o.Status);
                        tb.Rows[i][ORDERREF] = o.OrderRef;
                        tb.Rows[i][EXCHANGE] = o.Exchange;
                        tb.Rows[i][EXCHORDERID] = o.OrderSysID;
                        tb.Rows[i][COMMENT] = o.Comment;
                        tb.Rows[i][FORCECLOSE] = o.ForceClose ? "强平" : "";
                        tb.Rows[i][FORCEREASON] = o.ForceCloseReason;
                    }
                }
                catch (Exception ex)
                {
                    debug("OrderView got order error:" + ex.ToString());
                }
            }
            //debug("xxxxxxxxxxxxx got order");
        
        }


        BindingSource datasource = new BindingSource();
        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = orderGrid;

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
            tb.Columns.Add(EXCHORDERID);//0
            tb.Columns.Add(SYMBOL);//1
            tb.Columns.Add(DIRECTION);//2
            tb.Columns.Add(OPERATION);//3
            tb.Columns.Add(OFFSETFLAG);//3
            tb.Columns.Add(STATUS);//8
            tb.Columns.Add(STATUSSTR);//9

            tb.Columns.Add(PRICE);//6

            tb.Columns.Add(TOTALSIZE);//5
            tb.Columns.Add(SIZE);//5
            tb.Columns.Add(FILLED);//7
            tb.Columns.Add(COMMENT);


            tb.Columns.Add(DATETIME);//1
            tb.Columns.Add(ORDERREF);
            tb.Columns.Add(FORCECLOSE);
            tb.Columns.Add(FORCEREASON);
            tb.Columns.Add(EXCHANGE);
            tb.Columns.Add(ID);//0
            
            
            tb.Columns.Add(ACCOUNT);
        }
        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = orderGrid;
            datasource.DataSource = tb;
            datasource.Sort = DATETIME+ " DESC";
            grid.DataSource = datasource;

            grid.Columns[DIRECTION].Visible = false;
            grid.Columns[STATUS].Visible = false;
            grid.Columns[ID].Visible = false;
            grid.Columns[ORDERREF].Visible = false;

            ResetColumeSize();
           
            for (int i = 0; i < tb.Columns.Count; i++)
            {
                grid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        void ResetColumeSize()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = orderGrid;
            grid.Columns[EXCHORDERID].Width = 120;
            grid.Columns[SYMBOL].Width = 60;
            grid.Columns[DIRECTION].Width = 60;
            grid.Columns[OPERATION].Width = 60;
            grid.Columns[STATUSSTR].Width = 65;
            grid.Columns[PRICE].Width = 70;
            grid.Columns[TOTALSIZE].Width = 60;
            grid.Columns[SIZE].Width = 60;
            grid.Columns[FILLED].Width = 60;
            grid.Columns[DATETIME].Width = 80;
            grid.Columns[EXCHANGE].Width = 60;
            grid.Columns[FORCECLOSE].Width = 60;
            if (this.Width < 800)
            {
                grid.Columns[FORCECLOSE].Visible = false;
                grid.Columns[FORCEREASON].Visible = false;
            }
            else
            {
                grid.Columns[FORCECLOSE].Visible = true;
                grid.Columns[FORCEREASON].Visible = true;
            }
        }


        void WireEvent()
        {
            btnFilterAll.CheckedChanged += new EventHandler(btnFilterAll_CheckedChanged);
            btnFilterPlaced.CheckedChanged +=new EventHandler(btnFilterPlaced_CheckedChanged);
            btnFilterFilled.CheckedChanged += new EventHandler(btnFilterFilled_CheckedChanged);
            btnFilterCancelError.CheckedChanged +=new EventHandler(btnFilterCancelError_CheckedChanged);

            
            btnCancelOrder.Click +=new EventHandler(btnCancelOrder_Click);
            btnCancelAll.Click +=new EventHandler(btnCancelAll_Click);
            btnReserve.Click += new EventHandler(btnReserve_Click);

            orderGrid.SizeChanged += new EventHandler(orderGrid_SizeChanged);
            orderGrid.CellDoubleClick +=new DataGridViewCellEventHandler(orderGrid_CellDoubleClick);
            orderGrid.CellFormatting += new DataGridViewCellFormattingEventHandler(orderGrid_CellFormatting);
            orderGrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(orderGrid_RowPrePaint);

            CoreService.EventCore.RegIEventHandler(this);
        }

        string CurrentOrderID
        {
            get
            {
                if (orderGrid.SelectedRows.Count > 0)
                {
                    return  orderGrid.CurrentRow.Cells[ID].Value.ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        void btnReserve_Click(object sender, EventArgs e)
        {
            string orderid = CurrentOrderID;
            if (string.IsNullOrEmpty(orderid))
            {
                MoniterHelper.WindowMessage("请选择委托");
            }

            CoreService.TLClient.ReqContribRequest("MgrExchServer", "ReverseOrder", orderid);
        }

        void orderGrid_SizeChanged(object sender, EventArgs e)
        {
            ResetColumeSize();
        }

        void orderGrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts = e.PaintParts ^ DataGridViewPaintParts.Focus;
        }





        
        #region 界面事件
        void btnFilterAll_CheckedChanged(object sender, EventArgs e)
        {
            string strFilter = "";
            datasource.Filter = strFilter;
        }

        private void btnFilterPlaced_CheckedChanged(object sender, EventArgs args)
        {
            string strFilter = DATETIME + " ASC";
            strFilter = String.Format(STATUS + " = '{0}' or " + STATUS + " = '{1}'", "Placed", "Opened");
            datasource.Filter = strFilter;
        }

        private void btnFilterFilled_CheckedChanged(object sender, EventArgs args)
        {
            string strFilter = DATETIME + " ASC";
            strFilter = String.Format(STATUS + " = '{0}' ", "Filled");
            datasource.Filter = strFilter;
        }

        private void btnFilterCancelError_CheckedChanged(object sender, EventArgs args)
        {
            string strFilter = DATETIME + " ASC";
            strFilter = String.Format(STATUS + " = '{0}' or " + STATUS + " = '{1}' or " + STATUS + " = '{2}'", "Canceled", "Reject", "Unknown");
            datasource.Filter = strFilter;
        }

        private void btnCancelOrder_Click(object sender, EventArgs e)
        {

            long oid = SelectedOrderID;
            if (oid == -1)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请选择要撤销的委托");
            }
            else
            {
                Order o = ord.SentOrder(oid);//[oid];

                MessageBox.Show("orderid:" + oid +" order status:"+o.Status);
                if (ord.isPending(oid))
                {
                    if (SendOrderCancel != null)
                        SendOrderCancel(oid);
                }
                else
                {
                    ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("该委托无法撤销");
                }
            }
        }

        private void btnCancelAll_Click(object sender, EventArgs e)
        {
            foreach (Order o in ord)
            {
                if (ord.isPending(o.id))
                {
                    Thread.Sleep(5);
                    if (SendOrderCancel != null)
                        SendOrderCancel(o.id);
                }
            }
        }
        private void orderGrid_CellDoubleClick(object sender, EventArgs e)
        {
            try
            {
                long oid = SelectedOrderID;
                if (oid == -1)
                {
                    ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请选择要撤销的委托");
                }
                else
                {
                    if (ord.isPending(oid))
                    {
                        if (SendOrderCancel != null)
                            SendOrderCancel(oid);
                    }
                    else
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("该委托无法撤销");
                    }
                }
            }
            catch (Exception ex)
            { 
                
            }
        }
        #endregion


        long SelectedOrderID
        {
            get
            {
                int row = (orderGrid.SelectedRows.Count > 0 ? orderGrid.SelectedRows[0].Index : -1);
                return long.Parse(orderGrid[ID, row].Value.ToString());
            }
        }

        //格式化输出
        private void orderGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Font = UIConstant.BoldFont;
                int v = 0;
                int.TryParse(orderGrid[2, e.RowIndex].Value.ToString(), out v);
                if (v > 0)
                {
                    e.CellStyle.ForeColor = UIConstant.LongSideColor;
                }
                else
                {
                    e.CellStyle.ForeColor = UIConstant.ShortSideColor;
                }
            }

            if (e.ColumnIndex == 1 || e.ColumnIndex == 3)
            {
                e.CellStyle.Font = UIConstant.BoldFont;
            }
        }


        public void Clear()
        {
            orderGrid.DataSource = null;
            orderidxmap.Clear();
            tb.Rows.Clear();
            BindToTable();
        }
    }
}
