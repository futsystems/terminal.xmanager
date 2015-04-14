using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Resources;
using ICSharpCode.Core;
using TradingLib.MoniterCore;
using TradingLib.MoniterBase;

namespace MoniterSkeleton
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理非UI线程异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);


            
            /*

            // The LoggingService is a small wrapper around log4net.
            // Our application contains a .config file telling log4net to write
            // to System.Diagnostics.Trace.
            LoggingService.Info("Application start");

            // Get a reference to the entry assembly (Startup.exe)
            Assembly exe = typeof(Program).Assembly;

            // Set the root path of our application. ICSharpCode.Core looks for some other
            // paths relative to the application root:
            // "data/resources" for language resources, "data/options" for default options
            FileUtility.ApplicationRootPath = Path.GetDirectoryName(exe.Location);

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

            ResourceService.RegisterNeutralStrings(new ResourceManager("MoniterSkeleton.StringResources", exe));
            ResourceService.RegisterNeutralImages(new ResourceManager("MoniterSkeleton.ImageResources", exe));

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

            //CoreService.InitClient("127.0.0.1", 6670);
            //CoreService.TLClient.Start();
            //CoreService.TLClient.ReqLogin("root", "123456");

            LoggingService.Info("Initializing Workbench...");

            LoggingService.Info("Initializing Workbench...");
            // Workbench is our class from the base project, this method creates an instance
            // of the main form.
            Workbench.InitializeWorkbench();
            **/

            try
            {
                LoggingService.Info("Running application...");
                // Workbench.Instance is the instance of the main form, run the message loop.
                //Application.Run(Workbench.Instance);
                Application.Run(new Starter());
            }
            finally
            {

                try
                {
                    // Save changed properties
                    PropertyService.Save();

                    
                }
                catch (Exception ex)
                {
                    MessageService.ShowError(ex, "Error storing properties");
                }
            }

            LoggingService.Info("Application shutdown");

        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            MessageBox.Show(ex.ToString());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            MessageBox.Show(ex.ToString());
        }

    }
}
