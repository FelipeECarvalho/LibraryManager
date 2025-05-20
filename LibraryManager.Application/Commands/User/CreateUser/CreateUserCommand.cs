namespace LibraryManager.Application.Commands.User.CreateUser
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.User;
    using LibraryManager.Core.ValueObjects;

    public sealed record CreateUserCommand(
        Name Name,
        string Document,
        string Email,
        DateTimeOffset BirthDate,
        Address Address) : ICommand<UserResponse>;
}
