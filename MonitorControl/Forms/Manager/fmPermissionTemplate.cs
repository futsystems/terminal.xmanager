﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.Mixins.JsonObject;
using TradingLib.MoniterCore;



namespace TradingLib.MoniterControl
{

    public partial class fmPermissionTemplate : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmPermissionTemplate()
        {
            InitializeComponent();

            this.imageList1.Images.Add((System.Drawing.Image)Properties.Resources.folder);
            this.imageList1.Images.Add((System.Drawing.Image)Properties.Resources.folder_sel);
            this.imageList1.Images.Add((System.Drawing.Image)Properties.Resources.items);
            templateTree.ImageList = this.imageList1;

            templateTree.Nodes.Add(CreateBaseItem("权限模板"));

            InitLaylout();
            this.Load += new EventHandler(fmPermissionTemplate_Load);
        }

        private ComponentFactory.Krypton.Toolkit.KryptonTreeNode CreateBaseItem(string lb)
        {
            ComponentFactory.Krypton.Toolkit.KryptonTreeNode item = new ComponentFactory.Krypton.Toolkit.KryptonTreeNode();
            item.Text = lb;
            item.ImageIndex = 2;
            item.SelectedImageIndex = item.ImageIndex;
            item.Tag = lb;
            return item;
        }

        private ComponentFactory.Krypton.Toolkit.KryptonTreeNode CreatePermissionTemplateItem(Permission template)
        {
            ComponentFactory.Krypton.Toolkit.KryptonTreeNode item = new ComponentFactory.Krypton.Toolkit.KryptonTreeNode();
            item.Text = template.name;
            item.ImageIndex = 0;
            item.SelectedImageIndex = 1;
            item.Tag = template;
            return item;
        }



        void fmPermissionTemplate_Load(object sender, EventArgs e)
        {
            this.templateTree.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.templateTree.ContextMenuStrip.Items.Add("添加权限模板", null, new EventHandler(Add_Click));
            this.templateTree.ContextMenuStrip.Items.Add("删除权限模板", null, new EventHandler(Del_Click));

            templateTree.NodeMouseClick += new TreeNodeMouseClickEventHandler(templateTree_NodeMouseClick);
            btnAddTemplate.Click += new EventHandler(btnAddTemplate_Click);
           
            
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
            CoreService.EventCore.RegIEventHandler(this);


        }


        void templateTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                return;
            if (e.Node.Parent != null)
            {
                if (e.Node.Parent.Index == 0)
                {
                    Permission access = e.Node.Tag as Permission;
                    if (access != null)
                    {
                        foreach (string key in permissionmap.Keys)
                        {
                            if (permissioneditmap.ContainsKey(key))
                            {
                                ctTLPermissionEdit edit = permissioneditmap[key];
                                PermissionField field = permissionmap[key];
                                edit.Value = (bool)field.Info.GetValue(access, null);
                            }
                            else
                            {
                                PermissionField field = permissionmap[key];
                                field.Info.SetValue(access, false, null);
                            }
                        }

                        this.Text = "权限模板设置-" + access.name;
                    }
                }
            }
        }

        void Add_Click(object sender, EventArgs e)
        {
            fmPermissionTemplateEdit fm = new fmPermissionTemplateEdit();
            fm.ShowDialog();
            fm.Close();
        }
        void Del_Click(object sender, EventArgs e)
        {
            if (templateTree.SelectedNode.Parent != null)//父节点不为空 表面为二级节点
            {
                if (templateTree.SelectedNode.Parent.Index == 0)//父节点 index为0 表面为二级节点
                {
                    Permission t = templateTree.SelectedNode.Tag as Permission;

                    if (MoniterHelper.WindowConfirm(string.Format("确认删除权限模板:{0}?", t.name)) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //ClearItem();
                        CoreService.TLClient.ReqContribRequest("MgrExchServer", "DeletePermissionTemplate", t.id.ToString());
                    }
                }
            }
        }

        void btnAddTemplate_Click(object sender, EventArgs e)
        {
            fmPermissionTemplateEdit fm = new fmPermissionTemplateEdit();
            fm.ShowDialog();
            fm.Close();
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            TreeNode node = templateTree.SelectedNode;
            if (node != null && node.Parent != null && node.Parent.Index == 0)
            {
                Permission access = node.Tag as Permission;
                if (access != null)
                {
                    foreach (string key in permissionmap.Keys)
                    {
                        ctTLPermissionEdit edit = permissioneditmap[key];
                        PermissionField field = permissionmap[key];
                        //设置值
                        field.Info.SetValue(access, edit.Value, null);
                    }
                    if (MoniterHelper.WindowConfirm("确认更新权限模板?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        CoreService.TLClient.ReqUpdatePermissionTemplate(access.ToJson());
                    }
                }
            }
            else
            {
                MoniterHelper.WindowMessage("请选择权限模板");
            }
        }

        public void OnInit()
        {
            CoreService.EventCore.RegisterCallback("MgrExchServer", "QueryPermmissionTemplate", OnQryPermissionTemplate);
            CoreService.EventCore.RegisterNotifyCallback("MgrExchServer", "NotifyPermissionTemplate", OnNotifyPermissionTemplate);
            CoreService.EventCore.RegisterNotifyCallback("MgrExchServer", "NotifyDeletePermissionTemplate", OnNotifyDelPermissionTemplate);
            
            CoreService.TLClient.ReqQryPermmissionTemplateList();
        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback("MgrExchServer", "QueryPermmissionTemplate", OnQryPermissionTemplate);
            CoreService.EventCore.UnRegisterNotifyCallback("MgrExchServer", "NotifyPermissionTemplate", OnNotifyPermissionTemplate);
            CoreService.EventCore.UnRegisterNotifyCallback("MgrExchServer", "NotifyDeletePermissionTemplate", OnNotifyDelPermissionTemplate);
            

        }

        void OnNotifyDelPermissionTemplate(string json)
        {
            Permission obj = CoreService.ParseJsonResponse<Permission>(json);
            if (obj != null)
            {
                Permission template = templateTree.SelectedNode.Tag as Permission;
                if (template.id == obj.id)
                {
                    templateTree.SelectedNode.Parent.Nodes.Remove(templateTree.SelectedNode);
                }
            }
        }

        void OnNotifyPermissionTemplate(string json)
        {
            Permission obj = CoreService.ParseJsonResponse<Permission>(json);
            if (obj != null)
            {
                if (accessmap.Keys.Contains(obj.id))
                {
                    accessmap[obj.id] = obj;
                }
                else
                {
                    accessmap[obj.id] = obj;
                    InvokeGotAgentPermission(obj);
                }
                templateTree.ExpandAll();
            }
        }

        Dictionary<int, Permission> accessmap = new Dictionary<int, Permission>();
        void OnQryPermissionTemplate(string jsonstr, bool islast)
        {
            Permission[] objs = CoreService.ParseJsonResponse<Permission[]>(jsonstr);
            if (objs != null)
            {
                foreach (Permission access in objs)
                {
                    accessmap.Add(access.id, access);
                    InvokeGotAgentPermission(access);
                }
            }
            //展开树状菜单
            if (islast)
            {
                templateTree.ExpandAll();
            }
        }


        void InvokeGotAgentPermission(Permission access)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<Permission>(InvokeGotAgentPermission), new object[] { access });
            }
            else
            {
                templateTree.Nodes[0].Nodes.Add(CreatePermissionTemplateItem(access));
            }
        }


        Dictionary<string, PermissionField> permissionmap = new Dictionary<string, PermissionField>();
        Dictionary<string, ctTLPermissionEdit> permissioneditmap = new Dictionary<string, ctTLPermissionEdit>();

        void InitLaylout()
        {
            Type type = typeof(Permission);
            List<PropertyInfo> list = new List<PropertyInfo>();
            PropertyInfo[] propertyInfos = type.GetProperties();
            foreach (PropertyInfo pi in propertyInfos)
            {
                PermissionFieldAttr attr = (PermissionFieldAttr)Attribute.GetCustomAttribute(pi, typeof(PermissionFieldAttr));
                if (attr != null)
                {
                    list.Add(pi);
                    permissionmap.Add(pi.Name, new PermissionField(pi, attr));
                    bool needshow = true;
                    if(CoreService.SiteInfo.Manager.Type == QSEnumManagerType.AGENT)
                    {
                        needshow  = CoreService.SiteInfo.Permission.GetPermission(pi.Name);
                    }
                    if (needshow)
                    {
                        ctTLPermissionEdit edit = new ctTLPermissionEdit();
                        edit.PermissionTitle = attr.Title;
                        edit.PermissionDesp = attr.Desp;
                        laylout.Controls.Add(edit);
                        permissioneditmap.Add(pi.Name, edit);
                    }
                }
            }
        }
    }

    internal class PermissionField
    {
        public PermissionField(PropertyInfo info, PermissionFieldAttr attr)
        {
            this.Info = info;
            this.Attr = attr;
        }
        public PropertyInfo Info { get; set; }
        public PermissionFieldAttr Attr { get; set; }
    }
}
