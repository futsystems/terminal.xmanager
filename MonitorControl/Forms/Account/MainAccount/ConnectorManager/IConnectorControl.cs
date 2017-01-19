using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;

namespace TradingLib.MoniterControl
{
    public interface IConnectorControl
    {
        /// <summary>
        /// 将参数设置到输入控件
        /// </summary>
        /// <param name="cfg"></param>
        void SetConnectorConfig(ConnectorConfig cfg);

        /// <summary>
        /// 从输入控件获得参数
        /// </summary>
        /// <param name="cfg"></param>
        void GetConnectorConfig(ref ConnectorConfig cfg);

        /// <summary>
        /// 验证参数有效性
        /// </summary>
        /// <returns></returns>
        bool Valid();


        /// <summary>
        /// ID编号
        /// 用于拼接生成Token
        /// </summary>
        string ID { get; }

        /// <summary>
        /// ID编号变动事件
        /// 用于触发Token生成并查询是否有效
        /// </summary>
        event Action IDChanged;

        /// <summary>
        /// 前缀 用于拼接Token
        /// </summary>
        string Prefix { get; }
    }
}
