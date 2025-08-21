namespace LibraryManager.Application.Interfaces
{
    public interface ILogContextEnricher
    {
        IDisposable PushProperty(string propertyName, object value, bool destructureObjects = false);
    }
}
