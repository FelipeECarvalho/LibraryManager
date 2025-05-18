namespace LibraryManager.Application.Abstractions.Messaging
{
    using LibraryManager.Core.Common;

    public interface IMediator
    {
        Task<Result> DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) 
            where TCommand : ICommand;

        Task<Result<TResponse>> DispatchAsync<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand<TResponse>;

        Task<Result<TResponse>> QueryAsync<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : IQuery<TResponse>;
    }
}