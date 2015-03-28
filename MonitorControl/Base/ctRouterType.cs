﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;

namespace TradingLib.MoniterControl
{
    public partial class ctRouterType : UserControl,IEventBinder
    {
        public event VoidDelegate RouterTypeSelectedChangedEvent;
        public ctRouterType()
        {
            InitializeComponent();
            this.Load += new EventHandler(ctRouterType_Load);
        }

        void ctRouterType_Load(object sender, EventArgs e)
        {
            CoreService.EventCore.RegIEventHandler(this);
            routeType.SelectedIndexChanged += new EventHandler(routeType_SelectedIndexChanged);
        }

        void routeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RouterTypeSelectedChangedEvent!= null)
                RouterTypeSelectedChangedEvent();
        }
        //属性获得和设置
        [DefaultValue(true)]
        bool _enableany = false;
        public bool EnableAny
        {
            get
            {
                return _enableany;
            }
            set
            {
                _enableany = value;
            }
        }

        public int SelectedIndex
        {
            get
            {
                return routeType.SelectedIndex;
            }
        }
        public QSEnumOrderTransferType RouterType
        {
            get
            {
                try
                {
                    return (QSEnumOrderTransferType)routeType.SelectedValue;
                }
                catch(Exception ex)
                {
                    return 0;
                }
            }
            set
            {
                try
                {
                    routeType.SelectedValue = value;
                }
                catch (Exception ex)
                { 
                
                }
            }
        }

       

        public void OnInit()
        {
            MoniterHelper.AdapterToIDataSource(routeType).BindDataSource(MoniterHelper.GetRouterTypeCombList(this.EnableAny));
        }

        public void OnDisposed()
        {

        }
    }
}
