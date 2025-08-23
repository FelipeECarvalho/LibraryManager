namespace LibraryManager.Application.Commands.Auth.RefreshToken
{
    using LibraryManager.Application.Abstractions.Messaging;

    public sealed record RefreshTokenCommand(string RefreshToken)
        : ICommand<AuthResponse>;
}
