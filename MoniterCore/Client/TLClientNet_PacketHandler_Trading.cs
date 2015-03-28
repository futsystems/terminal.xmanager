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
            //logger.Debug("got holdposition notify " + response.PositionDetail.GetPositionDetailStr());
            PositionDetail pd = response.PositionDetail;

            pd.oSymbol = CoreService.BasicInfoTracker.GetSymbol(pd.Symbol);
            CoreService.TradingInfoTracker.GotHoldPosition(pd);
        }

        void CliOnOrderNotify(OrderNotify response)
        {
            //logger.Debug("got order notify:" + response.Order.ToString());
            Order o = response.Order;
            o.oSymbol = CoreService.BasicInfoTracker.GetSymbol(o.Symbol);
            CoreService.TradingInfoTracker.GotOrder(o);
        }

        void CliOnTradeNotify(TradeNotify response)
        {
            //logger.Debug("got trade notify:" + response.Trade.ToString());
            Trade f = response.Trade;
            f.oSymbol = CoreService.BasicInfoTracker.GetSymbol(f.Symbol);
            CoreService.TradingInfoTracker.GotFill(f);
        }

        void CliOnPositionUpdateNotify(PositionNotify response)
        {
            //debug("got postion notify:" + response.Position.ToString(), QSEnumDebugLevel.INFO);
            //this.handler.OnPositionUpdate(response.Position);
        }

        void CliOnErrorOrderNotify(ErrorOrderNotify response)
        {
            //logger.Debug(string.Format("got order error:{0} message:{1} order:{2}", response.RspInfo.ErrorID, response.RspInfo.ErrorMessage, OrderImpl.Serialize(response.Order)));
            Order o = response.Order;
            o.oSymbol = CoreService.BasicInfoTracker.GetSymbol(o.Symbol);
            CoreService.TradingInfoTracker.GotOrder(o);

            CoreService.EventCore.FireRspInfoEvent(response.RspInfo);
        }

    }
}
