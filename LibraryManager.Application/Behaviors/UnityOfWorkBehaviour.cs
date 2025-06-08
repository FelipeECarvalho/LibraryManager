namespace LibraryManager.Application.Behaviors
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Interfaces.Repositories;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class UnityOfWorkBehaviour<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseCommand
        where TResponse : Result
    {
        private readonly IUnitOfWork _unityOfWork;

        public UnityOfWorkBehaviour(IUnitOfWork unitOfWork)
        {
            _unityOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next(cancellationToken);

            await _unityOfWork.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}
