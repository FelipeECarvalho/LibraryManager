namespace LibraryManager.Infrastructure.Logging
{
    public interface ILogContextEnricher
    {
        IDisposable PushProperty(string propertyName, object value, bool destructureObjects = false);
    }
}
