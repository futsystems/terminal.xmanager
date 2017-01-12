using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;
using System.Net;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace TradingLib.MoniterControl
{
    public partial class ctAccountInfo : UserControl,IEventBinder
    {
        public ctAccountInfo()
        {
            InitializeComponent();
            btnExecute.Enabled = false;
            btnUpdateTransferType.Enabled = false;
            btnUpdateInterday.Enabled = false;
            SetPreferences();
            InitTable();
            BindToTable();

            MoniterHelper.AdapterToIDataSource(cbTransferType).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumOrderTransferType>());
            MoniterHelper.AdapterToIDataSource(cbCurrnecy).BindDataSource(MoniterHelper.GetEnumValueObjects<CurrencyType>());
            this.Load += new EventHandler(ctAccountInfo_Load);
            
        }




        void ctAccountInfo_Load(object sender, EventArgs e)
        {
            btnUpdateInterday.Click += new EventHandler(btnUpdateInterday_Click);
            btnUpdateTransferType.Click += new EventHandler(btnUpdateTransferType_Click);
            btnExecute.Click += new EventHandler(btnExecute_Click);
            btnUpdateCurrency.Click += new EventHandler(btnUpdateCurrency_Click);

            cbHoldNight.CheckedChanged += new EventHandler(cbHoldNight_CheckedChanged);
            cbTransferType.SelectedIndexChanged += new EventHandler(cbTransferType_SelectedIndexChanged);
            cbCurrnecy.SelectedIndexChanged += new EventHandler(cbCurrnecy_SelectedIndexChanged);
            cbCurrnecy.Enabled = false;

            CoreService.EventAccount.OnAccountSelectedEvent += new Action<AccountItem>(OnAccountSelected);
            CoreService.EventAccount.OnAccountChangedEvent += new Action<AccountItem>(EventAccount_OnAccountChangedEvent);

            CoreService.EventCore.RegIEventHandler(this);
            
        }



        

        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback(Modules.MSG_EXCH, Method_MSG_EXCH.QRY_SESSION_INFO, OnSessionInfo);
            CoreService.EventContrib.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_SESSION_INFO, OnSessionNotify);
            //只有管理员可以查看成交方式
            if (!CoreService.SiteInfo.Manager.IsRoot())
            {
                PanelRouter.Visible = false;
                btnExecute.Visible = CoreService.SiteInfo.UIAccess.r_block;
                btnUpdateInterday.Visible = CoreService.SiteInfo.UIAccess.r_account_interday;
                cbHoldNight.Enabled = CoreService.SiteInfo.UIAccess.r_account_interday;
            }
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback(Modules.MSG_EXCH, Method_MSG_EXCH.QRY_SESSION_INFO, OnSessionInfo);
            CoreService.EventContrib.UnRegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_SESSION_INFO, OnSessionNotify);
        }


        void OnSessionNotify(string json)
        {
            SessionInfo info = CoreService.ParseJsonResponse<SessionInfo>(json);
            if (info != null)
            {
                if (account != null && account.Account == info.Account)
                {
                    InvokeGotSessionInfo(info);
                }
            }
        }

        void OnSessionInfo(string json, bool islast)
        {
            SessionInfo info = CoreService.ParseJsonResponse<SessionInfo>(json);
            if (info != null)
            {
                InvokeGotSessionInfo(info);
            }
        }

        ConcurrentDictionary<string, int> sessionIDMap = new ConcurrentDictionary<string, int>();
        int SessionID2Idx(string id)
        {
            int idx = -1;
            if (sessionIDMap.TryGetValue(id, out idx))
            {
                return idx;
            }
            return -1;
        }
        
        void InvokeGotSessionInfo(SessionInfo info)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<SessionInfo>(InvokeGotSessionInfo), new object[] { info });
            }
            else
            {
                if (!string.IsNullOrEmpty(info.ClientID))
                {
                    int i = SessionID2Idx(info.ClientID);
                    if (i == -1)
                    {
                        DataRow r = tb.Rows.Add(info.ClientID);
                        i = tb.Rows.Count - 1;//得到新建的Row号
                        sessionIDMap.TryAdd(info.ClientID, i);

                        tb.Rows[i][SESSIONID] = info.ClientID;
                        tb.Rows[i][IPADDRESS] = info.IPAddress;
                        tb.Rows[i][TERMINAL] = info.ProductInfo;
                        tb.Rows[i][CREATEDTIME] = info.CreatedTime.ToString("HH:mm:ss");
                        tb.Rows[i][LOGIN] = info.Login;
                    }
                    else
                    {
                        tb.Rows[i][LOGIN] = info.Login;
                    }

                    //if (!string.IsNullOrEmpty(info.IPAddress))
                    //{
                    //    string[] rec = info.IPAddress.Split(':');
                    //    string ip =rec[0];
                    //    //异步查询物理位置
                    //    Func<string, string> del = new Func<string, string>(QryLocation);
                    //    del.BeginInvoke(ip, QryLocationCallback, null);
                    //}
                }
            }
        }

        

        void InvokeGotGLocation(string location)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(InvokeGotGLocation), new object[] { location });
            }
            else
            {
                //lbGLocation.Text = location;
            }
        }

        void btnExecute_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm("确认" + (account.Execute ? "冻结" : "激活") + "交易帐号:" + account.Account + "?") == DialogResult.Yes)
            {
                CoreService.TLClient.ReqUpdateAccountExecute(account.Account, !account.Execute);
            }
        }

        void btnUpdateCurrency_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm("确认更新交易帐户货币类型?") == DialogResult.Yes)
            {
                CoreService.TLClient.ReqUpdateAccountCurrency(account.Account, (CurrencyType)cbCurrnecy.SelectedValue);
            }
        }


        void EventAccount_OnAccountChangedEvent(AccountItem obj)
        {
            if (account != null && account.Account.Equals(obj.Account))
            {
                account = obj;
                lbAccountType.Text = Util.GetEnumDescription(account.Category);

                btnExecute.Text = obj.Execute ? "冻 结" : "激 活";
                btnExecute.StateCommon.Content.ShortText.Color1 = !obj.Execute ? UIConstant.ShortSideColor : UIConstant.LongSideColor;
            
                cbHoldNight.Checked = !account.IntraDay;
                cbTransferType.SelectedValue = account.OrderRouteType;
                btnUpdateInterday.Enabled = false;
                btnUpdateTransferType.Enabled = false;
                btnUpdateCurrency.Enabled = false;
            }
        }

        void cbTransferType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (account == null)
                return;
            if (account.OrderRouteType != (QSEnumOrderTransferType)cbTransferType.SelectedValue)
            {
                btnUpdateTransferType.Enabled = true;
            }
            else
            {
                btnUpdateTransferType.Enabled = false;
            }
        }

        void cbHoldNight_CheckedChanged(object sender, EventArgs e)
        {
            if (account == null)
                return;
            if (account.IntraDay != cbHoldNight.Checked)
            {
                btnUpdateInterday.Enabled = false;
            }
            else
            {
                btnUpdateInterday.Enabled = true;
            }
        }

        void cbCurrnecy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (account == null) return;
            if (account.Currency != (CurrencyType)cbCurrnecy.SelectedValue)
            {
                btnUpdateCurrency.Enabled = true;
            }
            else
            {
                btnUpdateCurrency.Enabled = false;
            }
        }

        void btnUpdateTransferType_Click(object sender, EventArgs e)
        {
            if (account == null)
            {
                MoniterHelper.WindowMessage("请选择交易帐号");
                return;
            }
            if (MoniterHelper.WindowConfirm("确认修改交易帐户:" + account.Account + "成交方式为:" + Util.GetEnumDescription((QSEnumOrderTransferType)cbTransferType.SelectedValue)) == DialogResult.Yes)
            {
                CoreService.TLClient.ReqUpdateRouteType(account.Account, (QSEnumOrderTransferType)cbTransferType.SelectedValue);
                //btnUpdateTransferType.Enabled = false;
            }
        }

        void btnUpdateInterday_Click(object sender, EventArgs e)
        {
            if (account == null)
            {
                MoniterHelper.WindowMessage("请选择交易帐号");
                return;
            }
            if (MoniterHelper.WindowConfirm("确认更新交易帐户:" + account.Account + "为" + (cbHoldNight.Checked ? "允许" : "禁止") + "隔夜") == DialogResult.Yes)
            {
                CoreService.TLClient.ReqUpdateAccountIntraday(account.Account, !cbHoldNight.Checked);
                
            }
        }


        AccountItem account = null;
        void OnAccountSelected(AccountItem obj)
        {
            account = obj;
            lbAccount.Text = account.Account;
            lbAccountType.Text = Util.GetEnumDescription(account.Category);

            btnExecute.Text = obj.Execute ? "冻 结" : "激 活";
            btnExecute.StateCommon.Content.ShortText.Color1 = !obj.Execute ? UIConstant.ShortSideColor : UIConstant.LongSideColor;
            
            cbHoldNight.Checked = !account.IntraDay;
            cbTransferType.SelectedValue = account.OrderRouteType;
            cbCurrnecy.SelectedValue = account.Currency;

            btnExecute.Enabled = true;

            Clear();

            QrySessionInfo(obj.Account);
        }


        void QrySessionInfo(string account)
        {
            CoreService.TLClient.ReqQrySessionInfo(account);
        }


        void QryLocationCallback(IAsyncResult async)
        {
            Func<string, string> proc = ((AsyncResult)async).AsyncDelegate as Func<string, string>;
            string location = proc.EndInvoke(async);
            InvokeGotGLocation(location);
        }


        string QryLocation(string ip)
        {
            try
            {
                String direction = "";
                WebRequest request = WebRequest.Create("http://ip.taobao.com/service/getIpInfo.php?ip=" + ip);
                using (WebResponse response = request.GetResponse())
                using (StreamReader stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("GB18030")))
                {
                    direction = stream.ReadToEnd();
                    var data = direction.DeserializeObject();
                    var code = int.Parse(data["code"].ToString());
                    if (code == 0)
                    {
                        var country = data["data"]["country"].ToString();
                        var area = data["data"]["area"].ToString();
                        var region = data["data"]["region"].ToString();
                        var city = data["data"]["city"].ToString();
                        var isp = data["data"]["isp"].ToString();

                        return string.Format("{0}-{1}-{2}({3})",country, region, city, isp);
                    }
                    return "返回错误";
                }
            }
            catch (Exception ex)
            {
                return "查询异常";
            }
        }
        private void kryptonButton1_Click(object sender, EventArgs e)
        {

            
        }


        #region 表格
        const string SESSIONID = "回话编号";
        const string IPADDRESS = "IP地址";
        const string TERMINAL = "终端类别";
        const string CREATEDTIME = "建立时间";
        const string LOGIN = "LOGIN";

        DataTable tb = new DataTable();
        BindingSource datasource = new BindingSource();
        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = sessionGrid;

            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.ColumnHeadersHeight = 25;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;

            grid.StateCommon.Background.Color1 = Color.WhiteSmoke;
            grid.StateCommon.Background.Color2 = Color.WhiteSmoke;
        }
        /// <summary>
        /// 初始化数据表格
        /// </summary>
        private void InitTable()
        {
            tb.Columns.Add(SESSIONID);//0
            tb.Columns.Add(TERMINAL);//1
            tb.Columns.Add(IPADDRESS);//2
            tb.Columns.Add(CREATEDTIME);
            tb.Columns.Add(LOGIN);
        }
        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = sessionGrid;
            datasource.DataSource = tb;
            //datasource.Sort = RAWDATETIME + " DESC";
            datasource.Filter = string.Format(LOGIN + " = '{0}'", true);
            grid.DataSource = datasource;

            grid.Columns[LOGIN].Visible = false;
            //grid.Columns[STATUS].Visible = false;
            //grid.Columns[ID].Visible = false;
            //grid.Columns[ORDERREF].Visible = false;
            //grid.Columns[RAWDATETIME].Visible = false;

            ResetColumeSize();

            for (int i = 0; i < tb.Columns.Count; i++)
            {
                grid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        void ResetColumeSize()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = sessionGrid;
            
        }
        #endregion

        public void Clear()
        {
            sessionGrid.DataSource = null;
            sessionIDMap.Clear();
            tb.Rows.Clear();
            BindToTable();
        }
    }
}
