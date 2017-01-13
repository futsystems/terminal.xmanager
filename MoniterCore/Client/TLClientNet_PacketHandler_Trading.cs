using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;



namespace TradingLib.MoniterCore
{
    public partial class TLClientNet
    {
        void CliOnOldPositionNotify(HoldPositionNotify response)
        {
            PositionDetail pd = response.PositionDetail;
            pd.oSymbol = CoreService.BasicInfoTracker.GetSymbol(pd.Symbol);
            CoreService.TradingInfoTracker.GotHoldPosition(pd);
        }

        void CliOnOrderNotify(OrderNotify response)
        {
            Order o = response.Order;
            if (o != null)
            {
                o.oSymbol = CoreService.BasicInfoTracker.GetSymbol(o.Symbol);
            }
            CoreService.TradingInfoTracker.GotOrder(o);
        }

        void CliOnTradeNotify(TradeNotify response)
        {
            Trade f = response.Trade;
            if (f != null)
            {
                f.oSymbol = CoreService.BasicInfoTracker.GetSymbol(f.Symbol);
            }
            CoreService.TradingInfoTracker.GotFill(f);
        }

        void CliOnErrorOrderNotify(ErrorOrderNotify response)
        {

            Order o = response.Order;
            o.oSymbol = CoreService.BasicInfoTracker.GetSymbol(o.Symbol);
            CoreService.TradingInfoTracker.GotOrder(o);

            CoreService.EventCore.FireRspInfoEvent(response.RspInfo);
        }

    }
}
