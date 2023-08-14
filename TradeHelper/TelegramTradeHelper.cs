using log4net.Config;
using System;
using System.Threading;
using System.Windows.Forms;
using TradeHelper.Misc;

namespace TradeHelper
{
    internal static class TelegramTradeHelper
    {
        [STAThread]
        static void Main()
        {
            Application.ThreadException += OnApplicationThreadException;
            AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainUnhandledException;
            XmlConfigurator.Configure();

            Init();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static void Init()
        {
            try
            {
                Settings.Init();
                Settings.SetLanguage();
                LogParser.InitLogReader();
                LogParser.InitWatcher();
            }
            catch (Exception ex)
            {
                DefaultLogger.Error($"Error happened while initialization: {ex.Message}", ex);
            }
        }

        public static void ProcessException(Exception ex)
        {
            DefaultLogger.Error(ex.Message, ex);
            MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void OnApplicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ProcessException(e.Exception);
        }

        private static void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ProcessException((Exception)e.ExceptionObject);
        }
    }
}
