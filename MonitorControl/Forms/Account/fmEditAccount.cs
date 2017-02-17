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
    public partial class fmEditAccount : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmEditAccount()
        {
            InitializeComponent();
            SmallView();
            this.Load += new EventHandler(fmAddFinAccount_Load);
        }

        void fmAddFinAccount_Load(object sender, EventArgs e)
        {
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
            btnFillInfo.CheckedChanged += new EventHandler(btnFillInfo_CheckedChanged);
            //生成帐户类型列表
            MoniterHelper.AdapterToIDataSource(cbAccountType).BindDataSource(MoniterHelper.GetAccountTypeCombList());
            MoniterHelper.AdapterToIDataSource(cbCurrency).BindDataSource(MoniterHelper.GetEnumValueObjects<CurrencyType>());

            cbAccountType.SelectedIndex = 0;
            CoreService.EventCore.RegIEventHandler(this);

        }

        void SmallView()
        {
            this.Height = 200;
            lbNotice.Visible = false;
            kryptonGroupBox1.Visible = false;
            kryptonGroupBox2.Visible = false;
            kryptonGroupBox3.Visible = false;
            btnSubmit.Location = new Point(215,135);
            this.Height = 195;
        }

        void BigView()
        {
            this.Height = 630;
            lbNotice.Visible = true;
            kryptonGroupBox1.Visible = true;
            kryptonGroupBox2.Visible = true;
            kryptonGroupBox3.Visible = true;
            btnSubmit.Location = new Point(215, 565);
        }
        void btnFillInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (btnFillInfo.Checked)
            {
                BigView();
            }
            else
            {
                SmallView();
            }
        }

        public void OnInit()
        {
            CoreService.EventCore.RegisterCallback(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_PROFILE, this.OnQryAccountProfile);
            
            //如果设定了交易帐户 则表明是编辑/查询个人信息
            if (_account != null)
            {
                CoreService.TLClient.ReqQryAccountProfile(_account.Account);
            }
        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_PROFILE, this.OnQryAccountProfile);
            
        }

        void OnQryAccountProfile(string json, bool islast)
        {
            AccountProfile profile = CoreService.ParseJsonResponse<AccountProfile>(json);
            if (profile != null && profile.Account.Equals(_account.Account))
            {
                account.Text = profile.Account;
                ctBankList1.BankSelected = profile.Bank_ID;
                bankac.Text = profile.BankAC;
                branch.Text = profile.Branch;
                email.Text = profile.Email;
                idcard.Text = profile.IDCard;
                mobile.Text = profile.Mobile;
                name.Text = profile.Name;
                qq.Text = profile.QQ;
                broker.Text = profile.Broker;
                memo.Text = profile.Memo;
            }
        }
        AccountItem _account = null;
        public void SetAccount(AccountItem acc)
        {
            _account = acc;
            this.Text = string.Format("查看/编辑帐户[{0}]个人信息", _account.Account);
            account.Enabled = false;
            cbAccountType.Enabled = false;
            cbCurrency.Enabled = false;
            ctAgentList1.Enabled = false;
            //设置可以修改扩展信息
            btnFillInfo.Checked = true;
            cbCurrency.SelectedValue = acc.Currency;
            BigView();
        }


        void btnSubmit_Click(object sender, EventArgs e)
        {
            AccountProfile profile = new AccountProfile();
            profile.Account = account.Text;
            profile.Bank_ID = ctBankList1.BankSelected;
            profile.BankAC = bankac.Text;
            profile.Branch = branch.Text;
            profile.Email = email.Text;
            profile.IDCard = idcard.Text;
            profile.Mobile = mobile.Text;
            profile.Name = name.Text;
            profile.QQ = qq.Text;
            profile.Broker = broker.Text;
            profile.Memo = memo.Text;

            AccountCreation createion = new AccountCreation();
            createion.Account = account.Text;
            createion.Category = (QSEnumAccountCategory)cbAccountType.SelectedValue;
            //默认交易帐户走模拟成交
            createion.RouterType = QSEnumOrderTransferType.SIM;
            createion.Profile = profile;
            createion.Currency = (CurrencyType)cbCurrency.SelectedValue;
            createion.BaseManagerID = ctAgentList1.CurrentAgentFK;

            //这里加入数据验证和检查
            if (_account == null)
            {
                if (btnFillInfo.Checked)
                {
                    if (MoniterHelper.WindowConfirm("请确认个人信息填写准确") == System.Windows.Forms.DialogResult.Yes)
                    {
                        CoreService.TLClient.ReqAddAccount(createion);
                        this.Close();
                    }
                }
                else
                {
                    CoreService.TLClient.ReqAddAccount(createion);
                    this.Close();
                }
            }
            else//如果交易帐户存在 则更新交易帐户对应的profile信息
            {
                if (btnFillInfo.Checked)
                {
                    if (MoniterHelper.WindowConfirm("确认更新个人信息") == System.Windows.Forms.DialogResult.Yes)
                    {
                        CoreService.TLClient.ReqUpdateAccountProfile(profile);
                        this.Close();
                    }
                }
                else
                {
                    this.Close();
                }
            }
        }


       
    }
}
