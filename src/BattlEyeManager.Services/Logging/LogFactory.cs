using System;
using BattlEyeManager.Core;

namespace BattlEyeManager.Services.Logging
{
    public static class LogFactory
    {
        public static ILog Create(Type owner)
        {
            return new Log(owner);
        }
    }
}