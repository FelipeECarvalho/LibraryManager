namespace LibraryManager.Application.Abstractions.Messaging
{
    using LibraryManager.Core.Common;

    public interface IQueryHandler<in TQuery, TResponse> 
        where TQuery : IQuery<TResponse>
    {
        Task<Result<TResponse>> HandleAsync(TQuery query, CancellationToken cancellationToken);
    }
}
