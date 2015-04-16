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
using ICSharpCode.Core;
using System.Reflection;
using TradingLib.MoniterCore;

namespace TradingLib.MoniterBase
{
    public class Starter : SplashScreenApplicationContext
    {
        LoginForm _loginform;
        Workbench _workbench;
        //System.Windows.Forms.Form mainfm;
        //用于调用升级逻辑,然后再显示启动窗口与主窗口
        protected override bool OnUpdate()
        {
            LogService.Info("check update information.............");
            //没有更新我们返回false 程序正常运行
            Updater update = new Updater();
            //MessageBox.Show("it is here");
            if (update.Detect())
            {
                //MessageBox.Show("it is here2");
                update.Update("Manager.exe", true);
                return true;
            }
            else
            {
                return false;
            }

        }

        protected override void OnCreateSplashScreenForm()
        {
            InitCSharpCode();

            _loginform = new LoginForm(this);
            this.SplashScreenForm = _loginform;//启动窗体
        }



        protected override void OnActiveMainForm()
        {
            
        }
        protected override void OnCreateMainForm()
        {
            //在线程中创建主窗体,防止登入界面卡顿
            new Thread(delegate()
            {
                InitWorkbench();
                //_workbench = new Workbench();
                this.PrimaryForm = Workbench.Instance;

                //主窗体初始化完毕后 开启登入按钮
                _loginform.EnableLogin();
            }).Start();
        }

        void InitCSharpCode()
        {
            LogService.Debug("在后台线程初始化主窗体对象");
            // The LoggingService is a small wrapper around log4net.
            // Our application contains a .config file telling log4net to write
            // to System.Diagnostics.Trace.
            LoggingService.Info("Application start");

            // Get a reference to the entry assembly (Startup.exe)
            Assembly exe = typeof(Starter).Assembly;

            // Set the root path of our application. ICSharpCode.Core looks for some other
            // paths relative to the application root:
            // "data/resources" for language resources, "data/options" for default options
            FileUtility.ApplicationRootPath = AppDomain.CurrentDomain.BaseDirectory;// Path.GetDirectoryName(exe.Location);

            LoggingService.Info("Starting core services...");

            // CoreStartup is a helper class making starting the Core easier.
            // The parameter is used as the application name, e.g. for the default title of
            // MessageService.ShowMessage() calls.
            CoreStartup coreStartup = new CoreStartup("Test application");
            // It is also used as default storage location for the application settings:
            // "%Application Data%\%Application Name%", but you can override that by setting c.ConfigDirectory

            // Specify the name of the application settings file (.xml is automatically appended)
            coreStartup.PropertiesName = "AppProperties";

            // Initializes the Core services (ResourceService, PropertyService, etc.)
            coreStartup.StartCoreServices();

            // Registeres the default (English) strings and images. They are compiled as
            // "EmbeddedResource" into Startup.exe.
            // Localized strings are automatically picked up when they are put into the
            // "data/resources" directory.

            //ResourceService.RegisterNeutralStrings(new ResourceManager("TradingLib.MoniterControl.StringResources", exe));
            //ResourceService.RegisterNeutralImages(new ResourceManager("TradingLib.MoniterControl.ImageResources", exe));
            ResourceService.RegisterNeutralStrings(new ResourceManager("TradingLib.MoniterBase.StringResources", exe));
            ResourceService.RegisterNeutralImages(new ResourceManager("TradingLib.MoniterBase.ImageResources", exe));


            LoggingService.Info("Looking for AddIns...");
            // Searches for ".addin" files in the application directory.
            coreStartup.AddAddInsFromDirectory(Path.Combine(FileUtility.ApplicationRootPath, "AddIns"));

            // Searches for a "AddIns.xml" in the user profile that specifies the names of the
            // add-ins that were deactivated by the user, and adds "external" AddIns.
            coreStartup.ConfigureExternalAddIns(Path.Combine(PropertyService.ConfigDirectory, "AddIns.xml"));

            // Searches for add-ins installed by the user into his profile directory. This also
            // performs the job of installing, uninstalling or upgrading add-ins if the user
            // requested it the last time this application was running.
            coreStartup.ConfigureUserAddIns(Path.Combine(PropertyService.ConfigDirectory, "AddInInstallTemp"),
                                            Path.Combine(PropertyService.ConfigDirectory, "AddIns"));

            LoggingService.Info("Loading AddInTree...");
            // Now finally initialize the application. This parses the ".addin" files and
            // creates the AddIn tree. It also automatically runs the commands in
            // "/Workspace/Autostart"
            coreStartup.RunInitialization();
        }
        void InitWorkbench()
        {


            LoggingService.Info("Initializing Workbench...");
            // Workbench is our class from the base project, this method creates an instance
            // of the main form.
            //Workbench.InitializeWorkbench();
            Workbench.InitializeWorkbench();
            
        }
        protected override void SetSeconds()
        {
            this.SecondsShow = 60 * 60;//启动窗体显示的时间(秒)
        }
    }


}
