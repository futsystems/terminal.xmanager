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
    public partial class Workbench : ComponentFactory.Krypton.Toolkit.KryptonForm
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

        MenuStrip menu;
        ToolStrip toolbar;
        StatusStrip status;
        Panel contentPanel;


        Dictionary<string, Control> controlmap = new Dictionary<string, Control>();
        Dictionary<string, EnumControlLocation> locationmap = new Dictionary<string, EnumControlLocation>();

        private Workbench()
        {
            InitializeComponent();

            // restore form location from last session
            FormLocationHelper.Apply(this, "StartupFormPosition", true);

            contentPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            contentPanel.Dock = DockStyle.Fill;
            this.Controls.Add(contentPanel);

            menu = new MenuStrip();
            MenuService.AddItemsToMenu(menu.Items, this, "/Workbench/MainMenu");

            toolbar = ToolbarService.CreateToolStrip(this, "/Workbench/Toolbar");

            status = new StatusStrip();
            status.RenderMode = ToolStripRenderMode.ManagerRenderMode;

            this.Controls.Add(toolbar);
            this.Controls.Add(menu);
            this.Controls.Add(status);

            InitSplitContainer();

            
            // Use the Idle event to update the status of menu and toolbar items.
            Application.Idle += OnApplicationIdle;
            this.Load += new EventHandler(Workbench_Load);
        }

        /// <summary>
        /// 窗体第一次显示时加载控件初始化操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Workbench_Load(object sender, EventArgs e)
        {
            LoggingService.Info("Run Control Init");
            foreach (ICommand command in AddInTree.BuildItems("/Workspace/ControlInit", this, false))
            {
                command.Run();
            }
        }


        ComponentFactory.Krypton.Toolkit.KryptonSplitContainer mainContainer = null;
        ComponentFactory.Krypton.Toolkit.KryptonSplitContainer bottomContainer = null;
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
        }

        public void ShowControl(string mcName)
        {
            if (controlmap.Keys.Contains(mcName.ToUpper()))
            {
                Control ctl = controlmap[mcName.ToUpper()];
                ctl.Visible = true;
            }
        }

        public void HideControl(string mcName)
        {
            if (controlmap.Keys.Contains(mcName.ToUpper()))
            {
                Control ctl = controlmap[mcName.ToUpper()];
                ctl.Visible = false;
            }
        }

        /// <summary>
        /// 将某个控件显示到对应的位置
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
        }

        /// <summary>
        /// 向工作区域添加控件
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
        ///// <summary>
        ///// 在某个工作区域显示某个控件
        ///// </summary>
        ///// <param name="ctl"></param>
        ///// <param name="location"></param>
        //public void ShowControl(Control ctl, EnumControlLocation location)
        //{
        //    IMoniterControl mc = ctl as IMoniterControl;
        //    if (mc == null)
        //    {
        //        MoniterHelper.WindowMessage("只允许在工作窗区域显示实现IMoniterControl的控件");
        //    }

        //    MoniterControlAttr attr = MoniterPlugin.GetMoniterControlAttr(ctl.GetType());
        //    if (attr == null)
        //    {
        //        MoniterHelper.WindowMessage("窗口显示组件需用用属性MoniterControlAttr进行标注");
        //    }


        //    var panel = this.GetPanel(location);
        //    panel.Controls.Clear();
        //    panel.Controls.Add(ctl);

        //    string key = attr.Name.ToUpper();

        //    locationmap[key] = location;
        //    if (controlmap.Keys.Contains(key))
        //    {
                
        //    }
        //    controlmap[key] = ctl;
        //}


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


        /// <summary>
        /// 在第一层split的上部主窗体显示控件
        /// </summary>
        /// <param name="ctl"></param>
        void ShowInTop(Control ctl)
        {
            this.mainContainer.Panel1.Controls.Add(ctl);
        }

        void ShowInBottomLeft(Control ctl)
        {
            this.bottomContainer.Panel1.Controls.Add(ctl);
        }

        void ShowInBottomRight(Control ctl)
        {
            this.bottomContainer.Panel2.Controls.Add(ctl);
        }

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

    }
}
