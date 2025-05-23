namespace LibraryManager.Application.Behaviors
{
    using FluentValidation;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Enums;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class ValidationPipelineBehavior<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
        where TResponse : Result 
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
        {
            if (!_validators.Any())
            {
                return await next(ct);
            }

            var errors = _validators
                .Select(validator => validator.Validate(request))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validatioFailure => validatioFailure is not null)
                .Select(failure => new Error(
                    failure.PropertyName,
                    failure.ErrorMessage,
                    ErrorType.Validation))
                .Distinct()
                .ToArray();

            if (errors.Length != 0) 
            {
                return CreateValidationResult<TResponse>(errors);
            }

            return await next(ct);
        }

        private static TResult CreateValidationResult<TResult>(Error[] errors)
            where TResult : Result
        {
            if (typeof(TResult) == typeof(Result))
            {
                return (ValidationResult.WithErrors(errors) as TResult)!;
            }

            var validationResult = typeof(ValidationResult<>)
                .GetGenericTypeDefinition()
                .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
                .GetMethod(nameof(ValidationResult.WithErrors))
                .Invoke(null, [errors]);

            return (TResult)validationResult;
        }
    }
}
