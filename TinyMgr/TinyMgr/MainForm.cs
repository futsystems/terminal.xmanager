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
using TradingLib.TinyMGRControl;
using Common.Logging;

namespace TinyMgr
{
    public partial class MainForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        Dictionary<string, IPage> pagemap = new Dictionary<string, IPage>();
        ILog logger = LogManager.GetLogger("StockTrader");

        public MainForm()
        {
            InitializeComponent();

            InitPage();

            InitMenu();

            ShowPage(PageTypes.PAGE_DASHBOARD);//显示欢迎页面
        }

        #region 右侧主页面
        void InitPage()
        {

            pagemap.Add(PageTypes.PAGE_DASHBOARD, new PageDashboard());
            pagemap.Add(PageTypes.PAGE_STK_SYMBOLS, new PageSTKSymbol());
            pagemap.Add(PageTypes.PAGE_QUERY, new PageSTKQuery());
            pagemap.Add(PageTypes.PAGE_ACCOUNT, new PageAccount());
            pagemap.Add(PageTypes.PAGE_TEMPLATE, new PageSTKTemplate());

            foreach (var page in pagemap.Values)
            {
                Control c = page as Control;
                if (c == null) continue;
                mainPanel.Controls.Add(c);
                c.Dock = DockStyle.Fill;
            }
        }

        /// <summary>
        /// 隐藏所有页面
        /// </summary>
        void HideAllPage()
        {
            foreach (var page in pagemap.Values)
            {
                page.Hide();
            }
        }

        /// <summary>
        /// 显示某个类别的页面
        /// </summary>
        /// <param name="type"></param>
        void ShowPage(string type)
        {
            IPage page = null;
            if (pagemap.TryGetValue(type, out page))
            {
                HideAllPage();
                page.Show();
            }
        }

        IPage GetPage(string pagetype)
        {
            IPage page = null;
            if (pagemap.TryGetValue(pagetype, out page))
            {
                return page;
            }
            return null;
        }
        #endregion




        /// <summary>
        /// 初始化左侧菜单
        /// </summary>
        void InitMenu()
        {
            btnHome.Click += new EventHandler(btnHome_Click);
            btnSymbol.Click += new EventHandler(btnSymbol_Click);
            btnTemplate.Click += new EventHandler(btnTemplate_Click);
            btnAccount.Click += new EventHandler(btnAccount_Click);
            btnQuery.Click += new EventHandler(btnQuery_Click);


        }

        void btnTemplate_Click(object sender, EventArgs e)
        {
            ShowPage(PageTypes.PAGE_TEMPLATE);
        }

        void btnAccount_Click(object sender, EventArgs e)
        {
            ShowPage(PageTypes.PAGE_ACCOUNT);
        }

        void btnQuery_Click(object sender, EventArgs e)
        {
            ShowPage(PageTypes.PAGE_QUERY);
        }

        void btnHome_Click(object sender, EventArgs e)
        {
            ShowPage(PageTypes.PAGE_DASHBOARD);
        }

        void btnSymbol_Click(object sender, EventArgs e)
        {
            ShowPage(PageTypes.PAGE_STK_SYMBOLS);
        }



        /// <summary>
        /// 绑定事件
        /// </summary>
        void WireEvent()
        { 
            
        }


        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
