using System;
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
    public partial class ctBankList : UserControl,IEventBinder
    {
        public event VoidDelegate BankSelectedChangedEvent;
        bool _gotdata = false;
        public ctBankList()
        {
            InitializeComponent();
            //如果已经初始化完成 则直接读取数据填充 否则将资金放入事件回调中

            this.Load += new EventHandler(ctBankList_Load);
            
        }

        void ctBankList_Load(object sender, EventArgs e)
        {
            CoreService.EventCore.RegIEventHandler(this);
            this.cbbank.SelectedIndexChanged += new EventHandler(cbbank_SelectedIndexChanged);
        }

        public void OnInit()
        {
            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_CONTRACT_BANK, this.OnQryBank);
            CoreService.TLClient.ReqQryBank();
        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_CONTRACT_BANK, this.OnQryBank);
        }


        void OnQryBank(string jsonstr, bool islast)
        {
            ContractBank[] obj = CoreService.ParseJsonResponse<ContractBank[]>(jsonstr);
            if (obj != null)
            {
                GotBankList(obj);
                _gotdata = true;
            }
            else
            {

            }
        }
        void GotBankList(ContractBank[] banks)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ContractBank[]>(GotBankList), new object[] { banks });
            }
            else
            {
                ArrayList list = new ArrayList();
                foreach (ContractBank bank in banks)
                {
                    ValueObject<int> vo = new ValueObject<int>
                    {
                        Name = bank.Name,
                        Value = bank.ID,
                    };
                    list.Add(vo);
                }

                MoniterHelper.AdapterToIDataSource(cbbank).BindDataSource(list);
                cbbank.SelectedValue = _bankselected;
                _gotdata = true;
            }
        }

        int _bankselected = 0;
        public int BankSelected 
        {
            get
            {
                try
                {
                    return int.Parse(cbbank.SelectedValue.ToString());
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
            set
            {
                try
                {
                    _bankselected = value;
                    cbbank.SelectedValue = value;
                }
                catch (Exception ex)
                { 
                    
                }
            }
        }

        private void cbbank_SelectedIndexChanged(object sender,EventArgs e)
        {

            if (BankSelectedChangedEvent!= null && _gotdata)
            {

                BankSelectedChangedEvent();
                
            }
        }


     
    }
}
