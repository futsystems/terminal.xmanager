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
using Common.Logging;


namespace TradingLib.MoniterControl
{
    public partial class ctPositionView : UserControl
    {
        string _defaultformat = "{0:F1}";

        ILog logger = LogManager.GetLogger("ctPositionView");

        //持仓记录器 客户端是通过TradingTracker组件来记录order position记录的,服务端我们需要自己内建一个positiontracker用于记录相关信息
        LSPositionTracker pt;
        public LSPositionTracker PositionTracker { get { return pt; } set { pt = value; } }
        
        //委托记录器
        OrderTracker _ot;
        public OrderTracker OrderTracker { get { return _ot; } set { _ot = value; } }

        //委托事务辅助,用于执行反手等功能
        OrderTransactionHelper _ordTransHelper;

        public event OrderDelegate SendOrderEvent;//发送委托
        public event LongDelegate SendCancelEvent;//取消委托

        public event PositionDelegate PositionSelectedEvent;//选择了某个持仓

        //通过symbol查找到对应的security
        Symbol findSecurity(string symbol)
        {
            return CoreService.BasicInfoTracker.GetSymbol(symbol);
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
                //btnCancel.Visible = _enableoperation;
                btnFlat.Visible = _enableoperation;
                btnFlatAll.Visible = _enableoperation;
                //btnCancel.Visible = _enableoperation;
                //isDoubleFlat.Visible = _enableoperation;
            }
        }

        

        public ctPositionView()
        {
            InitializeComponent();

            SetPreferences();
            InitTable();
            BindToTable();
        }

        private void ctPositionView_Load(object sender, EventArgs e)
        {
            WireEvent();
        }


        #region 止盈止损参数

        //frmPositionOffset frmPosOffset;
        //止损参数映射
        ConcurrentDictionary<string, PositionOffsetArg> lossOffsetMap = new ConcurrentDictionary<string, PositionOffsetArg>();
        //止盈参数映射
        ConcurrentDictionary<string, PositionOffsetArg> profitOffsetMap = new ConcurrentDictionary<string, PositionOffsetArg>();


        PositionOffsetArg GetLossArgs(string key)
        {
            if (lossOffsetMap.Keys.Contains(key))
                return lossOffsetMap[key];
            else
                return null;
        }

        PositionOffsetArg GetProfitArgs(string key)
        {
            if (profitOffsetMap.Keys.Contains(key))
                return profitOffsetMap[key];
            else
                return null;
        }

        //设置止盈止损参数时,弹出窗体,然后按相应操作触发 参数提交的操作
        //服务端获得参数设置成功后,回传对应的参数

        void ResetOffset(string key)
        {
            PositionOffsetArg p = GetProfitArgs(key);
            if (p != null)
                p.Enable = false;
            PositionOffsetArg l = GetLossArgs(key);
            if (l != null)
                l.Enable = false;
        }

        //获得显示的数值
        string GetGridOffsetText(Position pos,bool side, QSEnumPositionOffsetDirection direction)
        {
            string key = pos.GetKey(side);
            decimal price = direction == QSEnumPositionOffsetDirection.LOSS ? GetLossArgs(key).TargetPrice(pos) : GetProfitArgs(key).TargetPrice(pos);
            if (price == -1)
            {
                return "停止";
            }
            if (price == 0)
            {
                return "无持仓";
            }
            else
            {
                return string.Format(pos.oSymbol.SecurityFamily.GetPriceFormat(), price);
            }

        }

        #endregion


        


        //获得某个持仓的可平数量
        int getCanFlatSize(Position pos)
        {
            return pos.isFlat ? 0 : (pos.UnsignedSize - _ot.GetPendingExitSize(pos.Symbol,pos.DirectionType== QSEnumPositionDirectionType.Long?true:false));
        }


        //持仓key与rowid map
        ConcurrentDictionary<string, int> posRowMap = new ConcurrentDictionary<string, int>();
        int PositionRowIdx(string poskey)
        {
            if (posRowMap.Keys.Contains(poskey))
                return posRowMap[poskey];
            return -1;
        }

        //往datatable中插入一行记录
        int InsertNewRow(Position pos)
        {
            bool side = true;
            if (pos.DirectionType == QSEnumPositionDirectionType.Long)
            {
                side = true;
            }
            else if (pos.DirectionType == QSEnumPositionDirectionType.Short)
            {
                side = false;
            }
            else
            {
                return -1;
            }

            string account = pos.Account;
            string symbol = pos.Symbol;
            gt.Rows.Add(symbol);
            int i = gt.Rows.Count - 1;//得到新建的Row号
            //如果不存在,则我们将该account-symbol对插入映射列表我们的键用的是account_symbol配对
            string key = pos.GetPositionKey();
            gt.Rows[i][SIDE] = side;
            gt.Rows[i][DIRECTION] = side ? "买" : "卖";
            gt.Rows[i][KEY] = key;
            gt.Rows[i][ACCOUNT] = pos.Account;

            //debug("new row inserted,account-symbol-side:" + key);
            if (!posRowMap.ContainsKey(key))
                posRowMap.TryAdd(key, i);
            //同时为该key准备positoinoffsetarg
            lossOffsetMap[key] = new PositionOffsetArg(account, symbol, side, QSEnumPositionOffsetDirection.LOSS);
            profitOffsetMap[key] = new PositionOffsetArg(account, symbol, side, QSEnumPositionOffsetDirection.PROFIT);

            return i;
        }

        #region 辅助功能函数
        /// <summary>
        /// 发送委托
        /// </summary>
        /// <param name="o"></param>
        void SendOrder(Order o)
        {
            if (SendOrderEvent != null)
                SendOrderEvent(o);
        }

        /// <summary>
        /// 撤单
        /// </summary>
        /// <param name="oid"></param>
        void CancelOrder(long oid)
        {
            if (SendCancelEvent != null)
                SendCancelEvent(oid);
        }

        /// <summary>
        /// 平仓
        /// </summary>
        /// <param name="pos"></param>
        void FlatPosition(Position pos)
        {
            if (pos == null || pos.isFlat) return;
            bool side = pos.isLong ? true : false;

            //上期所需要区分平今和平昨 需要按照昨仓和今仓分开提交委托
            if (pos.oSymbol.SecurityFamily.Exchange.EXCode.Equals("SHFE"))
            {
                int voltd = pos.PositionDetailTodayNew.Sum(p => p.Volume);//今日持仓
                int volyd = pos.PositionDetailYdNew.Sum(p => p.Volume);//昨日持仓
                //MessageBox.Show("posyd:" + volyd.ToString() + " voltd:" + voltd.ToString());
                if (volyd != 0)
                {
                    Order oyd = new OrderImpl(pos.Symbol, volyd * (side ? 1 : -1) * -1);
                    oyd.OffsetFlag = QSEnumOffsetFlag.CLOSE;
                    SendOrder(oyd);
                }
                if (voltd != 0)
                {
                    Order otd = new OrderImpl(pos.Symbol, voltd * (side ? 1 : -1) * -1);
                    otd.OffsetFlag = QSEnumOffsetFlag.CLOSETODAY;
                    SendOrder(otd);
                }
            }
            else
            {
                Order o = new MarketOrderFlat(pos);
                o.OffsetFlag = QSEnumOffsetFlag.CLOSE;
                SendOrder(o);
            }
        }



        //获得当前选中持仓
        Position CurrentPositoin
        {
            get
            {

                if (positiongrid.SelectedRows.Count > 0)
                {
                    int row = positiongrid.SelectedRows[0].Index;
                    string account = positiongrid[ACCOUNT, row].Value.ToString();
                    string sym = positiongrid[SYMBOL, row].Value.ToString();
                    bool side = bool.Parse(positiongrid[SIDE, row].Value.ToString());
                    //debug("sym:" + sym + " account:" + account + " side:" + side.ToString());
                    Position pos = pt[sym, account, side];
                    //debug("Pos:" + pos.ToString());
                    return pos;
                }
                else
                {
                    return null;
                }
            }
        }

        //获得当前选中合约
        string CurrentSymbol
        {
            get
            {
                if (positiongrid.SelectedRows.Count > 0)
                {
                    int row = positiongrid.SelectedRows[0].Index;
                    string sym = positiongrid[SYMBOL, row].Value.ToString();
                    return sym;
                }
                else
                {
                    return null;
                }
            }
        }

        //获得当前key account-symbol
        string CurrentKey
        {
            get
            {
                if (positiongrid.SelectedRows.Count > 0)
                {
                    string key = positiongrid.CurrentRow.Cells[KEY].Value.ToString();
                    return key;
                }
                else
                {
                    return "";
                }
            }
        }

        
        #endregion


        #region 相应服务端数据回报
        //获得昨日隔夜持仓，作为基数累加后得到当前持仓数据
        public void GotPosition(Position pos)
        {
            if (InvokeRequired)
            {
                Invoke(new PositionDelegate(GotPosition), new object[] { pos });
            }
            else
            {
                try
                {
                    int posidx = PositionRowIdx(pos.GetPositionKey());
                    string _fromat = pos.oSymbol.SecurityFamily.GetPriceFormat();
                    if ((posidx > -1) && (posidx < gt.Rows.Count))//idx存在
                    {
                        int size = pos.Size;
                        gt.Rows[posidx][SIZE] = Math.Abs(size);
                        gt.Rows[posidx][YDSIZE] = pos.PositionDetailYdNew.Sum(p => p.Volume);
                        gt.Rows[posidx][CANFLATSIZE] = getCanFlatSize(pos);
                        gt.Rows[posidx][AVGPRICE] = pos.AvgPrice.ToFormatStr(_fromat);
                        gt.Rows[posidx][REALIZEDPL] = (pos.ClosedPL * pos.oSymbol.Multiple).ToFormatStr();
                        gt.Rows[posidx][REALIZEDPLPOINT] = pos.ClosedPL.ToFormatStr(_fromat);
                    }
                    else//idx不存在
                    {
                        //如果不存在,则我们将该account-symbol对插入映射列表我们的键用的是account_symbol配对
                        int i = InsertNewRow(pos);
                        if (i < 0) return;
                        int size = pos.Size;
                        gt.Rows[i][SIZE] = Math.Abs(size);
                        gt.Rows[i][YDSIZE] = pos.PositionDetailYdNew.Sum(p => p.Volume);
                        gt.Rows[i][CANFLATSIZE] = getCanFlatSize(pos);
                        gt.Rows[i][AVGPRICE] = pos.AvgPrice.ToFormatStr(_fromat);
                        gt.Rows[i][REALIZEDPL] = Util.FormatDecimal(pos.ClosedPL * pos.oSymbol.Multiple);
                        gt.Rows[i][REALIZEDPLPOINT] = pos.ClosedPL.ToFormatStr(_fromat);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("Got Position Error:" + ex.ToString());
                }
            }

        }

        public void GotTick(Tick t)
        {
            if (InvokeRequired)
            {
                Invoke(new TickDelegate(GotTick), new object[] { t });
            }
            else
            {
                try
                {
                    Symbol sym = CoreService.BasicInfoTracker.GetSymbol(t.Symbol);
                    if(sym == null) return;
                    string _fromat = sym.SecurityFamily.GetPriceFormat();
                    //数据列中如果是该symbol则必须全比更新
                    for (int i = 0; i < gt.Rows.Count; i++)
                    {
                        if (gt.Rows[i][SYMBOL].ToString() == t.Symbol)
                        {
                            //记录该仓位所属账户
                            string acc = gt.Rows[i][ACCOUNT].ToString();
                            bool posside = bool.Parse(gt.Rows[i][SIDE].ToString());
                            Position pos = pt[t.Symbol, acc, posside];
                            
                            string key = pos.GetKey(posside);
                            decimal unrealizedpl = pos.UnRealizedPL;
                            //更新最新成交价
                            if (t.isTrade)
                            {
                                gt.Rows[i][LASTPRICE] = t.Trade.ToFormatStr(_fromat);
                            }
                            //空仓 未平仓合约与 最新价格
                            if (pos.isFlat)
                            {
                                gt.Rows[i][UNREALIZEDPL] = 0;
                                gt.Rows[i][UNREALIZEDPLPOINT] = 0;
                            }
                            else
                            {
                                //更新unrealizedpl
                                gt.Rows[i][UNREALIZEDPL] = (unrealizedpl * pos.oSymbol.Multiple).ToFormatStr();
                                gt.Rows[i][UNREALIZEDPLPOINT] = unrealizedpl.ToFormatStr(_fromat);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("Got Tick Error:" + ex.ToString());
                }
        
            }
        }


        /// <summary>
        /// 获得委托数据回报 用于更新可平数量
        /// </summary>
        /// <param name="o"></param>
        public void GotOrder(Order o)
        {
            //当有委托近来时候,我们需要重新计算我们所对应的可以平仓数量
            if (InvokeRequired)
            {
                Invoke(new OrderDelegate(GotOrder), new object[] { o });
            }
            else
            {
                try
                {
                    bool posside = o.PositionSide;
                    Position pos = pt[o.Symbol, o.Account, posside];
                    int posidx = PositionRowIdx(pos.GetPositionKey());
                    //string _fromat = getDisplayFormat(pos.Symbol);
                    //如果持仓条目已经存在 更新可平数量 委托回报只会更新可平数量
                    if ((posidx > -1) && (posidx < gt.Rows.Count))
                    {
                        gt.Rows[posidx][CANFLATSIZE] = getCanFlatSize(pos);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("Got Order Error:" + ex.ToString());
                }

            }
        }

        /// <summary>
        /// 获得委托取消回报 获得委托取消回报 用于更新可平数量
        /// </summary>
        /// <param name="oid"></param>
        public void GotCancel(long oid)
        {
            //当有委托近来时候,我们需要重新计算我们所对应的可以平仓数量
            if (InvokeRequired)
            {
                Invoke(new LongDelegate(GotCancel), new object[] { oid });
            }
            else
            {
                try
                {
                    Order o = _ot.SentOrder(oid);
                    if (o == null || !o.isValid) return;
                    bool posside = o.PositionSide;
                    Position pos = pt[o.Symbol, o.Account, posside];
                    int posidx = PositionRowIdx(pos.GetPositionKey());
                    if ((posidx > -1) && (posidx < gt.Rows.Count))
                    {
                        gt.Rows[posidx][CANFLATSIZE] = getCanFlatSize(pos);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("Got Cancel Error:" + ex.ToString());
                }
            }
        }

        public void GotFill(Trade t)
        {
            if (InvokeRequired)
            {
                Invoke(new FillDelegate(GotFill), new object[] { t });
            }
            else
            {
                try
                {
                    bool posside = t.PositionSide;//每个成交可以确定仓位操作方向比如是多头操作(买入1手开仓 卖出1手平仓) 还是空头操作(卖出1手开仓 买入1手平仓)
                    Position pos = pt[t.Symbol, t.Account, posside];//获得对应持仓数据
                    string _format = t.oSymbol.SecurityFamily.GetPriceFormat();
                    string key = pos.GetPositionKey();
                    int posidx = PositionRowIdx(key);
                    if ((posidx > -1) && (posidx < gt.Rows.Count))
                    {
                        int size = pos.Size;
                        gt.Rows[posidx][SIZE] = Math.Abs(size);
                        gt.Rows[posidx][YDSIZE] = pos.PositionDetailYdNew.Sum(p => p.Volume);
                        gt.Rows[posidx][CANFLATSIZE] = getCanFlatSize(pos);
                        gt.Rows[posidx][AVGPRICE] = pos.AvgPrice.ToFormatStr(_format);
                        gt.Rows[posidx][REALIZEDPL] = (pos.ClosedPL * pos.oSymbol.Multiple).ToFormatStr();
                        gt.Rows[posidx][REALIZEDPLPOINT] = pos.ClosedPL.ToFormatStr(_format);

                        if (pos.isFlat)
                        {
                            ResetOffset(key);
                        }
                    }
                    else
                    {
                        //如果不存在,则我们将该account-symbol对插入映射列表我们的键用的是account_symbol配对
                        int i = InsertNewRow(pos);
                        if (i < 0) return;
                        int size = pos.Size;
                        gt.Rows[i][SIZE] = Math.Abs(size);
                        gt.Rows[i][YDSIZE] = pos.PositionDetailYdNew.Sum(p => p.Volume);
                        gt.Rows[i][CANFLATSIZE] = getCanFlatSize(pos);
                        gt.Rows[i][AVGPRICE] = pos.AvgPrice.ToFormatStr(_format);
                        gt.Rows[i][REALIZEDPL] = (pos.ClosedPL * pos.oSymbol.Multiple).ToFormatStr();
                        gt.Rows[i][REALIZEDPLPOINT] = pos.ClosedPL.ToFormatStr(_format);
                    }
                    _ordTransHelper.GotFill(t);
                }
                catch (Exception ex)
                {
                    logger.Error("Got Trade Error:" + ex.ToString());
                }
            }
        }

        #endregion


        #region 表格
        const string SYMBOL = "合约";
        const string SIDE = "方向";
        const string DIRECTION = "买卖";
        const string SIZE = "总持仓";
        const string YDSIZE = "昨仓";
        const string CANFLATSIZE = "可平量";//用于计算当前限价委托可以挂单数量
        const string LASTPRICE = "最新";//最新成交价
        const string AVGPRICE = "持仓均价";
        const string UNREALIZEDPL = "持仓盈亏";
        const string UNREALIZEDPLPOINT = "点数(持)";
        const string REALIZEDPL = "平仓盈亏";
        const string REALIZEDPLPOINT = "点数(平)";
        const string ACCOUNT = "交易帐户";
        const string PROFITTARGET = "止盈";
        const string STOPLOSS = "止损";
        const string KEY = "编号";

        BindingSource datasource = new BindingSource();
        DataTable gt = new DataTable();

        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = positiongrid;

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
            gt.Columns.Add(SYMBOL);//0
            gt.Columns.Add(SIDE);//1
            
            gt.Columns.Add(DIRECTION);//2

            gt.Columns.Add(SIZE, typeof(int));//3
            gt.Columns.Add(YDSIZE,typeof(int));//4
            gt.Columns.Add(CANFLATSIZE, typeof(int));//5
            gt.Columns.Add(LASTPRICE);//6
            gt.Columns.Add(AVGPRICE);//7
            gt.Columns.Add(UNREALIZEDPL);//8
            gt.Columns.Add(UNREALIZEDPLPOINT);//9
            gt.Columns.Add(REALIZEDPL);//10
            gt.Columns.Add(REALIZEDPLPOINT);//11
            
            gt.Columns.Add(PROFITTARGET);
            gt.Columns.Add(STOPLOSS);
            gt.Columns.Add(ACCOUNT);
            gt.Columns.Add(KEY);
            //gt.Columns.Add(STRATEGY, typeof(Image));

        }
        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = positiongrid;
            datasource.DataSource = gt;
            grid.DataSource = datasource;
            grid.Columns[SIDE].Visible = false;
            grid.Columns[KEY].Visible = false;
            grid.Columns[PROFITTARGET].Visible = false;
            grid.Columns[STOPLOSS].Visible = false;

            ResetColumeSize();

            for (int i = 0; i < gt.Columns.Count; i++)
            {
                grid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        void ResetColumeSize()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = positiongrid;
            grid.Columns[SYMBOL].Width = 70;
            grid.Columns[DIRECTION].Width = 40;
            grid.Columns[SIZE].Width = 50;
            grid.Columns[YDSIZE].Width = 50;
            grid.Columns[CANFLATSIZE].Width = 50;
            grid.Columns[LASTPRICE].Width = 80;

            grid.Columns[AVGPRICE].Width =80;

            grid.Columns[UNREALIZEDPL].Width = 80;
            grid.Columns[UNREALIZEDPLPOINT].Width = 60;
            grid.Columns[REALIZEDPL].Width = 80;
            grid.Columns[REALIZEDPLPOINT].Width = 60;

        }

        #endregion







        void WireEvent()
        {
            btnShowAll.CheckedChanged += new EventHandler(btnShowAll_CheckedChanged);
            btnShowHold.CheckedChanged += new EventHandler(btnShowHold_CheckedChanged);
            btnFlat.Click +=new EventHandler(btnFlat_Click);
            btnFlatAll.Click +=new EventHandler(btnFlatAll_Click);

            positiongrid.CellFormatting += new DataGridViewCellFormattingEventHandler(positiongrid_CellFormatting);
            positiongrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(positiongrid_RowPrePaint);
            positiongrid.SizeChanged += new EventHandler(positiongrid_SizeChanged);
        }

        void positiongrid_SizeChanged(object sender, EventArgs e)
        {
            ResetColumeSize();
        }

        void positiongrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts = e.PaintParts ^ DataGridViewPaintParts.Focus;
        }

        void positiongrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2)
                {
                    string direction = positiongrid[DIRECTION, e.RowIndex].Value.ToString();
                    if (direction.Equals("买"))
                    {
                        e.CellStyle.ForeColor = UIConstant.LongSideColor;
                        e.CellStyle.Font = UIConstant.BoldFont;
                    }
                    else if (direction.Equals("卖"))
                    {
                        e.CellStyle.ForeColor = UIConstant.ShortSideColor;
                        e.CellStyle.Font = UIConstant.BoldFont;
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.Font = UIConstant.BoldFont;
                    }
                }

                if (e.ColumnIndex == 8 || e.ColumnIndex == 9 || e.ColumnIndex == 10 || e.ColumnIndex == 11)
                {

                    decimal v = 0;
                    decimal.TryParse(positiongrid[e.ColumnIndex, e.RowIndex].Value.ToString(), out v);
                    if (v > 0)
                    {
                        e.CellStyle.ForeColor = UIConstant.LongSideColor;
                        e.CellStyle.Font = UIConstant.BoldFont;
                    }
                    else if (v < 0)
                    {
                        e.CellStyle.ForeColor = UIConstant.ShortSideColor;
                        e.CellStyle.Font = UIConstant.BoldFont;
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.Font = UIConstant.BoldFont;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Cell Format Error:" + ex.ToString());
            }

        }


        #region 界面操作事件
        private void btnShowAll_CheckedChanged(object sender, EventArgs e)
        {
            string strFilter = "";
            datasource.Filter = strFilter;
        }

        private void btnShowHold_CheckedChanged(object sender, EventArgs e)
        {
            string strFilter;
            strFilter = String.Format(SIZE + " > '{0}'","0");
            datasource.Filter = strFilter;
        }
        //平掉当前选中持仓
        private void btnFlat_Click(object sender, EventArgs e)
        {
            Position pos = CurrentPositoin;
            if (pos == null)
            {
                MoniterHelper.WindowMessage("请选择持仓");
                return;
            }
            if (pos.isFlat)
            {
                MoniterHelper.WindowMessage("该合约没有可平持仓");
                return;
            }
            if (MoniterHelper.WindowConfirm(string.Format("确认平掉持仓:{0}", pos.GetPositionKey())) == DialogResult.Yes)
            {
                FlatPosition(pos);
            }
        }

        //平调所有持仓
        private void btnFlatAll_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm(string.Format("确认平掉所有持仓?")) == DialogResult.Yes)
            {
                foreach (Position pos in pt)
                {
                    FlatPosition(pos);
                }
            }
        }

        //撤单
        private void btnCancel_Click(object sender, EventArgs e)
        {
            string sym = CurrentSymbol;
            if (string.IsNullOrEmpty(sym))
            {
                MoniterHelper.WindowMessage("请选择有效持仓");
                return;
            }
            if (MoniterHelper.WindowConfirm(string.Format("确认撤掉合约:{0}下的所有待成交委托?", sym)) == DialogResult.Yes)
            {
                foreach (Order o in OrderTracker)
                {
                    if ((o.Symbol == sym) && (OrderTracker.isPending(o.id)))
                    {
                        CancelOrder(o.id);
                    }
                }
            }
        }


        
        #endregion




        public void Clear()
        {
            positiongrid.DataSource = null;
            posRowMap.Clear();
            gt.Rows.Clear();
            BindToTable();
        }

        
        


    }
}
