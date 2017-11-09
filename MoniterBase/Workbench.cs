using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.Core;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;
using TradingLib.MoniterControl;


namespace TradingLib.MoniterBase
{
    public partial class Workbench : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        static Workbench instance;

        public static Workbench Instance
        {
            get
            {
                return instance;
            }
        }

        public static void InitializeWorkbench()
        {
            instance = new Workbench();
        }

        public void OnInit()
        {

            if (!CoreService.SiteInfo.Domain.Super)
            {
                //独立部署
                if (CoreService.SiteInfo.Domain.Dedicated)
                {
                    this.Text = string.Format("{0}-{1} {2}-{3}", GetProductName(CoreService.SiteInfo.ProductType), CoreService.SiteInfo.Domain.Name,CoreService.SiteInfo.Manager.Login, Util.GetEnumDescription(CoreService.SiteInfo.Manager.Type));
                }
                else
                {
                    this.Text = string.Format("{0}(云平台)-{1} 【{2}】 {3}-{4}", GetProductName(CoreService.SiteInfo.ProductType), CoreService.SiteInfo.Domain.Name, CoreService.SiteInfo.Domain.IsProduction ? "运营" : "测试", CoreService.SiteInfo.Manager.Login, Util.GetEnumDescription(CoreService.SiteInfo.Manager.Type));
                }
            }
            else
            {
                this.Text = string.Format("{0}-{1}", GetProductName(CoreService.SiteInfo.ProductType), CoreService.SiteInfo.Domain.Name);
            }

            //设置部署编号
            lbDeployID.Text = string.Format("{0}-{1}", CoreService.TLClient.Negotiation.DeployID,CoreService.TLClient.Negotiation.Version);

            if (!CoreService.SiteInfo.Domain.Super)
            {
                DateTime expire = Util.ToDateTime(CoreService.SiteInfo.Domain.DateExpired, 235959);
                DateTime now = DateTime.Now;
                double daysremain = expire.Subtract(now).TotalDays;
                if (daysremain < 7 && (!Global.HideExpire))
                {
                    lbExpireMessage.Text = string.Format("柜台还有{0:F}天过期,请及时续费", daysremain);
                    lbExpireMessage.ForeColor = UIConstant.ShortSideColor;
                }
                else
                {
                    lbExpireMessage.Visible = false;
                }
            }
            else
            {
                lbExpireMessage.Visible = false;
            }

            if (CoreService.TLClient.Connected)
            {
                imgLink.Image = (Image)Properties.Resources.online;
            }


            //订阅出入金通知
            CoreService.EventCore.RegisterNotifyCallback(Modules.APIService, Method_API.NOTIFY_CASH_OPERATION, this.OnNotifyCashOperation);

            //超管查看结算状态
            if (CoreService.SiteInfo.Domain.Super)
            {
                CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_SYSTEM_STATUS, this.OnQrySystemStatus);
                CoreService.TLClient.ReqQrySystemStatus();
            }
        }

        void OnNotifyCashOperation(string json)
        {
            CashOperation op = json.DeserializeObject<CashOperation>();
            if (op != null)
            {
                if (op.OperationType == QSEnumCashOperation.Deposit && op.Status == QSEnumCashInOutStatus.CONFIRMED)
                {
                    soundNotify.Play();
                }
                if (op.OperationType == QSEnumCashOperation.WithDraw && op.Status == QSEnumCashInOutStatus.PENDING)
                {
                    soundNotify.Play();
                }
                
            }
        }

        void OnQrySystemStatus(string json, bool islast)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<string, bool>(OnQrySystemStatus), new object[] { json, islast });
            }
            else
            {
                SystemStatus status = CoreService.ParseJsonResponse<SystemStatus>(json);
                if (status != null)
                {
                    //GotSystemStatus(status);
                    lbSettleStatus.Text = string.Format("{0}- O:{1} T:{2} E:{3}", status.LastSettleday, status.UnsettledAcctOrderNumOfPreSettleday, status.UnsettledAcctTradeNumOfPreSettleday, status.UnsettledExchangeSettlementNumOfPreSettleday);
                }
            }
        }


        public void OnDisposed()
        { 
        
        }
        MenuStrip menu;
        ToolStrip toolbar;
        StatusStrip status;
        Panel contentPanel;


        Dictionary<string, Control> controlmap = new Dictionary<string, Control>();
        Dictionary<string, EnumControlLocation> locationmap = new Dictionary<string, EnumControlLocation>();
        SoundNotify soundNotify = new SoundNotify();
        private Workbench()
        {
            InitializeComponent();
            
            // restore form location from last session
            //FormLocationHelper.Apply(this, "StartupFormPosition", true);

            

            

            // Use the Idle event to update the status of menu and toolbar items.
            Application.Idle += OnApplicationIdle;
            this.Load += new EventHandler(Workbench_Load);
            this.FormClosing += new FormClosingEventHandler(Workbench_FormClosing);
        }

        void Workbench_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MoniterHelper.WindowConfirm("确认退出管理端?") == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }



        /// <summary>
        /// 窗体第一次显示时加载控件初始化操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Workbench_Load(object sender, EventArgs e)
        {

            InitControl();

            InitSplitContainer();

            //初始化界面控件
            this.WindowState = FormWindowState.Maximized;
            foreach (ICommand command in AddInTree.BuildItems("/Workspace/ControlInit", this, false))
            {
                command.Run();
            }

            

            //启动弹窗线程
            InitPopBW();

            CoreService.EventCore.OnConnectedEvent += new VoidDelegate(EventCore_OnConnectedEvent);
            CoreService.EventCore.OnDisconnectedEvent += new VoidDelegate(EventCore_OnDisconnectedEvent);
            //需要在load后再注册核心调用函数 否则会出现 对象未初始化完毕
            CoreService.EventCore.RegIEventHandler(this);

            //管理端启动第一次加载时 执行查询行情快照
            CoreService.TLClient.ReqQryTickSnapshot();

            //使用全局图标
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        void EventCore_OnDisconnectedEvent()
        {
            if (InvokeRequired)
            {
                Invoke(new VoidDelegate(EventCore_OnDisconnectedEvent), new object[] { });
            }
            else
            {
                imgLink.Image = (Image)Properties.Resources.offline;
                contentPanel.Visible = false;
                lbSpring.Text = "网络故障,管理段掉线,请退出软件后重新登入";
            }
            
        }

        void EventCore_OnConnectedEvent()
        {
            if (InvokeRequired)
            {
                Invoke(new VoidDelegate(EventCore_OnDisconnectedEvent), new object[] { });
            }
            else
            {
                imgLink.Image = (Image)Properties.Resources.online;
                lbSpring.Text = "";
            }
        }

        
        string GetProductName(QSEnumProductType type)
        {
            return string.Format("{0}", Global.IsOEM ? Global.Brand : "柜台系统");
        }


        ComponentFactory.Krypton.Toolkit.KryptonSplitContainer mainContainer = null;
        ComponentFactory.Krypton.Toolkit.KryptonSplitContainer bottomContainer = null;


        #region 初始化
        /// <summary>
        /// 初始化split容器控件
        /// 在contentPanel中添加一个嵌套的SplitContainer
        /// </summary>
        void InitSplitContainer()
        {
            this.mainContainer = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.mainContainer.Cursor = System.Windows.Forms.Cursors.Default;
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(0, 0);
            this.mainContainer.Name = "MainSplitContainer";
            this.mainContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.mainContainer.Size = new System.Drawing.Size(689, 400);
            this.mainContainer.SplitterDistance = 260;
            this.mainContainer.TabIndex = 0;

            contentPanel.Controls.Add(this.mainContainer);

            this.bottomContainer = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.bottomContainer.Cursor = System.Windows.Forms.Cursors.Default;
            this.bottomContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomContainer.Location = new System.Drawing.Point(0, 0);
            this.bottomContainer.Name = "BottomSplitContainer";
            this.bottomContainer.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.mainContainer.Panel2.Controls.Add(this.bottomContainer);
            //this.mainContainer.Panel2.Controls.Add()

            this.bottomContainer.Panel2MinSize = 270;
            this.bottomContainer.SplitterDistance = this.Width - 430;
            this.mainContainer.SplitterDistance = this.Height-380;
            //SplitContainer s1 = new SplitContainer();
            //s1.SplitterDistance

            
        }

        #region 控件
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lbDeployID;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel lbSettleStatus;
        private System.Windows.Forms.ToolStripStatusLabel lbExpireMessage;
        private System.Windows.Forms.ToolStripStatusLabel lbSpring;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel imgLink;
        private System.Windows.Forms.Panel toolBarFilterPanel;
        private System.Windows.Forms.ToolStripControlHost toolBarFilterHost;
        #endregion

        /// <summary>
        /// 初始化菜单 工具栏 状态栏
        /// </summary>
        void InitControl()
        {
            LoggingService.Info("Run Control Init");

            contentPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            contentPanel.Dock = DockStyle.Fill;
            this.Controls.Add(contentPanel);

            //窗体第一次显示时才进行菜单和工具栏的加载，提前加载由于没有登入无法获得权限等信息
            menu = new MenuStrip();
            MenuService.AddItemsToMenu(menu.Items, this, "/Workbench/MainMenu");
            


            toolbar = ToolbarService.CreateToolStrip(this, "/Workbench/Toolbar");


            toolBarFilterPanel = new Panel();
            toolBarFilterPanel.BackColor = Color.Transparent;
            toolBarFilterHost = new ToolStripControlHost(toolBarFilterPanel);
            toolBarFilterPanel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            toolBarFilterPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            toolBarFilterHost.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            toolBarFilterHost.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);

            toolbar.Items.Add(toolBarFilterHost);
            //toolbar.Height = 26;

            //ctAccountFilter filter = new ctAccountFilter();
            //toolBarFilterPanel.Size = new Size(filter.Size.Width, filter.Size.Height);
            //toolBarFilterPanel.Controls.Add(filter);
            //host.Size = new Size(filter.Size.Width, filter.Size.Height);


            this.Controls.Add(toolbar);
            this.Controls.Add(menu);

            status = new StatusStrip();
            status.RenderMode = ToolStripRenderMode.ManagerRenderMode;

            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbDeployID = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbSettleStatus = new System.Windows.Forms.ToolStripStatusLabel();

            this.lbExpireMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbSpring = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.imgLink = new System.Windows.Forms.ToolStripStatusLabel();
            

            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(59, 17);
            this.toolStripStatusLabel1.Text = "部署编号:";
            // 
            // lbDeployID
            // 
            this.lbDeployID.Name = "lbDeployID";
            this.lbDeployID.Size = new System.Drawing.Size(18, 17);
            this.lbDeployID.Text = "--";

            

            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(18, 17);
            this.toolStripStatusLabel2.Text = "--";
            this.toolStripStatusLabel2.Visible = false;

            // 
            // lbSettleStatus
            // 
            this.lbSettleStatus.Name = "lbSettleStatus";
            this.lbSettleStatus.Size = new System.Drawing.Size(18, 17);
            this.lbSettleStatus.Text = "";
            // 
            // lbExpireMessage
            // 
            this.lbExpireMessage.Name = "lbExpireMessage";
            this.lbExpireMessage.Size = new System.Drawing.Size(162, 17);
            this.lbExpireMessage.Text = "柜台还有7天到期,请及时续费";

            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(18, 17);
            this.toolStripStatusLabel3.Text = "连接:";
            //this.toolStripStatusLabel2.Visible = false;


            // 
            // imgLink
            // 
            this.imgLink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.imgLink.Image = global::TradingLib.MoniterBase.Properties.Resources.offline;
            this.imgLink.Name = "imgLink";
            this.imgLink.Size = new System.Drawing.Size(16, 17);
            this.imgLink.Text = "--";
            // 
            // lbSpring
            // 
            this.lbSpring.Name = "lbSpring";
            this.lbSpring.Size = new System.Drawing.Size(418, 17);
            this.lbSpring.Spring = true;
            this.lbSpring.Text = "";
            //this.lbSpring.Visible = false;




            // 
            // statusStrip1
            // 
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lbDeployID,
            this.toolStripStatusLabel2,
            this.lbSettleStatus,
            this.lbExpireMessage,
            this.lbSpring,
            this.toolStripStatusLabel3,
            this.imgLink});
            this.status.Location = new System.Drawing.Point(0, 294);
            this.status.Name = "statusStrip1";
            this.status.Size = new System.Drawing.Size(737, 22);
            this.status.TabIndex = 0;
            this.status.Text = "statusStrip1";

            this.Controls.Add(status);
        }

        #endregion

        //public void ShowControl(string mcName)
        //{
        //    if (controlmap.Keys.Contains(mcName.ToUpper()))
        //    {
        //        Control ctl = controlmap[mcName.ToUpper()];
        //        ctl.Visible = true;
        //    }
        //}

        //public void HideControl(string mcName)
        //{
        //    if (controlmap.Keys.Contains(mcName.ToUpper()))
        //    {
        //        Control ctl = controlmap[mcName.ToUpper()];
        //        ctl.Visible = false;
        //    }
        //}

        /// <summary>
        /// 底部视区关闭
        /// </summary>
        public void CollapseBottom()
        {
            this.mainContainer.Panel2Collapsed = true;
        }

        /// <summary>
        /// 底部视区打开
        /// </summary>
        public void ExpandBottom()
        { 
            this.mainContainer.Panel2Collapsed = false;
        }

        /// <summary>
        /// 恢复视区到默认尺寸
        /// </summary>
        public void ResetView()
        {
            //this.mainContainer.Panel2Collapsed = false;
            //this.mainContainer.Panel2.Height = (int)(this.Size.Height / 4);
            //this.bottomContainer.Panel2.Width = 250;
            this.bottomContainer.SplitterDistance = this.Width -  580;
            this.mainContainer.SplitterDistance = this.Height - 480;
            this.ExpandBottom();

            //显示默认控件
            //ShowControl("AccountMoniter", EnumControlLocation.TopPanel);
        }


        string oldpanelkey = string.Empty;
        /// <summary>
        /// 将某个控件显示到对应的视区
        /// </summary>
        /// <param name="mcName"></param>
        /// <param name="location"></param>
        public void ShowControl(string mcName, EnumControlLocation location)
        {
            string key = mcName.ToUpper();
            if (!controlmap.Keys.Contains(key))
            {
                MoniterHelper.WindowMessage(string.Format("控件{0}不存在", mcName));
            }
            Control ctl = controlmap[key];
            var panel = this.GetPanel(location);
            panel.Controls.Clear();
            panel.Controls.Add(ctl);

            if (location == EnumControlLocation.TopPanel)
            {
                if (oldpanelkey != key)
                {
                    toolBarFilterPanel.Controls.Clear();//清空原有控件
                    IMoniterControl mc = ctl as IMoniterControl;
                    if (mc != null && mc.FilterToolBar != null)
                    {
                        ShowToolBar(mc.FilterToolBar);
                    }
                }
                oldpanelkey = key;
            }
        }

        void ShowToolBar(Control ctl)
        {
            //ctAccountFilter filter = new ctAccountFilter();

            toolBarFilterHost.AutoSize = false;
            toolBarFilterPanel.Size = new Size(ctl.Size.Width, ctl.Size.Height);
            toolBarFilterHost.Size = new Size(ctl.Size.Width, ctl.Size.Height);
            toolBarFilterPanel.Controls.Add(ctl);
            //MessageBox.Show(string.Format("{0} - {1} / {2}-{3}", toolBarFilterHost.Size.Height, toolBarFilterHost.Size.Width, ctl.Size.Height, ctl.Size.Width));
        }

        /// <summary>
        /// 向视区添加控件
        /// </summary>
        /// <param name="ctl"></param>
        public void AppendControl(Control ctl)
        {
            IMoniterControl mc = ctl as IMoniterControl;
            if (mc == null)
            {
                MoniterHelper.WindowMessage("只允许在工作窗区域显示实现IMoniterControl的控件");
                return;
            }

            MoniterControlAttr attr = MoniterPlugin.GetMoniterControlAttr(ctl.GetType());
            if (attr == null)
            {
                MoniterHelper.WindowMessage("窗口显示组件需用用属性MoniterControlAttr进行标注");
                return;
            }
            string key = attr.Name.ToUpper();
            if (controlmap.Keys.Contains(key))
            {
                MoniterHelper.WindowMessage("已经存在该控件,请勿重复添加");
                return;
            }
            controlmap[key] = ctl;
        }

        ComponentFactory.Krypton.Toolkit.KryptonSplitterPanel GetPanel(EnumControlLocation location)
        {
            switch (location)
            { 
                case EnumControlLocation.TopPanel:
                    return this.mainContainer.Panel1;
                case EnumControlLocation.BottomLeft:
                    return this.bottomContainer.Panel1;
                case EnumControlLocation.BottomRight:
                    return this.bottomContainer.Panel2;
                default:
                    return this.mainContainer.Panel1;
            }
        }


        ///// <summary>
        ///// 在第一层split的上部主窗体显示控件
        ///// </summary>
        ///// <param name="ctl"></param>
        //void ShowInTop(Control ctl)
        //{
        //    this.mainContainer.Panel1.Controls.Add(ctl);
        //}

        //void ShowInBottomLeft(Control ctl)
        //{
        //    this.bottomContainer.Panel1.Controls.Add(ctl);
        //}

        //void ShowInBottomRight(Control ctl)
        //{
        //    this.bottomContainer.Panel2.Controls.Add(ctl);
        //}

        void EventContrib_DemoEvent(string obj)
        {
            this.Text = obj;
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        Application.Idle -= OnApplicationIdle;
        //    }
        //    base.Dispose(disposing);
        //}

        void OnApplicationIdle(object sender, EventArgs e)
        {
            // Use the Idle event to update the status of menu and toolbar.
            // Depending on your application and the number of menu items with complex conditions,
            // you might want to update the status less frequently.
            UpdateMenuItemStatus();
        }

        /// <summary>Update Enabled/Visible state of items in the main menu based on conditions</summary>
        void UpdateMenuItemStatus()
        {
            foreach (ToolStripItem item in menu.Items)
            {
                if (item is IStatusUpdate)
                    (item as IStatusUpdate).UpdateStatus();
            }
        }

        /// <summary>The active view content</summary>
        //IViewContent viewContent;

        //public IViewContent ActiveViewContent
        //{
        //    get
        //    {
        //        return viewContent;
        //    }
        //}

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            //System.Diagnostics.Process.GetCurrentProcess().Kill();

            //CoreService.Destory();

            //if (!e.Cancel)
            //{
            //    e.Cancel = !CloseCurrentContent();
            //}
        }

        //public bool CloseCurrentContent()
        //{
        //    IViewContent content = viewContent;
        //    if (content != null)
        //    {
        //        if (!content.Close())
        //        {
        //            return false;
        //        }
        //        viewContent = null;
        //        content.TitleChanged -= OnTitleChanged;
        //        OnTitleChanged(content, EventArgs.Empty);
        //        foreach (Control ctl in contentPanel.Controls)
        //        {
        //            ctl.Dispose();
        //        }
        //        contentPanel.Controls.Clear();
        //    }
        //    return true;
        //}

        //public void ShowContent(IViewContent content)
        //{
        //    if (viewContent != null)
        //        throw new InvalidOperationException("There is still another content opened.");
        //    viewContent = content;
        //    Control ctl = content.Control;
        //    ctl.Dock = DockStyle.Fill;
        //    contentPanel.Controls.Add(ctl);
        //    ctl.Focus();

        //    content.TitleChanged += OnTitleChanged;
        //    OnTitleChanged(content, EventArgs.Empty);
        //}

        //void OnTitleChanged(object sender, EventArgs e)
        //{
        //    if (viewContent != null)
        //    {
        //        this.Text = viewContent.Title + " - ICSharpCode.Core.Demo";
        //    }
        //    else
        //    {
        //        this.Text = "ICSharpCode.Core.Demo";
        //    }
        //}


        #region pop message


        fmPopMessage popwindow = new fmPopMessage();
        System.ComponentModel.BackgroundWorker bg;

        RingBuffer<RspInfo> infobuffer = new RingBuffer<RspInfo>(1000);


        /// <summary>
        /// 将需要弹出的消息放入缓存
        /// </summary>
        /// <param name="info"></param>
        void OnRspInfo(TradingLib.API.RspInfo info)
        {
            //将RspInfo写入缓存 等待后台线程进行处理
            infobuffer.Write(info);
        }

        void InitPopBW()
        {
            bg = new BackgroundWorker();
            bg.WorkerReportsProgress = true;
            bg.DoWork += new DoWorkEventHandler(bg_DoWork);
            bg.ProgressChanged += new ProgressChangedEventHandler(bg_ProgressChanged);
            bg.RunWorkerAsync();

            CoreService.EventCore.OnRspInfoEvent += new Action<RspInfo>(OnRspInfo);
        }

        /// <summary>
        /// 当后台线程有触发时 调用显示窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bg_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            RspInfo info = e.UserState as RspInfo;
            System.Drawing.Point p = PointToScreen(status.Location);
            p = new System.Drawing.Point(p.X, p.Y - popwindow.Height + status.Height);

            popwindow.Location = p;
            popwindow.PopMessage(info);
        }

        /// <summary>
        /// 后台工作流程 当缓存中有数据是通过ReportProgress进行触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                //检查变量 然后对外触发 
                while (infobuffer.hasItems)
                {
                    RspInfo info = infobuffer.Read();
                    bg.ReportProgress(1, info);
                    Util.sleep(1000);
                }
                Util.sleep(50);
            }
        }

        #endregion
    }
}
