namespace LibraryManager.Application.Common
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;

    public class Mediator : IMediator
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public Mediator(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task<Result> DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken)
            where TCommand : ICommand
        {
            using var scope = _scopeFactory.CreateScope();
            
            var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();

            return await handler.HandleAsync(command, cancellationToken);
        }

        public async Task<Result<TResponse>> DispatchAsync<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken) 
            where TCommand : ICommand<TResponse>
        {
            using var scope = _scopeFactory.CreateScope();

            var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand, TResponse>>();

            return await handler.HandleAsync(command, cancellationToken);
        }

        public async Task<Result<TResponse>> QueryAsync<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken)
            where TQuery : IQuery<TResponse>
        {
            using var scope = _scopeFactory.CreateScope();

            var queryHandler = scope.ServiceProvider.GetRequiredService<IQueryHandler<TQuery, TResponse>>();

            return await queryHandler.HandleAsync(query, cancellationToken);
        }
    }
}
