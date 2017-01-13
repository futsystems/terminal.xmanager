using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;

namespace TradingLib.MoniterCore
{
    /// <summary>
    /// 全局服务单例
    /// 用于提供全局事件路由
    /// 底层TLClient调用对应EventInficator的Fire函数
    /// 订阅者通过订阅对应的事件集合的相关事件获得通知
    /// </summary>
    public  class CoreService
    {
        static CoreService defaultinstance = null;

        static CoreService()
        {
            defaultinstance = new CoreService();
        }

        EventIndicator _eventIndicator = null;

        /// <summary>
        /// 交易类事件
        /// </summary>
        public static EventIndicator EventIndicator
        { 
            get{
                if (defaultinstance._eventIndicator == null)
                    defaultinstance._eventIndicator = new EventIndicator();
                return defaultinstance._eventIndicator;
            }
        }

        EventContrib _eventContrib = null;
        /// <summary>
        /// 扩展类事件集合
        /// 此集合不对外暴露事件 而是通过注册回调的方式进行通知
        /// 比如某个控件进行查询操作，则需要将对应的响应回报函数注册到对应的命令上，当该命令获得返回时
        /// 注册函数被调用然后进行处理
        /// </summary>
        public static EventContrib EventContrib
        {
            get
            {
                if (defaultinstance._eventContrib == null)
                    defaultinstance._eventContrib = new EventContrib();
                return defaultinstance._eventContrib;
            }
        }



        EventOther _eventOther = null;
        /// <summary>
        /// 界面事件汇聚器
        /// </summary>
        public static EventOther EventOther
        {
            get
            {
                if (defaultinstance._eventOther == null)
                    defaultinstance._eventOther = new EventOther();
                return defaultinstance._eventOther;
            }
        }


        EventQuery _eventQuery = null;
        /// <summary>
        /// 查询事件汇聚
        /// </summary>
        public static EventQuery EventQuery
        {
            get
            {
                if (defaultinstance._eventQuery == null)
                    defaultinstance._eventQuery = new EventQuery();
                return defaultinstance._eventQuery;
            }
        }


    
        EventBasicInfo _eventBasicInfo = null;
        /// <summary>
        /// 基础数据事件集合
        /// 交易所，交易时间，品种合约，交易帐户，管理员列表，风控规则集等
        /// 相关数据的更新事件需要从该事件集订阅
        /// </summary>
        public static EventBasicInfo EventBasicInfo
        {
            get
            {
                if (defaultinstance._eventBasicInfo == null)
                    defaultinstance._eventBasicInfo = new EventBasicInfo();
                return defaultinstance._eventBasicInfo;
            }
        }

        BasicInfoTracker _basicinfortracker = null;

        /// <summary>
        /// 基础数据维护器
        /// 通过该维护器可以访问到基本数据 比如品种 合约 交易所等
        /// </summary>
        public static BasicInfoTracker BasicInfoTracker
        {
            get
            {
                if (defaultinstance._basicinfortracker == null)
                    defaultinstance._basicinfortracker = new BasicInfoTracker();
                return defaultinstance._basicinfortracker;
            }
        }

        //AccountTracker _accountTracker = null;
        ///// <summary>
        ///// 交易帐户维护器
        ///// </summary>
        //public static AccountTracker AccountTracker
        //{
        //    get
        //    {
        //        if (defaultinstance._accountTracker == null)
        //            defaultinstance._accountTracker = new AccountTracker();
        //        return defaultinstance._accountTracker;
        //    }
        //}

        TradingInfoTracker _tradinginfoTracker = null;

        public static TradingInfoTracker TradingInfoTracker
        {
            get
            {
                if (defaultinstance._tradinginfoTracker == null)
                    defaultinstance._tradinginfoTracker = new TradingInfoTracker();
                return defaultinstance._tradinginfoTracker;
            }
        }

        public static void InitClient(string address,int port)
        {
            if (defaultinstance._tlclient == null)
            {
                TLClientNet tlclient = new TLClientNet(new string[] { address }, port);

                defaultinstance._tlclient = tlclient;
            }
            //else
            //{ 
                
            //}
            //defaultinstance._tlclient.Start();

        }
        TLClientNet _tlclient = null;
        public static TLClientNet TLClient
        {
            get
            {
                return defaultinstance._tlclient;
            }
        }

        public static void Destory()
        {
            LogService.Debug("destory tlclient .........");
            if (defaultinstance._tlclient != null)
            {
                TLClient.Stop();
            }

        }

        EventCore _eventCore = null;
        /// <summary>
        /// 底层核心事件
        /// </summary>
        public static EventCore EventCore
        {
            get
            {
                if (defaultinstance._eventCore == null)
                    defaultinstance._eventCore = new EventCore();
                return defaultinstance._eventCore;
            }
        }

        EventAccount _eventAccount = null;
        /// <summary>
        /// 帐户类事件
        /// 比如登入状态变化，实时财务数据通知，交易帐户参数修改等
        /// </summary>
        public static EventAccount EventAccount
        {
            get
            {
                if (defaultinstance._eventAccount == null)
                    defaultinstance._eventAccount = new EventAccount();
                return defaultinstance._eventAccount;
            }
        }

        SiteInfo _siteInfo = null;
        /// <summary>
        /// 站点信息
        /// 用于记录登入管理员，柜台分区等全局信息
        /// </summary>
        public static SiteInfo SiteInfo
        {
            get
            {
                if (defaultinstance._siteInfo == null)
                    defaultinstance._siteInfo = new SiteInfo();
                return defaultinstance._siteInfo;
            }
        }

        /// <summary>
        /// 解析服务端返回回报
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ParseJsonResponse<T>(string json)
        {
            return json.DeserializeObject<T>();
            //JsonReply<T> reply = JsonReply.ParseReply<T>(json);
            //if (reply.Code == 0)
            //{
            //    return reply.Payload;
            //}
            //else
            //{
            //    return default(T);
            //}
        }

        public static Newtonsoft.Json.Linq.JToken ParseJsonResponse(string json)
        {
            return json.DeserializeObject();
            //return json.DeserializeObject()["Payload"];

            //JsonReply<T> reply = JsonReply.ParseReply<T>(json);
            //if (reply.Code == 0)
            //{
            //    return reply.Payload;
            //}
            //else
            //{
            //    return default(T);
            //}
        }
    }
}
