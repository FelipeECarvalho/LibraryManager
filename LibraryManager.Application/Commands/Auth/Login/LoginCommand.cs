namespace LibraryManager.Application.Commands.Auth.Login
{
    using LibraryManager.Application.Abstractions.Messaging;

    public sealed record LoginCommand(string Email, string Password) 
        : ICommand<string>;
}
