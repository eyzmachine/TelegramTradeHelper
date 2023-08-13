using System;
using System.Configuration;
using System.Globalization;

namespace TradeHelper.Misc
{
    public static class Settings
    {
        public static string TelegramToken { get; private set; }

        public static string LogfilePath { get; private set; }

        public static string Language { get; private set; }

        public static long? UserId { get; private set; }

        public static string UserName { get; private set; }

        public static bool AcceptAllMessages { get; private set; }

        public static int? UpdateTime { get; private set; }

        public static void Init()
        {
            TelegramToken = ReadSetting("telegramToken");
            LogfilePath = ReadSetting("pathToLogFile");
            Language = ReadSetting("language");
            UserName = ReadSetting("userName");
            UserId = ReadSetting<long>("userId");
            AcceptAllMessages = ReadSetting<bool>("allMessages") ?? true;
            UpdateTime = ReadSetting<int>("updateTime");

            SetLanguage();
        }

        private static string ReadSetting(string key)
        {
            string result = null;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                if (!string.IsNullOrWhiteSpace(appSettings[key]))
                {
                    result = appSettings[key];
                }

                DefaultLogger.Info($"Setting {key} has been read.");
            }
            catch (ConfigurationErrorsException ex)
            {
                DefaultLogger.Error(ex.Message, ex);
            }

            return result;
        }

        private static T? ReadSetting<T>(string key)
            where T : struct
        {
            T? result = null;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                var settingObj = appSettings[key] as object;

                if (!string.IsNullOrWhiteSpace(settingObj.ToString()))
                {
                    DefaultLogger.Info($"Setting {key} has been read.");

                    result = Convert.ChangeType(settingObj, typeof(T)) as T?;
                }
            }
            catch (ConfigurationErrorsException ex)
            {
                DefaultLogger.Error(ex.Message, ex);
            }

            return result;
        }

        public static void SetLanguage()
        {
            if (Language == null)
            {
                Language = "en";
            }

            switch (Language.ToLower())
            {
                case "ru":
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU", true);
                    break;
                case "en":
                default:
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-EN", true);
                    break;

            }
        }
    }
}
