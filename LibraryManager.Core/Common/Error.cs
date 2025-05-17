namespace LibraryManager.Core.Common
{
    public sealed record Error(string Code, string? Description = null)
    {
        public static readonly Error None = new(string.Empty, string.Empty);

        public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");

        public static readonly Error ConditionNotMet = new("Error.ConditionNotMet", "The specified condition was not met.");

        public static readonly Error ProvidedBooksNotFound = new("Books.ProvidedBooksNotFound", "One or more of the provided books were not found.");
    }
}
