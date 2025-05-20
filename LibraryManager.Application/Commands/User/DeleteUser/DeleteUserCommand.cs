namespace LibraryManager.Application.Commands.User.DeleteUser
{
    using LibraryManager.Application.Abstractions.Messaging;

    public sealed record DeleteUserCommand(Guid Id)
        : ICommand;
}
