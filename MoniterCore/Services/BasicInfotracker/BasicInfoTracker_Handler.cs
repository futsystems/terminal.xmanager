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

        #region MarketTime
        void OnNotifyMarketTime(string json)
        {
            string content = json.DeserializeObject<string>();
            MarketTimeImpl mt = MarketTimeImpl.Deserialize(content);
            GotMarketTime(mt);
        }

        void OnRspMarketTime(string json, bool isLast)
        {
            string content = json.DeserializeObject<string>();
            MarketTimeImpl mt = MarketTimeImpl.Deserialize(content);
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
                if (markettimemap.TryGetValue(mt.ID, out target))
                {
                    target.Name = mt.Name;
                    target.Description = mt.Description;
                    target.CloseTime = mt.CloseTime;
                    target.DeserializeTradingRange(mt.SerializeTradingRange());
                }
                else
                {
                    markettimemap.Add(mt.ID, mt);
                }
            }
        }
        #endregion

        #region Exchange
        void OnNotifyExchange(string json)
        {
            string content = json.DeserializeObject<string>();
            ExchangeImpl ex = ExchangeImpl.Deserialize(content);
            GotExchange(ex);
        }

        void OnRspExchange(string json, bool isLast)
        {
            string content = json.DeserializeObject<string>();
            ExchangeImpl ex = ExchangeImpl.Deserialize(content);
            GotExchange(ex);
            if (isLast && !_initialized)
            {
                Status("交易所查询完毕,查询品种信息");
                CoreService.TLClient.ReqQrySecurity();
            }
        }

        void GotExchange(ExchangeImpl ex)
        {
            if (ex != null)
            {
                ExchangeImpl target = null;
                if (exchangemap.TryGetValue(ex.ID, out target))
                {
                    target.Name = ex.Name;
                    target.Country = ex.Country;
                    target.Calendar = ex.Calendar;
                    target.Title = ex.Title;
                    target.SettleType = ex.SettleType;
                    target.CloseTime = ex.CloseTime;
                    target.DataFeed = ex.DataFeed;
                    target.TimeZoneID = ex.TimeZoneID;
                }
                else
                {
                    exchangemap.Add(ex.ID, ex);
                }
            }
        }
        #endregion


        #region Security
        void OnRspSecurity(string json, bool isLast)
        {
            string content = json.DeserializeObject<string>();
            SecurityFamilyImpl sec = SecurityFamilyImpl.Deserialize(content);
            GotSecurity(sec);
            if (isLast && !_initialized)
            {
                Status("品种查询完毕,查询合约信息");
                CoreService.TLClient.ReqQrySymbol();
            }
        }

        void OnNotifySecurity(string json)
        {
            string content = json.DeserializeObject<string>();
            SecurityFamilyImpl sec = SecurityFamilyImpl.Deserialize(content);
            GotSecurity(sec);
        }
        /// <summary>
        /// 获得品种信息
        /// </summary>
        /// <param name="sec"></param>
        void GotSecurity(SecurityFamilyImpl sec)
        {
            if (sec != null)
            {
                SecurityFamilyImpl target = null;
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
                }
                else
                {
                    securitymap.Add(sec.ID, sec);
                }
            }

           
        }
        #endregion

        #region Symbol
        void OnNotifySymbol(string json)
        {
            string content = json.DeserializeObject<string>();
            SymbolImpl sym = SymbolImpl.Deserialize(content);
            GotSymbol(sym);
        }

        void OnRspSymbol(string json, bool isLast)
        {
            string content = json.DeserializeObject<string>();
            SymbolImpl sym = SymbolImpl.Deserialize(content);
            GotSymbol(sym);
            if (isLast && !_initialized)
            {
                Status("合约查询完毕,查询汇率信息");
                CoreService.TLClient.ReqQryExchangeRate();
            }
        }

        /// <summary>
        /// 获得合约信息
        /// </summary>
        /// <param name="symbol"></param>
        void GotSymbol(SymbolImpl symbol)
        {
            if (symbol != null)
            {
                SymbolImpl target = null;
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
                }
                else //添加
                {
                    symbolmap.Add(symbol.ID, symbol);
                    symbol.SecurityFamily = this.GetSecurity(symbol.security_fk);
                    symbol.ULSymbol = this.GetSymbol(symbol.underlaying_fk);
                    symbol.UnderlayingSymbol = this.GetSymbol(symbol.underlayingsymbol_fk);
                    symbolnammap[symbol.Symbol] = symbol;
                }
            }
        }
        #endregion

        #region ExchangeRate
        void OnRspExchangeRate(string json, bool isLast)
        {
            ExchangeRate obj = CoreService.ParseJsonResponse<ExchangeRate>(json);
            this.GotExchangeRate(obj);
            if (isLast && !_initialized)
            {
                Status("汇率查询完毕,查询委托风控规则");
                CoreService.TLClient.ReqQryRuleSet();
            }
        }
        void OnNotifyExchangeRate(string json)
        {
            ExchangeRate obj = CoreService.ParseJsonResponse<ExchangeRate>(json);
            this.GotExchangeRate(obj);

        }

        void GotExchangeRate(ExchangeRate rate)
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
        }
        #endregion

        #region RuleSet
        void OnRspRuleSet(string json, bool islast)
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
                Status("风控规则下载完毕，下载路由组信息");
                CoreService.TLClient.ReqQryRouterGroup();
            }
        }
        #endregion

        #region RouterGroup


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
                Status("路由组信息下载完成,下载管理员列表");
                CoreService.TLClient.ReqQryManager();
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

        void GotRouterGroup(RouterGroupSetting rg)
        {
            if (rg == null) return;
            RouterGroupSetting target = null;

            if (rgmap.TryGetValue(rg.ID, out target))
            {
                target.Description = rg.Description;
                target.Name = rg.Name;
                target.Strategy = rg.Strategy;
            }
            else
            {
                rgmap.Add(rg.ID, rg);
            }
        }

        #endregion

        #region Manager
        void OnRspManager(string jsonstr, bool islast)
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
                Status("柜员信息下载完成,下载账户列表");
                CoreService.TLClient.ReqQryAccountList();
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


        void GotManager(ManagerSetting manager)
        {
            ManagerSetting target = null;
            ManagerSetting notify = null;
            if (managermap.TryGetValue(manager.ID, out target))
            {
                target.Mobile = manager.Mobile;
                target.Name = manager.Name;
                target.QQ = manager.QQ;
                target.Active = manager.Active;
                notify = target;
            }
            else
            {
                managermap.Add(manager.ID, manager);
                notify = manager;
            }

            //将获得的柜员列表中 属于本登入mgr_fk的manager绑定到全局对象
            if (CoreService.SiteInfo.MGRID == manager.ID)
            {
                CoreService.SiteInfo.Manager = manager;
            }
        }
        #endregion


        #region Account
        void OnQryAccountList(string json, bool isLast)
        {
            AccountItem item = CoreService.ParseJsonResponse<AccountItem>(json);
            if (item != null)
            {
                accountmap[item.Account] = item;
            }
            if (isLast && !_initialized)
            {
                Status("交易帐户列表同步成功");
                //结构化基础数据
                Bind();
                //输出基础数据数量信息
                logger.Info("============基础数据初始化完成============");
                logger.Info("     Markettime num:" + this.MarketTimes.Count().ToString());
                logger.Info("       Exchange num:" + this.Exchanges.Count().ToString());
                logger.Info("       Security num:" + this.Securities.Count().ToString());
                logger.Info("         Symbol num:" + this.Symbols.Count().ToString());
                logger.Info("   ExchangeRate num:" + this.ExchangeRates.Count().ToString());
                logger.Info("        RuleSet num:" + (this.AccountRuleClass.Count() + this.OrderRuleClass.Count()).ToString());
                logger.Info("    RouterGroup num:" + this.RouterGroups.Count().ToString());
                logger.Info("        Manager num:" + this.Managers.Count().ToString());
                logger.Info("        Account num:" + this.Accounts.Count().ToString());

                //查询行情快照
                CoreService.TLClient.ReqQryTickSnapshot();
                CoreService.TLClient.StartTick();
                //触发数据初始化完成事件
                CoreService.EventCore.FireInitializedEvent();
            }
        }

        void OnAccountChanged(string json)
        {
            AccountItem item = CoreService.ParseJsonResponse<AccountItem>(json);
            if (item != null)
            {
                accountmap[item.Account] = item;
            }
        }
        #endregion

        /// <summary>
        /// 重新绑定外键对象，比如引用外键的对象在该对象之后到达，那么第一次绑定时候会产生失败
        /// 因此在第一次数据加载完毕时,需要重新进行绑定外键对象
        /// </summary>
        void Bind()
        {
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
