namespace LibraryManager.Application.Abstractions
{
    public interface ILogContextEnricher
    {
        IDisposable PushProperty(string propertyName, object value, bool destructureObjects = false);
    }
}
