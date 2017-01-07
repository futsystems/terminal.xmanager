﻿//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
//using TradingLib.API;
//using TradingLib.Common;
//using TradingLib.Mixins.Json;


//namespace TradingLib.MoniterControl
//{
 
//    /// <summary>
//    /// 管理端动态控件的基类
//    /// 其余控件需从该控件进行集成并获得对系统的相关调用
//    /// 设计架构如下
//    /// MonitorControl 做成高内聚的组件
//    /// 1.响应用户输入的请求 形成服务端扩展请求调用通过 ReqContribRequest对外发送
//    /// 2.编写响应函数 用特性标记，系统在加载时自动获得对应的回调函数并注册到回调中心 当服务端有消息回报时自动回调该函数
//    /// </summary>
//    public partial class MoniterControl : UserControl,IMoniterControl
//    {
//        public MoniterControl()
//        {
//            InitializeComponent();            
//        }

//        IMGRClient _client = null;

//        /// <summary>
//        /// 注入管理端核心通讯组件
//        /// 这样本
//        /// </summary>
//        /// <param name="client"></param>
//        public void SetClient(IMGRClient client)
//        {
//            _client = client;
//        }

//        public virtual string Title
//        {
//            get
//            {
//                return "BaseMonitorControl";
//            }
//        }
//        /// <summary>
//        /// 提交请求
//        /// </summary>
//        /// <param name="module">模块标识</param>
//        /// <param name="cmd">命令标识</param>
//        /// <param name="args">参数</param>
//        protected void Request(string module, string cmd, string args)
//        {
//            MoniterHelper.Request(module, cmd, args);
//        }

//        /// <summary>
//        /// 以Json格式发送请求
//        /// </summary>
//        /// <param name="module"></param>
//        /// <param name="cmd"></param>
//        /// <param name="obj"></param>
//        protected void JsonRequest(string module, string cmd, object obj)
//        {
//            this.Request(module, cmd, JsonMapper.ToJson(obj));
//        }

//        /// <summary>
//        /// 对外输出日志
//        /// </summary>
//        /// <param name="message"></param>
//        /// <param name="level"></param>
//        //protected void Log(string message, QSEnumDebugLevel level)
//        //{
//        //    Util.Log(new LogItem(message, level, this.GetType().Name));
//        //}




//    }
//}
