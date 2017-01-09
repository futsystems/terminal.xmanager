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
    public partial class fmRouterGroup : ComponentFactory.Krypton.Toolkit.KryptonForm, IEventBinder
    {
        public fmRouterGroup()
        {
            InitializeComponent();
            SetPreferences();
            InitTable();
            BindToTable();
            this.imageList1.Images.Add((System.Drawing.Image)Properties.Resources.folder);
            this.imageList1.Images.Add((System.Drawing.Image)Properties.Resources.folder_sel);
            this.imageList1.Images.Add((System.Drawing.Image)Properties.Resources.items);
            this.rgTree.ImageList = this.imageList1;

            //this.rgGrid.ContextMenuStrip = new ContextMenuStrip();
            //this.rgGrid.ContextMenuStrip.Items.Add("添加主帐户项", null, new EventHandler(AddRouterItem_Click));
            //this.rgGrid.ContextMenuStrip.Items.Add("编辑主帐户项", null, new EventHandler(EditRouterItem_Click));

            //this.rgGrid.CellMouseClick += new DataGridViewCellMouseEventHandler(rgGrid_CellMouseClick);
            this.rgGrid.MouseClick += new MouseEventHandler(rgGrid_MouseClick);
            this.rgTree.Nodes.Add(CreateBaseItem("主帐户组"));
            this.rgTree.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.rgTree.ContextMenuStrip.Items.Add("添加主帐户组", null, new EventHandler(AddRouterGroup_Click));
            this.rgTree.ContextMenuStrip.Items.Add("编辑主帐户组", null, new EventHandler(EditRouterGroup_Click));


            this.Load += new EventHandler(fmRouterGroup_Load);
        }

        void rgGrid_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                //int rid = e.RowIndex;
                GenRouterItemContextMenu().Show(Control.MousePosition);
            }
        }


        //void rgGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
            
        //    if (e.Button == System.Windows.Forms.MouseButtons.Right)
        //    {
        //        int rid = e.RowIndex;
        //        GenRouterItemContextMenu(rid).Show(Control.MousePosition);
        //    }
        //}

        ContextMenuStrip GenRouterItemContextMenu()
        {
            ContextMenuStrip _menu1 = new ContextMenuStrip();
            _menu1.Items.Add("添加主帐户项", null, new EventHandler(AddRouterItem_Click));
            

            RouterItemSetting item = CurrentRouterItem;
            if (item != null)
            {
                _menu1.Items.Add("编辑主帐户项", null, new EventHandler(EditRouterItem_Click));
                _menu1.Items.Add(new System.Windows.Forms.ToolStripSeparator());

                int row = rgGrid.SelectedRows.Count > 0 ? rgGrid.SelectedRows[0].Index : -1;
                if (row >= 0)
                {
                    QSEnumConnectorStatus status = Util.ParseEnum<QSEnumConnectorStatus>(rgGrid[CONSTATUS, row].Value.ToString());
                    if (status == QSEnumConnectorStatus.Start)
                    {
                        _menu1.Items.Add("停止", null, new EventHandler(StopConnector));
                    }
                    else if (status == QSEnumConnectorStatus.Stop)
                    {
                        _menu1.Items.Add("启动", null, new EventHandler(StartConnector));
                    }
                }


                
            }
            return _menu1;
        }

        void StopConnector(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm(string.Format("确认停止主帐户:{0}", ID2ConnectorCfg(CurrentRouterItem.Connector_ID).Token)) == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqStopConnector(CurrentRouterItem.Connector_ID);
            }
        }

        void StartConnector(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm(string.Format("确认启动主帐户:{0}", ID2ConnectorCfg(CurrentRouterItem.Connector_ID).Token)) == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqStartConnector(CurrentRouterItem.Connector_ID);
            }

        }
        void AddRouterItem_Click(object sender, EventArgs e)
        {
            if (_current == null)
            {
                MoniterHelper.WindowMessage("请选择主账户组");
                return;
            }
            fmRouterItemEdit fm = new fmRouterItemEdit();
            fm.SetRouterGroup(_current);
            fm.ShowDialog();
            fm.Close();
        }

        void EditRouterItem_Click(object sender, EventArgs e)
        {
            if (_current == null)
            {
                MoniterHelper.WindowMessage("请选择主账户组");
                return;
            }
            RouterItemSetting _item = CurrentRouterItem;
            if (_item == null)
            {
                MoniterHelper.WindowMessage("请选择主帐户项");
                return;
            }

            ConnectorConfig _cfg = ID2ConnectorCfg(_item.Connector_ID);
            fmRouterItemEdit fm = new fmRouterItemEdit();

            fm.SetRouterItem(_item,_cfg);
            fm.SetRouterGroup(_current);
            fm.ShowDialog();
            fm.Close();
        }

        void AddRouterGroup_Click(object sender, EventArgs e)
        {
            fmRouterGroupEdit fm = new fmRouterGroupEdit();
            fm.ShowDialog();
            fm.Close();
        }

        void EditRouterGroup_Click(object sender, EventArgs e)
        {
            fmRouterGroupEdit fm = new fmRouterGroupEdit();
            fm.SetRouterGroup(_current);
            fm.ShowDialog();
            fm.Close();
        }


        void fmRouterGroup_Load(object sender, EventArgs e)
        {
            this.rgTree.NodeMouseClick += new TreeNodeMouseClickEventHandler(rgTree_NodeMouseClick);//树状节点点击
            CoreService.EventCore.RegIEventHandler(this);
            CoreService.TLClient.ReqContribRequest("ConnectorManager", "QryConnectorConfig", "");//查询所有通道设置

            
        }
        RouterGroupSetting _current = null;

        void rgTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                return;
            if (e.Node.Parent != null)
            {
                if (e.Node.Parent.Index == 0)//根节点下的子节点
                {
                    RouterGroupSetting t = e.Node.Tag as RouterGroupSetting;
                    if (t != null)
                    {

                        ClearItem();
                        _current = t;
                        CoreService.TLClient.ReqContribRequest("ConnectorManager", "QryRouterItem", t.ID.ToString());
                    }
                }
            }
        }

        void ClearItem()
        {
            rgGrid.DataSource = null;
            itemrowmap.Clear();
            itemmap.Clear();
            gt.Rows.Clear();
            BindToTable();
        }



        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback("ConnectorManager", "QryConnectorConfig", this.OnQryConnectorConfig);

            CoreService.EventContrib.RegisterCallback("ConnectorManager", "QryRouterGroup", this.OnQryRouterGroup);
            CoreService.EventContrib.RegisterNotifyCallback("ConnectorManager", "NotifyRouterGroup", this.OnNotifyRouterGroup);

            CoreService.EventContrib.RegisterCallback("ConnectorManager", "QryRouterItem", this.OnQryRouterItem);
            CoreService.EventContrib.RegisterNotifyCallback("ConnectorManager", "NotifyRouterItem", this.OnNotifyRouterItem);

            CoreService.EventContrib.RegisterCallback("ConnectorManager", "QryConnectorStatus", this.OnQryConnectorStatus);
            CoreService.EventContrib.RegisterNotifyCallback("ConnectorManager", "NotifyConnectorStatus", this.OnNotifyConnectorStatus);

        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("ConnectorManager", "QryConnectorConfig", this.OnQryConnectorConfig);
           
            CoreService.EventContrib.UnRegisterCallback("ConnectorManager", "QryRouterGroup", this.OnQryRouterGroup);
            CoreService.EventContrib.UnRegisterNotifyCallback("ConnectorManager", "NotifyRouterGroup", this.OnNotifyRouterGroup);
            
            CoreService.EventContrib.UnRegisterCallback("ConnectorManager", "QryRouterItem", this.OnQryRouterItem);
            CoreService.EventContrib.UnRegisterNotifyCallback("ConnectorManager", "NotifyRouterItem", this.OnNotifyRouterItem);

            CoreService.EventContrib.UnRegisterCallback("ConnectorManager", "QryConnectorStatus", this.OnQryConnectorStatus);
            CoreService.EventContrib.UnRegisterNotifyCallback("ConnectorManager", "NotifyConnectorStatus", this.OnNotifyConnectorStatus);

        }

        #region ConnectorStatus
        void OnQryConnectorStatus(string jsonstr, bool islast)
        {
            ConnectorStatus[] objs = CoreService.ParseJsonResponse<ConnectorStatus[]>(jsonstr);
            if (objs != null)
            {
                foreach (ConnectorStatus op in objs)
                {
                    InvokeGotConnectorStatus(op);
                }
                //_gotstatus = true;
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
                //_gotstatus = true;
            }
            else//如果没有配资服
            {

            }
        }

        void InvokeGotConnectorStatus(ConnectorStatus status)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ConnectorStatus>(InvokeGotConnectorStatus), new object[] { status });
            }
            else
            {
                
                int r = ConnectorID2Idx(status.ID);
                //MessageBox.Show("ConnectorID:" + status.ID.ToString() +" rowid:"+r.ToString());
                if (r == -1)
                {
                }
                else
                {
                    gt.Rows[r][CONSTATUS] = status.Status;
                    gt.Rows[r][CONIMG] = GetStatusImage(status.Status);
                }
            }
        }

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

        #endregion


        #region ConnectorConfig

        Dictionary<int, ConnectorConfig> configmap = new Dictionary<int, ConnectorConfig>();
        void OnQryConnectorConfig(string json,bool islast)
        {
            ConnectorConfig[] objs = CoreService.ParseJsonResponse<ConnectorConfig[]>(json);
            if (objs != null)
            {
                foreach (ConnectorConfig op in objs)
                {
                    configmap.Add(op.ID, op);
                }
                
            }
            if (islast)
            {
                CoreService.TLClient.ReqContribRequest("ConnectorManager", "QryRouterGroup", "");//查询路由组
            }
        }

        /// <summary>
        /// 通过Connector_ID获得对应的配置
        /// </summary>
        /// <param name="connector_id"></param>
        /// <returns></returns>
        ConnectorConfig ID2ConnectorCfg(int connector_id)
        {
            ConnectorConfig cfg = null;
            if (configmap.TryGetValue(connector_id, out cfg))
            {
                return cfg;
            }
            return null;
        }

        string GetConnectorToken(int connector_id)
        {
            ConnectorConfig cfg = ID2ConnectorCfg(connector_id);
            if (cfg == null)
            {
                return "不存在";
            }
            else
            {
                return cfg.Token;
            }
        }
        #endregion


        #region RouterGroup
        Dictionary<int, RouterGroupSetting> rgmap = new Dictionary<int, RouterGroupSetting>();
        void OnNotifyRouterGroup(string json)
        {
            RouterGroupSetting obj = CoreService.ParseJsonResponse<RouterGroupSetting>(json);
            if (obj != null)
            {
                RouterGroupSetting target = null;
                if (rgmap.TryGetValue(obj.ID, out target))
                {
                    target.Name = obj.Name;
                    target.Description = obj.Description;
                    rgTree.Refresh();
                }
                else
                {
                    rgmap.Add(obj.ID, obj);
                    InvokeGotRouterGroup(obj);
                }
            }
        }

        void OnQryRouterGroup(string json, bool islast)
        {
            RouterGroupSetting[] list = CoreService.ParseJsonResponse<RouterGroupSetting[]>(json);
            if (list != null)
            {
                foreach (RouterGroupSetting t in list)
                {
                    rgmap.Add(t.ID, t);
                    InvokeGotRouterGroup(t);
                }
            }
            if (islast)
            {
                rgTree.ExpandAll();
            }
        }

        void InvokeGotRouterGroup(RouterGroupSetting t)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<RouterGroupSetting>(InvokeGotRouterGroup), new object[] { t });
            }
            else
            {
                rgTree.Nodes[0].Nodes.Add(CreateRouterGroupItem(t));
            }
        }
        #endregion


        #region RouterItem
        void OnQryRouterItem(string json, bool islast)
        {
            RouterItemSetting[] list = CoreService.ParseJsonResponse<RouterItemSetting[]>(json);
            if (list != null)
            {
                foreach (RouterItemSetting t in list)
                {
                    InvokeGotRouterItem(t);
                }
            }
            if (islast)
            {
                //查询路由项完毕后 查询通道状态
                CoreService.TLClient.ReqQryConnectorStatus();

            }
        }

        void OnNotifyRouterItem(string json)
        {
            RouterItemSetting obj = CoreService.ParseJsonResponse<RouterItemSetting>(json);
            if (obj != null)
            {
                InvokeGotRouterItem(obj);
            }
        }

        //RouterItemID与行号和对象的map
        Dictionary<int, int> itemrowmap = new Dictionary<int, int>();
        Dictionary<int, RouterItemSetting> itemmap = new Dictionary<int, RouterItemSetting>();

        //通过通道ID获得对应的RouterItem的行号
        int ConnectorID2Idx(int connector_id)
        {
            int itemid = -1;
            foreach (var key in itemmap.Keys)
            {
                //MessageBox.Show("id:" + key.ToString() + " connid:" + itemmap[key].Connector_ID.ToString());
                if (itemmap[key].Connector_ID == connector_id)
                {
                    itemid = key;
                }
            }

            if (itemid == -1) return -1;
            return ItemIdx(itemid);
        }
        int ItemIdx(int id)
        {
            int rowid = -1;
            if (itemrowmap.TryGetValue(id, out rowid))
            {
                return rowid;
            }
            else
            {
                return -1;
            }
        }
        
        //得到当前选择的行号
        private int CurrentItemID
        {
            get
            {
                int row = rgGrid.SelectedRows.Count > 0 ? rgGrid.SelectedRows[0].Index : -1;
                if (row >= 0)
                {
                    return int.Parse(rgGrid[0, row].Value.ToString());
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 当前选中的路由项
        /// </summary>
        RouterItemSetting CurrentRouterItem
        {
            get
            {
                int id = CurrentItemID;
                RouterItemSetting item = null;
                if (itemmap.TryGetValue(id, out item))
                {
                    return item;
                }
                else
                {
                    return null;
                }
            }
        }




        void InvokeGotRouterItem(RouterItemSetting item)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<RouterItemSetting>(InvokeGotRouterItem), new object[] { item });
            }
            else
            {
                int r = ItemIdx(item.ID);
                if (r == -1)
                {
                    gt.Rows.Add(item.ID);
                    int i = gt.Rows.Count - 1;
                    gt.Rows[i][ID] = item.ID;
                    gt.Rows[i][CONNECTORID] = item.Connector_ID;
                    gt.Rows[i][CONNECTORTOKEN] = GetConnectorToken(item.Connector_ID);
                    gt.Rows[i][EQUITYLIMIT] = item.MarginLimit;
                    gt.Rows[i][PRIORITY] = item.priority;
                    gt.Rows[i][ACTIVE] = item.Active;

                    gt.Rows[i][CONSTATUS] = QSEnumConnectorStatus.Stop;
                    gt.Rows[i][CONIMG] = GetStatusImage(QSEnumConnectorStatus.Stop);

                    itemmap.Add(item.ID, item);
                    itemrowmap.Add(item.ID, i);

                }
                else
                {
                    int i = r;

                    gt.Rows[i][EQUITYLIMIT] = item.MarginLimit;
                    gt.Rows[i][PRIORITY] = item.priority;
                    gt.Rows[i][ACTIVE] = item.Active;

                    itemmap[item.ID] = item;
                }
            }
        }

        #endregion



        private ComponentFactory.Krypton.Toolkit.KryptonTreeNode CreateBaseItem(string lb)
        {
            ComponentFactory.Krypton.Toolkit.KryptonTreeNode item = new ComponentFactory.Krypton.Toolkit.KryptonTreeNode();
            item.Text = lb;
            item.ImageIndex = 2;
            item.SelectedImageIndex = item.ImageIndex;
            item.Tag = lb;
            return item;
        }

        private ComponentFactory.Krypton.Toolkit.KryptonTreeNode CreateRouterGroupItem(RouterGroupSetting t)
        {
            ComponentFactory.Krypton.Toolkit.KryptonTreeNode item = new ComponentFactory.Krypton.Toolkit.KryptonTreeNode();
            item.Text = t.Name;
            item.ImageIndex = 0;
            item.SelectedImageIndex = 1;
            item.Tag = t;
            return item;
        }

        #region 表格
        #region 显示字段

        const string ID = "ID";
        const string CONNECTORID = "ConnectorID";
        const string CONNECTORTOKEN = "主帐户编号";
        const string EQUITYLIMIT = "资金限额";

        const string PRIORITY = "优先级";
        const string ACTIVE = "接受";
        const string CONSTATUS = "Status";
        const string CONIMG = "状态";

        #endregion

        DataTable gt = new DataTable();
        BindingSource datasource = new BindingSource();

        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = rgGrid;

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
            gt.Columns.Add(CONNECTORID);//
            gt.Columns.Add(CONNECTORTOKEN);//
            gt.Columns.Add(EQUITYLIMIT);//
            gt.Columns.Add(PRIORITY);//
            gt.Columns.Add(ACTIVE);//
            gt.Columns.Add(CONSTATUS);//
            gt.Columns.Add(CONIMG, typeof(Image));//
        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = rgGrid;
            datasource.DataSource = gt;
            grid.DataSource = datasource;

            grid.Columns[CONNECTORID].Visible = false;
            grid.Columns[CONSTATUS].Visible = false;

            grid.Columns[ID].Width = 40;
            grid.Columns[CONNECTORTOKEN].Width = 120;
            grid.Columns[EQUITYLIMIT].Width = 60;
            grid.Columns[CONIMG].Width = 40;

        }





        #endregion
    }

    

}
