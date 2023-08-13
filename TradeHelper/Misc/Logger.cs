using log4net;
using System;

namespace TradeHelper
{
    class DefaultLogger
    {
        private static ILog _logger;

        static DefaultLogger()
        {
            _logger = LogManager.GetLogger("ApplicationAppender");
        }

        public DefaultLogger()
        {

        }

        public static void Info(string msg)
        {
            _logger.Info(msg);
        }

        public static void Error(string msg, Exception ex)
        {
            _logger.Error(msg, ex);
        }
    }
}
