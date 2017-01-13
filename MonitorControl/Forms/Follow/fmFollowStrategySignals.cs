using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;

namespace TradingLib.MoniterControl
{
    public partial class fmFollowStrategySignals : ComponentFactory.Krypton.Toolkit.KryptonForm, IEventBinder
    {
        public fmFollowStrategySignals()
        {
            InitializeComponent();
            this.Load += new EventHandler(fmFollowStrategySignals_Load);
        }

        void fmFollowStrategySignals_Load(object sender, EventArgs e)
        {
            signals.ValueMember = "Value";
            signals.DisplayMember = "Name";
            avabileSignals.ValueMember = "Value";
            avabileSignals.DisplayMember = "Name";

            //全局事件回调
            CoreService.EventCore.RegIEventHandler(this);

            btnAppend.Click += new EventHandler(btnAppend_Click);
            btnRemove.Click += new EventHandler(btnRemove_Click);
        }


        FollowStrategyConfig _cfg = null;
        public void SetFollowStrategyConfig(FollowStrategyConfig cfg)
        {
            _cfg = cfg;
            this.Text = string.Format("修改跟单策略:{0}[{1}]信号列表", cfg.ID, cfg.Token);

        }
        void btnRemove_Click(object sender, EventArgs e)
        {
            if (avabileSignals.SelectedItems != null)
            {
                foreach (var item in avabileSignals.SelectedItems)
                {
                    signals.Items.Add(item);
                    RemoveSignalFromStrategy((item as ValueObject<SignalConfig>).Value.ID, _cfg.ID);
                }
            }
            for (int i = 0; i < avabileSignals.SelectedIndices.Count; i++)
            {
                avabileSignals.Items.RemoveAt(avabileSignals.SelectedIndices[i]);
                i--;
            }
        }

        void btnAppend_Click(object sender, EventArgs e)
        {
            if (signals.SelectedItems != null)
            {
                foreach (var item in signals.SelectedItems)
                {
                    avabileSignals.Items.Add(item);
                    AddSignalToStrategy((item as ValueObject<SignalConfig>).Value.ID, _cfg.ID);
                }
            }
            for (int i = 0; i < signals.SelectedIndices.Count; i++)
            {
                signals.Items.RemoveAt(signals.SelectedIndices[i]);
                i--;
            }
        }


        void AddSignalToStrategy(int signalID, int strategyID)
        {
            CoreService.TLClient.ReqContribRequest("FollowCentre", "AppendSignalToStrategy", string.Format("{0},{1}", signalID, strategyID));
        }

        void RemoveSignalFromStrategy(int signalID, int strategyID)
        {
            CoreService.TLClient.ReqContribRequest("FollowCentre", "RemoveSignalFromStrategy", string.Format("{0},{1}", signalID, strategyID));
        }


        public void OnInit()
        {

            CoreService.EventCore.RegisterCallback("FollowCentre", "QrySignalConfigList", OnQrySignalconfigList);
            CoreService.EventCore.RegisterCallback("FollowCentre", "QryStrategySignalList", OnQryStrategySignalList);

            
            CoreService.TLClient.ReqContribRequest("FollowCentre", "QryStrategySignalList", _cfg.ID.ToString());
        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback("FollowCentre", "QrySignalConfigList", OnQrySignalconfigList);
            CoreService.EventCore.UnRegisterCallback("FollowCentre", "QryStrategySignalList", OnQryStrategySignalList);

        }

        void OnQrySignalconfigList(string json, bool islast)
        {
            SignalConfig item = CoreService.ParseJsonResponse<SignalConfig>(json);
            if (item != null)
            {
                //跟单信号列表中不包含的信号才在可用信号列表中显示
                if (!strategySignalList.Contains(item.ID))
                {
                    InvokeGotSignalConfig(item);
                }
            }
            
        }


        void InvokeGotSignalConfig(SignalConfig item)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<SignalConfig>(InvokeGotSignalConfig), new object[] { item });
            }
            else
            {
                ValueObject<SignalConfig> vo = new ValueObject<SignalConfig>();
                vo.Name = string.Format("ID:{0}Token:{1}", item.ID, item.SignalToken);
                vo.Value = item;
                signals.Items.Add(vo);

            }
        }

        List<int> strategySignalList = new List<int>();

        void OnQryStrategySignalList(string json, bool islast)
        {
            SignalConfig item = CoreService.ParseJsonResponse<SignalConfig>(json);
            if (item != null)
            {
                InvokeGotStrategySignalConfig(item);
                strategySignalList.Add(item.ID);
            }
            if (islast)
            {
                //CoreService.TLClient.ReqContribRequest("FollowCentre", "QryExitFollowItemList", "1");
                //查询跟单信号列表完毕后再查询所有信号源列表
                CoreService.TLClient.ReqContribRequest("FollowCentre", "QrySignalConfigList", "");
            }
        }


        void InvokeGotStrategySignalConfig(SignalConfig item)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<SignalConfig>(InvokeGotStrategySignalConfig), new object[] { item });
            }
            else
            {
                ValueObject<SignalConfig> vo = new ValueObject<SignalConfig>();
                vo.Name = string.Format("ID:{0}Token:{1}", item.ID, item.SignalToken);
                vo.Value = item;
                avabileSignals.Items.Add(vo);

            }
        }

    }


}
