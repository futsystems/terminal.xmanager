using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;

namespace TradingLib.MoniterCore
{
    public partial class BasicInfoTracker
    {

        void Status(string msg)
        {
            CoreService.EventCore.FireInitializeStatusEvent(msg);
            logger.Info(msg);
        }


        void OnNotifyMarketTime(string json)
        {
            string mtmessage = json.DeserializeObject<string>();
            MarketTimeImpl mt = MarketTimeImpl.Deserialize(mtmessage);
            
            GotMarketTime(mt);
        }

        void OnRspMarketTime(string json, bool isLast)
        {
            string mtmessage = json.DeserializeObject<string>();
            MarketTimeImpl mt = MarketTimeImpl.Deserialize(mtmessage);

            GotMarketTime(mt);
            if (isLast && !_initialized)
            {
                Status("交易时间查询完毕,查询交易所信息");
                CoreService.TLClient.ReqQryExchange();
            }
        }
        void GotMarketTime(MarketTimeImpl mt)
        {
            if (mt != null)
            {
                MarketTimeImpl target = null;
                MarketTimeImpl notify = null;
                if (markettimemap.TryGetValue(mt.ID, out target))
                {
                    //更新
                    target.Name = mt.Name;
                    target.Description = mt.Description;
                    //target.TimeZone = mt.TimeZone;
                    target.DeserializeTradingRange(mt.SerializeTradingRange());
                    notify = target;
                }
                else
                {
                    markettimemap.Add(mt.ID, mt);
                    notify = mt;
                }

                ////对外触发
                //if (_initialized)
                //{
                //    CoreService.EventBasicInfo.FireMarketTime(notify);
                //}
            }
        }

        public void GotExchange(ExchangeImpl ex, bool islast)
        {
            if (ex != null)
            {
                ExchangeImpl target = null;
                ExchangeImpl notify = null;
                if (exchangemap.TryGetValue(ex.ID, out target))
                {
                    //更新
                    target.Name = ex.Name;
                    target.Country = ex.Country;
                    target.Calendar = ex.Calendar;
                    target.Title = ex.Title;
                    target.SettleType = ex.SettleType;
                    notify = target;
                }
                else
                {
                    exchangemap.Add(ex.ID, ex);
                    notify = ex;
                }
                //对外触发
                if (_initialized)
                {
                    CoreService.EventBasicInfo.FireExchangeEvent(notify);
                }
            }
            if (islast && !_initialized)
            {
                Status("交易所查询完毕,查询品种信息");
                CoreService.TLClient.ReqQrySecurity();
            }
        }

        /// <summary>
        /// 获得品种信息
        /// </summary>
        /// <param name="sec"></param>
        public void GotSecurity(SecurityFamilyImpl sec,bool islast)
        {
            if (sec != null)
            {
                SecurityFamilyImpl target = null;
                SecurityFamilyImpl notify = null;
                if (securitymap.TryGetValue(sec.ID, out target))
                {
                    //更新
                    target.Code = sec.Code;
                    target.Name = sec.Name;
                    target.Currency = sec.Currency;
                    target.Type = sec.Type;

                    target.exchange_fk = sec.exchange_fk;
                    target.Exchange = this.GetExchange(target.exchange_fk);

                    target.mkttime_fk = sec.mkttime_fk;
                    target.MarketTime = this.GetMarketTime(target.mkttime_fk);

                    target.underlaying_fk = sec.underlaying_fk;
                    target.UnderLaying = this.GetSecurity(target.underlaying_fk);

                    target.Multiple = sec.Multiple;
                    target.PriceTick = sec.PriceTick;
                    target.EntryCommission = sec.EntryCommission;
                    target.ExitCommission = sec.ExitCommission;
                    target.Margin = sec.Margin;
                    target.ExtraMargin = sec.ExtraMargin;
                    target.MaintanceMargin = sec.MaintanceMargin;
                    target.Tradeable = sec.Tradeable;
                    notify = target;
                }
                else
                {
                    securitymap.Add(sec.ID, sec);
                    notify = sec;
                }

                //对外触发
                if (_initialized)
                {
                    CoreService.EventBasicInfo.FireSecurityEvent(notify);
                }
            }

            if (islast && !_initialized)
            {
                Status("品种查询完毕,查询合约信息");
                CoreService.TLClient.ReqQrySymbol();
            }
        }

        /// <summary>
        /// 获得合约信息
        /// </summary>
        /// <param name="symbol"></param>
        public void GotSymbol(SymbolImpl symbol,bool islast)
        {
            if (symbol != null)
            {
                SymbolImpl target = null;
                SymbolImpl notify = null;
                if (symbolmap.TryGetValue(symbol.ID, out target))
                {
                    //更新
                    target.Symbol = symbol.Symbol;
                    target.Name = symbol.Name;
                    target.EntryCommission = symbol._entrycommission;
                    target.ExitCommission = symbol._exitcommission;
                    target.Margin = symbol._margin;
                    target.ExtraMargin = symbol._extramargin;
                    target.MaintanceMargin = symbol._maintancemargin;
                    target.Strike = symbol.Strike;
                    target.OptionSide = symbol.OptionSide;
                    target.ExpireDate = symbol.ExpireDate;

                    target.security_fk = symbol.security_fk;
                    target.SecurityFamily = this.GetSecurity(target.security_fk);

                    target.underlaying_fk = symbol.underlaying_fk;
                    target.ULSymbol = this.GetSymbol(target.underlaying_fk);

                    target.underlayingsymbol_fk = symbol.underlayingsymbol_fk;
                    target.UnderlayingSymbol = this.GetSymbol(target.underlayingsymbol_fk);
                    target.Tradeable = symbol.Tradeable;

                    notify = target;
                }
                else //添加
                {
                    symbolmap.Add(symbol.ID, symbol);
                    symbol.SecurityFamily = this.GetSecurity(symbol.security_fk);
                    symbol.ULSymbol = this.GetSymbol(symbol.underlaying_fk);
                    symbol.UnderlayingSymbol = this.GetSymbol(symbol.underlayingsymbol_fk);
                    symbolnammap[symbol.Symbol] = symbol;
                    notify = symbol;
                }

                //如果已经初始化完毕则需要对外触发事件
                if (_initialized)
                {
                    CoreService.EventBasicInfo.FireSymbolEvent(notify);
                }
            }
            if (islast && !_initialized)
            {
                Status("合约查询完毕,查询汇率信息");
                CoreService.TLClient.ReqQryExchangeRate();
            }

        }

        public void GotExchangeRate(ExchangeRate rate,bool isLast)
        {
            if (rate != null)
            {
                ExchangeRate target = null;
                if (exchangeRateaIDMap.TryGetValue(rate.ID, out target))
                {
                    target.AskRate = rate.AskRate;
                    target.IntermediateRate = rate.IntermediateRate;
                    target.BidRate = rate.BidRate;
                    target.UpdateTime = rate.UpdateTime;
                }
                else
                {
                    exchangeRateaIDMap.Add(rate.ID, rate);
                    exchangeRateCurrencyMap[rate.Currency] = rate;
                }
            }
            if (isLast && !_initialized)
            {
                Status("汇率查询完毕,查询委托风控规则");
                CoreService.TLClient.ReqQryRuleSet();
            }
        }
        

        void OnQryRuleSet(string json, bool islast)
        {
            RuleClassItem item = CoreService.ParseJsonResponse<RuleClassItem>(json);

            if (item.Type == QSEnumRuleType.OrderRule)
            {
                if (!orderruleclassmap.Keys.Contains(item.ClassName))
                {
                    orderruleclassmap[item.ClassName] = item;
                }
            }
            else if (item.Type == QSEnumRuleType.AccountRule)
            {
                if (!accountruleclassmap.Keys.Contains(item.ClassName))
                {
                    accountruleclassmap[item.ClassName] = item;
                }
            }

            if (islast && !_initialized)
            {
                Status("风控规则下载完毕，下载管理员列表");
                CoreService.TLClient.ReqQryManager();
            }
        }



        void GotManager(ManagerSetting manager)
        {
            ManagerSetting target = null;
            ManagerSetting notify = null;
            //如果本地已经有该Manager则进行信息更新
            if (managermap.TryGetValue(manager.ID, out target))
            {
                target.Mobile = manager.Mobile;
                target.Name = manager.Name;
                target.QQ = manager.QQ;
                target.Active = manager.Active;
                notify = target;
            }
            else//否则添加该Manager
            {
                managermap.Add(manager.ID, manager);
                notify = manager;
            }

            //将获得的柜员列表中 属于本登入mgr_fk的manager绑定到全局对象
            if (CoreService.SiteInfo.MGRID == manager.ID)
            {
                CoreService.SiteInfo.Manager = manager;
            }
            //对外触发 初始化过程中不对外出发
            if (_initialized)
            {
                CoreService.EventBasicInfo.FireManagerEvent(notify);
            }
        }


        void OnQryManager(string jsonstr, bool islast)
        {
            ManagerSetting[] mgrlist = CoreService.ParseJsonResponse<ManagerSetting[]>(jsonstr);
            if (mgrlist != null)
            {
                foreach (ManagerSetting mgr in mgrlist)
                {
                    this.GotManager(mgr);
                }
            }

            if (islast && !_initialized)
            {
                Status("柜员信息下载完成,下载路由组信息");
                CoreService.TLClient.ReqQryRouterGroup();
            }
        }

        void OnNotifyManagerUpdate(string json)
        {
            ManagerSetting mgr = CoreService.ParseJsonResponse<ManagerSetting>(json);
            if (mgr != null)
            {
                this.GotManager(mgr);
            }
        }

        /// <summary>
        /// 响应管理员删除回报 删除本地内存中的管理员数据
        /// </summary>
        /// <param name="json"></param>
        void OnNotifyManagerDelete(string json)
        {
            ManagerSetting mgr = CoreService.ParseJsonResponse<ManagerSetting>(json);
            if (mgr != null)
            {
                if (managermap.Keys.Contains(mgr.ID))
                {
                    managermap.Remove(mgr.ID);
                }
            }
        }
        #region 路由组
        void GotRouterGroup(RouterGroupSetting rg)
        {
            //Globals.Debug("got routergroup setting .....");
            if (rg == null) return;
            RouterGroupSetting target = null;

            if (rgmap.TryGetValue(rg.ID, out target))
            {
                //已经存在RouterGroup
                target.Description = rg.Description;
                target.Name = rg.Name;
                target.Strategy = rg.Strategy;
            }
            else
            {
                rgmap.Add(rg.ID, rg);
            }

            //对外触发获得路由组事件
            if (_initialized)
            {
                CoreService.EventBasicInfo.FireRouterGroupEvent(target != null ? target : rg);
            }
        }

        void OnQryRouterGroup(string jsonstr, bool islast)
        {
            RouterGroupSetting[] rglist = CoreService.ParseJsonResponse<RouterGroupSetting[]>(jsonstr);
            if (rglist != null)
            {
                foreach (RouterGroupSetting rg in rglist)
                {
                    GotRouterGroup(rg);
                }
            }
            if (islast && !_initialized)
            {
                Status("路由组信息下载完成,下载帐户信息");
                CoreService.TLClient.ReqQryAccountList();
            }
        }

        void OnNotifyRouterGroup(string jsonstr)
        {
            RouterGroupSetting rg = CoreService.ParseJsonResponse<RouterGroupSetting>(jsonstr);
            if (rg != null)
            {
                GotRouterGroup(rg);
            }
        }

        #endregion


        #region 汇率更新
        void OnNotifyExchangeRate(string json)
        {
            ExchangeRate obj = CoreService.ParseJsonResponse<ExchangeRate>(json);
            if (obj != null)
            {
                this.GotExchangeRate(obj,true);
            }
        }
        #endregion


        /// <summary>
        /// 管理端交易账户更新
        /// </summary>
        /// <param name="account"></param>
        //internal void OnMGRAccountUpdate(AccountItem account)
        //{
        //    //更新本地内存记录数据
        //    if (account != null)
        //    {
        //        accountmap[account.Account] = account;
        //    }

        //    CoreService.EventHub.FireAccountChangedEvent(account);
        //}

        //internal void OnMGRQryAccountList(AccountItem account, bool islast)
        //{
        //    if (account != null)
        //    {
        //        accountmap[account.Account] = account;
        //    }
            
        //    //
        //    if (islast && !_initialized)
        //    {
        //        Status("交易帐户列表同步成功");
        //        //结构化基础数据
        //        Bind();
        //        //输出基础数据数量信息
        //        logger.Info("============基础数据初始化完成============");
        //        logger.Info("     Markettime num:" + this.MarketTimes.Count().ToString());
        //        logger.Info("       Exchange num:" + this.Exchanges.Count().ToString());
        //        logger.Info("       Security num:" + this.Securities.Count().ToString());
        //        logger.Info("         Symbol num:" + this.Symbols.Count().ToString());
        //        logger.Info("        RuleSet num:" + (this.AccountRuleClass.Count() + this.OrderRuleClass.Count()).ToString());
        //        logger.Info("        Manager num:" + this.Managers.Count().ToString());
        //        logger.Info("    RouterGroup num:" + this.RouterGroups.Count().ToString());
        //        logger.Info("        Account num:" + this.Accounts.Count().ToString());

        //        //查询行情快照
        //        CoreService.TLClient.ReqQryTickSnapshot();

        //        CoreService.TLClient.StartTick();
        //        //触发数据初始化完成事件
        //        CoreService.EventCore.FireInitializedEvent();
        //    }
        //}








        /// <summary>
        /// 重新绑定外键对象，比如引用外键的对象在该对象之后到达，那么第一次绑定时候会产生失败
        /// 因此在第一次数据加载完毕时,需要重新进行绑定外键对象
        /// </summary>
        void Bind()
        {
            //Globals.LogicEvent.RegisterNotifyCallback("MgrExchServer", "NotifyManagerUpdate", OnManagerNotify);
            //Globals.LogicEvent.RegisterNotifyCallback("ConnectorManager", "NotifyRouterGroup", OnRouterGroupNotify);

            foreach (SecurityFamilyImpl target in securitymap.Values)
            {
                target.Exchange = this.GetExchange(target.exchange_fk);
                target.MarketTime = this.GetMarketTime(target.mkttime_fk);
                target.UnderLaying = this.GetSecurity(target.underlaying_fk);
            }

            foreach (SymbolImpl target in symbolmap.Values)
            {
                target.SecurityFamily = this.GetSecurity(target.security_fk);
                target.ULSymbol = this.GetSymbol(target.underlaying_fk);
                target.UnderlayingSymbol = this.GetSymbol(target.underlayingsymbol_fk);
            }
            _initialized = true;
            
        }

    }
}
