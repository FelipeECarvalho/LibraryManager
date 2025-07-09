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

        public TValue Value => IsSuccess
            ? _value!
            : default;

        public static implicit operator Result<TValue>(TValue value) =>
            value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
    }
}
