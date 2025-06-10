namespace LibraryManager.Core.Exceptions
{
    using LibraryManager.Core.Errors;

    public class LibraryNotFoundException : Exception
    {
        public LibraryNotFoundException() : base(DomainErrors.Library.NotFound)
        {
        }
    }
}
