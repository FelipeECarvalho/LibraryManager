namespace LibraryManager.Application.Commands.Author.UpdateAuthor
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.ValueObjects;
    using System.Text.Json.Serialization;

    public sealed record UpdateAuthorCommand(Name Name, string Description) 
        : ICommand
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
