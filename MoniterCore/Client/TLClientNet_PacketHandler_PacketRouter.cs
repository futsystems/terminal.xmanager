using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;


namespace TradingLib.MoniterCore
{
    public delegate void RspMGRLoginResponseDel(RspMGRLoginResponse response);
    public partial class TLClientNet
    {
        void connecton_OnPacketEvent(IPacket packet)
        {
            //需要在所有回报处理之前 检查是否需要进行错误提示或者正常返回的消息显示
            if (packet is RspResponsePacket)
            {
                RspResponsePacket rsp = packet as RspResponsePacket;
                
                //错误回报 需要通过系统进行显示
                if (rsp.RspInfo.ErrorID != 0)
                {
                    //对外显示错误消息
                    CoreService.EventCore.FireRspInfoEvent(rsp.RspInfo);
                }
                else
                {
                    if (string.IsNullOrEmpty(rsp.RspInfo.ErrorMessage))
                    {
                        //logger.Info("操作成功 没有携带成功消息 静默");
                    }
                    else
                    { 
                        //对外显示成功消息
                        CoreService.EventCore.FireRspInfoEvent(rsp.RspInfo);
                    }
                }
            }
            switch (packet.Type)
            {
                #region Other
                case MessageTypes.MGR_RSP_LOGIN://管理登入回报
                    CliOnRspMGRLoginResponse(packet as RspMGRLoginResponse);
                    break;

                case MessageTypes.MGR_RSP_CONTRIB://管理扩展回报
                    CliOnMGRContribResponse(packet as RspMGRContribResponse);
                    break;

                case MessageTypes.MGR_RTN_CONTRIB://管理扩展通知
                    CliOnMGRContribNotify(packet as NotifyMGRContribNotify);
                    break;

                case MessageTypes.MGR_RSP://操作应答
                    CliOnRspMGRResponse(packet as RspMGRResponse);
                    break;
                #endregion

                #region Trading 交易记录通知 恢复账户当日交易记录或实时推送选中账户的实时交易记录
                //昨日持仓数据
                case MessageTypes.OLDPOSITIONNOTIFY:
                    CliOnOldPositionNotify(packet as HoldPositionNotify);
                    break;
                //委托回报
                case MessageTypes.ORDERNOTIFY:
                    CliOnOrderNotify(packet as OrderNotify);
                    break;
                case MessageTypes.ERRORORDERNOTIFY:
                    CliOnErrorOrderNotify(packet as ErrorOrderNotify);
                    break;
                //成交回报
                case MessageTypes.EXECUTENOTIFY:
                    CliOnTradeNotify(packet as TradeNotify);
                    break;
                #endregion

                default:
                    logger.Error("Packet Handler Not Set, Packet:" + packet.ToString());
                    break;
            }
        }
    }



}
