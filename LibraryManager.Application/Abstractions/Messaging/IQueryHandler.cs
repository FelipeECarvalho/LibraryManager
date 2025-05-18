namespace LibraryManager.Application.Abstractions.Messaging
{
    using LibraryManager.Core.Common;
    using MediatR;

    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>;
}
