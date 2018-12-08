using BattlEyeManager.Core;
using Microsoft.Extensions.Logging;
using System;

namespace BattlEyeManager.Spa.Core
{
    public class LogImpl : ILog
    {
        private readonly ILogger _logger;

        public LogImpl(ILogger<LogImpl> logger)
        {
            _logger = logger;
        }

        public void Debug(object message)
        {
            _logger.LogDebug($"{message}");
        }

        public void Info(object message)
        {
            _logger.LogInformation($"{message}");
        }

        public void Error(object message)
        {
            if (message is Exception)
                _logger.LogError(message as Exception, string.Empty);
            else
                _logger.LogError($"{message}");
        }

        public void Fatal(object message)
        {
            if (message is Exception)
                _logger.LogCritical(message as Exception, string.Empty);
            else
                _logger.LogCritical($"{message}");
        }
    }
}