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
        public fmManagerCentre()
        {
            InitializeComponent();
            SetPreferences();
            InitTable();
            BindToTable();

            this.Load += new EventHandler(fmManagerCentre_Load);
           
        }

        void fmManagerCentre_Load(object sender, EventArgs e)
        {
            this.mgrgrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(mgrgrid_RowPrePaint);
            CoreService.EventCore.RegIEventHandler(this);
        }

        public void OnInit()
        {

            CoreService.EventBasicInfo.OnManagerEvent += new Action<ManagerSetting>(GotManager);
            CoreService.EventContrib.RegisterNotifyCallback("MgrExchServer", "NotifyManagerDelete", OnNotifyManagerDelete);


            //分区管理员可以查询柜员密码和设置柜员权限
            if (!CoreService.SiteInfo.Manager.IsRoot())
            {
                mgrgrid.ContextMenuStrip.Items[5].Visible = false;
                mgrgrid.ContextMenuStrip.Items[6].Visible = false;
                mgrgrid.ContextMenuStrip.Items[7].Visible = false;
                mgrgrid.ContextMenuStrip.Items[8].Visible = false;
            }

            foreach (ManagerSetting m in CoreService.BasicInfoTracker.Managers)
            {
                this.GotManager(m);
            }

        }

        public void OnDisposed()
        {
            CoreService.EventBasicInfo.OnManagerEvent -= new Action<ManagerSetting>(GotManager);
        }
        void mgrgrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts = e.PaintParts ^ DataGridViewPaintParts.Focus;
        }

        void AddManager_Click(object sender, EventArgs e)
        {
            if (!CoreService.SiteInfo.Manager.RightAddManager())
            {
                MoniterHelper.WindowMessage("没有添加柜员的权限");
                return;
            }
            fmManagerEdit fm = new fmManagerEdit();
            fm.Show();
        }

        void EditManager_Click(object sender, EventArgs e)
        {
            int id = CurrentManagerID;
            ManagerSetting manger = CoreService.BasicInfoTracker.GetManager(id);
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

        void ActiveManager_Click(object sender, EventArgs e)
        {
            int id = CurrentManagerID;
            ManagerSetting manger = CoreService.BasicInfoTracker.GetManager(id);
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
            int id = CurrentManagerID;
            ManagerSetting manger = CoreService.BasicInfoTracker.GetManager(id);
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
        //得到当前选择的行号
        private int CurrentManagerID
        {
            get
            {
                int row = mgrgrid.SelectedRows.Count > 0 ? mgrgrid.SelectedRows[0].Index : -1;
                if (row>= 0)
                {
                    return int.Parse(mgrgrid[0,row].Value.ToString());
                }
                else
                {
                    return 0;
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

        void DelManager_Click(object sender, EventArgs e)
        {
            ManagerSetting manger = CoreService.BasicInfoTracker.GetManager(CurrentManagerID);
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
            ManagerSetting manger = CoreService.BasicInfoTracker.GetManager(CurrentManagerID);
            if (manger == null)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请选择管理员");
                return;
            }

            fmLoginInfo fm = new fmLoginInfo();
            fm.SetManager(manger);
            fm.ShowDialog();
            
        }

        void SetPermission_Click(object sender, EventArgs e)
        {
            ManagerSetting manager = CoreService.BasicInfoTracker.GetManager(CurrentManagerID);
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
            ManagerSetting mgr = MoniterUtil.ParseJsonResponse<ManagerSetting>(json);
            if (mgr != null)
            {
                OnManagerDelete(mgr);
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
                int r = MangerIdx(manager.ID);
                if (r > 0)
                {
                    gt.Rows[r][DELETE] = true;
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
                //Globals.Debug("got mangaer:" + manger.ID.ToString());
                int r = MangerIdx(manger.ID);
                //添加
                if (r == -1)
                {
                    //Globals.Debug("add row");
                    gt.Rows.Add(manger.ID);
                    int i = gt.Rows.Count - 1;
                    gt.Rows[i][LOGIN] = manger.Login;
                    gt.Rows[i][MGRTYPESTR] = (manger.Type == QSEnumManagerType.AGENT ? "" : "员工-") + Util.GetEnumDescription(manger.Type);
                    gt.Rows[i][MGRTYPE] = manger.Type;
                    gt.Rows[i][NAME] = manger.Name;
                    gt.Rows[i][MOBILE] = manger.Mobile;
                    gt.Rows[i][QQ] = manger.QQ;
                    gt.Rows[i][ACCNUMLIMIT] = manger.Type == QSEnumManagerType.AGENT ? manger.AccLimit.ToString() : "";
                    gt.Rows[i][AGENTLIMIT] = manger.Type == QSEnumManagerType.AGENT ? manger.AgentLimit.ToString() : "";
                    gt.Rows[i][MGRFK] = manger.mgr_fk;
                    gt.Rows[i][STATUS] = getStatusImage(manger.Active);
                    gt.Rows[i][DELETE] = false;
                    mgridmap.Add(manger.ID, i);//记录全局ID和table序号的映射

                }
                else
                {
                    //Globals.Debug("update manager:" + manger.Active.ToString());
                    int i = r;
                    gt.Rows[i][LOGIN] = manger.Login;
                    gt.Rows[i][MGRTYPESTR] = Util.GetEnumDescription(manger.Type);
                    gt.Rows[i][MGRTYPE] = manger.Type;
                    gt.Rows[i][NAME] = manger.Name;
                    gt.Rows[i][MOBILE] = manger.Mobile;
                    gt.Rows[i][QQ] = manger.QQ;
                    gt.Rows[i][ACCNUMLIMIT] = manger.Type == QSEnumManagerType.AGENT ? manger.AccLimit.ToString() : "";
                    gt.Rows[i][AGENTLIMIT] = manger.Type == QSEnumManagerType.AGENT ? manger.AgentLimit.ToString() : "";
                    gt.Rows[i][MGRFK] = manger.mgr_fk;
                    gt.Rows[i][STATUS] = getStatusImage(manger.Active);

                }
            }
        }


        #region 表格

        const string ID = "全局ID";
        const string LOGIN = "登入名";
        const string MGRTYPE = "MGRTYPE";
        const string MGRTYPESTR = "柜员类型";
        const string NAME = "姓名";
        const string MOBILE = "手机";
        const string QQ = "QQ号码";
        const string ACCNUMLIMIT = "帐户数量";
        const string AGENTLIMIT = "代理数量";

        const string MGRFK = "域ID";
        const string STATUS = "状态";
        const string DELETE = "删除";
        

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
            gt.Columns.Add(ID);//
            gt.Columns.Add(LOGIN);//
            gt.Columns.Add(MGRTYPE);//
            gt.Columns.Add(MGRTYPESTR);
            gt.Columns.Add(NAME);//
            gt.Columns.Add(MOBILE);//
            gt.Columns.Add(QQ);//
            gt.Columns.Add(ACCNUMLIMIT);//
            gt.Columns.Add(AGENTLIMIT);
            gt.Columns.Add(MGRFK);//
            gt.Columns.Add(STATUS,typeof(Image));//
            gt.Columns.Add(DELETE);
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
            datasource.Filter = DELETE + " ='false'";

            grid.Columns[MGRTYPE].Visible = false;
            grid.Columns[ID].Visible = false;
            grid.Columns[DELETE].Visible = false;
            grid.Columns[QQ].Visible = false;
        }


        #endregion


    }
}
