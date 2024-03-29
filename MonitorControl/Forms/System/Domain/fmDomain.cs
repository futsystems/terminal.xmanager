﻿using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;
using Common.Logging;

namespace TradingLib.MoniterControl
{
    public partial class fmDomain : ComponentFactory.Krypton.Toolkit.KryptonForm, IEventBinder
    {
        ILog logger = LogManager.GetLogger("DomanManager");
        public fmDomain()
        {
            InitializeComponent();
            SetPreferences();
            InitTable();
            BindToTable();

            this.Load += new EventHandler(fmDomain_Load);
        }

        void fmDomain_Load(object sender, EventArgs e)
        {
            domaingrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(domaingrid_RowPrePaint);
            CoreService.EventCore.RegIEventHandler(this);
        }

        void domaingrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts = e.PaintParts ^ DataGridViewPaintParts.Focus;
        }


        private DomainImpl CurrentDomain
        {
            get
            {
                int row = domaingrid.SelectedRows.Count > 0 ? domaingrid.SelectedRows[0].Index : -1;
                if (row >= 0)
                {
                    return domaingrid[TAG, row].Value as DomainImpl;
                }
                else
                {
                    return null;
                }
            }
        }

        public void OnInit()
        {
            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_DOMAIN, this.OnQryDomain);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_DOMAIN, this.OnNotifyDomain);
            CoreService.TLClient.ReqQryDomain();
        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_DOMAIN, this.OnQryDomain);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_DOMAIN, this.OnNotifyDomain);
       
        }

        void OnQryDomain(string jsonstr, bool islast)
        {
            DomainImpl[] objs = CoreService.ParseJsonResponse<DomainImpl[]>(jsonstr);
            if (objs != null)
            {
                foreach (DomainImpl op in objs.OrderBy(d=>d.ID))
                {
                    InvokeGotDomain(op);
                }
            }
        }

        void OnNotifyDomain(string json)
        {
            DomainImpl obj = CoreService.ParseJsonResponse<DomainImpl>(json);
            if (obj != null)
            {
                InvokeGotDomain(obj);
            }
        }

        ConcurrentDictionary<int, int> domainrowid = new ConcurrentDictionary<int, int>();

        int DomainIdx(int id)
        {
            int rowid = -1;
            if (!domainrowid.TryGetValue(id, out rowid))
            {
                return -1;
            }
            else
            {
                return rowid;
            }
        }

        void InvokeGotDomain(DomainImpl domain)
        {
            if (domain.ID == 1) return;
            if (InvokeRequired)
            {
                Invoke(new Action<DomainImpl>(InvokeGotDomain), new object[] { domain });
            }
            else
            {
                int r = DomainIdx(domain.ID);
                if (r == -1)
                {
                    gt.Rows.Add(domain.ID);
                    int i = gt.Rows.Count - 1;

                    gt.Rows[i][NAME] = domain.Name;
                    gt.Rows[i][LINKMAN] = domain.LinkMan;
                    gt.Rows[i][DATECREATED] = domain.DateCreated;
                    gt.Rows[i][DATEEXPIRED] = domain.DateExpired;
                    gt.Rows[i][VENDORLIMIT] = domain.VendorLimit;
                    gt.Rows[i][ACCLIMIT] = domain.AccLimit;
                    gt.Rows[i][ROUTERGROUPLIMIT] = domain.RouterGroupLimit;
                    //gt.Rows[i][ROUTERITEMLIMIT] = domain.RouterItemLimit;
                    gt.Rows[i][DISCOUNTNUM] = domain.DiscountNum;
                    gt.Rows[i][PRODUCTION] = domain.IsProduction ? "运营" : "测试";
                    gt.Rows[i][TAG] = domain;

                    domainrowid.TryAdd(domain.ID, i);
                    
                }
                else
                {
                    gt.Rows[r][NAME] = domain.Name;
                    gt.Rows[r][LINKMAN] = domain.LinkMan;

                    gt.Rows[r][DATEEXPIRED] = domain.DateExpired;
                    gt.Rows[r][VENDORLIMIT] = domain.VendorLimit;
                    gt.Rows[r][ACCLIMIT] = domain.AccLimit;
                    gt.Rows[r][ROUTERGROUPLIMIT] = domain.RouterGroupLimit;
                    //gt.Rows[r][ROUTERITEMLIMIT] = domain.RouterItemLimit;
                    gt.Rows[r][DISCOUNTNUM] = domain.DiscountNum;
                    gt.Rows[r][PRODUCTION] = domain.IsProduction?"运营":"测试";
                }

            }
        }



        #region 表格
        #region 显示字段

        const string DOMAINID = "域ID";
        const string NAME = "名称";
        const string LINKMAN = "联系人";
        const string DATECREATED = "创建日";
        const string DATEEXPIRED = "到期日";
        const string VENDORLIMIT = "实盘帐户";
        const string ACCLIMIT = "分帐户";
        const string ROUTERGROUPLIMIT = "路由组";
        //const string ROUTERITEMLIMIT = "路由项目";
        const string DISCOUNTNUM = "优惠数量";
        const string PRODUCTION = "状态";
        const string TAG = "TAG";

        #endregion

        DataTable gt = new DataTable();
        BindingSource datasource = new BindingSource();

        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = domaingrid;

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

            grid.ContextMenuStrip = new ContextMenuStrip();
            grid.ContextMenuStrip.Items.Add("添加Domain", null, new EventHandler(AddDomain_Click));
            grid.ContextMenuStrip.Items.Add("修改Domain", null, new EventHandler(EditDomain_Click));
            grid.ContextMenuStrip.Items.Add(new System.Windows.Forms.ToolStripSeparator());
            grid.ContextMenuStrip.Items.Add("查看管理员密码", null, new EventHandler(QryDomainRootPass_Click));
        }

        //初始化Account显示空格
        private void InitTable()
        {
            gt.Columns.Add(DOMAINID);//0
            gt.Columns.Add(NAME);//1
            gt.Columns.Add(LINKMAN);//1
            gt.Columns.Add(DATECREATED);
            gt.Columns.Add(DATEEXPIRED);
            gt.Columns.Add(VENDORLIMIT);
            gt.Columns.Add(ACCLIMIT);//1
            gt.Columns.Add(ROUTERGROUPLIMIT);//1
            gt.Columns.Add(DISCOUNTNUM);
            gt.Columns.Add(PRODUCTION);
            gt.Columns.Add(TAG, typeof(DomainImpl));
        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = domaingrid;

            datasource.DataSource = gt;
            //datasource.Sort = DOMAINID;

            grid.DataSource = datasource;

            grid.Columns[TAG].Visible = false;

            grid.Columns[DOMAINID].Width = 30;
            grid.Columns[NAME].Width = 100;
            //grid.Columns[ISXAPI].Width = 50;
            //grid.Columns[TYPE].Width = 50;
            grid.Columns[DATECREATED].Width = 60;
            grid.Columns[DATEEXPIRED].Width = 60;

            grid.Columns[VENDORLIMIT].Width = 60;
            grid.Columns[ACCLIMIT].Width = 60;
            grid.Columns[ROUTERGROUPLIMIT].Width = 60;
            //grid.Columns[ROUTERITEMLIMIT].Width = 60;
            grid.Columns[DISCOUNTNUM].Width = 60;
            grid.Columns[PRODUCTION].Width = 60;
            
           

            for (int i = 0; i < gt.Columns.Count; i++)
            {
                grid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }





        #endregion


        void AddDomain_Click(object sender, EventArgs e)
        {
            fmDomainEdit fm = new fmDomainEdit();
            fm.Show();
        }

        /// <summary>
        /// 查询域RootPass
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void QryDomainRootPass_Click(object sender, EventArgs e)
        {
            if(CurrentDomain != null)
            {
                fmLoginInfo fm = new fmLoginInfo();
                fm.SetDomain(CurrentDomain);
                fm.ShowDialog();
            }
        }


        void EditDomain_Click(object sender, EventArgs e)
        {
            DomainImpl domain = CurrentDomain;
            if (domain == null)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请选择要编辑分区");
                return;
            }
            fmDomainEdit fm = new fmDomainEdit();
            fm.SetDomain(domain);
            fm.Show();
        }

    }
}
