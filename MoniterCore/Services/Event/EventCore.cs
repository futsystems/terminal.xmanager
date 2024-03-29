﻿using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;
using Common.Logging;

namespace TradingLib.MoniterCore
{
    public class EventCore
    {
        ILog logger = LogManager.GetLogger("EventCore");

        /// <summary>
        /// 基础数据与帐户列表数据初始化完成事件
        /// </summary>
        public event VoidDelegate OnInitializedEvent;
        event VoidDelegate _OnInitializedEvent;
        internal void FireInitializedEvent()
        {
            //先调用本地初始化完成依赖回调
            if (_OnInitializedEvent != null)
            {
                _OnInitializedEvent();
            }
            //调用其他初始化完成事件订阅回调
            if (OnInitializedEvent != null)
                OnInitializedEvent();
        }


        /// <summary>
        /// 注册初始化完成依赖调用
        /// 在相关界面创建过程中 有些界面在初始化完成之前创建，有些在初始化完成之后创建
        /// 初始化完成之前创建的需要在初始化完成之后获得基础数据填充界面
        /// </summary>
        /// <param name="callback"></param>
        void RegisterInitializedCallBack(VoidDelegate callback)
        {
            if (!CoreService.BasicInfoTracker.Initialized)
            {
                _OnInitializedEvent += new VoidDelegate(callback);
            }
            else
            {
                callback();
            }
        }

        /// <summary>
        /// 注册EventHandler用于执行事件注册与延迟加载
        /// </summary>
        /// <param name="control"></param>
        public  void RegIEventHandler(object control)
        {
            if (control is UserControl)
            {
                if (control is IEventBinder)
                {
                    IEventBinder h = control as IEventBinder;
                    //注册初始化完成事件响应函数 用于响应初始化完成事件 当对象在初始化完成之前创建 需要在完成初始化后 加载基础数据
                    RegisterInitializedCallBack(h.OnInit);
                    //将组件销毁的事件与对应的注销函数进行绑定
                    (control as UserControl).Disposed += (s, e) => { h.OnDisposed(); };
                }
            }

            if (control is Form)
            {
                if (control is IEventBinder)
                {
                    IEventBinder h = control as IEventBinder;
                    //注册初始化完成事件响应函数 用于响应初始化完成事件 当对象在初始化完成之前创建 需要在完成初始化后 加载基础数据
                    RegisterInitializedCallBack(h.OnInit);
                    //将组件销毁的事件与对应的注销函数进行绑定
                    (control as Form).Disposed += (s, e) => { h.OnDisposed(); };
                }
            }
        }

        /// <summary>
        /// 通讯连接建立事件
        /// </summary>
        public event VoidDelegate OnConnectedEvent;
        internal void FireConnectedEvent()
        {
            if (OnConnectedEvent != null)
                OnConnectedEvent();
        }

        /// <summary>
        /// 通讯连接断开事件
        /// </summary>
        public event VoidDelegate OnDisconnectedEvent;
        internal void FireDisconnectedEvent()
        {
            if(OnDisconnectedEvent != null)
                OnDisconnectedEvent();
        }


        public event VoidDelegate OnDataConnectedEvent;
        internal void FireDataConnectedEvent()
        {
            if (OnDataConnectedEvent != null)
                OnDataConnectedEvent();
        }

        public event VoidDelegate OnDataDisconnectedEvent;
        internal void FireDataDisconnectedEvent()
        {
            if (OnDataDisconnectedEvent != null)
                OnDataDisconnectedEvent();
        }

        public event Action<RspMGRLoginResponse> OnLoginEvent;
        internal void FireLoginEvent(RspMGRLoginResponse response)
        {
            if (OnLoginEvent != null)
                OnLoginEvent(response);
        }


        /// <summary>
        /// 登入状态变化事件
        /// </summary>
        public event Action<string> OnInitializeStatusEvent;
        internal void FireInitializeStatusEvent(string msg)
        {
            if (OnInitializeStatusEvent != null)
                OnInitializeStatusEvent(msg);
        }



        /// <summary>
        /// 服务端返回通知信息
        /// </summary>
        public event Action<RspInfo> OnRspInfoEvent;
        internal void FireRspInfoEvent(RspInfo info)
        {
            if (OnRspInfoEvent != null)
                OnRspInfoEvent(info);
        }


        //public event Action<ManagerNotify> OnManagerNotifyEvent;
        //internal void FireManagerNotifyEvent(ManagerNotify notify)
        //{
        //    LogService.Debug("FireManagerNotifyEvent");

        //    RspInfo info = new TradingLib.Common.RspInfoImpl();
        //    info.ErrorID = notify.ErrorID;
        //    info.ErrorMessage = notify.ErrorMessage;

        //    //触发pop窗口
        //    FireRspInfoEvent(info);

        //    //触发其他监听事件
        //    if (OnManagerNotifyEvent != null)
        //        OnManagerNotifyEvent(notify);
        //}

        #region 扩展命令处理
        /// <summary>
        /// 注册Request回调函数
        /// </summary>
        /// <param name="module"></param>
        /// <param name="cmd"></param>
        /// <param name="del"></param>
        public void RegisterCallback(string module, string cmd, Action<string, bool> del)
        {
            string key = module.ToUpper() + "-" + cmd.ToUpper();

            if (!callbackmap.Keys.Contains(key))
            {
                callbackmap.TryAdd(key, new List<Action<string, bool>>());
            }
            callbackmap[key].Add(del);

        }

        /// <summary>
        /// 注册Notify回调函数
        /// </summary>
        /// <param name="module"></param>
        /// <param name="cmd"></param>
        /// <param name="del"></param>
        public void RegisterNotifyCallback(string module, string cmd, Action<string> del)
        {
            string key = module.ToUpper() + "-" + cmd.ToUpper();

            if (!notifycallbackmap.Keys.Contains(key))
            {
                notifycallbackmap.TryAdd(key, new List<Action<string>>());
            }

            notifycallbackmap[key].Add(del);

        }



        /// <summary>
        /// 注销Request回调函数
        /// </summary>
        /// <param name="module"></param>
        /// <param name="cmd"></param>
        /// <param name="del"></param>
        public void UnRegisterCallback(string module, string cmd, Action<string, bool> del)
        {
            string key = module.ToUpper() + "-" + cmd.ToUpper();

            if (!callbackmap.Keys.Contains(key))
            {
                callbackmap.TryAdd(key, new List<Action<string, bool>>());
            }

            if (callbackmap[key].Contains(del))
            {
                callbackmap[key].Remove(del);
            }
        }

        /// <summary>
        /// 注销Notify回调函数
        /// </summary>
        /// <param name="module"></param>
        /// <param name="cmd"></param>
        /// <param name="del"></param>
        public void UnRegisterNotifyCallback(string module, string cmd, Action<string> del)
        {
            string key = module.ToUpper() + "-" + cmd.ToUpper();

            if (!notifycallbackmap.Keys.Contains(key))
            {
                notifycallbackmap.TryAdd(key, new List<Action<string>>());
            }
            if (notifycallbackmap[key].Contains(del))
            {
                notifycallbackmap[key].Remove(del);
            }
        }


        ConcurrentDictionary<string, List<Action<string>>> notifycallbackmap = new ConcurrentDictionary<string, List<Action<string>>>();
        ConcurrentDictionary<string, List<Action<string, bool>>> callbackmap = new ConcurrentDictionary<string, List<Action<string, bool>>>();
        /// <summary>
        /// 响应服务端的扩展回报 通过扩展模块ID 操作码 以及具体的json回报内容
        /// </summary>
        /// <param name="module"></param>
        /// <param name="cmd"></param>
        /// <param name="result"></param>
        internal void GotMGRContribResponse(string module, string cmd, string result, bool islast)
        {
            string key = module.ToUpper() + "-" + cmd.ToUpper();
            if (callbackmap.Keys.Contains(key))
            {
                foreach (Action<string, bool> del in callbackmap[key])
                {
                    try
                    {
                        del(result, islast);
                    }
                    catch (Exception ex)
                    {
                        logger.Error("ContribResponse Callback Error", ex);
                    }
                }
            }
            else
            {
                logger.Warn("do not have any callback for " + key + " registed!");
            }
        }

        /// <summary>
        /// 响应服务端的通知回报
        /// </summary>
        /// <param name="module"></param>
        /// <param name="cmd"></param>
        /// <param name="result"></param>
        internal void GotMGRContribNotifyResponse(string module, string cmd, string result)
        {
            string key = module.ToUpper() + "-" + cmd.ToUpper();
            if (notifycallbackmap.Keys.Contains(key))
            {
                foreach (Action<string> del in notifycallbackmap[key])
                {
                    try
                    {
                        del(result);
                    }
                    catch (Exception ex)
                    {
                        logger.Error("ContribNotifyResponse Callback Error", ex);
                    }
                }
            }
            else
            {
                logger.Warn("do not have any callback for " + key + " registed!");
            }
        }
        #endregion
    }
}
