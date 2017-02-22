using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;

namespace TradingLib.MoniterControl
{
    public interface IGateWayControl
    {

        QSEnumGateWayType GateWayType { get; }

        void SetGatewayConfig(GateWayConfig config);

        string GetGateWayConfig();
    }
}
