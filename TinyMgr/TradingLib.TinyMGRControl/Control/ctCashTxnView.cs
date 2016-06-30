using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using Common.Logging;

using ComponentFactory.Krypton.Toolkit;

using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;


namespace TradingLib.TinyMGRControl
{
    public partial class ctCashTxnView : UserControl
    {
        public ctCashTxnView()
        {
            InitializeComponent();
            SetPreferences();
            InitTable();
            BindToTable();

        }


        public void GotTxn(CashTransaction txn)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<CashTransaction>(GotTxn), new object[] { txn });
            }
            else
            {
                try
                {
                    DataRow r = tb.Rows.Add(txn.TxnID);
                    int i = tb.Rows.Count - 1;//得到新建的Row号
                    tb.Rows[i][TXNID] = txn.TxnID;
                    tb.Rows[i][SETTLEDAY] = txn.Settleday;
                    tb.Rows[i][ACCOUNT] = txn.Account;
                    tb.Rows[i][TXNTYPE] = Util.GetEnumDescription(txn.TxnType);
                    tb.Rows[i][EQUITYTYPE] = Util.GetEnumDescription(txn.EquityType);
                    tb.Rows[i][AMOUNT] = txn.Amount.ToFormatStr();
                    tb.Rows[i][DATETIME] = Util.ToDateTime(txn.DateTime).ToString("yyyy-MM-dd HH:mm:ss");
                    tb.Rows[i][OPERATOR] = txn.Operator;
                    tb.Rows[i][TXNREF] = txn.TxnRef;
                    tb.Rows[i][COMMENT] = txn.Comment;
                   

                }
                catch (Exception ex)
                { 
                    
                }
            }
        }

        const string TXNID = "出入金编号";
        const string ACCOUNT = "交易账户";
        const string AMOUNT = "金额";
        const string TXNTYPE = "出金/入金";
        const string EQUITYTYPE = "资金类别";
        const string SETTLEDAY = "结算日";
        const string DATETIME = "时间";
        const string OPERATOR = "操作员";
        const string TXNREF = "引用单号";
        const string COMMENT = "备注";



        DataTable tb = new DataTable();
        BindingSource datasource = new BindingSource();
        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            KryptonDataGridView grid = txnGrid;

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

        }

        /// <summary>
        /// 初始化数据表格
        /// </summary>
        private void InitTable()
        {
            
            tb.Columns.Add(TXNID);
            tb.Columns.Add(SETTLEDAY);
            tb.Columns.Add(ACCOUNT);
            tb.Columns.Add(TXNTYPE);
            tb.Columns.Add(EQUITYTYPE);
            tb.Columns.Add(AMOUNT);
            tb.Columns.Add(DATETIME);

            tb.Columns.Add(OPERATOR);
            tb.Columns.Add(TXNREF);
            tb.Columns.Add(COMMENT);


        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            KryptonDataGridView grid = txnGrid;
            //grid.TableElement.BeginUpdate();             
            //grid.MasterTemplate.Columns.Clear(); 
            datasource.DataSource = tb;
            //datasource.Sort = DATETIME + " DESC";
            grid.DataSource = datasource;

            //grid.Columns[ORDERID].Visible = false;
            //grid.Columns[TRADEID].Visible = false;
            //grid.Columns[DATETIME].Visible = false;

            //set width
            //grid.Columns[SYMBOL].Width = 80;
            for (int i = 0; i < tb.Columns.Count; i++)
            {
                grid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        /// <summary>
        /// 清空表格内容
        /// </summary>
        public void Clear()
        {

            txnGrid.DataSource = null;
            tb.Rows.Clear();
            BindToTable();
        }

    }
}
