namespace LibraryManager.Application.Commands.User.UpdateUser
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.ValueObjects;
    using System.Text.Json.Serialization;

    public sealed record UpdateUserCommand(
        Name Name,
        Address Address)
        : ICommand
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
