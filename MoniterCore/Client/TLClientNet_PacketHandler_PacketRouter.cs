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
                case MessageTypes.MGRLOGINRESPONSE://管理登入回报
                    CliOnRspMGRLoginResponse(packet as RspMGRLoginResponse);
                    break;

                case MessageTypes.MGRCONTRIBRESPONSE://管理扩展回报
                    CliOnMGRContribResponse(packet as RspMGRContribResponse);
                    break;

                case MessageTypes.MGRCONTRIBRNOTIFY://管理扩展通知
                    CliOnMGRContribNotify(packet as NotifyMGRContribNotify);
                    break;

                case MessageTypes.MGROPERATIONRESPONSE://操作统一回报
                    CliOnOperationResponse(packet as RspMGROperationResponse);
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

                #region Account 交易账户 查询 财务统计更新 修改更新 日内交易记录恢复
                //case MessageTypes.MGRQRYACCOUNTSRESPONSE://管理查询帐户列表回报
                //    CliOnMGRQryAccountList(packet as RspMGRQryAccountResponse);
                //    break;
                //case MessageTypes.MGRACCOUNTINFOLITENOTIFY://管理帐户财务数据更新
                //    CliOnNotifyMGRAccountStatistic(packet as NotifyMGRAccountStatistic);
                //    break;
                case MessageTypes.MGRRESUMEACCOUNTRESPONE://管理恢复帐户日内交易数据回报
                    CliOnMGRResumeAccountResponse(packet as RspMGRResumeAccountResponse);
                    break;
                //case MessageTypes.MGRACCOUNTCHANGEUPDATE://交易账户更改通知
                //    CliOnMGRAccountUpdate(packet as NotifyMGRAccountChangeUpdateResponse);
                //    break;
                #endregion

                #region BasicInfo 基础数据查询与更新回报
                case MessageTypes.MGRMARKETTIMERESPONSE://交易时间段回报
                    CliOnMGRMarketTime(packet as RspMGRQryMarketTimeResponse);
                    break;
                case MessageTypes.MGRUPDATEMARKETTIMERESPONSE://交易时间段更新回报
                    CliOnMGRUpdateMarketTimeResponse(packet as RspMGRUpdateMarketTimeResponse);
                    break;
                case MessageTypes.MGREXCHANGERESPONSE://交易所列表回报
                    CliOnMGRExchange(packet as RspMGRQryExchangeResponse);
                    break;
                case MessageTypes.MGRUPDATEEXCHANGERESPONSE://更新交易所回报
                    CliOnMGRUpdateExchangeResponse(packet as RspMGRUpdateExchangeResponse);
                    break;
                
                case MessageTypes.MGRSECURITYRESPONSE://品种回报
                    CliOnMGRSecurity(packet as RspMGRQrySecurityResponse);
                    break;
                case MessageTypes.MGRUPDATESECURITYRESPONSE://更新品种回报
                    CliOnMGRUpdateSecurity(packet as RspMGRUpdateSecurityResponse);
                    break;
                case MessageTypes.MGRSYMBOLRESPONSE://合约回报
                    CliOnMGRQrySymbol(packet as RspMGRQrySymbolResponse);
                    break;
                case MessageTypes.MGRUPDATESYMBOLRESPONSE://合约更新回报
                    CliOnMGRUpdateSymbol(packet as RspMGRUpdateSymbolResponse);
                    break;
                case MessageTypes.MGRQRYEXCHANGERATERESPONSE://汇率信息汇报啊
                    CliOnMGRQryExchageRate(packet as RspMGRQryExchangeRateResponse);
                    break;
                case MessageTypes.MGRQRYTICKSNAPSHOTRESPONSE://行情快照更新
                    CliOnMGRQryTickSnapshot(packet as RspMGRQryTickSnapShotResponse);
                    break;
                #endregion

               
                default:
                    logger.Error("Packet Handler Not Set, Packet:" + packet.ToString());
                    break;
            }
        }
    }



}
