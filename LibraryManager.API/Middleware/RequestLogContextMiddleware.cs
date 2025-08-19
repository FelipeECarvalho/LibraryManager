namespace LibraryManager.API.Middleware
{
    using LibraryManager.Core.Abstractions;

    public class RequestLogContextMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogContextEnricher _contextEnricher;

        public RequestLogContextMiddleware(
            RequestDelegate next,
            ILogContextEnricher contextEnricher)
        {
            _next = next;
            _contextEnricher = contextEnricher;
        }

        public Task InvokeAsync(HttpContext context)
        {
            using (_contextEnricher.PushProperty("CorrelationId", context.TraceIdentifier))
            {
                return _next(context);
            }
        }
    }
}
