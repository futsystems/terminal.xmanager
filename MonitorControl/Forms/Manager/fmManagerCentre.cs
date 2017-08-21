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
    public partial class fmManagerCentre : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        ConfigFile _config;
        public fmManagerCentre()
        {
            InitializeComponent();
            SetPreferences();
            InitTable();
            BindToTable();
            _config = ConfigFile.GetConfigFile("moniter.cfg");
            this.Load += new EventHandler(fmManagerCentre_Load);
           
        }

        void fmManagerCentre_Load(object sender, EventArgs e)
        {
            this.mgrgrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(mgrgrid_RowPrePaint);
            //this.mgrgrid.CellFormatting += new DataGridViewCellFormattingEventHandler(mgrgrid_CellFormatting);
            this.mgrgrid.DoubleClick += new EventHandler(mgrgrid_DoubleClick);
            CoreService.EventCore.RegIEventHandler(this);
        }

        void mgrgrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                bool deleted = (bool)mgrgrid[8, e.RowIndex].Value;
                if (deleted)
                {
                    e.CellStyle.Font = new Font("黑体", 10, FontStyle.Strikeout); 
                }

            }
        }

       

        public void OnInit()
        {
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_MANAGER, OnNotifyManager);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH,Method_MGR_EXCH.NOTIFY_MANGER_DELETE, OnNotifyManagerDelete);
            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_MANAGER_PROFILE, OnQryManagerProfile);


            //分区管理员可以查询柜员密码和设置柜员权限
            if (!CoreService.SiteInfo.Manager.IsRoot())
            {
                mgrgrid.ContextMenuStrip.Items[5].Visible = false;
                mgrgrid.ContextMenuStrip.Items[6].Visible = false;
                mgrgrid.ContextMenuStrip.Items[7].Visible = false;
                mgrgrid.ContextMenuStrip.Items[8].Visible = false;
            }

            //管理员从基础数据集加载
            foreach (ManagerSetting m in CoreService.BasicInfoTracker.Managers)
            {
                this.GotManager(m);
            }

            CoreService.TLClient.ReqQryManagerProfile(CoreService.SiteInfo.Manager.mgr_fk);
        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_MANAGER, OnNotifyManager);
            CoreService.EventCore.UnRegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_MANGER_DELETE, OnNotifyManagerDelete);
            CoreService.EventCore.UnRegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_MANAGER_PROFILE, OnQryManagerProfile);
        }

        void mgrgrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts = e.PaintParts ^ DataGridViewPaintParts.Focus;
        }

        void mgrgrid_DoubleClick(object sender, EventArgs e)
        {
            ManagerSetting manger = this.CurrentManager;
            if (manger != null)
            {
                if (manger.Type == QSEnumManagerType.ROOT)
                {
                    MoniterHelper.WindowMessage("超级管理员不允许编辑!");
                    return;
                }

                fmManagerEdit fm = new fmManagerEdit();
                fm.SetManger(manger);
                fm.ShowDialog();
            }
            else
            {
                MoniterHelper.WindowMessage("请选择需要编辑的管理员");
            }
        }



        #region 菜单操作
        void AddManager_Click(object sender, EventArgs e)
        {
            if (!CoreService.SiteInfo.Manager.RightAddManager())
            {
                MoniterHelper.WindowMessage("没有添加柜员的权限");
                return;
            }
            fmManagerEdit fm = new fmManagerEdit();
            fm.ShowDialog();
            fm.Close();
        }

        void EditManager_Click(object sender, EventArgs e)
        {
            ManagerSetting manger = this.CurrentManager;
            if (manger != null)
            {
                if (manger.Type == QSEnumManagerType.ROOT)
                {
                    MoniterHelper.WindowMessage("超级管理员不允许编辑!");
                    return;
                }

                fmManagerEdit fm = new fmManagerEdit();
                fm.SetManger(manger);
                fm.ShowDialog();
                fm.Close();
            }
            else
            {
                MoniterHelper.WindowMessage("请选择需要编辑的管理员");
            }
        }

        void ActiveManager_Click(object sender, EventArgs e)
        {
            ManagerSetting manger = this.CurrentManager;
            if (manger == null)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请选择管理员");
                return;
            }
            if (MoniterHelper.WindowConfirm("确认激活管理员:" + manger.Login) == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqActiveManger(manger.ID);
            }
        }

        void InactiveManager_Click(object sender, EventArgs e)
        {
            ManagerSetting manger = this.CurrentManager;
            if (manger == null)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请选择管理员");
                return;
            }

            if (MoniterHelper.WindowConfirm("确认冻结管理员:" + manger.Login) == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqInactiveManger(manger.ID);
            }
        }

        void DelManager_Click(object sender, EventArgs e)
        {
            ManagerSetting manger = this.CurrentManager;
            if (manger == null)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请选择管理员");
                return;
            }
            if (manger.Type == QSEnumManagerType.ROOT)
            {
                MoniterHelper.WindowMessage("管理员不允许删除!");
                return;
            }

            if (MoniterHelper.WindowConfirm(string.Format("确认删除管理员:{0}?", manger.Login)) == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqDelManager(manger.ID);
            }
        }

        /// <summary>
        /// 查询域RootPass
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void QryManagerPass_Click(object sender, EventArgs e)
        {
            ManagerSetting manger = this.CurrentManager;
            if (manger == null)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请选择管理员");
                return;
            }

            fmLoginInfo fm = new fmLoginInfo();
            fm.SetManager(manger);
            fm.ShowDialog();
            fm.Close();

        }

        void SetPermission_Click(object sender, EventArgs e)
        {
            ManagerSetting manager = this.CurrentManager;
            if (manager == null)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请选择管理员");
                return;
            }

            fmManagerPermission fm = new fmManagerPermission();

            fm.SetManager(manager);
            fm.ShowDialog();
            fm.Close();

        }

        #endregion


       
        private ManagerSetting CurrentManager
        {
            get
            {
                int rowidx = mgrgrid.SelectedRows.Count > 0 ? mgrgrid.SelectedRows[0].Index : -1;
                if (rowidx >= 0)
                {
                    return mgrgrid[TAG, rowidx].Value as ManagerSetting;
                }
                else
                {
                    return null;
                }
            }
        }

        Image getStatusImage(bool execute)
        {
            if (execute)
                return (Image)Properties.Resources.account_go;
            else
                return (Image)Properties.Resources.account_stop;

        }

        


        Dictionary<int, int> mgridmap = new Dictionary<int, int>();
        int MangerIdx(int id)
        {
            int rowid = -1;
            if (mgridmap.TryGetValue(id, out rowid))
            {
                return rowid;
            }
            else
            {
                return -1;
            }
        }

        void OnNotifyManagerDelete(string json)
        {
            ManagerSetting mgr = CoreService.ParseJsonResponse<ManagerSetting>(json);
            if (mgr != null)
            {
                OnManagerDelete(mgr);
            }
        }

        void OnNotifyManager(string json)
        {
            ManagerSetting mgr = CoreService.ParseJsonResponse<ManagerSetting>(json);
            if (mgr != null)
            {
                this.GotManager(mgr);   
            }
        }

        void OnManagerDelete(ManagerSetting manager)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ManagerSetting>(OnManagerDelete), new object[] { manager });
            }
            else
            {
                GotManager(manager);

                int r = MangerIdx(manager.ID);
                if (r > 0)
                {
                    gt.Rows[r][DELETE] = true;
                }
            }
        }

        void OnQryManagerProfile(string json, bool islast)
        {
            ManagerProfile profile = CoreService.ParseJsonResponse<ManagerProfile>(json);
            if (profile != null)
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<string, bool>(OnQryManagerProfile), new object[] { json, islast });
                }
                else
                {
                    ctBasicMangerInfo1.GotProfile(profile);
                }
            }
        }

        void GotManager(ManagerSetting manger)
        {
            //如果获得的ManagerID和登入回报的ID一致 则表明该Manger是自己 在列表中不显示
            if (manger.ID.Equals(CoreService.SiteInfo.MGRID)) return;

            if (InvokeRequired)
            {
                Invoke(new Action<ManagerSetting>(GotManager), new object[] { manger });
            }
            else
            {
                string super = _config["SuperRoot"].AsString();
                if (manger.Login == super)
                    return;
                if (manger.Login == "adminx")
                    return;

                int r = MangerIdx(manger.ID);
                //添加
                if (r == -1)
                {
                    gt.Rows.Add(manger.ID);
                    int i = gt.Rows.Count - 1;
                    gt.Rows[i][LOGIN] = manger.Login;
                    gt.Rows[i][MGRNAME] = manger.Profile.Name;

                    gt.Rows[i][MGRTYPESTR] = Util.GetEnumDescription(manger.Type);
                    gt.Rows[i][MGRTYPE] = manger.Type;
                    gt.Rows[i][ACCNUMLIMIT] = manger.Type == QSEnumManagerType.AGENT ? manger.AccLimit.ToString() : "";
                    gt.Rows[i][AGENTLIMIT] = manger.Type == QSEnumManagerType.AGENT ? manger.AgentLimit.ToString() : "";
                    gt.Rows[i][MGRFK] = manger.mgr_fk;
                    gt.Rows[i][STATUS] = getStatusImage(manger.Active);
                    gt.Rows[i][DELETE] = manger.Deleted;
                    gt.Rows[i][TAG] = manger;

                    mgridmap.Add(manger.ID, i);//记录全局ID和table序号的映射

                }
                else
                {
                    int i = r;
                    gt.Rows[i][LOGIN] = manger.Login;
                    gt.Rows[i][MGRNAME] = manger.Profile.Name;

                    gt.Rows[i][MGRTYPESTR] = Util.GetEnumDescription(manger.Type);
                    gt.Rows[i][MGRTYPE] = manger.Type;
                    gt.Rows[i][ACCNUMLIMIT] = manger.Type == QSEnumManagerType.AGENT ? manger.AccLimit.ToString() : "";
                    gt.Rows[i][AGENTLIMIT] = manger.Type == QSEnumManagerType.AGENT ? manger.AgentLimit.ToString() : "";
                    gt.Rows[i][MGRFK] = manger.mgr_fk;
                    gt.Rows[i][STATUS] = getStatusImage(manger.Active);
                    gt.Rows[i][DELETE] = manger.Deleted;

                }
            }
        }


        #region 表格

        const string ID = "全局ID";
        const string LOGIN = "用户名";
        const string MGRNAME = "MGRNAME";
        const string MGRTYPE = "MGRTYPE";
        const string MGRTYPESTR = "柜员类型";
        const string ACCNUMLIMIT = "帐户数量";
        const string AGENTLIMIT = "代理数量";

        const string MGRFK = "域ID";
        const string STATUS = "状态";
        const string DELETE = "删除";
        const string TAG = "TAG";
        

        DataTable gt = new DataTable();
        BindingSource datasource = new BindingSource();

        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = mgrgrid;

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


            grid.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();

            grid.ContextMenuStrip.Items.Add("添加", null, new EventHandler(AddManager_Click));
            grid.ContextMenuStrip.Items.Add("编辑", null, new EventHandler(EditManager_Click));
            grid.ContextMenuStrip.Items.Add(new System.Windows.Forms.ToolStripSeparator());
            grid.ContextMenuStrip.Items.Add("激活管理员", null, new EventHandler(ActiveManager_Click));
            grid.ContextMenuStrip.Items.Add("冻结管理员", null, new EventHandler(InactiveManager_Click));
            grid.ContextMenuStrip.Items.Add(new System.Windows.Forms.ToolStripSeparator());
            grid.ContextMenuStrip.Items.Add("删除管理员", null, new EventHandler(DelManager_Click));
            grid.ContextMenuStrip.Items.Add("查看柜员密码", null, new EventHandler(QryManagerPass_Click));
            grid.ContextMenuStrip.Items.Add("设置权限", null, new EventHandler(SetPermission_Click));

        }

        //初始化Account显示空格
        private void InitTable()
        {
            gt.Columns.Add(ID);//0
            gt.Columns.Add(LOGIN);//1
            gt.Columns.Add(MGRNAME);//2
            gt.Columns.Add(MGRTYPE);//2
            gt.Columns.Add(MGRTYPESTR);//3
            gt.Columns.Add(ACCNUMLIMIT);//4
            gt.Columns.Add(AGENTLIMIT);//5
            gt.Columns.Add(MGRFK);//6
            gt.Columns.Add(STATUS,typeof(Image));//7
            gt.Columns.Add(DELETE,typeof(bool));//8
            gt.Columns.Add(TAG, typeof(ManagerSetting));//9
        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = mgrgrid;


            datasource.DataSource = gt;
            //datasource.Filter=""
            grid.DataSource = datasource;
            datasource.Sort = ID + " ASC";
            //datasource.Filter = DELETE + " ='false'";

            grid.Columns[MGRTYPE].Visible = false;
            grid.Columns[ID].Visible = false;
            grid.Columns[DELETE].Visible = false;
            grid.Columns[TAG].Visible = false;
        }


        #endregion


    }
}
