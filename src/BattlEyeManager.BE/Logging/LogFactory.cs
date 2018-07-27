using System;

namespace BattlEyeManager.BE.Logging
{
    public static class LogFactory
    {
        public static ILog Create(Type owner)
        {
            return new Log(owner);
        }
    }
}