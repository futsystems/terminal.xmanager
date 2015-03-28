﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;


namespace TradingLib.MoniterCore
{
    /// <summary>
    /// 响应类回调特性
    /// 比如 提交查询然后服务端给返回
    /// 从而实现回调函数的自动注册与注销
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CallbackAttr:Attribute
    {

        public string Module { get; private set; }

        public string Cmd { get; private set; }

        public QSEnumCallbackTypes Type { get; private set; }

        public CallbackAttr(string module, string cmd,QSEnumCallbackTypes type = QSEnumCallbackTypes.Response)
        {
            this.Module = module;
            this.Cmd = cmd;
            this.Type = type;
        }
    }
}
