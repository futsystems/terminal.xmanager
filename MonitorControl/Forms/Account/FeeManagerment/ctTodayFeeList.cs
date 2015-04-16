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
using TradingLib.Protocol.MainAcctFinService;

namespace TradingLib.MoniterControl
{
    public partial class ctTodayFeeList : UserControl,IEventBinder
    {
        public ctTodayFeeList()
        {
            InitializeComponent();
            SetPreferences();
            InitTable();
            BindToTable();
            this.Load += new EventHandler(ctTodayFeeList_Load);
        }

        void ctTodayFeeList_Load(object sender, EventArgs e)
        {

            try
            {
                WireEvent();
            }
            catch (Exception ex)
            { 
            
            }
    
        }

        void WireEvent()
        {
            feeGrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(feeGrid_RowPrePaint);

            CoreService.EventCore.RegIEventHandler(this);
            feeGrid.MouseClick += new MouseEventHandler(feeGrid_MouseClick);
            feeGrid.ContextMenuStrip = new ContextMenuStrip();
            feeGrid.ContextMenuStrip.Items.Add("手工收取", null, new EventHandler(CollectFee_Click));//1
            feeGrid.ContextMenuStrip.Items.Add("编辑收费项目", null, new EventHandler(EditFee_Click));//1
        }

        void feeGrid_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            { 
                
            }
        }



        void feeGrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts = e.PaintParts ^ DataGridViewPaintParts.Focus;
        }

        void EditFee_Click(object sender, EventArgs e)
        {
            FeeSetting f = CurrentFee;
            if (f == null)
            {
                MoniterHelper.WindowMessage("请选择收费项目");
                return;
            }
            fmEditFee fm = new fmEditFee();
            fm.SetFee(f);
            fm.ShowDialog();
            fm.Close();
        }


        void CollectFee_Click(object sender, EventArgs e)
        {
            FeeSetting f = CurrentFee;
            if (f == null)
            {
                MoniterHelper.WindowMessage("请选择收费项目");
                return;
            }
            fmCollectFeeManual fm = new fmCollectFeeManual();
            fm.SetFee(f);
            fm.ShowDialog();
            fm.Close();
        }
        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback("MainAcctFinService", "QryTodayFee", this.OnFee);
            CoreService.EventContrib.RegisterNotifyCallback("MainAcctFinService", "NotifyFee", this.OnNotifyFee);
            CoreService.TLClient.ReqContribRequest("MainAcctFinService", "QryTodayFee", "");
            
        }

        public void OnDisposed()
        {
            
            CoreService.EventContrib.UnRegisterCallback("MainAcctFinService", "QryTodayFee", this.OnFee);
            CoreService.EventContrib.UnRegisterNotifyCallback("MainAcctFinService", "NotifyFee", this.OnNotifyFee);
        }

        void OnNotifyFee(string json)
        {
            FeeSetting f = MoniterHelper.ParseJsonResponse<FeeSetting>(json);
            if (f != null)
            {
                InvokeGotFee(f);
            }
        }

        void OnFee(string json, bool islast)
        {
            FeeSetting f = MoniterHelper.ParseJsonResponse<FeeSetting>(json);
            if (f != null)
            {
                InvokeGotFee(f);
            }
        }


        private int CurrentRow { get { return feeGrid.SelectedRows.Count > 0 ? feeGrid.SelectedRows[0].Index : -1; } }

        //得到当前选择的行号
        private int CurrentFeeID
        {
            get
            {
                int row = CurrentRow;
                if (row >= 0)
                {
                    return int.Parse(feeGrid[0, row].Value.ToString());
                }
                else
                {
                    return 0;
                }
            }
        }

        private FeeSetting CurrentFee
        {
            get
            { 
                int feeid = CurrentFeeID;
                if (feemap.Keys.Contains(feeid))
                {
                    return feemap[feeid];
                }
                return null;
            }
        }


        Dictionary<int, int> feeidmap = new Dictionary<int, int>();
        Dictionary<int, FeeSetting> feemap = new Dictionary<int, FeeSetting>();

        int FeeIdx(int id)
        {
            int rowid = -1;
            if (feeidmap.TryGetValue(id, out rowid))
            {
                return rowid;
            }
            else
            {
                return -1;
            }
        }

        void InvokeGotFee(FeeSetting f)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<FeeSetting>(InvokeGotFee), new object[] { f });
            }
            else
            {
                int r = FeeIdx(f.ID);
                if (r == -1)
                {
                    gt.Rows.Add(f.ID);
                    int i = gt.Rows.Count - 1;
                    gt.Rows[i][ID] = f.ID;
                    gt.Rows[i][SETTLEDAY] = f.Settleday;
                    gt.Rows[i][ACCOUNT] = f.Account;
                    gt.Rows[i][FEETYPE] = Util.GetEnumDescription(f.FeeType);
                    gt.Rows[i][AMOUNT] = f.Amount;
                    gt.Rows[i][DATETIME] = f.DateTime;
                    gt.Rows[i][COLLECTED] = f.Collected;
                    gt.Rows[i][COMMENT] = f.Comment;
                    gt.Rows[i][CHARGETIME] =  Util.GetEnumDescription(f.ChargeTime);
                    gt.Rows[i][CHARGEMETHOD] =  Util.GetEnumDescription(f.ChargeMethod);
                    gt.Rows[i][FEESTATUS] = f.FeeStatus;
                    gt.Rows[i][FEESTATUSSTR] = Util.GetEnumDescription(f.FeeStatus);
                    gt.Rows[i][ERROR] = f.Error;
                    feemap.Add(f.ID, f);
                    feeidmap.Add(f.ID, i);

                }
                else
                {

                    gt.Rows[r][AMOUNT] = f.Amount;

                    gt.Rows[r][COLLECTED] = f.Collected;
                    gt.Rows[r][COMMENT] = f.Comment;

                    gt.Rows[r][FEESTATUS] = f.FeeStatus;
                    gt.Rows[r][FEESTATUSSTR] = Util.GetEnumDescription(f.FeeStatus);
                    gt.Rows[r][ERROR] = f.Error;
                }
            }
        }
        #region 表格
        const string ID = "全局ID";
        const string SETTLEDAY = "结算日期";
        const string ACCOUNT = "交易帐户";
        const string FEETYPE = "收费类别";
        const string AMOUNT = "金额";
        const string DATETIME = "创建时间";
        const string COLLECTED = "是否收取";
        const string COMMENT = "备注";
        const string CHARGEMETHOD = "收费方式";
        const string CHARGETIME = "收费时间";
        const string FEESTATUS = "Status";
        const string FEESTATUSSTR = "状态";
        const string ERROR = "错误原因";


        DataTable gt = new DataTable();
        BindingSource datasource = new BindingSource();

        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = feeGrid;

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
            gt.Columns.Add(ID);//
            gt.Columns.Add(SETTLEDAY);//
            gt.Columns.Add(ACCOUNT);//
            gt.Columns.Add(FEETYPE);//
            gt.Columns.Add(AMOUNT);//
            gt.Columns.Add(CHARGEMETHOD);//
            gt.Columns.Add(CHARGETIME);//
            
            gt.Columns.Add(COMMENT);//
            gt.Columns.Add(DATETIME);//
            gt.Columns.Add(FEESTATUS);
            gt.Columns.Add(FEESTATUSSTR);
            gt.Columns.Add(ERROR);
            gt.Columns.Add(COLLECTED);//
        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = feeGrid;

            datasource.DataSource = gt;
            grid.DataSource = datasource;


            grid.Columns[ID].Visible = false;
            grid.Columns[FEESTATUS].Visible = false;
            grid.Columns[DATETIME].Visible = false;
            grid.Columns[FEESTATUS].Visible = false;
            //grid.Columns[ID].Visible = false;
            //grid.Columns[ID].Visible = false;


        }





        #endregion

    }
}
