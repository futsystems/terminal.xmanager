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
    public partial class fmSecurity : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmSecurity()
        {
            InitializeComponent();

            SetPreferences();
            InitTable();
            BindToTable();

            MoniterHelper.AdapterToIDataSource(cbtradeable).BindDataSource(MoniterHelper.GetTradeableCBList(true));
            MoniterHelper.AdapterToIDataSource(cbexchange).BindDataSource(MoniterHelper.GetExchangeComboxArray(true));
  
            this.Load += new EventHandler(fmSecurity_Load);
        }

        void fmSecurity_Load(object sender, EventArgs e)
        {
            CoreService.EventCore.RegIEventHandler(this);
            cbexchange.SelectedIndexChanged += new EventHandler(cbexchange_SelectedIndexChanged);
            cbtradeable.SelectedIndexChanged += new EventHandler(cbtradeable_SelectedIndexChanged);

            secgrid.DoubleClick += new EventHandler(secgrid_DoubleClick);
            btnAddSecurity.Click += new EventHandler(btnAddSecurity_Click);
            btnSyncSec.Click += new EventHandler(btnSyncSec_Click);
            secgrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(secgrid_RowPrePaint);

            foreach (SecurityFamilyImpl sec in CoreService.BasicInfoTracker.Securities)
            {
                InvokGotSecurity(sec);
            }
        }


        public void OnInit()
        {
            //根据权限显示界面按钮
            if (!CoreService.SiteInfo.Domain.Super)
            {

                btnSyncSec.Visible = false;
                btnAddSecurity.Visible = false;

                //如果是管理员 并且没有品种数据 则同步品种按钮可见
                if (CoreService.SiteInfo.Manager.IsRoot())
                {
                    btnSyncSec.Visible = CoreService.BasicInfoTracker.Securities.Count() == 0;
                }
            }

            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_INFO_SEC, OnNotifySecurity);
        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_INFO_SEC, OnNotifySecurity);
        }

        void OnNotifySecurity(string json)
        {
            string content = json.DeserializeObject<string>();
            SecurityFamilyImpl sec = SecurityFamilyImpl.Deserialize(content);
            InvokGotSecurity(sec);
        }



        Dictionary<int, int> securityidxmap = new Dictionary<int, int>();
        int SecurityFamilyIdx(int id)
        {

            int rowid = -1;
            if (securityidxmap.TryGetValue(id, out rowid))
            {
                return rowid;
            }
            else
            {
                return -1;
            }
        }


        string GetTradeableTitle(bool tradeable)
        {
            return tradeable ? "是" : "否";
        }


        void InvokGotSecurity(SecurityFamilyImpl sec)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<SecurityFamilyImpl>(InvokGotSecurity), new object[] { sec });
            }
            else
            {
                int r = SecurityFamilyIdx(sec.ID);
                if (r == -1)
                {
                    gt.Rows.Add(sec.ID);
                    int i = gt.Rows.Count - 1;
                    securityidxmap.Add(sec.ID, i);

                    gt.Rows[i][CCODE] = sec.Code;
                    gt.Rows[i][NAME] = sec.Name;
                    gt.Rows[i][CURRENCY] = Util.GetEnumDescription(sec.Currency);
                    gt.Rows[i][TYPE] = sec.Type;
                    gt.Rows[i][MULTIPLE] = sec.Multiple;
                    gt.Rows[i][PRICETICK] = sec.PriceTick;
                    //gt.Rows[i][TRADABLE] = sec.TradeAble;;
                    gt.Rows[i][ENTRYCOMMISSION] = sec.EntryCommission;
                    gt.Rows[i][EXITCOMMISSION] = sec.ExitCommission;
                    gt.Rows[i][EXITCOMMISSIONTODAY] = sec.ExitCommissionToday;
                    gt.Rows[i][MARGIN] = sec.Margin;
                    gt.Rows[i][EXTRAMARGIN] = sec.ExtraMargin;
                    gt.Rows[i][MAINTANCEMARGIN] = sec.MaintanceMargin;
                    gt.Rows[i][EXCHANGEID] = sec.exchange_fk;
                    sec.Exchange = CoreService.BasicInfoTracker.GetExchange(sec.exchange_fk);
                    gt.Rows[i][EXCHANGE] = sec.Exchange != null ? sec.Exchange.Title : "未设置";
                    gt.Rows[i][UNDERLAYINGID] = sec.underlaying_fk;
                    gt.Rows[i][UNDERLAYING] = "";
                    gt.Rows[i][MARKETTIMEID] = sec.mkttime_fk;
                    sec.MarketTime = CoreService.BasicInfoTracker.GetMarketTime(sec.mkttime_fk);
                    gt.Rows[i][MARKETTIME] = sec.MarketTime != null ? sec.MarketTime.Name : "未设置";

                    gt.Rows[i][TRADEABLE] = sec.Tradeable ? 1 : -1;
                    gt.Rows[i][TRADEABLETITLE] = GetTradeableTitle(sec.Tradeable);
                    gt.Rows[i][TAG] = sec;
                }
                else
                {
                    int i = r;
                    SecurityFamilyImpl target = CoreService.BasicInfoTracker.GetSecurity(sec.ID);
                    if (target != null)
                    {
                        gt.Rows[i][CCODE] = target.Code;
                        gt.Rows[i][NAME] = target.Name;
                        gt.Rows[i][CURRENCY] = Util.GetEnumDescription(sec.Currency);
                        gt.Rows[i][TYPE] = target.Type;
                        gt.Rows[i][MULTIPLE] = target.Multiple;
                        gt.Rows[i][PRICETICK] = target.PriceTick;
                        //gt.Rows[i][TRADABLE] = target.TradeAble; ;
                        gt.Rows[i][ENTRYCOMMISSION] = target.EntryCommission;
                        gt.Rows[i][EXITCOMMISSION] = target.ExitCommission;
                        gt.Rows[i][EXITCOMMISSIONTODAY] = target.ExitCommissionToday;
                        gt.Rows[i][MARGIN] = target.Margin;
                        gt.Rows[i][EXTRAMARGIN] = target.ExtraMargin;
                        gt.Rows[i][MAINTANCEMARGIN] = target.MaintanceMargin;
                        gt.Rows[i][EXCHANGEID] = target.exchange_fk;
                        gt.Rows[i][EXCHANGE] = target.Exchange != null ? target.Exchange.Title : "未设置";
                        gt.Rows[i][UNDERLAYINGID] = target.underlaying_fk;
                        gt.Rows[i][UNDERLAYING] = "";
                        gt.Rows[i][MARKETTIMEID] = target.mkttime_fk;
                        gt.Rows[i][MARKETTIME] = target.MarketTime != null ? target.MarketTime.Name : "未设置";

                        gt.Rows[i][TRADEABLE] = sec.Tradeable ? 1 : -1;
                        gt.Rows[i][TRADEABLETITLE] = GetTradeableTitle(sec.Tradeable);

                    }


                }
            }
        }

        #region 表格
        #region 显示字段

        const string ID = "全局ID";
        const string CCODE = "字头";
        const string NAME = "名称";
        const string CURRENCY = "货币";
        const string TYPE = "证券品种";
        const string MULTIPLE = "乘数";
        const string PRICETICK = "价格变动";
        const string ENTRYCOMMISSION = "开仓手续费";
        const string EXITCOMMISSION = "平仓手续费";
        const string EXITCOMMISSIONTODAY = "平今手续费";
        const string MARGIN = "保证金";
        const string EXTRAMARGIN = "额外保证金";
        const string MAINTANCEMARGIN = "过夜保证金";
        const string EXCHANGEID = "ExchangeID";
        const string EXCHANGE = "交易所";
        const string UNDERLAYINGID = "UnderLayingID";
        const string UNDERLAYING = "底层证券";
        const string MARKETTIMEID = "MarketTimeID";
        const string MARKETTIME = "交易时间段";
        const string TRADEABLE = "TRADEABLE";
        const string TRADEABLETITLE = "允许交易";
        const string TAG = "TAG";
        #endregion

        DataTable gt = new DataTable();
        BindingSource datasource = new BindingSource();

        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = secgrid;

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
            gt.Columns.Add(ID);
            gt.Columns.Add(CCODE);
            gt.Columns.Add(NAME);
            gt.Columns.Add(CURRENCY);
            gt.Columns.Add(TYPE);
            gt.Columns.Add(EXCHANGEID);
            gt.Columns.Add(EXCHANGE);
            gt.Columns.Add(MARKETTIMEID);
            gt.Columns.Add(MARKETTIME);

            gt.Columns.Add(MULTIPLE);
            gt.Columns.Add(PRICETICK);
            gt.Columns.Add(ENTRYCOMMISSION);
            gt.Columns.Add(EXITCOMMISSION);
            gt.Columns.Add(EXITCOMMISSIONTODAY);
            gt.Columns.Add(MARGIN);
            gt.Columns.Add(EXTRAMARGIN);
            gt.Columns.Add(MAINTANCEMARGIN);
            
            gt.Columns.Add(UNDERLAYINGID);
            gt.Columns.Add(UNDERLAYING);
            
            gt.Columns.Add(TRADEABLE);
            gt.Columns.Add(TRADEABLETITLE);
            gt.Columns.Add(TAG,typeof(SecurityFamilyImpl));
        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = secgrid;

            datasource.DataSource = gt;
            grid.DataSource = datasource;

            //需要在绑定数据源后设定具体的可见性
            grid.Columns[ID].Visible = false;
            grid.Columns[UNDERLAYING].Visible = false;
            grid.Columns[TYPE].Visible = false;

            grid.Columns[EXCHANGEID].Visible = false;
            grid.Columns[UNDERLAYINGID].Visible = false;
            grid.Columns[MARKETTIMEID].Visible = false;
            grid.Columns[TRADEABLE].Visible = false;

            grid.Columns[EXTRAMARGIN].Visible = false;
            grid.Columns[MAINTANCEMARGIN].Visible = false;
            grid.Columns[TAG].Visible = false;
        }





        #endregion


        void RefreshSecurityQuery()
        {
            string sectype = string.Empty;
            sectype = "*";
            string strFilter = string.Empty;

            strFilter = string.Format(TYPE + " > '{0}'", sectype);

            if (cbexchange.SelectedIndex != 0)
            {
                strFilter = string.Format(strFilter + " and " + EXCHANGEID + " = '{0}'", cbexchange.SelectedValue);
            }

            if (cbtradeable.SelectedIndex != 0)
            {
                int sv = int.Parse(cbtradeable.SelectedValue.ToString());
                strFilter = string.Format(strFilter + " and " + TRADEABLE + " = '{0}'", sv);

            }
            datasource.Filter = strFilter;
        }

        private SecurityFamilyImpl CurrentSecurity
        {
            get
            {
                int row = secgrid.SelectedRows.Count > 0 ? secgrid.SelectedRows[0].Index : -1;
                if (row >= 0)
                {
                    return secgrid[TAG, row].Value as SecurityFamilyImpl;
                }
                else
                {
                    return null;
                }
            }
        }


        void btnSyncSec_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm("确认同步品种信息？") == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqSyncSecurity();
            }
        }

        void secgrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts = e.PaintParts ^ DataGridViewPaintParts.Focus;
        }

        private void cbsecurity_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshSecurityQuery();
        }

        private void cbexchange_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshSecurityQuery();
        }

        private void cbtradeable_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshSecurityQuery();
        }

        private void secgrid_DoubleClick(object sender, EventArgs e)
        {
            if (!CoreService.SiteInfo.Manager.IsRoot()) return;
            SecurityFamilyImpl sec = CurrentSecurity;
            if (sec != null)
            {
                fmSecurityEdit fm = new fmSecurityEdit();
                fm.Security = sec;
                fm.ShowDialog();
            }
        }

        private void btnAddSecurity_Click(object sender, EventArgs e)
        {
            fmSecurityEdit fm = new fmSecurityEdit();
            fm.ShowDialog();
        }
    }
}
