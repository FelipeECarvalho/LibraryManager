namespace LibraryManager.Application.Behaviors
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using MediatR;
    using System.Threading.Tasks;

    internal sealed class TransactionResiliencePipelineBehaviour<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseCommand
        where TResponse : Result
    {
        private readonly ITransaction _transaction;

        public TransactionResiliencePipelineBehaviour(
            ITransaction transaction)
        {
            _transaction = transaction;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            return await _transaction.ExecuteWithRetryAsync(
                (token) => next(token),
                cancellationToken);
        }
    }
}
