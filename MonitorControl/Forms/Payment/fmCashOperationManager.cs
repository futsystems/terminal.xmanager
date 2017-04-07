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

            menu = new ContextMenuStrip();
            menu.Items.Add("取消", null, new EventHandler(Cancel_Click));
            menu.Items.Add("拒绝", null, new EventHandler(Reject_Click));
            menu.Items.Add("确认", null, new EventHandler(Confirm_Click));

            start.Value = DateTime.Now.AddDays(-1);
            btnQry.Click += new EventHandler(btnQry_Click);
            status.SelectedIndexChanged += new EventHandler(status_SelectedIndexChanged);
            optype.SelectedIndexChanged += new EventHandler(optype_SelectedIndexChanged);
            account.TextChanged += new EventHandler(account_TextChanged);
            cashOperationGrid.MouseClick += new MouseEventHandler(cashOperationGrid_MouseClick);
            cashOperationGrid.CellFormatting += new DataGridViewCellFormattingEventHandler(cashOperationGrid_CellFormatting);
            cashOperationGrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(cashOperationGrid_RowPrePaint);
            this.Load += new EventHandler(fmCashOperationManager_Load);
            this.FormClosing += new FormClosingEventHandler(fmCashOperationManager_FormClosing);
        }

        void fmCashOperationManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        void cashOperationGrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts = e.PaintParts ^ DataGridViewPaintParts.Focus;
        }

        void cashOperationGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                QSEnumCashOperation op = (QSEnumCashOperation)cashOperationGrid[4, e.RowIndex].Value;
                if (op== QSEnumCashOperation.Deposit)
                {
                    e.CellStyle.ForeColor = UIConstant.LongSideColor;
                }
                else
                {
                    e.CellStyle.ForeColor = UIConstant.ShortSideColor;
                }
            }
            if (e.ColumnIndex == 5)
            {
                e.CellStyle.Font = UIConstant.BoldFont;
            }
        }

        void cashOperationGrid_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                var menu = GetMenu();
                if (menu != null)
                {
                    menu.Show(Control.MousePosition);
                }
            }
        }

        CashOperation CurrentCashOperation
        {
            get
            {

                if (cashOperationGrid.SelectedRows.Count > 0)
                {
                    return cashOperationGrid.CurrentRow.Cells[TAG].Value as CashOperation;
                }
                else
                {
                    return null;
                }
            }
        }
        ContextMenuStrip menu = null;

        ContextMenuStrip GetMenu()
        {
            CashOperation op = CurrentCashOperation;
            if (op == null) return null;
            switch (op.Status)
            { 
                case QSEnumCashInOutStatus.CANCELED:
                case QSEnumCashInOutStatus.CONFIRMED:
                case QSEnumCashInOutStatus.REFUSED:
                    menu.Items[0].Enabled = false;
                    menu.Items[1].Enabled = false;
                    menu.Items[2].Enabled = false;
                    return menu;
                case QSEnumCashInOutStatus.PENDING:
                     menu.Items[0].Enabled = true;
                     menu.Items[1].Enabled = true;
                     menu.Items[2].Enabled = true;
                    return menu;
                default:
                    menu.Items[0].Enabled = false;
                    menu.Items[1].Enabled = false;
                    menu.Items[2].Enabled = false;
                    return null;
            }      
        }

        void Cancel_Click(object sender, EventArgs e)
        {
            CashOperation op = CurrentCashOperation;
            if (op != null)
            {
                if (ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("取消该出入金请求?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqCancelCashOperation(op.Ref);
                }
            }
        }
        void Confirm_Click(object sender, EventArgs e)
        {
            CashOperation op = CurrentCashOperation;
            if (op != null)
            {
                if (ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("确认该出入金请求?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqConfirmCashOperation(op.Ref);
                }
            }
        }

        void Reject_Click(object sender, EventArgs e)
        {
            CashOperation op = CurrentCashOperation;
            if (op != null)
            {
                if (ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("拒绝该出入金请求?", "", MessageBoxButtons.YesNo,MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqRejectCashOperation(op.Ref);
                }
            }
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
            Clear();
            CoreService.TLClient.ReqQryCashOperation(start.Value.ToTLDate(), end.Value.ToTLDate());
        }


        public void OnInit()
        {
            CoreService.EventCore.RegisterCallback(Modules.APIService, Method_API.QRY_CASH_OPERATION, this.OnQryCashOperation);
            CoreService.EventCore.RegisterNotifyCallback(Modules.APIService, Method_API.NOTIFY_CASH_OPERATION, this.OnNotifyCashOperation);
            CoreService.TLClient.ReqQryCashOperation(start.Value.ToTLDate(), end.Value.ToTLDate());
        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback(Modules.APIService, Method_API.QRY_CASH_OPERATION, this.OnQryCashOperation);
            CoreService.EventCore.UnRegisterNotifyCallback(Modules.APIService, Method_API.NOTIFY_CASH_OPERATION, this.OnNotifyCashOperation);
        }

        void OnNotifyCashOperation(string json)
        {
            CashOperation op = json.DeserializeObject<CashOperation>();
            if (op != null)
            {
                InvokeGotCashOperation(op);
                UpdateText();
            }
        }
        void OnQryCashOperation(string json, bool islast)
        {
            CashOperation op = json.DeserializeObject<CashOperation>();
            if (op != null)
            {
                InvokeGotCashOperation(op);
            }

            if (islast)
            {
                UpdateText();
            }
        }

        void UpdateText()
        {
            this.Text = string.Format("未处理:{0}个【在线出入金管理】 ", opmap.Values.Where(c => c.Status == QSEnumCashInOutStatus.PENDING).Count());
        }


        Dictionary<string, int> oprowidmap = new Dictionary<string, int>();
        Dictionary<string, CashOperation> opmap = new Dictionary<string, CashOperation>();
        int CashOperatioinIdx(CashOperation op)
        {
            int rowid = -1;
            if (!oprowidmap.TryGetValue(op.Ref, out rowid))
            {
                return -1;
            }
            else
            {
                return rowid;
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
                int i = CashOperatioinIdx(op);
                if (i == -1)
                {
                    tb.Rows.Add();
                    i = tb.Rows.Count - 1;
                    tb.Rows[i][ID] = 0;
                    tb.Rows[i][ACCOUNT] = op.Account;
                    tb.Rows[i][DATETIME] = Util.ToDateTime(op.DateTime).ToString("yyy/MM/dd HH:mm:ss");
                    tb.Rows[i][OPERATION] = op.OperationType;
                    tb.Rows[i][OPERATIONSTR] = Util.GetEnumDescription(op.OperationType);
                    tb.Rows[i][AMOUNT] = op.Amount.ToFormatStr();
                    tb.Rows[i][GATEWAYTYPE] = (int)op.GateWayType == -1 ? "手工" : Util.GetEnumDescription(op.GateWayType);
                    tb.Rows[i][STATUS] = op.Status;
                    tb.Rows[i][STATUSSTR] = Util.GetEnumDescription(op.Status);
                    tb.Rows[i][BUSINESSTYPE] = op.BusinessType;
                    tb.Rows[i][BUSINESSTYPESTR] = Util.GetEnumDescription(op.BusinessType);
                    tb.Rows[i][REF] = op.Ref;
                    tb.Rows[i][COMMENT] = op.Comment;
                    tb.Rows[i][TAG] = op;

                    oprowidmap.Add(op.Ref, i);
                    opmap.Add(op.Ref, op);
                }
                else
                {
                    tb.Rows[i][STATUS] = op.Status;
                    tb.Rows[i][STATUSSTR] = Util.GetEnumDescription(op.Status);
                    tb.Rows[i][COMMENT] = op.Comment;
                    tb.Rows[i][TAG] = op;
                    opmap[op.Ref] = op;
                }
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
        const string BUSINESSTYPE = "F类别";
        const string BUSINESSTYPESTR = "业务类别";
        const string REF = "本地订单号";
        const string COMMENT = "备注";
        const string TAG = "TAG";

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

            tb.Columns.Add(OPERATION,typeof(QSEnumCashOperation));//2
            tb.Columns.Add(OPERATIONSTR);
            tb.Columns.Add(AMOUNT);//
            tb.Columns.Add(GATEWAYTYPE);
            tb.Columns.Add(STATUS);
            tb.Columns.Add(STATUSSTR);
            tb.Columns.Add(BUSINESSTYPE);
            tb.Columns.Add(BUSINESSTYPESTR);
            tb.Columns.Add(COMMENT);
            tb.Columns.Add(TAG, typeof(CashOperation));
            

        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = cashOperationGrid;


            datasource.DataSource = tb;
            datasource.Sort = REF + " DESC";// " ASC"
            grid.DataSource = datasource;

            //需要在绑定数据源后设定具体的可见性
            grid.Columns[REF].Width =120;
            grid.Columns[ACCOUNT].Width = 80;
            grid.Columns[DATETIME].Width = 90;
            grid.Columns[OPERATIONSTR].Width = 30;
            grid.Columns[STATUSSTR].Width = 40;
            grid.Columns[BUSINESSTYPESTR].Width = 60;
            grid.Columns[AMOUNT].Width = 60;

            grid.Columns[OPERATION].Visible = false;
            grid.Columns[ID].Visible = false;
            grid.Columns[STATUS].Visible = false;
            grid.Columns[TAG].Visible = false;
            grid.Columns[BUSINESSTYPE].Visible = false;
        }

        void Clear()
        {
            oprowidmap.Clear();
            cashOperationGrid.DataSource = null;
            tb.Rows.Clear();
            BindToTable();
        }
        #endregion

    }
}
