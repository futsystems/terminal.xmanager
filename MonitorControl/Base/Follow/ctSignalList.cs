using System;
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


namespace TradingLib.MoniterControl.Base.Follow
{
    public partial class ctSignalList : UserControl, IEventBinder
    {
        public ctSignalList()
        {
            InitializeComponent();

            SetPreferences();
            InitTable();
            BindToTable();

            this.Load += new EventHandler(ctSignalList_Load);
        }

        void ctSignalList_Load(object sender, EventArgs e)
        {
            CoreService.EventCore.RegIEventHandler(this);
        }

        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback("FollowCentre", "QrySignalConfigList", OnQrySignalconfigList);

            //查询跟单信号列表
            CoreService.TLClient.ReqContribRequest("FollowCentre", "QrySignalConfigList", "");
            //CoreService.TLClient.ReqContribRequest("FollowCentre", "AppendSignalToStrategy", string.Format("{0},{1}", signalID, strategyID));
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("FollowCentre", "QrySignalConfigList", OnQrySignalconfigList);
           
        }

        void OnQrySignalconfigList(string json, bool islast)
        {
            SignalConfig item = MoniterHelper.ParseJsonResponse<SignalConfig>(json);
            if (item != null)
            {
                InvokeGotSignalConfig(item);
            }

        }

        Dictionary<int, SignalConfig> configmap = new Dictionary<int, SignalConfig>();
        Dictionary<int, int> idxmap = new Dictionary<int, int>();

        /// <summary>
        /// 信号ID对应的RowID
        /// </summary>
        /// <param name="signalID"></param>
        /// <returns></returns>
        int ID2Idx(int signalID)
        {
            int rowid = -1;
            if (idxmap.TryGetValue(signalID, out rowid))
            {
                return rowid;
            }
            return -1;
        }
        void InvokeGotSignalConfig(SignalConfig item)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<SignalConfig>(InvokeGotSignalConfig), new object[] { item });
            }
            else
            {
                int i = ID2Idx(item.ID);
                if (i == -1)
                {
                    gt.Rows.Add();
                    i = gt.Rows.Count - 1;

                    gt.Rows[i][SIGNALID] = item.ID;
                    gt.Rows[i][SIGNALTYPE] = Util.GetEnumDescription(item.SignalType);
                    gt.Rows[i][TOKEN] = item.SignalToken;

                    configmap.Add(item.ID, item);
                    idxmap.Add(item.ID, i);
                }
                else
                { 
                    
                }

            }
        }



        #region 表格

        const string SIGNALID = "ID";
        const string SIGNALTYPE = "信号类别";
        const string TOKEN = "Token";

       


        DataTable gt = new DataTable();
        BindingSource datasource = new BindingSource();




        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = signalGrid;

            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            grid.ColumnHeadersHeight = 25;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.StateCommon.Background.Color1 = Color.WhiteSmoke;
            grid.StateCommon.Background.Color2 = Color.WhiteSmoke;

        }

        //初始化Account显示空格
        private void InitTable()
        {

            gt.Columns.Add(SIGNALID);//0
            gt.Columns.Add(SIGNALTYPE);//1

            gt.Columns.Add(TOKEN);//2
          

        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            datasource.DataSource = gt;
            //datasource.Sort = ACCOUNT + " ASC";
            signalGrid.DataSource = datasource;

            //strategyGrid.Columns[CATEGORY].Visible = false;
            //strategyGrid.Columns[AGENTMGRFK].Visible = false;


            for (int i = 0; i < gt.Columns.Count; i++)
            {
                signalGrid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        #endregion

    }
}
