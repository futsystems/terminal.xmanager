﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;
using ICSharpCode.Core;

using TradingLib.MoniterCore;

using Common.Logging;

namespace TradingLib.MoniterBase
{
    public partial class LoginForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {

        ILog logger = LogManager.GetLogger("LoginForm");

        Starter mStart;
        void layout1()
        {
            ckremberuser.Location = new Point(286, 50);
            ckremberpass.Location = new Point(286, 82);
            btnLogin.Location = new Point(134, 107);
            btnExit.Location = new Point(221, 107);
            btnLogin.Size = new Size(60, 31);

        }
        void layout2()
        {
            ckremberuser.Location = new Point(129, 107);
            ckremberpass.Location = new Point(208, 107);
            btnLogin.Location = new Point(299, 46);
            btnExit.Location = new Point(299, 96);
            btnLogin.Size = new Size(60, 45);

        }
        public LoginForm(Starter start)
        {
            //允许线程间调用控件属性 否则无法本地调试
            CheckForIllegalCrossThreadCalls = false;
            logger.Debug("loginform init....");

            InitializeComponent();
            
            ConfigFile config = ConfigFile.GetConfigFile("moniter.cfg");
            Global.IsOEM = config["OEM"].AsBool();
            Global.Brand = config["Brand"].AsString();
            Global.HideExpire = config["HideExpire"].AsBool();
            Global.HideConn = config["HideConn"].AsBool();
            string layout = config["Layout"].AsString();
            int style = config["Style"].AsInt();
            int layoutid = 1;
            if (layout == "1") layoutid = 1;
            if (layout == "2") layoutid = 2;
            if (layoutid == 1) layout1();
            if (layoutid == 2) layout2();
            if (style == 0)
            {
                style = 6;
            }

            kryptonPalette1.BasePaletteMode = (ComponentFactory.Krypton.Toolkit.PaletteMode)style;

            TradingLib.MoniterControl.ControlService.SuperRoot = config["SuperRoot"].AsString();

            mStart = start;

            //初始状态设置
            this.AcceptButton = btnLogin;
            btnLogin.Enabled = false;

            if (File.Exists("config/login.png"))
            {
                imageheader.Image = Image.FromFile("config/login.png");

            }

            //加载服务端IP地址
            string[] addresses = config["Servers"].AsString().Split(',');
            foreach (string s in addresses)
            {
                if (string.IsNullOrEmpty(s))
                    continue;
                servers.Items.Add(s);
            }
            
            servers.SelectedIndex = 0;
            //如果只有一个服务端地址 则影藏地址选择列表
            if (servers.Items.Count == 1)
            {
                servers.Visible = false;
                label0.Visible = false;
            }

            //获得对应用户名和密码
            bool saveuser = PropertyService.Get<bool>("saveuser", false);
            ckremberuser.Checked = saveuser;
            bool savepass = PropertyService.Get<bool>("savepass", false);
            ckremberpass.Checked = savepass;

            if (saveuser)
            {
                username.Text = PropertyService.Get<string>("username", "");
            }
            if (savepass)
            {
                password.Text = PropertyService.Get<string>("password", "");
            }


            this.Text = string.Format("{0}", Global.IsOEM ? Global.Brand : "系统登入");
            
            

            InitBW();
            
            WireEvent();
        }

        void WireEvent()
        { 
            btnLogin.Click +=new EventHandler(btnLogin_Click);
            btnExit.Click += new EventHandler(btnExit_LinkClicked);
            this.FormClosing += new FormClosingEventHandler(LoginForm_FormClosing);

            CoreService.EventCore.OnConnectedEvent += new VoidDelegate(EventCore_OnConnectedEvent);
            CoreService.EventCore.OnDisconnectedEvent += new VoidDelegate(EventCore_OnDisconnectedEvent);
            CoreService.EventCore.OnLoginEvent += new Action<RspMGRLoginResponse>(EventCore_OnLoginEvent);
            CoreService.EventCore.OnInitializeStatusEvent += new Action<string>(EventCore_OnInitializeStatusEvent);
            CoreService.EventCore.OnInitializedEvent += new VoidDelegate(EventCore_OnInitializedEvent);

            
        }

        void UnBindEvent()
        {
            CoreService.EventCore.OnConnectedEvent -= new VoidDelegate(EventCore_OnConnectedEvent);
            CoreService.EventCore.OnDisconnectedEvent -= new VoidDelegate(EventCore_OnDisconnectedEvent);
            CoreService.EventCore.OnLoginEvent -= new Action<RspMGRLoginResponse>(EventCore_OnLoginEvent);
            CoreService.EventCore.OnInitializeStatusEvent -= new Action<string>(EventCore_OnInitializeStatusEvent);
            CoreService.EventCore.OnInitializedEvent -= new VoidDelegate(EventCore_OnInitializedEvent);
        }

        #region 事件响应
        /// <summary>
        /// 退出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_LinkClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        /// <summary>
        /// 登入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            SaveLoginConfig();
            new Thread(delegate()
            {
                Connect();
            }).Start();
            

        }

        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLoginConfig();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        #endregion


        /// <summary>
        /// 按设置保存用户名和密码
        /// </summary>
        void SaveLoginConfig()
        {
            bool rbuser = ckremberuser.Checked;
            PropertyService.Set<bool>("saveuser", rbuser);
            bool rbpass = ckremberpass.Checked;
            PropertyService.Set<bool>("savepass", rbpass);

            PropertyService.Set<string>("username", rbuser ? username.Text : "");
            PropertyService.Set<string>("password", rbpass ? password.Text : "");

        }

        /// <summary>
        /// 显示登入过程信息
        /// </summary>
        /// <param name="msg"></param>
        public void ShowLoginStatus(string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(ShowLoginStatus), new object[] { msg });
            }
            else
            {
                lbLoginStatus.Text = msg;
            }
        }



        bool _connected = false;
        bool _loggedin = false;
        bool _gotloginrep = false;

        bool _connectstart = false;
        DateTime _connecttime = DateTime.Now;

        /// <summary>
        /// 执行连接请求
        /// 连接动作
        /// </summary>
        void Connect()
        {
            //禁止登入按钮操作
            this.btnLogin.Enabled = false;

            string address = servers.SelectedItem.ToString();
            //登入过程开始
            _connectstart = true;
            _connecttime = DateTime.Now;

            //初始化Client并启动
            CoreService.InitClient(address, 8890);
            CoreService.TLClient.Start();
        }


        bool _loginstart = false;
        DateTime _logintime = DateTime.Now;

        /// <summary>
        /// 响应连接事件 请求登入
        /// </summary>
        void EventCore_OnConnectedEvent()
        {
            ShowLoginStatus("服务端连接成功,请求登入");

            _connected = true;

            _loginstart = true;
            _logintime = DateTime.Now;

            CoreService.TLClient.ReqLogin(username.Text, password.Text);

        }


        bool _qrybasicinfo = false;
        DateTime qrybasicinfoTime = DateTime.Now;
        /// <summary>
        /// 响应登入 请求查询基础信息(底层 无需人工操作)
        /// </summary>
        /// <param name="response"></param>
        void EventCore_OnLoginEvent(RspMGRLoginResponse response)
        {
            _gotloginrep = true;
            if (response.RspInfo.ErrorID == 0)
            {
                ShowLoginStatus("登入成功");
                _loggedin = true;
                _qrybasicinfo = true;
                qrybasicinfoTime = DateTime.Now;
            }
            else
            {
                ShowLoginStatus("登入失败:" + response.RspInfo.ErrorMessage);
                _loggedin = false;
                //重置
                Reset();
            }
        }

        bool initsuccess = false;

        /// <summary>
        /// 响应初始化完成事件 用于检查系统是否初始化正常
        /// </summary>
        void EventCore_OnInitializedEvent()
        {
            if (CoreService.SiteInfo.Manager == null)
            {
                ShowLoginStatus("柜员数据获取异常,请重新登入!");
            }
            else
            {
                initsuccess = true;
            }
        }

        void EventCore_OnInitializeStatusEvent(string obj)
        {
            ShowLoginStatus(obj);
        }


        void EventCore_OnDisconnectedEvent()
        {
            _connected = false;
            _loggedin = false;
        }


        
        public void EnableLogin()
        {
            if (InvokeRequired)
            {
                Invoke(new VoidDelegate(EnableLogin), new object[] { });
            }
            else
            {
                btnLogin.Enabled = true;
            }
        }

        /// <summary>
        /// 重置异常连接并恢复界面状态
        /// </summary>
        void Reset()
        {
            new Thread(delegate()
            {
                _connectstart = false;
                _connected = false;

                _loginstart = false;
                _gotloginrep = false;
                _loggedin = false;

                _qrybasicinfo = false;
                initsuccess = false;

                CoreService.TLClient.Stop();
                this.btnLogin.Enabled = true;
                //lbLoginStatus.Text = "请登入";
            }).Start();
        }

        #region 线程内处理消息并触发显示
        /// <summary>
        /// 登入窗口建立线程循环检查全局变量
        /// 登入窗口点击按钮,启动后台登入线程,后台登入线程动态的更新登入窗口的界面,同时将登入的消息实时的写入到系统中
        /// 如果登入窗口的线程检测到该信息,则执行弹窗提醒或者是进入主界面
        /// </summary>
        private System.ComponentModel.BackgroundWorker bg;
        private void bgDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (true)
            {
                //数据初始化完毕
                if(CoreService.BasicInfoTracker.Initialized && !mStart.IsSplashScreenClosed && initsuccess)
                {
                    logger.Info("关闭登入窗体");
                    mStart.CloseSplashScreen();
                    UnBindEvent();
                }

                if (_connectstart && (DateTime.Now - _connecttime).TotalSeconds > 5 && (!_connected))
                {
                    logger.Info("连接服务器超过5秒没有连接事件回报");
                    ShowLoginStatus("连接超时,无法连接到服务器");
                    Reset();
                    this.EnableLogin();
                }

                //5秒内没有登入回报
                if (_loginstart && (DateTime.Now - _logintime).TotalSeconds > 5 && (!_gotloginrep))
                {
                    logger.Info("登入服务器超过5秒没有连接事件回报");
                    ShowLoginStatus("登入超时,无法登入到服务器");
                    Reset();
                    this.EnableLogin();
                }
                if (_qrybasicinfo && (DateTime.Now - qrybasicinfoTime).TotalSeconds > 30 && (!initsuccess))
                {
                    logger.Info("获取基础数据超过10秒没有成功");
                    ShowLoginStatus("获取基础数据失败,请重新登入");
                    Reset();
                    this.EnableLogin();
                }
                Thread.Sleep(100);
            }
        }

        //启动后台工作进程 用于检查信息并调用弹出窗口
        private void InitBW()
        {
            bg = new System.ComponentModel.BackgroundWorker();
            bg.WorkerReportsProgress = true;
            bg.DoWork += new System.ComponentModel.DoWorkEventHandler(bgDoWork);
            //bg.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bg_ProgressChanged);
            bg.RunWorkerAsync();
        }
        #endregion


        #region 移动窗体
        private bool m_isMouseDown = false;
        private Point m_mousePos = new Point();
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            m_mousePos = Cursor.Position;
            m_isMouseDown = true;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            m_isMouseDown = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (m_isMouseDown)
            {
                Point tempPos = Cursor.Position;
                this.Location = new Point(Location.X + (tempPos.X - m_mousePos.X), Location.Y + (tempPos.Y - m_mousePos.Y));
                m_mousePos = Cursor.Position;
            }
        }

        private void imageheader_MouseDown(object sender, MouseEventArgs e)
        {
            m_mousePos = Cursor.Position;
            m_isMouseDown = true;
        }

        private void imageheader_MouseUp(object sender, MouseEventArgs e)
        {
            m_isMouseDown = false;
        }

        private void imageheader_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_isMouseDown)
            {
                Point tempPos = Cursor.Position;
                this.Location = new Point(Location.X + (tempPos.X - m_mousePos.X), Location.Y + (tempPos.Y - m_mousePos.Y));
                m_mousePos = Cursor.Position;
            }
        }
        #endregion



    }
}
