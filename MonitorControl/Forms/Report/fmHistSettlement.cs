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
    public partial class fmHistSettlement : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmHistSettlement()
        {
            InitializeComponent();
            this.HistReportType = EnumHistReportType.Account;

            this.Load += new EventHandler(fmReportSettlement_Load);
        }

        void fmReportSettlement_Load(object sender, EventArgs e)
        {
            SetPreferences();
            InitTable();
            BindToTable();

            start.Value = Convert.ToDateTime(DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd") + " 0:00:00");
            end.Value = DateTime.Now;

            btnQry.Click +=new EventHandler(btnQry_Click);
            inputAccount.TextChanged += new EventHandler(inputAccount_TextChanged);
            CoreService.EventCore.RegIEventHandler(this);

            settlementGrid.DoubleClick += new EventHandler(settlementGrid_DoubleClick);

            UpdateTitle();
        }

        /// <summary>
        /// 当前选中交易日
        /// </summary>
        public int  CurrentSettleday
        {
            get
            {
                int row = (settlementGrid.SelectedRows.Count > 0 ? settlementGrid.SelectedRows[0].Index : -1);
                return row == -1 ? 0 : int.Parse((settlementGrid[SETTLEDAY, row].Value.ToString()));
            }
        }

        void settlementGrid_DoubleClick(object sender, EventArgs e)
        {
            int settleday = CurrentSettleday;
            if (settleday <= 0) return;

            if (HistReportType == EnumHistReportType.Account)
            {
                fmSettlementDetail fm = new fmSettlementDetail();
                fm.SetAccount(inputAccount.Text);
                fm.SetSettleday(settleday);
                fm.QrySettlementDetail();
                fm.ShowDialog();
                fm.Close();
            }

        }

        void inputAccount_TextChanged(object sender, EventArgs e)
        {
            UpdateTitle();
        }

        void UpdateTitle()
        {
            this.Text = string.Format("结算单查询[{0}]-{1}", this.HistReportType == EnumHistReportType.Agent ? "代理" : "投资者", inputAccount.Text);
        }
        void  btnQry_Click(object sender, EventArgs e)
        {
 	        this.Clear();
            if (string.IsNullOrEmpty(inputAccount.Text))
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请输入帐户");
                return;
            }
            if (this.HistReportType == EnumHistReportType.Account)
            {
                CoreService.TLClient.ReqQryAccountSettlements(inputAccount.Text, Util.ToTLDate(start.Value), Util.ToTLDate(end.Value));
            }
            else
            {
                CoreService.TLClient.ReqQryAgentSettlement(inputAccount.Text, Util.ToTLDate(start.Value), Util.ToTLDate(end.Value));
            }
        }

        public void OnInit()
        {
            CoreService.EventCore.RegisterCallback(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_SETTLEMENTS, this.OnQrySettlements);
            CoreService.EventCore.RegisterCallback(Modules.AgentManager, Method_AGENT_MGR.QRY_AGENT_SETTLEMENTS, this.OnQrySettlements);
        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_SETTLEMENTS, this.OnQrySettlements);
            CoreService.EventCore.UnRegisterCallback(Modules.AgentManager, Method_AGENT_MGR.QRY_AGENT_SETTLEMENTS, this.OnQrySettlements);
        }

        void OnQrySettlements(string jsonstr, bool islast)
        {
            AccountSettlementImpl obj = CoreService.ParseJsonResponse<AccountSettlementImpl>(jsonstr);
            if (obj != null)
            {
                GotSettlement(obj);
                realizedpl += obj.CloseProfitByDate;
                unrealizedpl += obj.PositionProfitByDate;
                commission += obj.Commission;
                cashin += obj.CashIn;
                cashout += obj.CashOut;

                if (empty)
                {
                    empty = false;
                    startEquity.Text = obj.LastEquity.ToFormatStr();
                }
            }
            if (islast)
            {
                trealizedpl.Text = realizedpl.ToFormatStr();
                tunrealized.Text = unrealizedpl.ToFormatStr();
                tcommission.Text = commission.ToFormatStr();
                tcashin.Text = cashin.ToFormatStr();
                tcashout.Text = cashout.ToFormatStr();

                endequity.Text = obj.EquitySettled.ToFormatStr();
            }
        }

        void GotSettlement(AccountSettlementImpl settle)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<AccountSettlementImpl>(GotSettlement), new object[] { settle });
            }
            else
            {
                DataRow r = gt.Rows.Add("");
                int i = gt.Rows.Count - 1;//得到新建的Row号
                gt.Rows[i][SETTLEDAY] = settle.Settleday;
                gt.Rows[i][ACCOUNT] = settle.Account;
                gt.Rows[i][LASTEQUITY] = settle.LastEquity.ToFormatStr();
                gt.Rows[i][REALIZEDPL] = settle.CloseProfitByDate.ToFormatStr();
                gt.Rows[i][UNREALIZEDPL] = settle.PositionProfitByDate.ToFormatStr();
                gt.Rows[i][COMMISSION] = settle.Commission.ToFormatStr();
                gt.Rows[i][CASHIN] = settle.CashIn.ToFormatStr();
                gt.Rows[i][CASHOUT] = settle.CashOut.ToFormatStr();
                gt.Rows[i][EQUITYSETTLED] = settle.EquitySettled.ToFormatStr();
                gt.Rows[i][LASTCREDIT] = settle.LastCredit.ToFormatStr();
                gt.Rows[i][CREDITCASHIN] = settle.CreditCashIn.ToFormatStr();
                gt.Rows[i][CREDITCASHOUT] = settle.CreditCashOut.ToFormatStr();
                gt.Rows[i][CREDITSETTLED] = settle.CreditSettled.ToFormatStr();
                gt.Rows[i][TAG] = settle;
            }
        }


        decimal realizedpl = 0;
        decimal unrealizedpl = 0;
        decimal cashin = 0;
        decimal cashout = 0;
        decimal commission = 0;
        bool empty = true;
        public void Clear()
        {
            settlementGrid.DataSource = null;
            gt.Rows.Clear();
            BindToTable();

            empty = true;
            realizedpl = 0;
            unrealizedpl = 0;
            cashin = 0;
            cashout = 0;
            commission = 0;
        }


        public EnumHistReportType HistReportType { get; set; }
        public void SetAccount(string account)
        {
            inputAccount.Text = account;
            UpdateTitle();
        }
        #region 表格

        #region 显示字段
        const string SETTLEDAY = "结算日";
        const string ACCOUNT = "帐户";
        const string LASTEQUITY = "上日权益";
        const string REALIZEDPL = "平仓盈亏";
        const string UNREALIZEDPL = "盯市浮动盈亏";
        const string COMMISSION = "手续费";
        const string CASHIN = "入金";
        const string CASHOUT = "出金";
        const string EQUITYSETTLED = "结算权益";
        const string LASTCREDIT = "信用额度";
        const string CREDITCASHIN = "信用入金";
        const string CREDITCASHOUT = "信用出金";
        const string CREDITSETTLED = "结算信用额度";
        const string TAG = "TAG";
        #endregion

        DataTable gt = new DataTable();
        BindingSource datasource = new BindingSource();

        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = settlementGrid;

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
            gt.Columns.Add(SETTLEDAY);
            gt.Columns.Add(ACCOUNT);//4
            gt.Columns.Add(LASTEQUITY);//2
            gt.Columns.Add(REALIZEDPL);//3
            gt.Columns.Add(UNREALIZEDPL);//6
            gt.Columns.Add(COMMISSION);//
            gt.Columns.Add(CASHIN);
            gt.Columns.Add(CASHOUT);
            gt.Columns.Add(EQUITYSETTLED);
            gt.Columns.Add(LASTCREDIT);
            gt.Columns.Add(CREDITCASHIN);
            gt.Columns.Add(CREDITCASHOUT);
            gt.Columns.Add(CREDITSETTLED);
            gt.Columns.Add(TAG, typeof(AccountSettlementImpl));
           
        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = settlementGrid;
            datasource.DataSource = gt;
            grid.DataSource = datasource;

            //需要在绑定数据源后设定具体的可见性
            grid.Columns[SETTLEDAY].Width = 50;
            grid.Columns[ACCOUNT].Width = 70;
            grid.Columns[LASTEQUITY].Width = 60;
            grid.Columns[REALIZEDPL].Width = 50;
            grid.Columns[UNREALIZEDPL].Width = 70;
            grid.Columns[COMMISSION].Width = 50;
            grid.Columns[CASHIN].Width = 50;
            grid.Columns[CASHOUT].Width = 50;
            grid.Columns[EQUITYSETTLED].Width = 60;
            grid.Columns[LASTCREDIT].Width = 60;
            grid.Columns[CREDITCASHIN].Width = 50;
            grid.Columns[CREDITCASHOUT].Width = 50;
            grid.Columns[CREDITSETTLED].Width = 70;

            grid.Columns[ACCOUNT].Visible = false;
            grid.Columns[TAG].Visible = false;


        }
        #endregion

    }
}
