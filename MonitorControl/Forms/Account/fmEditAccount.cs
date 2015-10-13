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
            this.Load += new EventHandler(fmAddFinAccount_Load);
        }

        void fmAddFinAccount_Load(object sender, EventArgs e)
        {
            btnSubmit.Click += new EventHandler(btnSubmit_Click);

            //if (CoreService.SiteInfo.ProductType == QSEnumProductType.VendorMoniter)
            //{

            //    ValueObject<QSEnumAccountCategory> vo = new ValueObject<QSEnumAccountCategory>();
            //    vo.Name = "配资帐户";
            //    vo.Value = QSEnumAccountCategory.MONITERACCOUNT;

            //    ArrayList list = new ArrayList();
            //    list.Add(vo);
            //    MoniterHelper.AdapterToIDataSource(cbAccountType).BindDataSource(list);
            //    cbAccountType.SelectedIndex = 0;
            //    cbAccountType.Enabled = false;
            //}

            //分帐户柜台系统 可以选择添加的帐户类别
            //if (CoreService.SiteInfo.ProductType == QSEnumProductType.CounterSystem)
            {

                //ValueObject<QSEnumAccountCategory> vo1 = new ValueObject<QSEnumAccountCategory>();
                //vo1.Name = "模拟帐户";
                //vo1.Value = QSEnumAccountCategory.SUBACCOUNT;

                //ValueObject<QSEnumAccountCategory> vo2 = new ValueObject<QSEnumAccountCategory>();
                //vo2.Name = "实盘帐户";
                //vo2.Value = QSEnumAccountCategory.SUBACCOUNT;

                //ArrayList list = new ArrayList();
                //list.Add(vo1);
                //list.Add(vo2);


            }
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
            btnSubmit.Location = new Point(215,109);
            this.Height = 175;
        }

        void BigView()
        {
            this.Height = 590;
            lbNotice.Visible = true;
            kryptonGroupBox1.Visible = true;
            kryptonGroupBox2.Visible = true;
            kryptonGroupBox3.Visible = true;
            btnSubmit.Location = new Point(215, 525);
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
            CoreService.EventContrib.RegisterCallback("AccountManager", "QryAccountProfile", this.OnQryAccountProfile);
            
            //如果设定了交易帐户 则表明是编辑/查询个人信息
            if (_account != null)
            {
                this.ReqQryAccountProfile(_account.Account);
            }
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("AccountManager", "QryAccountProfile", this.OnQryAccountProfile);
            
        }

        void OnQryAccountProfile(string json, bool islast)
        {
            AccountProfile profile = MoniterHelper.ParseJsonResponse<AccountProfile>(json);
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
            }
        }
        AccountLite _account=null;
        public void SetAccount(AccountLite acc)
        {
            _account = acc;
            this.Text = string.Format("查看/编辑帐户[{0}]个人信息", _account.Account);
            account.Enabled = false;
            cbAccountType.Enabled = false;
            cbCurrency.Enabled = false;

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

            AccountCreation createion = new AccountCreation();
            createion.Account = account.Text;
            createion.Category = (QSEnumAccountCategory)cbAccountType.SelectedValue;
            //默认交易帐户走模拟成交
            createion.RouterType = QSEnumOrderTransferType.SIM;
            createion.Profile = profile;
            createion.Currency = (CurrencyType)cbCurrency.SelectedValue;

            //if (createion.Category == QSEnumAccountCategory.SUBACCOUNT)
            //{
            //    createion.RouterType = QSEnumOrderTransferType.LIVE;
            //}
            //else
            //{
            //    createion.RouterType = QSEnumOrderTransferType.SIM;
            //}

            

            //这里加入数据验证和检查
            if (_account == null)
            {
                if (btnFillInfo.Checked)
                {
                    if (MoniterHelper.WindowConfirm("请确认个人信息填写准确") == System.Windows.Forms.DialogResult.Yes)
                    {
                        ReqAddAccount(createion);
                        this.Close();
                    }
                }
                else
                {
                    ReqAddAccount(createion);
                    this.Close();
                }
            }
            else//如果交易帐户存在 则更新交易帐户对应的profile信息
            {
                if (btnFillInfo.Checked)
                {
                    if (MoniterHelper.WindowConfirm("确认更新个人信息") == System.Windows.Forms.DialogResult.Yes)
                    {
                        ReqUpdateAccountProfile(profile);
                        this.Close();
                    }
                }
                else
                {
                    this.Close();
                }
            }
        }


        /// <summary>
        /// 添加交易帐户
        /// </summary>
        /// <param name="createion"></param>
        void ReqAddAccount(AccountCreation createion)
        {
            CoreService.TLClient.ReqContribRequest("AccountManager", "AddAccountFacde", createion);
        }

        /// <summary>
        /// 请求添加配资客户
        /// </summary>
        /// <param name="profile"></param>
        //void ReqAddFinServiceAccount(AccountProfile profile)
        //{
        //    CoreService.TLClient.ReqContribRequest("AccountManager", "AddFinServiceAccount", profile);
        //}

        /// <summary>
        /// 查询个人信息
        /// </summary>
        /// <param name="account"></param>
        void ReqQryAccountProfile(string account)
        {
            CoreService.TLClient.ReqContribRequest("AccountManager", "QryAccountProfile", account);
        }

        /// <summary>
        /// 更新个人信息
        /// </summary>
        /// <param name="account"></param>
        void ReqUpdateAccountProfile(AccountProfile profile)
        {
            CoreService.TLClient.ReqContribRequest("AccountManager", "UpdateAccountProfile", profile);
        }
    }
}
