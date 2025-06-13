namespace LibraryManager.Core.Common
{
    using LibraryManager.Core.Enums;

    public interface IValidationResult
    {
        public static readonly Error ValidationError = new(
            "ValidationError",
            "A validation problem occorred.",
            ErrorType.Validation);

        Error[] Errors { get; }
    }
}
