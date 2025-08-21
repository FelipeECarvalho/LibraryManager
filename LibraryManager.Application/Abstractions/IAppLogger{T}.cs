namespace LibraryManager.Application.Abstractions
{
    using LibraryManager.Core.Common;

    public interface IAppLogger<T>
    {
        void LogInformation(string message, params object[] args);

        void LogErrorWithContext(Error error, string message, params object[] args);
    }
}
