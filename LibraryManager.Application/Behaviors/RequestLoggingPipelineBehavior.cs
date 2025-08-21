namespace LibraryManager.Application.Behaviors
{
    using LibraryManager.Application.Interfaces;
    using LibraryManager.Core.Common;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class
        where TResponse : Result
    {
        private readonly IAppLogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> _logger;

        public RequestLoggingPipelineBehavior(
            IAppLogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            string requestName = typeof(TRequest).Name;

            _logger.LogInformation("Processing request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

            TResponse result = await next(cancellationToken);

            if (result.IsSuccess)
            {
                _logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);
            }
            else
            {
                _logger.LogErrorWithContext(result.Error, "Completed request {@RequestName}, {@DateTimeUtc} with error", requestName, DateTime.UtcNow);
            }

            return result;
        }
    }
}
