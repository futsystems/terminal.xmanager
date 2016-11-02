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
        void CliOnOperationResponse(RspMGROperationResponse response)
        {
            //
            logger.Info("got operation response:" + response.ToString());
            CoreService.EventCore.FireRspInfoEvent(response.RspInfo);
        }

        #region 查询
        //void CliOnRspQryAccountInfoResponse(RspQryAccountInfoResponse response)
        //{
        //    logger.Info("------------帐户信息-------------");
        //    logger.Info("         Account:" + response.AccInfo.Account);
        //    logger.Info("LastEqutiy:" + response.AccInfo.LastEquity.ToString());
        //    logger.Info("NowEquity:" + response.AccInfo.NowEquity.ToString());
        //    logger.Info("RealizedPL:" + response.AccInfo.RealizedPL.ToString());
        //    logger.Info("UnRealizedPL:" + response.AccInfo.UnRealizedPL.ToString());
        //    logger.Info("Commission:" + response.AccInfo.Commission.ToString());
        //    logger.Info("Profit:" + response.AccInfo.Profit.ToString());
        //    logger.Info("CashIn:" + response.AccInfo.CashIn.ToString());
        //    logger.Info("CashOut:" + response.AccInfo.CashOut.ToString());
        //    logger.Info("MoneyUsed:" + response.AccInfo.MoneyUsed.ToString());
        //    logger.Info("TotalLiquidation:" + response.AccInfo.TotalLiquidation.ToString());
        //    logger.Info("AvabileFunds:" + response.AccInfo.AvabileFunds.ToString());
        //    logger.Info("Category:" + response.AccInfo.Category.ToString());
        //    logger.Info("OrderRouterType:" + response.AccInfo.OrderRouteType.ToString());
        //    logger.Info("Excute:" + response.AccInfo.Execute.ToString());
        //    logger.Info("Intraday:" + response.AccInfo.IntraDay.ToString());

        //    logger.Info("FutMarginUsed:" + response.AccInfo.FutMarginUsed.ToString());
        //    logger.Info("FutMarginFrozen:" + response.AccInfo.FutMarginFrozen.ToString());
        //    logger.Info("FutRealizedPL:" + response.AccInfo.FutRealizedPL.ToString());
        //    logger.Info("FutUnRealizedPL:" + response.AccInfo.FutUnRealizedPL.ToString());
        //    logger.Info("FutCommission:" + response.AccInfo.FutCommission.ToString());
        //    logger.Info("FutCash:" + response.AccInfo.FutCash.ToString());
        //    logger.Info("FutLiquidation:" + response.AccInfo.FutLiquidation.ToString());
        //    logger.Info("FutMoneyUsed:" + response.AccInfo.FutMoneyUsed.ToString());
        //    logger.Info("FutAvabileFunds:" + response.AccInfo.FutAvabileFunds.ToString());

        //    logger.Info("OptPositionCost:" + response.AccInfo.OptPositionCost.ToString());
        //    logger.Info("OptPositionValue:" + response.AccInfo.OptPositionValue.ToString());
        //    logger.Info("OptRealizedPL:" + response.AccInfo.OptRealizedPL.ToString());
        //    logger.Info("OptCommission:" + response.AccInfo.OptCommission.ToString());
        //    logger.Info("OptMoneyFrozen:" + response.AccInfo.OptMoneyFrozen.ToString());
        //    logger.Info("OptCash:" + response.AccInfo.OptCash.ToString());
        //    logger.Info("OptMarketValue:" + response.AccInfo.OptMarketValue.ToString());
        //    logger.Info("OptLiquidation:" + response.AccInfo.OptLiquidation.ToString());
        //    logger.Info("OptMoneyUsed:" + response.AccInfo.OptMoneyUsed.ToString());
        //    logger.Info("OptAvabileFunds:" + response.AccInfo.OptAvabileFunds.ToString());

        //    logger.Info("InnovPositionCost:" + response.AccInfo.InnovPositionCost.ToString());
        //    logger.Info("InnovPositionValue:" + response.AccInfo.InnovPositionValue.ToString());
        //    logger.Info("InnovCommission:" + response.AccInfo.InnovCommission.ToString());
        //    logger.Info("InnovRealizedPL:" + response.AccInfo.InnovRealizedPL.ToString());


        //}

        //void CliOnMaxOrderVol(RspQryMaxOrderVolResponse response)
        //{

        //}

        ///// <summary>
        ///// 查询委托回报
        ///// </summary>
        ///// <param name="response"></param>
        //void CliOnRspQryOrderResponse(RspQryOrderResponse response)
        //{

        //}
        ///// <summary>
        ///// 查询成交回报
        ///// </summary>
        ///// <param name="response"></param>
        //void CliOnRspQryTradeResponse(RspQryTradeResponse response)
        //{

        //}

        ///// <summary>
        ///// 查询持仓回报
        ///// </summary>
        ///// <param name="response"></param>
        //void CliOnRspQryPositionResponse(RspQryPositionResponse response)
        //{

        //}

        //void CliOnRspQryInvestorResponse(RspQryInvestorResponse response)
        //{

        //}

        


        #endregion


        #region 交易帐号类操作

        #endregion






        //#region 管理员管理

        //void CliOnMGRManagerResponse(RspMGRQryManagerResponse response)
        //{
        //    logger.Info("got manager response:" + response.ToString());
        //    //this.handler.OnMGRMangerResponse(response.ManagerToSend, response.IsLast);
        //}
        //#endregion


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
                //Tick数据
                //case MessageTypes.TICKNOTIFY:
                //    CliOnTickNotify(packet as TickNotify);
                //    break;
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
                //持仓更新回报
                case MessageTypes.POSITIONUPDATENOTIFY:
                    CliOnPositionUpdateNotify(packet as PositionNotify);
                    break;
                //委托操作回报
                //case MessageTypes.ORDERACTIONNOTIFY:
                //    CliOnOrderAction(packet as OrderActionNotify);
                //    break;

                case MessageTypes.ERRORORDERACTIONNOTIFY:
                    break;

                case MessageTypes.MGRLOGINRESPONSE://管理登入回报
                    CliOnRspMGRLoginResponse(packet as RspMGRLoginResponse);
                    break;
                //--------------Account --------------------------------------------
                case MessageTypes.MGRQRYACCOUNTSRESPONSE://管理查询帐户列表回报
                    CliOnMGRQryAccountList(packet as RspMGRQryAccountResponse);
                    break;
                case MessageTypes.MGRACCOUNTINFOLITENOTIFY://管理帐户财务数据更新
                    CliOnNotifyMGRAccountInfo(packet as NotifyMGRAccountInfoLiteResponse);
                    break;
                case MessageTypes.MGRRESUMEACCOUNTRESPONE://管理恢复帐户日内交易数据回报
                    CliOnMGRResumeAccountResponse(packet as RspMGRResumeAccountResponse);
                    break;
                case MessageTypes.MGRSESSIONSTATUSUPDATE://管理 交易帐户登入 退出信息更新
                    CliOnMGRSesssionUpdate(packet as NotifyMGRSessionUpdateNotify);
                    break;
                case MessageTypes.MGRACCOUNTINFORESPONSE://管理 查询交易帐户信息
                    //CliOnMGRQryAccountInfo(packet as RspMGRQryAccountInfoResponse);
                    break;
                case MessageTypes.MGRACCOUNTCHANGEUPDATE://交易客户端更改通知
                    CliOnMGRAccountUpdate(packet as NotifyMGRAccountChangeUpdateResponse);
                    break;

                case MessageTypes.MGREXCHANGERESPONSE://交易所列表回报
                    CliOnMGRExchange(packet as RspMGRQryExchangeResponse);
                    break;
                case MessageTypes.MGRUPDATEEXCHANGERESPONSE://更新交易所回报
                    CliOnMGRUpdateExchangeResponse(packet as RspMGRUpdateExchangeResponse);
                    break;
                case MessageTypes.MGRMARKETTIMERESPONSE://交易时间段回报
                    CliOnMGRMarketTime(packet as RspMGRQryMarketTimeResponse);
                    break;
                case MessageTypes.MGRUPDATEMARKETTIMERESPONSE://交易时间段更新回报
                    CliOnMGRUpdateMarketTimeResponse(packet as RspMGRUpdateMarketTimeResponse);
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
                case MessageTypes.MGRQRYEXCHANGERATERESPONSE://汇率信息汇报啊
                    CliOnMGRQryExchageRate(packet as RspMGRQryExchangeRateResponse);
                    break;
                case MessageTypes.MGRUPDATESYMBOLRESPONSE://合约更新回报
                    CliOnMGRUpdateSymbol(packet as RspMGRUpdateSymbolResponse);
                    break;
                

                case MessageTypes.MGRRULECLASSRESPONSE://风控规则回报
                    //CliOnMGRRuleClass(packet as RspMGRQryRuleSetResponse);
                    break;


                case MessageTypes.MGRRULEITEMRESPONSE://帐户风控项目回报
                    //CliOnMGRRuleItem(packet as RspMGRQryRuleItemResponse);
                    break;
                case MessageTypes.MGRUPDATERULEITEMRESPONSE://帐户风控更新回报
                    //CliOnMGRUpdateRuleItem(packet as RspMGRUpdateRuleResponse);
                    break;
                case MessageTypes.MGRDELRULEITEMRESPONSE://删除风控项回报
                    //CliOnMGRDelRule(packet as RspMGRDelRuleItemResponse);
                    break;
                //case MessageTypes.MGRSYSTEMSTATUSRESPONSE://查询系统状态回报
                //    CliOnMGRystemStatus(packet as RspMGRQrySystemStatusResponse);
                //    break;

                #region 查询历史交易记录
                case MessageTypes.MGRORDERRESPONSE://查询委托回报
                    CliOnMGROrderResponse(packet as RspMGRQryOrderResponse);
                    break;
                case MessageTypes.MGRTRADERESPONSE://查询成交回报
                    CliOnMGRTradeResponse(packet as RspMGRQryTradeResponse);
                    break;
                case MessageTypes.MGRPOSITIONRESPONSE://查询结算持仓回报
                    CliOnMGRPositionResponse(packet as RspMGRQryPositionResponse);
                    break;
                case MessageTypes.MGRCASHRESPONSE://查询出入金记录
                    CliOnMGRCashTransactionResponse(packet as RspMGRQryCashResponse);
                    break;
                case MessageTypes.MGRSETTLEMENTRESPONSE://查询结算单回报
                    CliOnMGRSettlementResponse(packet as RspMGRQrySettleResponse);
                    break;
                #endregion


                //case MessageTypes.MGRCHANGEACCOUNTPASSRESPONSE://修改密码回报
                //    CliOnMGRChangePassResponse(packet as RspMGRChangeAccountPassResponse);
                //    break;
                //case MessageTypes.MGRADDSECURITYRESPONSE://添加品种回报
                //    CliOnMGRAddSecurityResponse(packet as RspMGRReqAddSecurityResponse);
                    break;
                //case MessageTypes.MGRUPDATESYMBOLRESPONSE://更新合约回报
                //    CliOnMGRAddSymbolResponse(packet as RspMGRReqAddSymbolResponse);
                //    break;
                case MessageTypes.MGROPERATIONRESPONSE://常规操作回报
                    CliOnOperationResponse(packet as RspMGROperationResponse);
                    break;
                //case MessageTypes.MGRMANAGERRESPONSE://管理员查询回报
                //    CliOnMGRManagerResponse(packet as RspMGRQryManagerResponse);
                //    break;


                case MessageTypes.MGRCONTRIBRESPONSE://管理扩展回报
                    CliOnMGRContribResponse(packet as RspMGRContribResponse);
                    break;
                case MessageTypes.MGRCONTRIBRNOTIFY://管理扩展通知
                    CliOnMGRContribNotify(packet as NotifyMGRContribNotify);
                    break;

                default:
                    logger.Error("Packet Handler Not Set, Packet:" + packet.ToString());
                    break;
            }
        }
    }



}
