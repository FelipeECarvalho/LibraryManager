namespace LibraryManager.Core.Common
{
    using LibraryManager.Core.Enums;

    public record Error
    {
        public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);

        public static readonly Error NullValue = new("General.Null", "Null value was provided", ErrorType.Failure);

        public Error(string code, string? description = null, ErrorType? type = null)
        {
            Code = code;
            Description = description;
            Type = type;
        }

        public string Code { get; }

        public string? Description { get; }

        public ErrorType? Type { get; }

        public static implicit operator string?(Error error)
        {
            return error != null ? error.Description : default;
        }
    }
}
