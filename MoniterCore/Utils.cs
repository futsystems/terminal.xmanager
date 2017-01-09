using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.Common;

namespace TradingLib.MoniterCore
{
    public class MoniterUtil
    {
        public static string AnyCBStr = "所有";

        ///// <summary>
        ///// 解析返回的Json数据到对象
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="json"></param>
        ///// <returns></returns>
        //public static T ParseJsonResponse<T>(string json)
        //{
        //    JsonReply<T> reply = JsonReply.ParseReply<T>(json);
        //    if (reply.Code == 0)
        //    {
        //        return reply.Payload;
        //    }
        //    else
        //    {
        //        return default(T);
        //    }
        //}
    }
}
