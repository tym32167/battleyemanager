using BattlEyeManager.Core;

namespace BattlEyeManager.Services.Logging
{
    public class Log : ILog
    {
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