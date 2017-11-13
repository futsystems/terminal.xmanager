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
    public partial class fmHistSecurityStatistic : ComponentFactory.Krypton.Toolkit.KryptonForm, IEventBinder
    {
        public fmHistSecurityStatistic()
        {
            InitializeComponent();

          
            this.Load += new EventHandler(fmReportSettlement_Load);
        }

        void fmReportSettlement_Load(object sender, EventArgs e)
        {
            SetPreferences();
            InitTable();
            BindToTable();

            start.Value = Convert.ToDateTime(DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd") + " 0:00:00");
            end.Value = DateTime.Now;

            btnQry.Click += new EventHandler(btnQry_Click);
            CoreService.EventCore.RegIEventHandler(this);
            btn_export.Click += new EventHandler(btn_export_Click);

            this.Text = "统计-下级账户品种汇总";
        }




        void btn_export_Click(object sender, EventArgs e)
        {
            MoniterHelper.ExportToCSV(GetCsvFilePrefix(), settlementGrid);
        }

        string GetCsvFilePrefix()
        {
           return string.Format("{0}_{1}_SecurityStatistic",inputAccount.Text, string.Format("{0}-{1}", Start, End));
        }

       

        void  btnQry_Click(object sender, EventArgs e)
        {
 	        this.Clear();
            if (string.IsNullOrEmpty(inputAccount.Text))
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请输入帐户");
                return;
            }

            CoreService.TLClient.ReqQrySecurityStatistic(inputAccount.Text, Start, End, direct.Checked);
            
        }

        int Start { get { return Util.ToTLDate(start.Value); } }
        int End { get { return Util.ToTLDate(end.Value); } }

        public void OnInit()
        {
            CoreService.EventCore.RegisterCallback(Modules.AgentManager, Method_AGENT_MGR.QRY_STATISTIC_SECURITY, this.OnQryStatisticSecurity);
        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback(Modules.AgentManager, Method_AGENT_MGR.QRY_STATISTIC_SECURITY, this.OnQryStatisticSecurity);
        }

        void OnQryStatisticSecurity(string jsonstr, bool islast)
        {
            SecurityStatistic obj = CoreService.ParseJsonResponse<SecurityStatistic>(jsonstr);
            if (obj != null)
            {
                GotSecurity(obj);
            }
        }

        void GotSecurity(SecurityStatistic st)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<SecurityStatistic>(GotSecurity), new object[] { st });
            }
            else
            {
                DataRow r = gt.Rows.Add("");
                int i = gt.Rows.Count - 1;//得到新建的Row号

                gt.Rows[i][EXCHANGE] = st.exchange;
                gt.Rows[i][SECURITY] = st.securitycode;
                gt.Rows[i][TOTALSIZE] = st.total_size;
                gt.Rows[i][COMMISSION] = st.total_commission.ToFormatStr();
          
                gt.Rows[i][TAG] = st;
            }
        }

        public void Clear()
        {
            settlementGrid.DataSource = null;
            gt.Rows.Clear();
            BindToTable();
        }

        public void SetAccount(string account)
        {
            inputAccount.Text = account;
        }
        #region 表格

        #region 显示字段
       
        const string EXCHANGE = "交易所";
        const string SECURITY = "品种";
        const string TOTALSIZE = "总手数";
        const string COMMISSION = "手续费";
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
            gt.Columns.Add(EXCHANGE);
            gt.Columns.Add(SECURITY);//2
            gt.Columns.Add(TOTALSIZE);//3
            gt.Columns.Add(COMMISSION);//6
            gt.Columns.Add(TAG, typeof(SecurityStatistic));
           
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
            grid.Columns[EXCHANGE].Width = 100;
            grid.Columns[SECURITY].Width = 60;
            grid.Columns[TOTALSIZE].Width = 120;
            grid.Columns[COMMISSION].Width = 120;
            grid.Columns[TAG].Visible = false;


        }
        #endregion

    }

    public class SecurityStatistic
    {
        public int total_size { get; set; }

        public decimal total_commission { get; set; }

        public string exchange { get; set; }

        public string securitycode { get; set; }
    }
}
