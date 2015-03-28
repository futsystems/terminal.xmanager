﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;
using TradingLib.Mixins.JsonObject;

namespace TradingLib.MoniterControl
{
    public partial class ctFinServicePlanList : UserControl,IEventBinder
    {
        public event VoidDelegate ServicePlanSelectedChangedEvent;
        bool _gotdata = false;

        public ctFinServicePlanList()
        {
            InitializeComponent();

            this.Load += new EventHandler(ctFinServicePlanList_Load);
            
            
        }

        void ctFinServicePlanList_Load(object sender, EventArgs e)
        {
            CoreService.EventCore.RegIEventHandler(this);
            cbServicePlan.SelectedIndexChanged += new EventHandler(cbServicePlan_SelectedIndexChanged);
        }

        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback("FinServiceCentre", "QryFinServicePlan", this.OnQryServicePlan);
            CoreService.TLClient.ReqQryServicePlan();
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("FinServiceCentre", "QryFinServicePlan", this.OnQryServicePlan);
          
        }



        //响应服务端回报
        void OnQryServicePlan(string jsonstr, bool islast)
        {
            if (_gotdata) return;
            //JsonData jd = TradingLib.Mixins.LitJson.JsonMapper.ToObject(jsonstr);
            //int code = int.Parse(jd["Code"].ToString());
            JsonWrapperServicePlane[] plans = MoniterUtil.ParseJsonResponse<JsonWrapperServicePlane[]>(jsonstr);
            if (plans != null)
            {
                //JsonWrapperServicePlane[] plans = TradingLib.Mixins.LitJson.JsonMapper.ToObject<JsonWrapperServicePlane[]>(jd["Playload"].ToJson());
                SetServicePlans(plans);
                _gotdata = true;
            }
        }

        delegate void del1(JsonWrapperServicePlane[] plans);
        void SetServicePlans(JsonWrapperServicePlane[] plans)
        {

            if (InvokeRequired)
            {
                Invoke(new del1(SetServicePlans), new object[] { plans });
            }
            else
            {
                ArrayList list = new ArrayList();
                foreach (JsonWrapperServicePlane sp in plans)
                {
                    ValueObject<int> vo = new ValueObject<int>();
                    vo.Name = sp.Title;
                    vo.Value = sp.ID;
                    list.Add(vo);
                }
                MoniterHelper.AdapterToIDataSource(cbServicePlan).BindDataSource(list);
            }
        }


        /// <summary>
        /// 返回当前选中的ServicePlanFK
        /// </summary>
        public int CurrentServicePlanFK
        {
            get
            {
                return int.Parse(cbServicePlan.SelectedValue.ToString());
            }
        }

        /// <summary>
        /// 返回当前选中的ServicePlanName
        /// </summary>
        public string CurrentServicePlanName
        {
            get
            {
                return cbServicePlan.SelectedValue.ToString();
            }
        }
        /// <summary>
        /// 选择项变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbServicePlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ServicePlanSelectedChangedEvent != null && _gotdata)
                ServicePlanSelectedChangedEvent();
        }
    }
}
