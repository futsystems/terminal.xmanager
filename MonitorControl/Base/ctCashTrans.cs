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
using TradingLib.Mixins.JsonObject;



namespace TradingLib.MoniterControl
{
    public partial class ctCashTrans : UserControl,IEventBinder
    {
        public ctCashTrans()
        {
            InitializeComponent();

            this.AccountType = CashOpViewType.Account;

            //控件设置的属性在构造函数中是不起作用的，需要构造函数完毕在load事件内才发生作用，没有构造完毕则无法获得对应的参数 因此初始化工程中 如果有用到控件参数的需要延迟到load事件过程中执行
            //这里需要研究下控件的相关加载机制和事件先后顺序
            this.Load += new EventHandler(ctCashTrans_Load);
        }

        void ctCashTrans_Load(object sender, EventArgs e)
        {
            SetPreferences();
            InitTable();
            BindToTable();

            start.Value = Convert.ToDateTime(DateTime.Today.AddMonths(-1).ToString("yyyy-MM-01") + " 0:00:00");
            end.Value = DateTime.Now;

            //全局事件回调
            CoreService.EventCore.RegIEventHandler(this);

            //绑定查询事件
            this.btnQryReport.Click += new EventHandler(btnQryReport_Click);
            cashgrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(cashgrid_RowPrePaint);
            cashgrid.CellFormatting += new DataGridViewCellFormattingEventHandler(cashgrid_CellFormatting);
        }

        public void OnInit()
        {
            CoreService.EventCore.RegisterCallback(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_TXN, this.OnQryCashTrans);
            CoreService.EventCore.RegisterCallback(Modules.AgentManager, Method_AGENT_MGR.QRY_AGENT_CASH_TXN, this.OnQryCashTrans);
        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_TXN, this.OnQryCashTrans);
            CoreService.EventCore.UnRegisterCallback(Modules.AgentManager, Method_AGENT_MGR.QRY_AGENT_CASH_TXN, this.OnQryCashTrans);
        }


        List<CashTransactionImpl> txnlist = new List<CashTransactionImpl>();
        void OnQryCashTrans(string jsonstr, bool islast)
        {
            CashTransactionImpl obj = CoreService.ParseJsonResponse<CashTransactionImpl>(jsonstr);
            if (obj != null)
            {
                GotCashTrans(obj);
                txnlist.Add(obj);
            }
            if (islast)
            {
                equity_in.Text = txnlist.Where(t => (t.EquityType == QSEnumEquityType.OwnEquity) && (t.TxnType == QSEnumCashOperation.Deposit)).Sum(t => t.Amount).ToFormatStr();
                equity_out.Text = txnlist.Where(t => (t.EquityType == QSEnumEquityType.OwnEquity) && (t.TxnType == QSEnumCashOperation.WithDraw)).Sum(t => t.Amount).ToFormatStr();
                credit_in.Text = txnlist.Where(t => (t.EquityType == QSEnumEquityType.CreditEquity) && (t.TxnType == QSEnumCashOperation.Deposit)).Sum(t => t.Amount).ToFormatStr();
                credit_out.Text = txnlist.Where(t => (t.EquityType == QSEnumEquityType.CreditEquity) && (t.TxnType == QSEnumCashOperation.WithDraw)).Sum(t => t.Amount).ToFormatStr();
            }
        }

        void GotCashTrans(CashTransactionImpl trans)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<CashTransactionImpl>(GotCashTrans), new object[] { trans });
            }
            else
            {
                DataRow r = gt.Rows.Add("");
                int i = gt.Rows.Count - 1;//得到新建的Row号
                gt.Rows[i][TXNID] = trans.TxnID;
                gt.Rows[i][SETTLEDAY] = trans.Settleday;
                gt.Rows[i][DATETIME] = Util.ToDateTime(trans.DateTime);
                gt.Rows[i][ACCOUNT] = trans.Account;
                gt.Rows[i][OPERATION] = trans.TxnType == QSEnumCashOperation.Deposit?"入金" : "出金";
                gt.Rows[i][TYPE] = Util.GetEnumDescription(trans.EquityType);
                gt.Rows[i][AMOUNT] = trans.Amount;
                gt.Rows[i][OPERATOR] = trans.Operator;
                gt.Rows[i][TAG] = trans;
            }
        }




        string EMPTY = "--";
        public void Clear()
        {
            cashgrid.DataSource = null;
            gt.Rows.Clear();
            BindToTable();
            txnlist.Clear();
            equity_in.Text = EMPTY;
            equity_out.Text = EMPTY;
            credit_in.Text = EMPTY;
            credit_out.Text = EMPTY;

        }


        #region 表格

        #region 显示字段

        const string TXNID = "交易编号";
        const string SETTLEDAY = "交易日";
        const string DATETIME = "时间";
        const string ACCOUNT = "帐户";
        const string OPERATION = "操作";
        const string AMOUNT = "金额";
        const string TYPE = "资金类别";
        const string OPERATOR = "操作员";
        const string TAG = "TAG";
        #endregion

        DataTable gt = new DataTable();
        BindingSource datasource = new BindingSource();

        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = cashgrid;

            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
            gt.Columns.Add(TXNID);
            gt.Columns.Add(ACCOUNT);//4
            gt.Columns.Add(SETTLEDAY);//2
            gt.Columns.Add(DATETIME);//3
            gt.Columns.Add(OPERATION);//6
            gt.Columns.Add(AMOUNT);//
            gt.Columns.Add(TYPE);
            gt.Columns.Add(OPERATOR);

            gt.Columns.Add(TAG, typeof(CashTransactionImpl));
        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = cashgrid;
            datasource.DataSource = gt;
            grid.DataSource = datasource;

            //需要在绑定数据源后设定具体的可见性
            grid.Columns[TXNID].Width = 200;
            grid.Columns[SETTLEDAY].Width = 80;
            grid.Columns[DATETIME].Width = 120;
            grid.Columns[OPERATION].Width = 40;
            grid.Columns[ACCOUNT].Width = 80;
            grid.Columns[TYPE].Width = 60;
            grid.Columns[AMOUNT].Width = 100;
            grid.Columns[TAG].Visible = false;


        }
        #endregion


        private void btnQryReport_Click(object sender, EventArgs e)
        {
            this.Clear();

            if (string.IsNullOrEmpty(inputAccount.Text))
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请输入帐户");
                return;
            }
            if (this.AccountType == CashOpViewType.Account)
            {
                CoreService.TLClient.ReqQryAccountCashTxn(inputAccount.Text, Util.ToTLDateTime(start.Value), Util.ToTLDateTimeEnd(end.Value));
            }
            else
            {
                CoreService.TLClient.ReqQryAgentCashTxn(inputAccount.Text, Util.ToTLDateTime(start.Value), Util.ToTLDateTimeEnd(end.Value));
            }
        }

        void cashgrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts = e.PaintParts ^ DataGridViewPaintParts.Focus;
        }

        void cashgrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                string op = cashgrid[4, e.RowIndex].Value.ToString();
                if (op.Equals("入金"))
                {
                    e.CellStyle.ForeColor = UIConstant.LongSideColor;
                }
                else
                {
                    e.CellStyle.ForeColor = UIConstant.ShortSideColor;
                }
            }
            if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
            {
                e.CellStyle.Font = UIConstant.BoldFont;
            }
        }


        public CashOpViewType AccountType { get; set; }


        public void SetAccount(string account)
        {
            inputAccount.Text = account;
        }
    }
}
