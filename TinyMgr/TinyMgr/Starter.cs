using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using Ant.Component;
using System.Windows.Forms;
using System.Resources;
using TradingLib.API;
using TradingLib.Common;

using System.Reflection;
using TradingLib.MoniterCore;

namespace TinyMgr
{
    public class Starter : SplashScreenApplicationContext
    {
        LoginForm _loginform;
        /// <summary>
        /// 检查自动更新
        /// </summary>
        /// <returns></returns>
        protected override bool OnUpdate()
        {
            //没有更新我们返回false 程序正常运行
            Updater update = new Updater();
            if (update.Detect())
            {
                update.Update("TinyMgr.exe", true);
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 创建登入窗口
        /// </summary>
        protected override void OnCreateSplashScreenForm()
        {
            _loginform = new LoginForm(this);
            this.SplashScreenForm = _loginform;//启动窗体
        }



        protected override void OnActiveMainForm()
        {
            
        }

        /// <summary>
        /// 创建主窗体
        /// </summary>
        protected override void OnCreateMainForm()
        {
            //在线程中创建主窗体,防止登入界面卡顿
            new Thread(delegate()
            {
                this.PrimaryForm = new MainForm();
                //主窗体初始化完毕后 开启登入按钮
                _loginform.EnableLogin();

            }).Start();
        }

       
        protected override void SetSeconds()
        {
            this.SecondsShow = 60 * 60;//启动窗体显示的时间(秒)
        }
    }


}
