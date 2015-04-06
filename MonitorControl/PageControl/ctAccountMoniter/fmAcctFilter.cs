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
    public partial class fmAcctFilter : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        static fmAcctFilter instance;

        public static fmAcctFilter Instance
        {
            get
            {
                return instance;
            }
        }

        static fmAcctFilter()
        {
            instance = new fmAcctFilter();

            int ScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int ScreenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            //计算窗体显示的坐标值，可以根据需要微调几个像素
            int x = ScreenWidth - instance.Width - 5;
            int y = ScreenHeight - instance.Height - 300;

            instance.Location = new Point(x, y);
        }


        //public event VoidDelegate FilterArgsChangedEvent;

        private  fmAcctFilter()
        {
            InitializeComponent();
            accexecute.Items.Add("<所有>");
            accexecute.Items.Add("允许");
            accexecute.Items.Add("冻结");
            accexecute.SelectedIndex = 0;

            macctconnected.Items.Add("<所有>");
            macctconnected.Items.Add("<连接>");
            macctconnected.Items.Add("<断开>");
            macctconnected.SelectedIndex = 0;

            macctbinded.Items.Add("<所有>");
            macctbinded.Items.Add("<分配>");
            macctbinded.Items.Add("<未分配>");
            macctbinded.SelectedIndex = 0;

            this.Load += new EventHandler(fmAcctFilter_Load);
        }

        /// <summary>
        /// 所有ct组件初始化完毕后才会调用OnInit 这样才可以正常的将参数显示到界面 如果组件没有OnInit成功，则对应的SelectedValue无法正常的显示到界面
        /// </summary>
        public void OnInit()
        {
            if (_args.AcctExecuteEnable)
            {
                accexecute.SelectedIndex = _args.AcctExecute ? 1 : 2;
            }

            if (_args.AcctTypeEnable)
            {
                ctAccountType1.AccountType = _args.AcctType;
            }

            if (_args.AgentEnable)
            {
                ctAgentList1.CurrentAgentFK = _args.AgentID;
            }

            if (_args.OrderRouterTypeEnable)
            {
                ctRouterType1.RouterType = _args.OrderRouterType;
            }

            if (_args.RouterGroupEnable)
            {
                ctRouterGroupList1.RouterGroupID = _args.RouterGroupID;
            }

            if (!CoreService.SiteInfo.Domain.Super)
            {
                ctRouterType1.Visible = CoreService.SiteInfo.Manager.IsRoot();
                ctAccountType1.Visible = CoreService.SiteInfo.Manager.IsRoot();
                //有实盘交易权限的才需要查看路由组
                ctRouterGroupList1.Visible = CoreService.SiteInfo.Domain.Router_Live;


                if (!CoreService.SiteInfo.Manager.IsRoot())
                {
                    this.Height = this.Height - (ctRouterType1.Height + 5) * (2 + (!CoreService.SiteInfo.Domain.Router_Live ? 1 : 0));
                }
            }
        }

        public void OnDisposed()
        { 
            
        }

        FilterArgs _args = ControlService.FilterArgs;
        /// <summary>
        /// 设定过滤参数
        /// </summary>
        /// <param name="args"></param>
        //internal void SetFilterArgs(ref FilterArgs args)
        //{
        //    _args = args;
        //}

        void fmAcctFilter_Load(object sender, EventArgs e)
        {
            //this.Deactivate += new EventHandler(fmAcctFilter_Deactivate);

            accexecute.SelectedIndexChanged += new EventHandler(accexecute_SelectedIndexChanged);
            ctAccountType1.AccountTypeSelectedChangedEvent += new VoidDelegate(ctAccountType1_AccountTypeSelectedChangedEvent);
            ctAgentList1.AgentSelectedChangedEvent += new VoidDelegate(ctAgentList1_AgentSelectedChangedEvent);
            ctRouterType1.RouterTypeSelectedChangedEvent += new VoidDelegate(ctRouterType1_RouterTypeSelectedChangedEvent);
            ctRouterGroupList1.RouterGroupSelectedChangedEvent += new VoidDelegate(ctRouterGroupList1_RouterGroupSelectedChangedEvent);
            macctbinded.SelectedIndexChanged += new EventHandler(macctbinded_SelectedIndexChanged);
            macctconnected.SelectedIndexChanged += new EventHandler(macctconnected_SelectedIndexChanged);
            account.TextChanged += new EventHandler(account_TextChanged);
            
            CoreService.EventCore.RegIEventHandler(this);

            this.kryptonPanel1.MouseDown += new MouseEventHandler(kryptonPanel1_MouseDown);
            this.kryptonPanel1.MouseUp += new MouseEventHandler(kryptonPanel1_MouseUp);
            this.kryptonPanel1.MouseMove += new MouseEventHandler(kryptonPanel1_MouseMove);

            closeimg.Click += new EventHandler(closeimg_Click);

            this.FormClosing += new FormClosingEventHandler(fmAcctFilter_FormClosing);
        }



        void fmAcctFilter_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        void closeimg_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        #region 鼠标移动窗体
        private bool m_isMouseDown = false;
        private Point m_mousePos = new Point();

        void kryptonPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (m_isMouseDown)
            {
                Point tempPos = Cursor.Position;
                this.Location = new Point(Location.X + (tempPos.X - m_mousePos.X), Location.Y + (tempPos.Y - m_mousePos.Y));
                m_mousePos = Cursor.Position;
            }
        }

        void kryptonPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
            m_isMouseDown = false;
        }

        void kryptonPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            m_mousePos = Cursor.Position;
            m_isMouseDown = true;
        }
        #endregion

        void account_TextChanged(object sender, EventArgs e)
        {
            _args.AcctFilter = account.Text;
            FilterAccount();
        }

        void ctRouterGroupList1_RouterGroupSelectedChangedEvent()
        {
            if (ctRouterGroupList1.SelectedIndex == 0)
            {
                _args.RouterGroupEnable = false;
            }
            else
            {
                _args.RouterGroupEnable = true;
                _args.RouterGroupID = ctRouterGroupList1.RouterGroupID;
            }
            FilterAccount();
        }

        void macctconnected_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (macctconnected.SelectedIndex == 0)
            {
                _args.MAcctConnectedEnable = false;
            }
            else
            {
                _args.MAcctConnectedEnable = true;
                _args.MAcctConnected = macctconnected.SelectedIndex == 1 ? true : false;
            }
            FilterAccount();
        }



        void macctbinded_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (macctbinded.SelectedIndex == 0)
            {
                _args.MAcctBindedEnable = false;
            }
            else
            {
                _args.MAcctBindedEnable = true;
                _args.MAcctBinded = macctbinded.SelectedIndex == 1 ? true : false;
            }
            FilterAccount();
        }


        void ctRouterType1_RouterTypeSelectedChangedEvent()
        {
            if (ctRouterType1.SelectedIndex == 0)
            {
                _args.OrderRouterTypeEnable = false;
            }
            else
            {
                _args.OrderRouterTypeEnable = true;
                _args.OrderRouterType = ctRouterType1.RouterType;
            }
            FilterAccount();
        }

        void ctAgentList1_AgentSelectedChangedEvent()
        {
            if (ctAgentList1.SelectedIndex == 0)
            {
                _args.AgentEnable = false;
            }
            else
            {
                _args.AgentEnable = true;
                _args.AgentID = ctAgentList1.CurrentAgentFK;
            }
            FilterAccount();
        }

        void ctAccountType1_AccountTypeSelectedChangedEvent()
        {
            if (ctAccountType1.SelectedIndex == 0)
            {
                _args.AcctTypeEnable = false;
            }
            else
            {
                _args.AcctTypeEnable = true;
                _args.AcctType = ctAccountType1.AccountType;
            }
            FilterAccount();
        }

        void accexecute_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (accexecute.SelectedIndex == 0)
            {
                _args.AcctExecuteEnable = false;
            }
            else
            {
                _args.AcctExecuteEnable = true;
                _args.AcctExecute = accexecute.SelectedIndex == 1 ? true : false;
            }
            FilterAccount();
        }

        void fmAcctFilter_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 过滤帐户
        /// </summary>
        void FilterAccount()
        {
            ControlService.FireFilterArgsChangeEvent();
            //if (FilterArgsChangedEvent!=null)
            //    FilterArgsChangedEvent();
        }
    }
}
