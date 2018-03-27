﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZeroMQ;

using TradingLib.Common;
using System.Threading;
using System.Net;
using TradingLib.API;
using Common.Logging;

namespace TradingLib.MoniterCore
{
    /// <summary>
    /// 底层传输客户端,用于发起向服务器的底层传输连接,实现消息的收发功能
    /// client端会给自己绑定一个ID,用于通讯的唯一标识,但是经过实际运行发现,该ID可能会与已经登入的客户端重复.
    /// 因此导致登入信息错误，收不到注册回报估计就是该问题,大概想一下应该是这样
    /// </summary>
    public class AsyncClient
    {

        ILog logger = LogManager.GetLogger("AsyncClient");
        const string PROGRAME = "AsyncClient";
        public event DebugDelegate SendDebugEvent;
        public event Action<Message> SendTLMessage;

        /// <summary>
        /// 消息处理函数事件,当客户端接收到消息 解析后调用该函数实现相应函数调用
        /// </summary>
        /// <param name="type"></param>
        /// <param name="msg"></param>
        private void handleMessage(Message msg)
        {
            if (SendTLMessage != null)
                SendTLMessage(msg);

        }

        bool _noverb = false;
        public bool VerboseDebugging { get { return !_noverb; } set { _noverb = !value; } }
        private void v(string msg)
        {
            if (!_noverb)
                msgdebug(msg);
        }


        private void msgdebug(string msg)
        {
            if (SendDebugEvent != null)
                SendDebugEvent(msg);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverip">服务器地址</param>
        /// <param name="port">服务器端口</param>
        /// <param name="verbos">是否日志输出</param>
        public AsyncClient(string serverip, int port, bool verbos)
            : this(serverip, port, serverip, port + 2, verbos) { }

        /// <summary>
        /// 将tickserver/exserver分开，分别提供行情ip 端口 交易ip 交易端口
        /// </summary>
        /// <param name="serverip"></param>
        /// <param name="port"></param>
        /// <param name="tickserver"></param>
        /// <param name="tickport"></param>
        /// <param name="verbos"></param>
        public AsyncClient(string serverip, int port, string tickserver, int tickport, bool verbos)
        {
            _serverip = serverip;
            _serverport = port;
            VerboseDebugging = verbos;

            _tickip = tickserver;
            _tickport = tickport;

        }

        /// <summary>
        /// 断开与服务器的连接
        /// </summary>
        public void Disconnect()
        {
            Stop();
        }
        /// <summary>
        /// 客户端是否已经连接
        /// </summary>
        public bool isConnected { get { return _msg_go; } }

        public bool isTickConnected { get { return _tick_go; } }

        public void Stop()
        {

            logger.Info("___________________AnsyncClient Stop Thread and Socket....");
            StopRecvThread();
            StopTickReciver();
            StopSendThread();
              
        }

        /// <summary>
        /// 与服务器连接时候 获得的唯一的ID标示 用于区分客户端
        /// </summary>
        public string ID { get { return _identity; } }
        public string Name { get { return ID; } }



        string _identity = "";
        string _serverip = "127.0.0.1";
        public string ServerAddress { get { return _serverip; } set { _serverip = value; } }
        int _serverport = 5570;

        string _tickip = "127.0.0.1";
        int _tickport = 5572;

        public int Port
        {
            get { return _serverport; }
            set
            {

                if (value < 1000)
                    _serverport = 5570;
                else
                    _serverport = value;
            }
        }

        /// <summary>
        /// 启动客户端连接
        /// </summary>
        public void Start()
        {
            //启动数据接收线程
            StartRecvThread();
            int _wait = 0;
            while (!isConnected && (_wait++ < 5))
            {
                //等待1秒,当后台正式启动完毕后方可有进入下面的程序端运行
                Thread.Sleep(500);
                logger.Info(PROGRAME + "#:" + _wait.ToString() + "Starting....");
            }

            //启动数据发送线程
            StartSendThread();
        }


        #region 管理通道
        Thread _cliThread = null;
        ZSocket _client = null;
        bool _msg_go = false;
        ZContext _msg_ctx = null;
        public void StartRecvThread()
        {
            if (_msg_go)
                return;
            v("[AsyncClient] starting ....");
            _msg_go = true;
            _cliThread = new Thread(new ThreadStart(MessageTranslate));
            _cliThread.IsBackground = true;
            _cliThread.Start();
            
        }

        public void StopRecvThread()
        {
            if (!_msg_go)
                return;
            logger.Info("Stop Client Message Reciving Thread....");
            _msg_go = false;
            _msg_ctx.Shutdown();
        }


        TimeSpan timeout = new TimeSpan(0, 0, 2);
        //消息翻译线程,当socket有新的数据进来时候,我们将数据转换成TL交易协议的内部信息,并触发SendTLMessage事件,从而TLClient可以用于调用对应的处理逻辑对信息进行处理
        private void MessageTranslate()
        {
            using (_msg_ctx = new ZContext())
            {
                using (_client = new ZSocket(_msg_ctx, ZSocketType.DEALER))
                {
                    _client.SendHighWatermark = 1000000;
                    _client.ReceiveHighWatermark = 1000000;

                    _identity = System.Guid.NewGuid().ToString();
                    _client.Identity = Encoding.UTF8.GetBytes(_identity);
                    string cstr = "tcp://" + _serverip.ToString() + ":" + Port.ToString();
                    logger.Info(PROGRAME + ":Connect to Message Server:" + cstr);
                    _client.Connect(cstr);

                    ZMessage zmsg;
                    ZError error;
                    while (_msg_go)
                    {
                        try
                        {
                            if (null == (zmsg = _client.ReceiveMessage(out error)))
                            {
                                if (error == ZError.ETERM)
                                {
                                    _msg_go = false;
                                    logger.Error("Message Socket ETERM");
                                }
                                else
                                {
                                    _msg_go = false;
                                    logger.Error("Message Socket Error:" + error.ToString());
                                }
                            }
                            else
                            {
                                byte[] data = zmsg.Last().Read();
                                zmsg.Clear();
                                Message msg = Message.gotmessage(data);
                                handleMessage(msg);
                            }
                        }
                        catch (ZException ex)
                        {
                            _msg_go = false;
                            logger.Error("Message Socket 错误:" + ex.ToString());

                        }
                        catch (System.Exception ex)
                        {
                            _msg_go = false;
                            logger.Error("Message数据处理错误" + ex.ToString());
                        }
                    }
                    logger.Info("Message Thread stopped");
                }
            }
            _client.Close();
        }

        #endregion


        #region 行情连接
        ZSocket subscriber;
        Thread _tickthread;
        bool _tick_go = false;
        ZContext _tickCtx = null;

        bool _suballtick = true;
        /// <summary>
        /// 启动Tick数据接收,如果TLClient所连接的服务器支持Tick数据,则我们可以启动单独的Tick对话流程,用于接收数据
        /// </summary>
        public void StartTickReciver()
        {
            if (_tick_go)
                return;
            logger.Info("Start Client Tick Reciving Thread....");
            _tick_go = true;
            _tickthread = new Thread(TickHandler);
            _tickthread.IsBackground = true;
            _tickthread.Start();
        }

        public void StopTickReciver()
        {
            if (!_tick_go)
                return;
            logger.Info("Stop Client Tick Reciving Thread....");
            _tick_go = false;
            _tickCtx.Shutdown();
        }

        private void TickHandler()
        {
            using (_tickCtx = new ZContext())
            {
                using (subscriber = new ZSocket(_tickCtx, ZSocketType.SUB))
                {
                    string cstr = "tcp://" + _tickip.ToString() + ":" + _tickport.ToString();
                    logger.Info(PROGRAME + ":Connect to TickServer :" + cstr);
                    subscriber.Connect(cstr);
                    SubscribeTickHeartBeat();

                    if (_suballtick)
                    {
                        subscriber.Subscribe("");
                    }
                    ZMessage tickdata;
                    ZError error;
                    string tickstr = string.Empty;
                    while (_tick_go)
                    {
                        try
                        {
                            if (null == (tickdata = subscriber.ReceiveMessage(out error)))
                            {
                                if (error == ZError.ETERM)
                                {
                                    _tick_go = false;
                                    logger.Error("Tick Socket ETERM");
                                }
                                else
                                {
                                    _tick_go = false;
                                    logger.Error("Tick Socket Error:" + error.ToString());
                                }
                            }
                            else
                            {

                                tickstr = tickdata.First().ReadString(Encoding.UTF8);
                                //清空zmessage 否则内存溢出
                                tickdata.Clear();
                                //logger.Info("tickstr:" + tickstr);
                                if (!string.IsNullOrEmpty(tickstr))// && tickstr!="H,")
                                {
                                    if (tickstr == "H,")
                                    {
                                        Message msg = new Message();
                                        msg.Type = MessageTypes.TICKHEARTBEAT;
                                        msg.Content = "H,";
                                        handleMessage(msg);
                                    }
                                    else
                                    {
                                        Message msg = new Message();
                                        msg.Type = MessageTypes.TICKNOTIFY;
                                        msg.Content = tickstr;
                                        handleMessage(msg);
                                    }
                                }
                            }
                        }
                        catch (ZException ex)
                        {
                            _tick_go = false;
                            logger.Error("Message Socket 错误:" + ex.ToString());

                        }
                        catch (System.Exception ex)
                        {
                            _tick_go = false;
                            logger.Error("Message数据处理错误" + ex.ToString());
                        }
                    }
                    logger.Info("Tick Handler Thread Stopped");
                }
            }
            subscriber.Close();
        }

        #endregion


        #region 消息发送线程

        /// <summary>
        /// 启动消息发送线程
        /// </summary>
        void StartSendThread()
        {
            if (msggo) return;
            //启动消息处理线程
            _msgthread = new Thread(procmessageout);
            _msgthread.IsBackground = true;
            msggo = true;
            _msgthread.Start();
        }

        /// <summary>
        /// 停止消息发送线程
        /// </summary>
        bool StopSendThread()
        {
            if (!msggo) return true;
            logger.Info("Stop Client Send Thread....");
            int _wait = 0;
            while (_msgthread.IsAlive && (_wait++ < 5))
            {
                //等待1秒,当后台正式启动完毕后方可有进入下面的程序端运行
                logger.Info("#:" + _wait.ToString() + "  AsynClient[Message Sender] is stoping...." + "SenderThread Status:" + _msgthread.IsAlive.ToString());
                if (_msgthread.IsAlive)
                {
                    msggo = false;
                }
                Thread.Sleep(1000);
            }
            if (!_msgthread.IsAlive)
            {
                _msgthread = null;
                logger.Info("SenderThread Stopped successfull...");
                return true;
            }
            logger.Info("Some Error Happend In Stoping SenderThread");
            return false;
        }
        //发送byte信息
        public void Send(byte[] msg)
        {
            //放入消息缓存统一对外发送
            msgcache.Write(msg);
        }


        Thread _msgthread = null;
        bool msggo = false;
        RingBuffer<byte[]> msgcache = new RingBuffer<byte[]>(1000);
        void procmessageout()
        {
            ZError error;
            while (msggo)
            {
                try
                {
                    while (msgcache.hasItems)
                    {
                        byte[] data = msgcache.Read();
                        if (_client != null && data != null && data.Length>0)
                        {
                            using (ZMessage zmsg = new ZMessage())
                            {
                                zmsg.Add(new ZFrame(data));
                                if (!_client.Send(zmsg, out error))
                                {
                                    if (error == ZError.ETERM)
                                    {
                                        logger.Error("got ZError.ETERM,return directly");
                                        return;	// Interrupted
                                    }
                                    throw new ZException(error);
                                }
                            }
                           
                        }
                    }
                    Thread.Sleep(10);
                }
                catch (Exception ex)
                {
                    logger.Info("client send message out error:" + ex.ToString());
                }
            }
        }
        #endregion


        /// <summary>
        /// 用于检查某个ip地址是否提供有效服务,没有有效服务端则引发QSNoServerException异常,如果有数据或交易服务 则服务器会返回一个Provider名称 用于标识服务器
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="debug"></param>
        /// <returns></returns>
        public static string HelloServer(string ip, int port)
        {
            //("[AsyncClient]Start say hello to server...");
            using (var context = new ZContext())
            {
                using (ZSocket requester = new ZSocket(context, ZSocketType.REQ))
                {
                    string cstr = "tcp://" + ip + ":" + (port + 1).ToString();
                    requester.ReceiveTimeout = new TimeSpan(0,0,2);
                    requester.Connect(cstr);
                    BrokerNameResponse br=null;

                    ZMessage zresponse;
                    ZError error;
                    BrokerNameRequest package = new BrokerNameRequest();
                    package.SetRequestID(10001);
                    requester.Send(new ZFrame(package.Data));

                    var poller = ZPollItem.CreateReceiver();
                    if (requester.PollIn(poller, out zresponse, out error, new TimeSpan(0, 0, 2)))
                    {
                        //loger.Debug(string.Format("Got Rep Response:", response.First().ReadString(Encoding.UTF8)));
                        TradingLib.Common.Message message = TradingLib.Common.Message.gotmessage(zresponse.Last().Read());
                        br = ResponseTemplate<BrokerNameResponse>.CliRecvResponse(message);
                        zresponse.Clear();
                    }
                    else
                    {
                        br = null;
                    }
                    requester.Close();
                    //debug("socket closed....");
                    if (br == null)
                    {
                        throw new QSNoServerException();
                    }
                    return ((int)br.Provider).ToString();
                }
            }
        }


        /// <summary>
        /// 用于注册publisher的heartbeat
        /// </summary>
        void SubscribeTickHeartBeat()
        {
            if (subscriber == null) return;
            string prefix = "TICKHEARTBEAT";
            subscriber.Subscribe(Encoding.UTF8.GetBytes(prefix));
        }
        /// <summary>
        /// 订阅某个合约的数据
        /// </summary>
        /// <param name="symbol"></param>
        public void Subscribe(string symbol)
        {
            if (subscriber == null) return;
            string prefix = symbol + "^";
            subscriber.Subscribe(Encoding.UTF8.GetBytes(prefix));
        }

        public void SubscribeAll()
        {
            if (subscriber == null) return;
            SubscribeTickHeartBeat();
        }

    }
}