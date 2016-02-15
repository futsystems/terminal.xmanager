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

            try
            {
                Application.Run(new Starter());
            }
            catch (Exception ex)
            {
                MessageService.ShowError(ex, "Error storing properties");
            }
            finally
            {

                try
                {
                    //保存属性
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
