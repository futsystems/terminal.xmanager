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

        public void ReqQryRuleSet()
        {
            logger.Info("请求查询风控规则列表");
            this.ReqContribRequest("RiskCentre", "QryRuleSet", "");
        }

        public void ReqQryRuleItem(string account, QSEnumRuleType type)
        {
            logger.Info("请求查询帐户风控规则列表");
            this.ReqContribRequest("RiskCentre", "QryRuleItem", Mixins.Json.JsonMapper.ToJson(new {account=account,ruletype=type }));
        }
        public void ReqUpdateRuleItem(RuleItem item)
        {
            logger.Info("请求更新风控规则");
            this.ReqContribRequest("RiskCentre", "UpdateRuleItem", Mixins.Json.JsonMapper.ToJson(item));
        }

        public void ReqDelRuleItem(RuleItem item)
        {
            logger.Info("请求删除风控规则");
            this.ReqContribRequest("RiskCentre", "DelRuleItem", Mixins.Json.JsonMapper.ToJson(item));
        }
    }
}
