using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.Mixins.Json;

namespace TradingLib.MoniterCore
{
    public class MoniterUtil
    {

        /// <summary>
        /// 解析返回的Json数据到对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ParseJsonResponse<T>(string json)
        {
            JsonReply<T> reply = JsonReply.ParseReply<T>(json);
            if (reply.Code == 0)
            {
                return reply.Payload;
            }
            else
            {
                return default(T);
            }
        }
    }
}
