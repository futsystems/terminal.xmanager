﻿using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Collections;
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
    public partial class fmConnectorManager : ComponentFactory.Krypton.Toolkit.KryptonForm, IEventBinder
    {
        public fmConnectorManager()
        {
            InitializeComponent();
            SetPreferences();
            InitTable();
            BindToTable();

            this.Load += new EventHandler(fmConnectorManager_Load);
        }

        void fmConnectorManager_Load(object sender, EventArgs e)
        {
            WireEvent();
        }

        void WireEvent()
        {
           

            configgrid.DoubleClick += new EventHandler(configgrid_DoubleClick);
            configgrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(RowPrePaint);
            configgrid.MouseClick += new MouseEventHandler(configgrid_MouseClick);


            //vendorgrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(RowPrePaint);
            //routeritemgrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(RowPrePaint);
            //ctRouterGroupList1.RouterGroupSelectedChangedEvent += new TradingLib.API.VoidDelegate(ctRouterGroupList1_RouterGroupSelectedChangedEvent);

            //btnUpdateRouterGroup.Click += new EventHandler(btnUpdateRouterGroup_Click);
            //tabholder.SelectedPageChanged += new EventHandler(tabholder_SelectedPageChanged);
            //ctRouterGroupList1.RouterGroupSelectedChangedEvent +=new TradingLib.API.VoidDelegate(ctRouterGroupList1_RouterGroupSelectedChangedEvent);
            CoreService.EventCore.RegIEventHandler(this);
        }

        void RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts = e.PaintParts ^ DataGridViewPaintParts.Focus;
        }

        public void OnInit()
        {
            CoreService.EventCore.RegisterCallback(Modules.CONN_MGR,Method_CONN_MGR.QRY_CONN_CONFIG, this.OnQryConnectorConfig);
            CoreService.EventCore.RegisterCallback(Modules.CONN_MGR, Method_CONN_MGR.QRY_CONN_STATUS, this.OnQryConnectorStatus);
            CoreService.EventCore.RegisterCallback(Modules.CONN_MGR,Method_CONN_MGR.QRY_INTERFACE, this.OnQryInterface);

            CoreService.EventCore.RegisterNotifyCallback(Modules.CONN_MGR, Method_CONN_MGR.NOTIFY_CONN_CONFIG, this.OnNotifyConnectorConfig);
            CoreService.EventCore.RegisterNotifyCallback(Modules.CONN_MGR, Method_CONN_MGR.NOTIFY_CONN_STATUS, this.OnNotifyConnectorStatus);

            CoreService.TLClient.ReqQryInterface();
        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback(Modules.CONN_MGR, Method_CONN_MGR.QRY_CONN_CONFIG, this.OnQryConnectorConfig);
            CoreService.EventCore.UnRegisterCallback(Modules.CONN_MGR, Method_CONN_MGR.QRY_CONN_STATUS, this.OnQryConnectorStatus);
            CoreService.EventCore.UnRegisterCallback(Modules.CONN_MGR, Method_CONN_MGR.QRY_INTERFACE, this.OnQryInterface);

            CoreService.EventCore.UnRegisterNotifyCallback(Modules.CONN_MGR, Method_CONN_MGR.NOTIFY_CONN_CONFIG, this.OnNotifyConnectorConfig);
            CoreService.EventCore.UnRegisterNotifyCallback(Modules.CONN_MGR, Method_CONN_MGR.NOTIFY_CONN_STATUS, this.OnNotifyConnectorStatus);

        }

        ConnectorInterface ID2Interface(int id)
        {
            if (interfacemap.Keys.Contains(id))
                return interfacemap[id];
            return null;

        }

        public ArrayList GetInterfaceCBList()
        {
            ArrayList list = new ArrayList();
            foreach (ConnectorInterface item in interfacemap.Values)
            {
                ValueObject<int> vo = new ValueObject<int>();
                vo.Name = item.Name;
                vo.Value = item.ID;
                list.Add(vo);
            }
            return list;
        }


        ConcurrentDictionary<int, ConnectorInterface> interfacemap = new ConcurrentDictionary<int, ConnectorInterface>();
        bool _gotinterface = false;

        void OnQryInterface(string jsonstr, bool islast)
        {
            ConnectorInterface[] objs = CoreService.ParseJsonResponse<ConnectorInterface[]>(jsonstr);
            if (objs != null)
            {
                foreach (ConnectorInterface op in objs)
                {
                    if (!interfacemap.Keys.Contains(op.ID))
                        interfacemap.TryAdd(op.ID, op);
                }
                _gotinterface = true;
                //if (!_gotvendor)
                //{
                //    Globals.TLClient.ReqQryVendor();
                //}
                if (!_gotconnector)
                {
                    CoreService.TLClient.ReqQryConnectorConfig();
                }
            }
            else//如果没有配资服
            {

            }
        }







        void OnNotifyConnectorConfig(string jsonstr)
        {
            ConnectorConfig cfg = CoreService.ParseJsonResponse<ConnectorConfig>(jsonstr);
            if (cfg != null)
            {
                InvokeGotConnector(cfg);
            }
            else//如果没有配资服
            {

            }
        }

        bool _gotconnector = false;
        void OnQryConnectorConfig(string jsonstr, bool islast)
        {
            ConnectorConfig[] objs = CoreService.ParseJsonResponse<ConnectorConfig[]>(jsonstr);
            if (objs != null)
            {
                foreach (ConnectorConfig op in objs)
                {
                    InvokeGotConnector(op);
                }
                _gotconnector = true;
                if (!_gotstatus)
                {
                    CoreService.TLClient.ReqQryConnectorStatus();
                }
            }
            else//如果没有配资服
            {

            }
        }

        bool _gotstatus = false;
        void OnQryConnectorStatus(string jsonstr, bool islast)
        {
            ConnectorStatus[] objs = CoreService.ParseJsonResponse<ConnectorStatus[]>(jsonstr);
            if (objs != null)
            {
                foreach (ConnectorStatus op in objs)
                {
                    InvokeGotConnectorStatus(op);
                }
                _gotstatus = true;
            }
            else//如果没有配资服
            {

            }
        }

        void OnNotifyConnectorStatus(string jsonstr)
        {
            ConnectorStatus obj = CoreService.ParseJsonResponse<ConnectorStatus>(jsonstr);
            if (obj != null)
            {
                InvokeGotConnectorStatus(obj);
                _gotstatus = true;
            }
            else//如果没有配资服
            {

            }
        }

        //得到当前选择的行号
        private ConnectorConfig CurrentConnectorConfig
        {
            get
            {
                int row = configgrid.SelectedRows.Count > 0 ? configgrid.SelectedRows[0].Index : -1;
                if (row >= 0)
                {
                    int id = int.Parse(configgrid[0, row].Value.ToString());

                    if (connectormap.Keys.Contains(id))
                        return connectormap[id];
                    else
                        return null;
                }
                else
                {
                    return null;
                }
            }
        }

        void configgrid_DoubleClick(object sender, EventArgs e)
        {
            ConnectorConfig cfg = CurrentConnectorConfig;
            if (cfg != null)
            {
                fmConnectorEdit fm = new fmConnectorEdit();
                fm.SetInterfaceCBList(this.GetInterfaceCBList());
                fm.SetConnectorConfig(cfg);
                fm.ShowDialog();
            }
        }


        ConcurrentDictionary<int, ConnectorConfig> connectormap = new ConcurrentDictionary<int, ConnectorConfig>();
        ConcurrentDictionary<int, int> connectorrowid = new ConcurrentDictionary<int, int>();

        int ConnectorIdx(int id)
        {
            int rowid = -1;
            if (!connectorrowid.TryGetValue(id, out rowid))
            {
                return -1;
            }
            else
            {
                return rowid;
            }
        }

        string GetInterfaceTite(int id)
        {
            string title = string.Empty;
            if (id == 0)
            {
                title = "未绑定";
            }
            else
            {
                ConnectorInterface setting = ID2Interface(id);

                if (setting == null)
                {
                    title = "绑定异常";
                }
                else
                {
                    title = string.Format("[{0}]{1}", setting.ID, setting.Name);
                }
            }
            return title;
        }

        //bool GetVendorBindStatus(ConnectorConfig c)
        //{
        //    return false;
        //    //string vtitle = string.Empty;
        //    //if (c.vendor_id == 0)
        //    //{
        //    //    return false;
        //    //}
        //    //else
        //    //{
        //    //    VendorSetting setting = ID2VendorSetting(c.vendor_id);
        //    //    if (setting == null)
        //    //    {
        //    //        return false;
        //    //    }
        //    //    else
        //    //    {
        //    //        return true;
        //    //    }
        //    //}
        //}

        //string GetVendorTitle(ConnectorConfig c)
        //{
        //    return "Vendor";
        //    //string vtitle = string.Empty;
        //    //if (!c.NeedVendor)
        //    //{
        //    //    return "";
        //    //}
        //    //if (c.vendor_id == 0)
        //    //{
        //    //    vtitle = "未绑定";
        //    //}
        //    //else
        //    //{
        //    //    VendorSetting setting = ID2VendorSetting(c.vendor_id);
        //    //    if (setting == null)
        //    //    {
        //    //        vtitle = "绑定异常";
        //    //    }
        //    //    else
        //    //    {
        //    //        vtitle = setting.Name;
        //    //    }
        //    //}
        //    //return vtitle;
        //}

        Image GetStatusImage(QSEnumConnectorStatus status)
        {
            if (status == QSEnumConnectorStatus.Start)
            {
                return (Image)Properties.Resources.connect;
            }
            else if (status == QSEnumConnectorStatus.Stop)
            {
                return (Image)Properties.Resources.disconnect;
            }
            return (Image)Properties.Resources.disconnect;
        }


        void InvokeGotConnectorStatus(ConnectorStatus status)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ConnectorStatus>(InvokeGotConnectorStatus), new object[] { status });
            }
            else
            {
                int r = ConnectorIdx(status.ID);
                if (r == -1)
                {
                }
                else
                {
                    gt.Rows[r][CONSTATUS] = status.Status;
                    gt.Rows[r][CONSTATUSIMG] = GetStatusImage(status.Status);
                }
            }
        }
        void InvokeGotConnector(ConnectorConfig c)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ConnectorConfig>(InvokeGotConnector), new object[] { c });
            }
            else
            {
                int r = ConnectorIdx(c.ID);
                if (r == -1)
                {
                    gt.Rows.Add(c.ID);
                    int i = gt.Rows.Count - 1;

                    gt.Rows[i][TOKEN] = c.Token;
                    gt.Rows[i][NAME] = c.Name;

                    gt.Rows[i][SRVADDRESS] = c.srvinfo_ipaddress;
                    gt.Rows[i][SRVPORT] = c.srvinfo_port;
                    gt.Rows[i][SRV1] = c.srvinfo_field1;
                    gt.Rows[i][SRV2] = c.srvinfo_field2;
                    gt.Rows[i][SRV3] = c.srvinfo_field3;
                    gt.Rows[i][USERID] = c.usrinfo_userid;
                    gt.Rows[i][PASSWORD] = c.usrinfo_password;
                    gt.Rows[i][USR1] = c.usrinfo_field1;
                    gt.Rows[i][USR2] = c.usrinfo_field2;

                    gt.Rows[i][INTERFACE] = GetInterfaceTite(c.interface_fk);

                    //gt.Rows[i][VENDORACCOUNT] = GetVendorTitle(c);
                    //gt.Rows[i][ISBINDED] = GetVendorBindStatus(c);

                    connectorrowid.TryAdd(c.ID, i);
                    connectormap.TryAdd(c.ID, c);
                }
                else
                {
                    //更新状态
                    gt.Rows[r][SRVADDRESS] = c.srvinfo_ipaddress;
                    gt.Rows[r][SRVPORT] = c.srvinfo_port;
                    gt.Rows[r][SRV1] = c.srvinfo_field1;
                    gt.Rows[r][SRV2] = c.srvinfo_field2;
                    gt.Rows[r][SRV3] = c.srvinfo_field3;
                    gt.Rows[r][USERID] = c.usrinfo_userid;
                    gt.Rows[r][PASSWORD] = c.usrinfo_password;
                    gt.Rows[r][USR1] = c.usrinfo_field1;
                    gt.Rows[r][USR2] = c.usrinfo_field2;
                    gt.Rows[r][NAME] = c.Name;
                    //gt.Rows[r][VENDORACCOUNT] = GetVendorTitle(c);
                    //gt.Rows[r][ISBINDED] = GetVendorBindStatus(c);
                    connectormap[c.ID] = c;
                }

            }
        }

        #region 表格
        #region 显示字段

        const string ID = "通道ID";
        const string SRVADDRESS = "服务器地址";
        const string SRVPORT = "端口";
        const string SRV1 = "参数1";
        const string SRV2 = "参数2";
        const string SRV3 = "参数3";
        const string USERID = "登入ID";
        const string PASSWORD = "密码";
        const string USR1 = "参数1/U";
        const string USR2 = "参数2/U";
        const string INTERFACE = "接口";
        const string TOKEN = "主帐户编号";
        const string NAME = "名称";
        //const string VENDORACCOUNT = "实盘帐户";
        const string ISBINDED = "Binded";
        const string CONSTATUS = "status";
        const string CONSTATUSIMG = "连接";
        #endregion

        DataTable gt = new DataTable();
        BindingSource datasource = new BindingSource();

        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = configgrid;

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

            //grid.ContextMenuStrip = new ContextMenuStrip();

            routergridmenu = new ContextMenuStrip();
            routergridmenu.Items.Add("添加主帐户", null, new EventHandler(AddConnector_Click));//0
            routergridmenu.Items.Add("修改主帐户", null, new EventHandler(EditConnector_Click));//1
        }

        ContextMenuStrip routergridmenu = null;


        void configgrid_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                GetConnectorGridRightMenu().Show(Control.MousePosition);
            }
        }

        /// <summary>
        /// 动态生成右键菜单
        /// </summary>
        /// <returns></returns>
        ContextMenuStrip GetConnectorGridRightMenu()
        {
            ConnectorConfig cfg = CurrentConnectorConfig;
            if (cfg == null)
            {
                routergridmenu.Items[0].Visible = true;
                routergridmenu.Items[1].Visible = false;
                return routergridmenu;
            }
            return routergridmenu;
        }

        //初始化Account显示空格
        private void InitTable()
        {
            gt.Columns.Add(ID);//0
            gt.Columns.Add(TOKEN);//1
            gt.Columns.Add(NAME);//1

            gt.Columns.Add(SRVADDRESS);
            gt.Columns.Add(SRVPORT);//1
            gt.Columns.Add(SRV1);//1
            gt.Columns.Add(SRV2);//1
            gt.Columns.Add(SRV3);//1
            gt.Columns.Add(USERID);//1
            gt.Columns.Add(PASSWORD);//1
            gt.Columns.Add(USR1);//1
            gt.Columns.Add(USR2);//1

            gt.Columns.Add(INTERFACE);//1
            //gt.Columns.Add(VENDORACCOUNT);
            gt.Columns.Add(ISBINDED);
            gt.Columns.Add(CONSTATUS);
            gt.Columns.Add(CONSTATUSIMG, typeof(Image));

        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = configgrid;

            datasource.DataSource = gt;
            grid.DataSource = datasource;
            grid.Columns[SRV1].Visible = false;
            grid.Columns[SRV2].Visible = false;
            grid.Columns[SRV3].Visible = false;
            grid.Columns[PASSWORD].Visible = false;
            grid.Columns[USR1].Visible = false;
            grid.Columns[USR2].Visible = false;
            grid.Columns[SRVADDRESS].Visible = false;
            grid.Columns[SRVPORT].Visible = false;
            grid.Columns[ISBINDED].Visible = false;
            grid.Columns[CONSTATUS].Visible = false;
            grid.Columns[ID].Visible = false;
            grid.Columns[INTERFACE].Visible = false;

            //grid.Columns[ID].Width = 50;

            grid.Columns[TOKEN].Width = 80;
            grid.Columns[NAME].Width = 100;
            grid.Columns[USERID].Width = 150;
            //grid.Columns[INTERFACE].Width = 150;
            grid.Columns[CONSTATUSIMG].Width = 40;
        }

        #endregion

        /// <summary>
        /// 添加通道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AddConnector_Click(object sender, EventArgs e)
        {
            fmConnectorEdit fm = new fmConnectorEdit();
            fm.SetInterfaceCBList(this.GetInterfaceCBList());
            fm.ShowDialog();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EditConnector_Click(object sender, EventArgs e)
        {
            ConnectorConfig cfg = CurrentConnectorConfig;
            if (cfg != null)
            {
                fmConnectorEdit fm = new fmConnectorEdit();
                fm.SetInterfaceCBList(this.GetInterfaceCBList());
                fm.SetConnectorConfig(cfg);
                fm.ShowDialog();
            }
            else
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请选择需要编辑的通道");
            }
        }
    }
}
