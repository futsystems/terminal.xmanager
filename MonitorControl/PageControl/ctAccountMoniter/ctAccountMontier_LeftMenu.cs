using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;
using System.Windows.Forms;

namespace TradingLib.MoniterControl
{
    public partial class ctAccountMontier
    {
        ManagerSetting _managerSelected = null;
        void OnNotifyAgentCreate(string json)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(OnNotifyAgentCreate), new object[] { json });
            }
            else
            {
                ReCreateMenuTree();
            }
        }


        void OnManagerDelete(string json)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(OnManagerDelete), new object[] { json });
            }
            else
            {
                ReCreateMenuTree();
            }
        }

        

        void agentTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            MenuItem menu = e.Node.Tag as MenuItem;
            if (menu != null)
            {
                if (menu.AgentAccount == null)
                {
                    ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show(string.Format("代理:{0}账户不存在", menu.Manager.Login));
                    return;
                }
                _managerSelected = menu.Manager;
                //设定代理财务统计账户
                ctAgentSummary.SetAgent(menu.Manager, menu.AgentAccount);

                CoreService.TLClient.ReqWatchAgents(new string[] { menu.AgentAccount.Account });
                //过滤账户列表
                FilterAccount();

            }
        }


        void ReCreateMenuTree()
        {
            agentTree.Enabled = false;
            agentTree.SelectedNode = null;
            agentTree.Nodes.Clear();
            InitTreeMenu();
            agentTree.Enabled = true;
        }

        /// <summary>
        /// 初始化左侧树状菜单
        /// </summary>
        void InitTreeMenu()
        {
            MenuItem menu = GetAgentMenu();

            TreeNode mainNode = new TreeNode(menu.Title);
            mainNode.Tag = menu;
            agentTree.Nodes.Add(mainNode);

            foreach (var subItem in menu.SubAgents.OrderBy(item => item.Manager.Login))
            {
                this.AddMenuNode(mainNode, subItem);
            }
        }

        void AddMenuNode(TreeNode node, MenuItem item)
        {
            TreeNode itemNode = new TreeNode(item.Title);
            itemNode.Tag = item;
            node.Nodes.Add(itemNode);

            foreach (var subItem in item.SubAgents)
            {
                this.AddMenuNode(itemNode, subItem);
            }
        }

        /// <summary>
        /// 生成代理树状菜单项
        /// </summary>
        /// <returns></returns>
        MenuItem GetAgentMenu()
        {
            Dictionary<int, MenuItem> menuMap = new Dictionary<int, MenuItem>();
            foreach (var mgr in CoreService.BasicInfoTracker.Managers.ToArray())
            {
                if (mgr.mgr_fk == CoreService.SiteInfo.Manager.mgr_fk) continue;//如果对应的管理域ID与当前管理域ID相同则过滤
                if (mgr.Type != QSEnumManagerType.AGENT) continue;//只生成代理对应菜单项
                menuMap.Add(mgr.mgr_fk, new MenuItem(mgr.mgr_fk, mgr.parent_fk, CoreService.BasicInfoTracker.GetAgent(mgr.Login), mgr));
            }
            //最后生成当前管理域菜单项
            MenuItem menu = null;
            if (CoreService.SiteInfo.Manager.Type != QSEnumManagerType.STAFF)
            {
                menu = new MenuItem(CoreService.SiteInfo.Manager.mgr_fk, CoreService.SiteInfo.Manager.parent_fk, CoreService.BasicInfoTracker.GetAgent(CoreService.SiteInfo.Manager.Login), CoreService.SiteInfo.Manager);
                menuMap.Add(menu.MGR_ID, menu);
            }
            else
            {
                var basemgr = CoreService.BasicInfoTracker.GetManager(CoreService.SiteInfo.Manager.GetBaseMGR());

                menu = new MenuItem(basemgr.mgr_fk, basemgr.parent_fk, CoreService.BasicInfoTracker.GetAgent(basemgr.Login), basemgr);
                menuMap.Add(menu.MGR_ID, menu);
            }

            //遍历所有结点通过上级管理员ID进行树状关联
            foreach (var item in menuMap.Values)
            {
                MenuItem parent = null;
                if (menuMap.TryGetValue(item.Parent_ID, out parent))
                {
                    if (parent.MGR_ID == item.MGR_ID) continue;
                    parent.SubAgents.Add(item);
                }

            }
            return menu;
        }
    }
}
