using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Xml;
using TradingLib.API;
using TradingLib.Common;
using Common.Logging;
using System.Windows.Forms;


/*关于客户端与服务端的连接机制
 * 1.客户端与服务端通过ZeroMQ组件进行连接
 * 2.客户端与服务端有双向心跳机制,客户端按一定频率向服务端发送HeartBeat心跳信息,告诉服务端该客户端存活状态,服务端向客户端不断的推送tick以及交易数据
 * 客户端记录服务端上次消息时间,若超过一定时间间隔,客户端则请求服务端发送一个心跳信息(HeartBeatRequest)，若服务端正常发送了该心跳信息,则客户端知道该
 * 服务端存活，若心跳信息回报异常则关闭连接，重新Mode()创立连接
 * 3.connect只是针对上次Mode创建连接之后对当前的连接尝试重新连接
 *   Mode则通过TLFound重新搜索服务端列表,将可用的服务端缓存起来,并连接第一个可用连接
 * 4.客户端的心跳维护机制发现心跳异常则尝试重新Mode.
 * 5.客户端TLSend中若发现客户连接丢失,则会调用retryconnect进行connect当前连接(注 该连接并不重新Mode 不重新TLFound 服务端列表)
 * 20客户端200k/s(通过kvmessage可以降低至50k/s左右) 单台服务端应该可以扩充到500个客户端并发，因此冗余2台具有同步机制的服务器 应该可以支持1000个并发左右。
 * 
 * 
 * 
 * 
 * 
 * */
namespace TradingLib.MoniterCore
{
    /// <summary>
    /// 用于建立到服务器的连接,进行数据或者交易信息通讯
    /// </summary>
    [System.ComponentModel.DesignerCategory("")]
    public class TLClient_MQ
    {
        ILog logger = LogManager.GetLogger("TLClient_MQ");
        const string _skip = "        ";
        AsyncClient _mqcli = null;//通讯client组件
        int _tickerrors = 0;//tick数据处理错误计数
        int port = Const.TLDEFAULTBASEPORT;//默认服务端口
        int _wait = 1000;//后台检测连接状态频率
        public const int DEFAULTWAIT = Const.DEFAULTWAIT;//心跳检测线程检测频率

        int heartbeatperiod = Const.HEARTBEATPERIOD;//向服务端发送心跳信息间隔

        bool _started = false;//后台检测连接状态线程是否启动
        bool _connect = false;//客户端是否连接到服务端

        int _tickheartbeatdead = Const.TICKHEARTBEATDEADMS;
        int _tickprocesscheckfreq = Const.TICKHEARTBEATCHECKFREQ;

        int _sendheartbeat =10*1000;// Const.SENDHEARTBEATMS;//发送心跳请求间隔
        int _heartbeatdeadat = 3 * 10 * 1000;// Const.HEARTBEATDEADMS;//心跳死亡间隔

        long _lastheartbeat = 0;//最后心跳时间
        long _tickhartbeat = 0;//最后tick心跳时间
        bool _requestheartbeat = false;//请求心跳回复
        bool _recvheartbeat = false;//收到心跳回复
        bool _reconnectreq = false;//请求重新连接

        List<MessageTypes> _rfl = new List<MessageTypes>();
        public List<MessageTypes> RequestFeatureList { get { return _rfl; } }//功能列表


        List<string> serverip = new List<string>();//服务端IP列表 参数给定的IP地址列表
        //以下数据由TLFound查询 知道哪些地址的服务端是激活的，并将其地址记录
        List<Providers> servers = new List<Providers>();//当前可用服务端
        List<string> avabileip = new List<string>();//当前可用的IP列表

        string tickip = "";
        int tickport=5572;


        //客户端标识
        string _name = string.Empty;
        public string Name { get { return _name; } set { _name = value; } }
        //尝试连接次数
        int _disconnectretry = 3;
        public const int DEFAULTRETRIES = 3;//默认尝试连接次数
        public int DisconnectRetries { get { return _disconnectretry; } set { _disconnectretry = value; } }

        int _remodedelay = Const.RECONNECTDELAY;//在心跳机制中重新建立连接中 Mode失败后再次Mode的时间间隔 单位秒
        int _modeRetries = Const.RECONNECTTIMES;//在心跳机制中通过Mode重新搜索服务列表 并建立连接，重试次数
        public int ModeRetries { get { return _modeRetries; } set { _modeRetries = value; } }
        //可用服务端列表
        public Providers[] ProvidersAvailable { get { return servers.ToArray(); } }
        //当前连接服务端序号
        int _curprovider = -1;
        public int ProviderSelected { get { return _curprovider; } }
        //BrokerName
        Providers _bn = Providers.Unknown;
        public Providers BrokerName { get { return _bn; } }
        //服务端版本
        private TLNegotiation _negotiation;
        public TLNegotiation Negotiation { get { return _negotiation; } }

        //服务端版本与客户端API版本是否匹配
        //public bool IsAPIOK { get { return Util.Version >= _serverversion; } }

        public bool IsConnected { get { return _connect; } }//是否连接
        //心跳相应是否正常 连接正常 并且 请求心跳与接收心跳一致(确定发送心跳回复请求后是否收到心跳回报)
        public bool isHeartbeatOk { get { return _connect && (_requestheartbeat == _recvheartbeat); } }

        #region Event
        public event ConnectDel OnConnectEvent;//客户端连接事件
        public event DisconnectDel OnDisconnectEvent;//客户端断开连接事件
        public event DataPubConnectDel OnDataPubConnectEvent;//Tick publisher成功
        public event DataPubDisconnectDel OnDataPubDisconnectEvent;//Tick publisher成功
        public event RspMGRLoginResponseDel OnLoginResponse;
        public event TickDelegate OnTick;//tick数据回报
        public event IPacketDelegate OnPacketEvent;//其他逻辑数据包处理
        #endregion


        #region 后台维护线程

        #region tick数据维护线程 用于检测tick数据 若消息流中断,自动重连
        Thread _tickwatchthread = null;
        bool ticktrackergo = false;
        bool _tickreconnectreq = false;

        void StartTickWatcher()
        {
            if (ticktrackergo) return;
            ticktrackergo = true;
            _tickwatchthread = new Thread(tickprocess);
            _tickwatchthread.IsBackground = true;
            _tickwatchthread.Start();
            logger.Info("TickWatcher threade started");
        }

        void StopTickWatcher()
        {
            if (!ticktrackergo) return;
            ticktrackergo = false;
            _tickwatchthread.Abort();
            _tickwatchthread = null;
            logger.Info("TickWatcher threade stopped");
        }
        

        void tickprocess()
        {
            while (ticktrackergo)
            {
                // 获得当前时间
                long now = DateTime.Now.Ticks;
                //计算上次heartbeat以来的时间间隔
                long diff = (now - _tickhartbeat) / 10000;//(ticks/10000得到MS)
                //debug("connect:" + _connect.ToString() + " reconnecttick:" + _tickreconnectreq.ToString() + " diff:" + diff.ToString() + " _tickheartbeatdead:" + _tickheartbeatdead.ToString());
                if (!(_connect && (!_tickreconnectreq) && (!_reconnectreq)  && (diff < _tickheartbeatdead)))//任何一个条件不满足将进行下面的操作
                {
                    //debug("connect:" + _connect.ToString() + " reconnecttick:" + _tickreconnectreq.ToString() + " diff:" + diff.ToString() + " _tickheartbeatdead:" + _tickheartbeatdead.ToString() + " now:" + now.ToString() + " _lastick:" + _tickhartbeat.ToString());
                    if (_tickreconnectreq == false)
                    {
                        logger.Info("Tick  Stream Stopped");
                        //重新启动tick数据连接
                        _tickreconnectreq = true;
                        new Thread(reconnectTick).Start();
                    }
                }

                Thread.Sleep(_tickprocesscheckfreq);
            }
            
        
        }
        #endregion


        #region 服务端连接监控线程(该服务端连接处理委托/成交/查询)
        Thread _bwthread = null;

        void StartMessageWatcher()
        {
            if (_started) return;

            _started = true;
            _bwthread = new Thread(_bw_DoWork);
            _bwthread.IsBackground = true;
            _bwthread.Start();
            logger.Info("MessageWatcher  threade started");
        }

        void StopMessageWatcher()
        {
            if (!_started) return;

            _started = false;
            _bwthread.Abort();
            _bwthread = null;
            logger.Info("MessageWatcher  threade stopped");
        }
    
        /// <summary>
        /// 心跳维护线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _bw_DoWork()
        {
            while (_started)
            {

                // 获得当前时间
                long now = DateTime.Now.Ticks;
                //计算上次heartbeat以来的时间间隔
                long diff = (now - _lastheartbeat) / 10000;//(ticks/10000得到MS)
                //debug("连接:" + _connect.ToString() + " 请求重新连接:" + (!_reconnectreq).ToString() + "心跳间隔"+(diff < _sendheartbeat).ToString()+" 上次心跳时间:" + _lastheartbeat.ToString() + " Diff:" + diff.ToString() + " 发送心跳间隔:" + _sendheartbeat.ToString());
                if (!(_connect && (!_reconnectreq) && (diff < _sendheartbeat)))//任何一个条件不满足将进行下面的操作
                {
                    try
                    {
                        if (isHeartbeatOk)
                        {
                            //debug(PROGRAME + ":heartbeat request at: " + DateTime.Now.ToString()+" _heartbeatdeadat:"+_heartbeatdeadat.ToString() + " _diff:"+diff.ToString());
                            //当得到响应请求后,_recvheartbeat = !_recvheartbeat; 因此在发送了一个hearbeatrequest后 在没有得到服务器反馈前不会再次重新发送
                            _requestheartbeat = !_recvheartbeat;
                            //发送请求心跳响应
                            HeartBeatRequest hbr = RequestTemplate<HeartBeatRequest>.CliSendRequest(0);

                            TLSend(hbr);
                        }
                        else if (diff > _heartbeatdeadat)//心跳间隔超时后,我们请求服务端的心跳回报,如果服务端的心跳响应超过心跳死亡时间,则我们尝试 重新建立连接
                        {
                            //debug("xxxxxxxxxxxxxxx diff:" + diff.ToString() + " dead:" + _heartbeatdeadat.ToString());
                            if (_reconnectreq == false)
                            {
                                _reconnectreq = true;//请求重新连接标识,避免重复请求连接
                                logger.Info("Message Heartbeat Lost,reconnect");
                                new Thread(reconnect).Start();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Info(ex.ToString());

                    }
                }
                //注等待要放在最后,否则有些情况下停止了服务_started = false,但是刚才检查过了在等待，从而进入上面的检查过程，stop连接后 自动重连。
                Thread.Sleep(_wait);//每隔多少秒检查心跳时间MS
               
            }
        }
        #endregion 


        #region 心跳检测 线程 用于定时向服务器发送心跳数据
        bool _heartbeatgo = false;
        Thread _heartbeatthread = null;

        void _hbproc()
        {
            while (_heartbeatgo)
            {
                HeartBeat();
                Thread.Sleep(heartbeatperiod * 1000);
            }
        }
        private void StartHartBeat()
        {
            if (_heartbeatgo) return;
            _heartbeatgo = true;
            _heartbeatthread = new Thread(_hbproc);
            _heartbeatthread.IsBackground = true;
            _heartbeatthread.Start();
            logger.Info("HeartBeatSend threade started");
        }

        void StopHeartBeat()
        {
            if (!_heartbeatgo) return;
            _heartbeatgo = false;
            _heartbeatthread.Abort();
            _heartbeatthread = null;
            logger.Info("HeartBeatSend threade stoped");
        }
        #endregion

        #endregion 

        #region TLClient_IP 构造函数
        /// <summary>
        /// 每个接入节点包含tick数据分发/成交接入 避免的单点服务器带宽占用过大形成单点故障
        /// 系统包含 核心成交服务器 + 外围接入服务器若干台 客户端通过外围服务器接入可以就近接入 北方一台服务器
        /// 上海一台服务器  南方一台服务器 中心服务器 与三台接入服务器进行通讯/接入服务器可以使用虚拟机 只要有网速保证即可。
        /// 这样外网的攻击只会发现虚拟接入主机。接入主机对外开放4个端口 5570 5571 5572 ssh端口在纯linux下运行。
        /// 中心服务器与外围服务器 通过 防火墙单点通讯。
        /// </summary>
        /// <param name="servers">ip地址列表</param>
        /// <param name="srvport">服务端口</param>
        /// <param name="ClientName">客户端连接名称</param>
        /// <param name="deb">日志输出回调</param>
        /// <param name="verbose">是否输出详细日志</param>
        public TLClient_MQ(string[] servers, int srvport)
        {
            port = srvport;//服务器端口
            foreach (string s in servers)//服务器地址
                serverip.Add(s);
        }

        #endregion


        #region Start Stop Section
        /// <summary>
        /// 启动服务
        /// </summary>
        public void Start(bool retry=false)
        {
            logger.Info("Start to connect to server....");

            bool _modesuccess = false;
            int _retry = 0;
            Stop();
            while (_modesuccess == false && _retry < (retry?_modeRetries:1))
            {
                _retry++;
                logger.Info("attempting connect to server... retry times:" + _retry.ToString());
                _modesuccess = Mode(_curprovider, false);//尝试连接第一可用服务端,对一组IP地址进行服务查询后,将可用服务端放入队列，并尝试连接第一个服务端
                //因此重新连接用Mode来进行,有重新搜索服务端列表的功能
                Thread.Sleep(_remodedelay * 1000);
            }
            if (!_modesuccess)
            {
                logger.Info("网络故障,无法连接到服务器");
                MessageBox.Show("网络故障，无法连接到服务器");
            } 
        }
       
        /// <summary>
        /// 停止连接服务
        /// </summary>
        public void Stop()
        {
            logger.Info("Stop Client");
            try
            {
                
                StopTickWatcher();//停止tick监控
                StopMessageWatcher();//停止后台心跳监控
                StopHeartBeat();//停止心跳发送
                if (_mqcli!=null && _mqcli.isConnected) //如果实现已经stop了brokerfeed 会造成服务器循环相应。应该将_stated放在这里进行相应
                {
                    try
                    {
                        UnregisterClientRequest req = RequestTemplate<UnregisterClientRequest>.CliSendRequest(0);
                        TLSend(req);//向服务器发送clearClient消息用于注销客户端
                        _mqcli.Disconnect(); 
                    }
                    catch (Exception ex){
                        logger.Info("stop mqcli error:" + ex.ToString());
                    }
                }
                markdisconnect();
            }
            catch (Exception ex)
            {
                logger.Info("Error stopping TLClient_MQ " + ex.Message + ex.StackTrace);
            }
            finally
            {
                logger.Info("Realse asyncClient and thread resource");
                _mqcli = null;
                _bwthread = null;
                _heartbeatthread = null;
                _tickwatchthread = null;
            }
        }


        #endregion


        #region 连接与断开连接

        bool connect() { return connect(_curprovider != -1 ? _curprovider : 0); }//连接到当前服务端或者是第一服务端
        bool connect(int providerindex) { return connect(providerindex, false); }
        /// <summary>
        /// 初始化mqclient并建立对应的连接通道
        /// </summary>
        /// <param name="providerindex"></param>
        /// <param name="showwarn"></param>
        /// <returns></returns>
        bool connect(int providerindex, bool showwarn)
        {
            logger.Info("[connect] Connect to prvider....");
            if ((providerindex >= servers.Count) || (providerindex < 0))
            {
                logger.Info(_skip + " Ensure provider is running and Mode() is called with correct provider number.   invalid provider: " + providerindex);
                return false;
            }

            try
            {
                logger.Info(_skip + "Attempting connection to server: " + avabileip[providerindex]);
                //如果原来的连接存活 则先断开连接
                if ((_mqcli != null) && (_mqcli.isConnected))
                {
                    logger.Info(_skip + "Disconnect old Connection...");
                    _mqcli.Disconnect();
                    markdisconnect();
                }
                bool acceptable = true;////false;
                if (acceptable)
                {
                    //实例化asyncClient并绑定对已的函数
                    _mqcli = new AsyncClient(avabileip[providerindex], port, true);
                    //_mqcli.SendDebugEvent += new DebugDelegate(msgdebug);
                    _mqcli.SendTLMessage += new Action<Common.Message>(handle);
                    //开始启动连接
                    _mqcli.Start();
                    updateheartbeat();
                    if (_mqcli.isConnected)
                    {
                        // set our name 获得连接的唯一标识序号
                        _name = _mqcli.ID;
                        // notify
                        logger.Info(_skip + "connected to server: " + serverip[providerindex] + ":" + this.port + " via:" + Name);
                        _reconnectreq = false;//注通过Mod重新建立连接的过程中,连接线程会停止在 TLFound过程中，会一直等待服务器返回服务名
                        _recvheartbeat = true;
                        _requestheartbeat = true;
                        _connect = true;//建立连接标识
                        _tickreconnectreq = false;
                        //初始化化连接 注册,请求FeatureList,请求版本等
                        InitConnection();
                    }
                    else
                    {
                        _connect = false;
                        logger.Info(_skip + "unable to connect to server at: " + serverip[providerindex].ToString());
                    }
                }
                else
                {
                    _connect = false;
                    logger.Info(_skip + "unable to connect to server at: " + serverip[providerindex].ToString());
                }

            }
            catch (Exception ex)
            {
                logger.Info(_skip + "exception creating connection to: " + serverip[providerindex].ToString() + ex.ToString());
                logger.Info(ex.Message + ex.StackTrace);
                _connect = false;
            }
            return _connect;
        }

        //简单的通过尝试重新恢复对当前服务端的连接
        //本地客户端的小问题导致的连接暂时失效 可以通过再次尝试连接原来的服务端建立服务，然后恢复正常通讯
        //若服务端无法建立 则在心跳机制下面 会有服务器尝试重连的机制，这个重连机制将调用TLFound查询IP列表中所有有效的服务端.
        bool retryconnect()
        {
            logger.Info("     disconnected from server: " + serverip[_curprovider] + ", attempting reconnect...");
            bool rok = false;
            int count = 0;
            while (count++ < _disconnectretry)
            {
                rok = connect(_curprovider, false);
                if (rok)
                    break;
            }
            logger.Info(rok ? "reconnect suceeded." : "reconnect failed.");
            return rok;
        }

        void reconnect()
        {

            bool _modesuccess = false;
            int _retry = 0;
            Stop();
            while (_modesuccess == false && _retry < _modeRetries)
            {
                _retry++;
                logger.Info("attempting reconnect... retry times:" + _retry.ToString());
                _modesuccess = Mode();//尝试连接第一可用服务端,对一组IP地址进行服务查询后,将可用服务端放入队列，并尝试连接第一个服务端
                //因此重新连接用Mode来进行,有重新搜索服务端列表的功能
                Thread.Sleep(_remodedelay * 1000);
            }
            if (!_modesuccess)
            {
                logger.Info( "网络故障,无法连接到服务器");
                MessageBox.Show("网络故障，无法连接到服务器");
            }
        }
        /// <summary>
        /// 重新连接tick数据服务
        /// </summary>
        void reconnectTick()
        {
            try
            {
                _mqcli.StopTickReciver();
                if (OnDataPubDisconnectEvent != null)
                    OnDataPubDisconnectEvent();
                _mqcli.StartTickReciver();
                _tickhartbeat = DateTime.Now.Ticks;

                if (OnDataPubConnectEvent != null)
                    OnDataPubConnectEvent();
                _tickreconnectreq = false;
            }
            catch (Exception ex)
            {
                logger.Info("reconnect tick error:" + ex.ToString());
            }
        }

        public void debug_StopTick()
        {
            if (_mqcli != null)
            {
                _mqcli.StopTickReciver();
            }
        }

        public void debug_StopMessage()
        {
            if (_mqcli != null)
            {
                _mqcli.StopRecvThread();
            }
        }
        /// <summary>
        /// 启动行情通道
        /// </summary>
        public void StartTick()
        {
            connectTick();
        }
        /// <summary>
        /// 首次连接tick数据服务
        /// </summary>
        void connectTick()
        {
            //debug("xxxxxxxxxxxxxxxxxxx 建立tick数据连接...............",QSEnumDebugLevel.MUST);
            _mqcli.StopTickReciver();
            if (OnDataPubDisconnectEvent != null)
                OnDataPubDisconnectEvent();


            _mqcli.StartTickReciver();//建立tickpublisher的连接
            _tickhartbeat = DateTime.Now.Ticks;//将当前时间设定为tickheartbeat时间
            StartTickWatcher();
            //启动市场数据接收之后才会触发datapubavabile,这样外层逻辑才可以注册市场数据
            if (OnDataPubConnectEvent != null)
                OnDataPubConnectEvent();

            _tickreconnectreq = false;
        }
        #endregion


        #region Mode 用于寻找可用服务 并进行连接


        //delegate bool ModeDel(int pi, bool warn);
        /// <summary>
        /// 默认从序号0开始连接服务器
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <returns></returns>
        public bool Mode() { return Mode(0, true); }
        public bool Mode(int ProviderIndex, bool showwarning)
        {
            //1.在对应的服务器列表中查询可提供服务的服务端
            logger.Info("[Mode] Mode to Provider");
            TLFound();//查询提供的IP上是否存在对应的服务响应
            //不存在有效服务则直接返回
            if (servers.Count == 0)
            {
                logger.Info("There is no any server avabile... try angain later");
                return false;
            }
            // see if called from start
            if (ProviderIndex < 0)
            {
                logger.Info("provider index cannot be less than zero, using first provider.");
                ProviderIndex = 0;
            }
            
            //2.正式与服务器建立连接,这里会新建实例 并发出一个新的会话连接
            bool ok = connect(ProviderIndex, false);//_mqcli在connect里面创建,具体创建逻辑见connect函数
            
            //3.如果连接到对应的服务器成功 启动心跳维护线程与心跳发送线程
            if (ok)
            {
                logger.Info("connected ok, try to start thread");
                StartMessageWatcher();
                StartHartBeat();
            }
            else
            {
                logger.Info("Unable to connect to provider: " + ProviderIndex);
                return false;
            }

            try
            {
                _curprovider = ProviderIndex;
                _bn = servers[_curprovider];
                //当所有组件初始化完毕 统一启动Start就可以正确调用到这里的回调
                //初始化initconnection/启动后台线程完毕后触发事件
                //logger.Info("??????????????????????????????????");
                //if (OnConnectEvent != null)
                //{
                //    OnConnectEvent();
                //}
                return true;
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message + ex.StackTrace);

            }
            logger.Info("Server not found at index: " + ProviderIndex);
            return false;
        }
        /// <summary>
        /// 初始化交易协议通讯
        /// </summary>
        void InitConnection()
        {
            logger.Info(":InitConnection......");
            // register ourselves with provider 注册
            Register();
            Util.sleep(100);
            //// request list of features from provider 请求功能支持列表
            //RequestFeatures();
            ////request server version;查询服务器版本
            //ReqServerVersion();
            ////当我们得到服务器版本后 我们设定 _reconnectreq = false; 这样 就不会一直进行重连
        }
        /// <summary>
        /// 用于通过ip地址来获得对应的provider名称,可以检查是否有对应的服务存在
        /// 注意 参数传递过来的serverlist未必全部有效,在TLFound中要将有效的保存在有效的列表中
        /// </summary>
        /// <returns></returns>
        public Providers[] TLFound()
        {
            logger.Info("[TLFound] Searching provider list...");
            logger.Info(_skip + "clearing existing list of available providers");
            servers.Clear();
            avabileip.Clear();
            // get name for every server provided by client
            //注这里需要将可用的服务端IP建立缓存列表,这样就会自动的连接到服务可用的服务端,
            foreach (string ip in serverip)
            {
                logger.Info(_skip + "Attempting to found server at : " + ip + ":" + port.ToString());
                //只需要让asynclient向某个特定的ip地址发送个寻名消息即可
                int pcode = (int)Providers.Unknown;
                //建立延迟错误退出机制 这样就可以尝试连接其他服务器 FIX ME
                try
                {
                    string r = AsyncClient.HelloServer(ip, port);
                    logger.Info("got brokernameresponse:" + r);
                    pcode = Convert.ToInt16(r);
                }
                catch (QSNoServerException ex)
                {
                    logger.Info(_skip + "There is no service avabile at:" + ip);
                    //如果在查询服务端的时候出现错误则跳过该IP检查,并进行下一个IP的服务端检查
                    continue;
                }
				//???? fix android ???????????,???????
				//??hello2server????????
				catch(Exception ex) {
                    logger.Info(_skip + "HelloServer To Server error:" + ex.ToString());
					try
					{
						Thread.Sleep(100);
						string r = AsyncClient.HelloServer(ip, port);
						pcode = Convert.ToInt16(r);
					}
					catch (QSNoServerException nex)
					{
                        logger.Info(_skip + "There is no service avabile at:" + ip);
						//如果在查询服务端的时候出现错误则跳过该IP检查,并进行下一个IP的服务端检查
						continue;
					}
					catch(Exception nex)
					{
						continue;
					}
				}
                //如果服务端有效则记录该provider并记录对应的server IP
                try
                {
                    Providers p = (Providers)pcode;
                    if (p != Providers.Unknown)
                    {
                        logger.Info(_skip + "Found provider: " + p.ToString() + " at: " + ip + ":" + port.ToString());
                        servers.Add(p);//将有用的brokername保存到server中
                        avabileip.Add(ip);
                    }
                    else
                    {
                        logger.Info(_skip + "skipping unknown provider at: " + ip + ":" + port.ToString());
                    }
                }
                catch (Exception ex)
                {
                    logger.Info(_skip + "error adding providing at: " + ip + ":" + port.ToString() + " pcode: " + pcode);
                    logger.Info(ex.Message + ex.StackTrace);
                }
            }
            logger.Info(_skip + "Total Found " + servers.Count + " providers.");
            return servers.ToArray();
        }
        #endregion


        #region TLSend
        MessageTypes[] _initfl = new MessageTypes[] { MessageTypes.REGISTERCLIENT,MessageTypes.CLEARCLIENT, MessageTypes.FEATUREREQUEST, MessageTypes.VERSIONREQUEST,MessageTypes.HEARTBEAT,MessageTypes.HEARTBEATREQUEST,MessageTypes.LOGINREQUEST };

        public long TLSend(IPacket package)
        {
            try
            {

                if (_mqcli != null && _mqcli.isConnected)
                {
                    _mqcli.Send(package.Data);
                    return 0;
                }
                else
                {
                    //当发送信息的过程中，如果当前连接无效,则我们尝试重新建立当前连接
                    //注意:本地heartbeat线程会一致通过TLSend发送心跳信息到服务器,因此如果连接中断,则他会在这里触发重新建立连接的要求。
                    //if (_started)
                    //    retryconnect();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                logger.Info("error sending packet " + package.ToString());
                logger.Info(ex.Message + ex.StackTrace);
                return -1;
            }   
        }

        #endregion




        int requestid = 0;

        #region 其他程序---> TLClient 用于向TLServer发送请求的操作
        /// <summary>
        /// 注册
        /// </summary>
        void Register()
        {
            logger.Info("注册到服务端...");
            RegisterClientRequest req = RequestTemplate<RegisterClientRequest>.CliSendRequest(++requestid);
            req.VersionToken = "Mars";
            TLSend(req);
        }
        /// <summary>
        /// 请求功能特征列表
        /// </summary>
        void RequestFeatures()
        {
            logger.Info(":请求服务端功能列表...");
            _rfl.Clear();
            FeatureRequest request = RequestTemplate<FeatureRequest>.CliSendRequest(++requestid);
            TLSend(request);
        }
        string neoKey = string.Empty;
        string neoString = string.Empty;
        /// <summary>
        /// 请求服务器版本
        /// </summary>
        void ReqServerVersion()
        {
            logger.Info(":请求服务端版本...");
            VersionRequest request = RequestTemplate<VersionRequest>.CliSendRequest(++requestid);
            request.ClientVersion = "2.0";
            request.DeviceType = "PC";
            neoKey = Util.GetRandomString(8);
            neoString = Util.GetRandomString(12);
            request.NegotiationKey = neoKey;
            request.NegotiationString = neoString;
            request.EncryptUUID = StringCipher.Encrypt(_sessionID, neoKey);

            TLSend(request);
        }



        //SymbolBasket _lastbasekt;
        List<string> symlist = new List<string>();
        /// <summary>
        /// 请求市场数据
        /// </summary>
        /// <param name="mb"></param>
        public void Subscribe(string[] symbols)
        {
            //如果mb=null 则订阅所有数据(订阅发布者当前所发布的所有数据)
            if (symbols == null || symbols.Length == 0)
            {
                SubscribeAll_sub();//
                return;
            }
            else
            {
                //通知服务端该客户端请求symbol basket数据
                //Unsubscribe();//先清除原来数据请求
                logger.Info("TLClient注册合约列表:" + string.Join(",", symbols));
                List<string> newlist = new List<string>();
                foreach (string sym in newlist)
                {
                    if (!symlist.Contains(sym))
                    {
                        newlist.Add(sym);
                    }
                }
                foreach(string sym in newlist)
                {
                    Subscribe_sub(sym);
                }
            }

        }

        #region tick subscriber 订阅 退订 等函数
        void Subscribe_sub(string symbol)
        {
            if (_mqcli != null && _mqcli.isConnected)
            {
                _mqcli.Subscribe(symbol);//订阅合约

            }
        }
        void SubscribeAll_sub()
        {
            if (_mqcli != null && _mqcli.isConnected)
            {
                _mqcli.SubscribeAll();

            }
        }
        /// <summary>
        /// sub 向 pub注销symbol数据请求
        /// </summary>
        void Unsubscribe_sub(string symbol)
        {
            //if (_mqcli != null && _mqcli.isConnected)
            //{
            //    _mqcli.Unsubscribe(symbol);

            //}
        }
        /// <summary>
        /// sub 向 pub注销所有数据请求
        /// </summary>
        void UnsubscribeAll_sub()
        {
            logger.Info("注销所有市场订阅...");
            if (_mqcli != null && _mqcli.isConnected)
            {
                //if (_lastbasekt != null)
                {
                    //foreach (Security sec in _lastbasekt)
                    //{
                    //    _mqcli.Unsubscribe(sec.Symbol);
                    //}
                }

            }
        }
        #endregion


        /// <summary>
        /// 注销市场数据
        /// </summary>
        public void Unsubscribe()
        {
            //告诉服务端清除数据
            UnregisterSymbolTickRequest req = RequestTemplate<UnregisterSymbolTickRequest>.CliSendRequest(0);
            TLSend(req);
            UnsubscribeAll_sub();

        }
        /// <summary>
        /// 发送心跳响应 告诉服务器 该客户端存活
        /// </summary>
        /// <returns></returns>
        public void HeartBeat()
        {
            HeartBeat hb = RequestTemplate<HeartBeat>.CliSendRequest(0);
            TLSend(hb);
        }
        #endregion


        #region 客户端消息回报处理函数

        /// <summary>
        /// 客户端响应版本查询请求回报
        /// </summary>
        /// <param name="response"></param>
        void CliOnVersionResponse(VersionResponse response)
        {
            _negotiation = response.Negotiation;

            //_uuid = response.ClientUUID;
            logger.Info("Client got version response, version:" + _negotiation.Version.ToString());

            string rawstr = string.Empty;
            try
            {
                rawstr = StringCipher.Decrypt(response.Negotiation.NegoResponse,neoKey);
            }
            catch (Exception ex)
            {
                logger.Error("Negotiation Error");
            }

            if (rawstr != neoString)
            {
                //如果原来的连接存活 则先断开连接
                if ((_mqcli != null) && (_mqcli.isConnected))
                {
                    logger.Info(_skip + "Disconnect old Connection...");
                    _mqcli.Disconnect();
                    markdisconnect();
                }
            }

            else
            {
                if (OnConnectEvent != null)
                {
                    OnConnectEvent();
                }
            }
        }

        /// <summary>
        /// 客户端响应功能码查询请求回报
        /// </summary>
        /// <param name="response"></param>
        void CliOnFeatureResponse(FeatureResponse response)
        {
            _rfl.Clear();
            foreach (MessageTypes mt in response.Features)
            {
                _rfl.Add(mt);
            }
            //检查是否支持tick然后我们就可以启动tickreceive
            //只有当返回特征列表支持数据服务时,并且设定客户端同时支持 数据服务时 TLClient_MQ才会注册到数据服务地址
            //checkTickSupport();
        }

        /// <summary>
        /// 客户端响应登入请求回报
        /// </summary>
        /// <param name="response"></param>
        void CliOnLoginResponse(RspMGRLoginResponse response)
        {
            if (OnLoginResponse != null)
                OnLoginResponse(response);
        }
        /// <summary>
        /// 客户端相应心跳请求回报
        /// </summary>
        /// <param name="response"></param>
        void CliOnHeartbeatResponse(HeartBeatResponse response)
        {
            _recvheartbeat = !_recvheartbeat;
        }

        #endregion


        #region 功能函数

        void updateheartbeat()
        {
            _lastheartbeat = DateTime.Now.Ticks;
        }

        //断开连接后我们需要进行标识并输出事件
        void markdisconnect()
        {
            _connect = false;
            //_mqcli.SendDebugEvent -= new DebugDelegate(msgdebug);
            _mqcli.SendTLMessage -= new Action<Common.Message>(handle);
            _mqcli = null;//将_mqcli至null 内存才会被回收

            if (OnDisconnectEvent != null)
                OnDisconnectEvent();
        }


        

        /// <summary>
        /// 检查客户端API版本与服务端API版本 用于版本检查
        /// </summary>
        /// <param name="srvVersion"></param>
        /// <returns></returns>
        private bool checkVersion(int srvVersion)
        {
            //debug(PROGRAME + " :API版本检查...");
            if (srvVersion > Util.Version)
            {
                logger.Info("API版本检查-->请更新API");
                return false;
            }
            else
            {
                logger.Info("API版本检查-->API兼容");
                return true;
            }
        }
        #endregion

        string _sessionID = string.Empty;

        //消息处理逻辑
        void handle(Common.Message msg)
        {
            //if (type != MessageTypes.TICKHEARTBEAT && type != MessageTypes.MGRACCOUNTINFOLITENOTIFY && type != MessageTypes.TICKNOTIFY)
            //{

            //    debug("raw message type:" + type.ToString() + " message:" + msg, QSEnumDebugLevel.INFO);
            //}
            //logger.Debug("Packet:" + msg.Content);
            IPacket packet = PacketHelper.CliRecvResponse(msg);
            switch (packet.Type)
            {
               
                case MessageTypes.TICKNOTIFY:
                    Tick t;
                    try
                    {
                        t = TickImpl.Deserialize2(msg.Content);
                    }
                    catch (Exception ex)
                    {
                        _tickerrors++;
                        logger.Info("Error processing tick: " + msg);
                        logger.Info("TickErrors: " + _tickerrors + " tickfields:" + msg.Content.Split(',').Length.ToString());
                        logger.Info("Error: " + ex.Message + ex.StackTrace);
                        break;
                    }
                    //debug("client got a tick:" + t.ToString(), QSEnumDebugLevel.INFO);
                    if(OnTick != null)
                    {
                        OnTick(t);
                    }
                    break;
                //行情心跳回报
                case MessageTypes.TICKHEARTBEAT:
                    {
                        _tickhartbeat = DateTime.Now.Ticks;
                        //debug("tickheartbeat:"+_tickhartbeat.ToString(),QSEnumDebugLevel.MUST);
                        break;
                    }

                //登入回报
                //case MessageTypes.LOGINRESPONSE:
                //    {
                //        updateheartbeat();
                //        CliOnLoginResponse(packet as LoginResponse);
                //    }
                //    break;
                //功能特征回报
                case MessageTypes.FEATURERESPONSE:
                    {
                        updateheartbeat();
                        CliOnFeatureResponse(packet as FeatureResponse);                        
                    }
                    break;
                //版本回报
                case MessageTypes.VERSIONRESPONSE:
                    {
                        updateheartbeat();
                        CliOnVersionResponse(packet as VersionResponse);
                    }
                    break;
                //心跳请求回报
                case MessageTypes.HEARTBEATRESPONSE:
                    {
                        updateheartbeat();
                        CliOnHeartbeatResponse(packet as HeartBeatResponse);
                    }
                    break;
                case MessageTypes.REGISTERCLIENTRESPONSE:
                    { 
                        RspRegisterClientResponse response = packet as RspRegisterClientResponse;
                        _sessionID = response.SessionID;

                        // request list of features from provider 请求功能支持列表
                        RequestFeatures();
                        //request server version;查询服务器版本
                        ReqServerVersion();
                        //当我们得到服务器版本后 我们设定 _reconnectreq = false; 这样 就不会一直进行重连

                    }
                    break;
                //其余逻辑数据包
                default:
                    {
                        updateheartbeat();
                        if (OnPacketEvent != null)
                        {
                            OnPacketEvent(packet);
                        }
                    }
                    break;
            }

        }
    }
}
