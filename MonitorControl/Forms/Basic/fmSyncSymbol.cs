using System;
using System.Collections.Generic;
using System.Collections;
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
    public partial class fmSyncSymbol : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmSyncSymbol()
        {
            InitializeComponent();
            this.Load += new EventHandler(fmSyncSymbol_Load);

        }

        void SetCutVendor()
        {
            if (CoreService.SiteInfo.Domain.CFG_SyncVendor_ID == 0)
            {
                this.cutVendor.Text = "从主域同步";
                this.ckSyncMainDomain.Checked = true;
            }
            else
            {
                if (vendormap.Keys.Contains(CoreService.SiteInfo.Domain.CFG_SyncVendor_ID))
                {
                    this.cutVendor.Text = string.Format("从帐户[{0}]同步", vendormap[CoreService.SiteInfo.Domain.CFG_SyncVendor_ID].Name);
                }
                else
                {
                    this.cutVendor.Text = "设置异常";
                }
            }
        }


        void fmSyncSymbol_Load(object sender, EventArgs e)
        {
            CoreService.EventCore.RegIEventHandler(this);
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
            ckSyncMainDomain.CheckedChanged += new EventHandler(ckSyncMainDomain_CheckedChanged);
        }

        void ckSyncMainDomain_CheckedChanged(object sender, EventArgs e)
        {
            cbVendor.Enabled = !ckSyncMainDomain.Checked;
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            VendorSetting vendor = (VendorSetting)cbVendor.SelectedValue;
            string msg = "确认默认从该实盘帐户:[" + vendor.Name + "] 同步合约数据?";
            string msg2 = "确认默认从主域同步合约数据?";
            if (MoniterHelper.WindowConfirm(ckSyncMainDomain.Checked?msg2:msg) == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqUpdateSyncVendor(ckSyncMainDomain.Checked?0:vendor.ID);
            }
        }

        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback("MgrExchServer", "QryVendor", this.OnQryVendor);
            //Globals.LogicEvent.GotDomainEvent += new Action<DomainImpl>(LogicEvent_GotDomainEvent);
            CoreService.TLClient.ReqQryVendor();
        }

        void LogicEvent_GotDomainEvent(DomainImpl obj)
        {
            SetCutVendor();
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("MgrExchServer", "QryVendor", this.OnQryVendor);
            //Globals.LogicEvent.GotDomainEvent -= new Action<DomainImpl>(LogicEvent_GotDomainEvent);
        }


        Dictionary<int, VendorSetting> vendormap = new Dictionary<int, VendorSetting>();

        bool _gotvendor = false;
        void OnQryVendor(string jsonstr, bool islast)
        {
            VendorSetting[] objs = MoniterHelper.ParseJsonResponse<VendorSetting[]>(jsonstr);
            if (objs != null)
            {
                MoniterHelper.AdapterToIDataSource(cbVendor).BindDataSource(GetVendorCBList(objs));
                foreach (VendorSetting vendor in objs)
                {
                    vendormap.Add(vendor.ID, vendor);
                }
                SetCutVendor();
                _gotvendor = true;
                
            }
        }

        public ArrayList GetVendorCBList(VendorSetting[] objs)
        {
            ArrayList list = new ArrayList();
            foreach (VendorSetting item in objs)
            {
                ValueObject<VendorSetting> vo = new ValueObject<VendorSetting>();
                vo.Name = item.Name;
                vo.Value = item;
                list.Add(vo);
            }
            return list;
        }



    }
}
