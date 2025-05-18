namespace LibraryManager.Application.Commands.Author.Add
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.ValueObjects;

    public sealed record AddAuthorCommand(Name Name, string Description) : ICommand;
}
