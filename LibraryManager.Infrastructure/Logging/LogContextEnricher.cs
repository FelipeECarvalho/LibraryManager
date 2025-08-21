namespace LibraryManager.Infrastructure.Logging
{
    using LibraryManager.Application.Interfaces;
    using Serilog.Context;

    internal sealed class LogContextEnricher 
        : ILogContextEnricher
    {
        public IDisposable PushProperty(string propertyName, object value, bool destructureObjects = false)
        {
            return LogContext.PushProperty(propertyName, value, destructureObjects);
        }
    }
}
