namespace LibraryManager.Core.Abstractions
{
    public interface ILogContextEnricher
    {
        IDisposable PushProperty(string propertyName, object value, bool destructureObjects = false);
    }
}
