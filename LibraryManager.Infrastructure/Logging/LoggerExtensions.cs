namespace LibraryManager.Infrastructure.Logging
{
    using Microsoft.Extensions.Logging;
    using Serilog.Context;

    public static class LoggerExtensions
    {
        public static void LogErrorWithContext<T>(
            this ILogger<T> logger,
            Core.Common.Error error,
            string message,
            params object[] args)
        {
            using (LogContext.PushProperty("ErrorDetails", error, true))
            {
                logger.LogError(message, args);
            }
        }

        public static void LogInformationWithContext<T>(
            this ILogger<T> logger,
            string propertyName,
            object value,
            string message,
            params object[] args)
        {
            using (LogContext.PushProperty(propertyName, value, true))
            {
                logger.LogInformation(message, args);
            }
        }
    }
}
