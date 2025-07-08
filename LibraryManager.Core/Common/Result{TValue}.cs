namespace LibraryManager.Core.Common
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json.Serialization;

    public class Result<TValue> : Result
    {
        private readonly TValue _value;

        [JsonConstructor]
        protected internal Result(TValue value, bool isSuccess, Error error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        [NotNull]
        public TValue Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("The value of a failure result can not be accessed.");

        public static implicit operator Result<TValue>(TValue value) =>
            value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
    }
}
