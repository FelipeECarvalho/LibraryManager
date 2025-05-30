namespace LibraryManager.Application.Abstractions.Messaging
{
    using LibraryManager.Core.Common;
    using MediatR;

    public interface IBaseCommand;

    public interface ICommand : IBaseCommand, IRequest<Result>;

    public interface ICommand<TResponse> : IBaseCommand, IRequest<Result<TResponse>>;
}
