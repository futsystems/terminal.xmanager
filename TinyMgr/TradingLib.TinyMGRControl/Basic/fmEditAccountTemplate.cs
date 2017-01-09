using System;
using System.Linq;
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


namespace TradingLib.TinyMGRControl
{
    public partial class fmBindAccountCommissionTemplate : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmBindAccountCommissionTemplate()
        {
            InitializeComponent();
            this.Load += new EventHandler(fmEditAccountTemplate_Load);
        }

        void fmEditAccountTemplate_Load(object sender, EventArgs e)
        {
            btnSubmit.Click += new EventHandler(btnSubmit_Click);

            CoreService.EventCore.RegIEventHandler(this);
        }

        public void OnInit()
        {
            cbCommissionTemplate.Enabled = CoreService.SiteInfo.UIAccess.r_commission;
            CoreService.EventContrib.RegisterCallback("MgrExchServer", "QryCommissionTemplate", this.OnQryCommissionTemplate);
            CoreService.TLClient.ReqQryCommissionTemplate();
            
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("MgrExchServer", "QryCommissionTemplate", this.OnQryCommissionTemplate);
       
        }

        bool AnyMathItem(ComponentFactory.Krypton.Toolkit.KryptonComboBox cb, int id)
        {
            foreach (var item in cb.Items)
            {
                if (((ValueObject<int>)item).Value == id)
                    return true;
            }
            return false;
        }


        void OnQryCommissionTemplate(string json, bool islast)
        {
            CommissionTemplateSetting[] list = CoreService.ParseJsonResponse<CommissionTemplateSetting[]>(json);
            if (list != null)
            {
                MoniterHelper.AdapterToIDataSource(cbCommissionTemplate).BindDataSource(GetCommissionTemplateCBList(list));
                int commissoinid = AnyMathItem(cbCommissionTemplate, _account.Commissin_ID) ? _account.Commissin_ID : 0;
                cbCommissionTemplate.SelectedValue = commissoinid;
            }
        }


        static ArrayList GetCommissionTemplateCBList(CommissionTemplateSetting[] items)
        {
            ArrayList list = new ArrayList();
            ValueObject<int> vo1 = new ValueObject<int>();
            vo1.Name = "系统默认";
            vo1.Value = 0;
            list.Add(vo1);

            foreach (CommissionTemplateSetting item in items)
            {
                ValueObject<int> vo = new ValueObject<int>();
                vo.Name = string.Format("{0}{1}", item.Name, item.Manager_ID != CoreService.SiteInfo.Manager.ID ? "*" : "");
                vo.Value = item.ID;
                list.Add(vo);
            }
            return list;
        }



        AccountLite _account = null;
        public void SetAccount(AccountLite account)
        {
            _account = account;
            this.Text = string.Format("编辑帐户:{0}模板项", account.Account);

        }
        void btnSubmit_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm("确认更新帐户手续费与保证金模板?") == System.Windows.Forms.DialogResult.Yes)
            {
                if (CoreService.SiteInfo.UIAccess.r_commission)
                {
                    int commissionid = (int)cbCommissionTemplate.SelectedValue;
                    if (_account.Commissin_ID != commissionid)
                    {
                        CoreService.TLClient.ReqUpdateAccountCommissionTemplate(_account.Account, commissionid);
                    }
                }
                this.Close();
            }
            
        }
    }
}
