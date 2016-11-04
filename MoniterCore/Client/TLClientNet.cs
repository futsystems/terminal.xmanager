using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;
using Common.Logging;

namespace TradingLib.MoniterCore
{
    public partial class TLClientNet
    {

        ILog logger = LogManager.GetLogger("TLClient");

        TLClient_MQ connecton = null;

        /// <summary>
        /// 是否处于连接状态
        /// </summary>
        public bool Connected
        {
            get
            {
                if (connecton == null) return false;
                return connecton.IsConnected;
            }
        }

        public TLVersion ServerVersion { get { return connecton == null ? null : connecton.ServerVersion; } }
        string[] _servers = new string[] { };
        int _port = 5570;


        string _account = "";
        public TLClientNet(string[] servers, int port)
        {
            _servers = servers;
            _port = port;


        }
        public void Start()
        {
            logger.Info("TLClientNet Starting......");
            connecton = new TLClient_MQ(_servers, _port, "demo");
            connecton.ProviderType = QSEnumProviderType.Both;
            BindConnectionEvent();

            connecton.Start();


        
        }

        /// <summary>
        /// 启动行情连接
        /// </summary>
        public void StartTick()
        {
            if (connecton != null && connecton.IsConnected)
            {
                connecton.StartTick();
            }
        }

        public void Stop()
        {
            logger.Info("TLClientNet Stopping......");
            if (connecton != null && connecton.IsConnected)
            {
                connecton.Stop();
            }
            connecton = null;
            logger.Info("TLClientNet Stopped");
        }

        void BindConnectionEvent()
        {
            connecton.OnConnectEvent += new ConnectDel(connecton_OnConnectEvent);
            connecton.OnDisconnectEvent += new DisconnectDel(connecton_OnDisconnectEvent);
            connecton.OnDataPubConnectEvent += new DataPubConnectDel(connecton_OnDataPubConnectEvent);
            connecton.OnDataPubDisconnectEvent += new DataPubDisconnectDel(connecton_OnDataPubDisconnectEvent);

            connecton.OnTick += new TickDelegate(connecton_OnTick);
            connecton.OnPacketEvent += new IPacketDelegate(connecton_OnPacketEvent);


        }

        
        int requestid = 0;

        void SendPacket(IPacket packet)
        {
            //权限或者登入状态检查
            if (connecton != null && connecton.IsConnected)
            {
                connecton.TLSend(packet);
            }
        }

        void connecton_OnDataPubDisconnectEvent()
        {
            CoreService.EventCore.FireDataDisconnectedEvent();
        }

        void connecton_OnDataPubConnectEvent()
        {
            CoreService.EventCore.FireDataConnectedEvent();
        }

        void connecton_OnDisconnectEvent()
        {
            CoreService.EventCore.FireDisconnectedEvent();
        }

        void connecton_OnConnectEvent()
        {
            CoreService.EventCore.FireConnectedEvent();
        }


        void connecton_OnTick(Tick t)
        {
            CoreService.BasicInfoTracker.GotTick(t);//更新快照
            CoreService.TradingInfoTracker.GotTick(t);//驱动交易数据更新
            CoreService.EventIndicator.FireTick(t);//触发Tick事件
        }







        //ILogicHandler handler = null;
        //public void BindLogicHandler(ILogicHandler h)
        //{
        //    handler = h;
        //}

        //#region 功能函数

        //bool _debugEnable = true;
        //public bool DebugEnable { get { return _debugEnable; } set { _debugEnable = value; } }
        //QSEnumDebugLevel _debuglevel = QSEnumDebugLevel.DEBUG;
        //public QSEnumDebugLevel DebugLevel { get { return _debuglevel; } set { _debuglevel = value; } }

        ///// <summary>
        ///// 判断日志级别 然后再进行输出
        ///// </summary>
        ///// <param name="msg"></param>
        ///// <param name="level"></param>
        //protected void debug(string msg, QSEnumDebugLevel level = QSEnumDebugLevel.DEBUG)
        //{
        //    if ((int)level <= (int)_debuglevel && _debugEnable)
        //        msgdebug("[" + level.ToString() + "] " + msg);
        //}
        //void msgdebug(string msg)
        //{
        //    if (OnDebugEvent != null)
        //        OnDebugEvent(msg);

        //}
        //bool _noverb = true;
        ///// <summary>
        ///// enable/disable extended debugging
        ///// </summary>
        //public bool VerboseDebugging { get { return !_noverb; } set { _noverb = !value; } }

        //void v(string msg)
        //{
        //    if (_noverb) return;
        //    msgdebug(msg);
        //}


        //#endregion

    }
}
