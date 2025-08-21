namespace LibraryManager.Infrastructure.Logging
{
    using LibraryManager.Application.Interfaces;
    using LibraryManager.Core.Common;
    using Microsoft.Extensions.Logging;

    internal class LoggerAdapter<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;

        public void LogErrorWithContext(Error error, string message, params object[] args)
        {
            _logger.LogErrorWithContext(error, message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }
    }
}
