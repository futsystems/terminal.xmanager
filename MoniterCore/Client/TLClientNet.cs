﻿using System;
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

        int requestid = 0;
        object _reqidobj = new object();
        protected int NextRequestID
        {
            get
            {
                lock (_reqidobj)
                {
                    return ++requestid;
                }
            }
        }

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

        public TLNegotiation Negotiation { get { return connecton == null ? null : connecton.Negotiation; } }
        string[] _servers = new string[] { };
        int _port = 5570;

        public TLClientNet(string[] servers, int port)
        {
            _servers = servers;
            _port = port;


        }

        public void Start()
        {
            logger.Info("TLClientNet Starting......");
            connecton = new TLClient_MQ(_servers, _port);
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
            UnbindConnectionEvent();
            connecton = null;
            _everlogin = false;//or relogin will not avabile ,not request marketinfo
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

        void UnbindConnectionEvent()
        {
            connecton.OnConnectEvent -= new ConnectDel(connecton_OnConnectEvent);
            connecton.OnDisconnectEvent -= new DisconnectDel(connecton_OnDisconnectEvent);
            connecton.OnDataPubConnectEvent -= new DataPubConnectDel(connecton_OnDataPubConnectEvent);
            connecton.OnDataPubDisconnectEvent -= new DataPubDisconnectDel(connecton_OnDataPubDisconnectEvent);

            connecton.OnTick -= new TickDelegate(connecton_OnTick);
            connecton.OnPacketEvent -= new IPacketDelegate(connecton_OnPacketEvent);
        }



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
            logger.Info("OnDataPubDisconnectEvent");
            CoreService.EventCore.FireDataDisconnectedEvent();
        }

        void connecton_OnDataPubConnectEvent()
        {
            logger.Info("OnDataPubConnectEvent");
            CoreService.EventCore.FireDataConnectedEvent();
        }

        void connecton_OnDisconnectEvent()
        {
            logger.Info("OnDisconnectEvent");
            CoreService.EventCore.FireDisconnectedEvent();
        }

        void connecton_OnConnectEvent()
        {
            logger.Info("OnConnectEvent");
            CoreService.EventCore.FireConnectedEvent();
            if (_everlogin)
            {
                this.ReqLogin(_user, _pass);
            }
        }


        void connecton_OnTick(Tick t)
        {
            CoreService.BasicInfoTracker.GotTick(t);//更新快照
            CoreService.TradingInfoTracker.GotTick(t);//驱动交易数据更新
            CoreService.EventIndicator.FireTick(t);//触发Tick事件
        }


        public void DebugStopTick()
        {
            if (connecton != null)
            {
                connecton.debug_StopTick();
            }
        }

        public void DebugStopMessage()
        {
            if (connecton != null)
            {
                connecton.debug_StopMessage();
            }
        }
    }
}
