namespace LibraryManager.Application.Abstractions.Messaging
{
    using LibraryManager.Core.Common;
    using MediatR;

    public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
}
