namespace LibraryManager.Application.Abstractions.Messaging
{
    using LibraryManager.Core.Common;
    using MediatR;

    public interface ICommand : IRequest<Result>;

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>;
}
