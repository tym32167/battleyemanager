using BattlEyeManager.Core;

namespace BattlEyeManager.Services.Logging
{
    //public class Log : ILog
    //{
    //    private readonly log4net.ILog _log;

    //    public Log()
    //    {
    //        _log = LogManager.GetLogger(typeof(Log));
    //    }

    //    public Log(Type targetType)
    //    {
    //        _log = LogManager.GetLogger(targetType);
    //    }

    //    #region Implementation of ILog

    //    public void Debug(object message)
    //    {
    //        DebugConditional(message);
    //    }

    //    [Conditional("DEBUG")]
    //    private void DebugConditional(object message)
    //    {
    //        _log.Debug(message);
    //    }


    //    public void Info(object message)
    //    {
    //        _log.Info(message);
    //    }

    //    public void Error(object message)
    //    {
    //        _log.Error(message);
    //    }

    //    public void Fatal(object message)
    //    {
    //        _log.Fatal(message);
    //    }

    //    #endregion
    //}

    public class Log : ILog
    {
        public Log(System.Type targetType)
        {
        }

        public void Debug(object message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Info(object message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Error(object message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Fatal(object message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}