using System;

namespace WinFramework.Utility
{
    public static class LogUtility
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void WriteLogInfo(string msg)
        {
            logger.Info(msg);
        }

        public static void WriteLogInfo(string msg, Exception e)
        {
            logger.Info(msg, e);
        }

        public static void WriteLogWarning(string msg)
        {
            logger.Warn(msg);
        }

        public static void WriteLogWarning(string msg, Exception e)
        {
            logger.Warn(msg, e);
        }

        public static void WriteLogError(string msg)
        {
            logger.Error(msg);
        }

        public static void WriteLogError(string msg, Exception e)
        {
            logger.Error(msg, e);
        }

    }
}
