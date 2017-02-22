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
    public partial class fmCashOperationManager : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmCashOperationManager()
        {
            InitializeComponent();

            MoniterHelper.AdapterToIDataSource(status).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumCashInOutStatus>(true));
            MoniterHelper.AdapterToIDataSource(optype).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumCashOperation>(true));
            SetPreferences();
            InitTable();
            BindToTable();

            btnQry.Click += new EventHandler(btnQry_Click);
            status.SelectedIndexChanged += new EventHandler(status_SelectedIndexChanged);
            optype.SelectedIndexChanged += new EventHandler(optype_SelectedIndexChanged);
            account.TextChanged += new EventHandler(account_TextChanged);
            this.Load += new EventHandler(fmCashOperationManager_Load);
        }

        void account_TextChanged(object sender, EventArgs e)
        {
            FilterItems();
        }

        void optype_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterItems();
        }

        void status_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterItems();
        }

        void FilterItems()
        { 
            string strFilter = ID + " >=0 ";

            if (!string.IsNullOrEmpty(account.Text))
            {
                strFilter = string.Format(strFilter + " and " + ACCOUNT + " like '{0}*'", account.Text);
            }
            if (status.SelectedIndex != 0)
            {
                strFilter = string.Format(strFilter + " and " + STATUS + " = '{0}'", (QSEnumCashInOutStatus)status.SelectedValue);
            }
            if (optype.SelectedIndex != 0)
            {
                strFilter = string.Format(strFilter + " and " + OPERATION + " = '{0}'", (QSEnumCashOperation)optype.SelectedValue);
            }

            datasource.Filter = strFilter;
        }

        void fmCashOperationManager_Load(object sender, EventArgs e)
        {
            CoreService.EventCore.RegIEventHandler(this);
        }

        void btnQry_Click(object sender, EventArgs e)
        {

            CoreService.TLClient.ReqQryCashOperation(start.Value.ToTLDate(), end.Value.ToTLDate());
        }


        public void OnInit()
        {
            CoreService.EventCore.RegisterCallback(Modules.APIService, Method_API.QRY_CASH_OPERATION, this.OnQryCashOperation);
        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback(Modules.APIService, Method_API.QRY_CASH_OPERATION, this.OnQryCashOperation);
        }


        void OnQryCashOperation(string json, bool islast)
        {
            CashOperation op = json.DeserializeObject<CashOperation>();
            if (op != null)
            {
                InvokeGotCashOperation(op);
            }
        }

        void InvokeGotCashOperation(CashOperation op)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<CashOperation>(InvokeGotCashOperation), new object[] { op });
            }
            else
            {
                DataRow r = tb.Rows.Add(0);
                int i = tb.Rows.Count - 1;//得到新建的Row号
                tb.Rows[i][ACCOUNT] = op.Account;
                tb.Rows[i][DATETIME] = Util.ToDateTime(op.DateTime).ToString("yyy/MM/dd HH:mm:ss");
                tb.Rows[i][OPERATION] = op.OperationType;
                tb.Rows[i][OPERATIONSTR] = Util.GetEnumDescription(op.OperationType);
                tb.Rows[i][AMOUNT] = op.Amount.ToFormatStr();
                tb.Rows[i][GATEWAYTYPE] = op.GateWayType;
                tb.Rows[i][STATUS] = op.Status;
                tb.Rows[i][STATUSSTR] = Util.GetEnumDescription(op.Status);
                tb.Rows[i][REF] = op.Ref;
                tb.Rows[i][COMMENT] = op.Comment;
            }
        }
        #region 表格

        #region 显示字段

        const string ID = "ID";
        const string ACCOUNT = "帐户";
        const string DATETIME = "时间";
        const string OPERATION = "F操作";
        const string OPERATIONSTR = "操作";
        const string AMOUNT = "金额";
        const string GATEWAYTYPE = "支付通道";
        const string STATUS = "F状态";
        const string STATUSSTR = "状态";
        const string REF = "本地订单号";
        const string COMMENT = "备注";

        #endregion

        DataTable tb = new DataTable();
        BindingSource datasource = new BindingSource();

        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = cashOperationGrid;

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
            tb.Columns.Add(ID);//1
            tb.Columns.Add(REF);//
            tb.Columns.Add(ACCOUNT);//4
            tb.Columns.Add(DATETIME);//5

            tb.Columns.Add(OPERATION);//2
            tb.Columns.Add(OPERATIONSTR);
            tb.Columns.Add(AMOUNT);//
            tb.Columns.Add(GATEWAYTYPE);
            tb.Columns.Add(STATUS);
            tb.Columns.Add(STATUSSTR);
            tb.Columns.Add(COMMENT);
            

        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = cashOperationGrid;


            datasource.DataSource = tb;
            grid.DataSource = datasource;

            //需要在绑定数据源后设定具体的可见性
            grid.Columns[REF].Width =120;
            grid.Columns[ACCOUNT].Width = 80;
            grid.Columns[DATETIME].Width = 90;
            grid.Columns[OPERATIONSTR].Width = 30;
            grid.Columns[STATUSSTR].Width = 40;
            grid.Columns[AMOUNT].Width = 60;

            grid.Columns[OPERATION].Visible = false;
            grid.Columns[ID].Visible = false;
            grid.Columns[STATUS].Visible = false;
        }

        void Clear()
        {
            cashOperationGrid.DataSource = null;
            tb.Rows.Clear();
            BindToTable();
        }
        #endregion

    }
}
