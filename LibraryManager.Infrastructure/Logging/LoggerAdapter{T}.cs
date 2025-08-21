namespace LibraryManager.Infrastructure.Logging
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Core.Common;
    using Microsoft.Extensions.Logging;
    using System;

    internal sealed class LoggerAdapter<T>
        : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;

        public void LogCritical(string message, params object[] args)
        {
            _logger.LogCritical(message, args);
        }

        public void LogError(Exception exception, string message, params object[] args)
        {
            _logger.LogError(exception, message, args);
        }

        public void LogError(string message, params object[] args)
        {
            _logger.LogError(message, args);
        }

        public void LogErrorWithContext(Error error, string message, params object[] args)
        {
            _logger.LogErrorWithContext(error, message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }
    }
}
