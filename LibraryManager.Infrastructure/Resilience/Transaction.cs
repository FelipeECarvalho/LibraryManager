namespace LibraryManager.Infrastructure.Resilience
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Infrastructure.Constants;
    using Polly;
    using Polly.Registry;
    using System.Threading;

    internal sealed class ResilientTransaction : ITransaction
    {
        private readonly ResiliencePipeline _resiliencePipeline;
        private readonly IAppLogger<ResilientTransaction> _logger;

        public ResilientTransaction(
            ResiliencePipelineProvider<string> pipelineProvider,
            IAppLogger<ResilientTransaction> logger)
        {
            _logger = logger;
            _resiliencePipeline = pipelineProvider
                .GetPipeline(ResiliencePipelineConstants.DelayedRetry);
        }

        public async Task ExecuteWithRetryAsync(
            Func<CancellationToken?, Task> operation,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _resiliencePipeline.ExecuteAsync(
                    async token => await operation(token),
                    cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(
                "[FATAL] Operation failed after all attempts. Final error: {@Message}",
                ex.Message);
                throw;
            }
        }
    }
}
