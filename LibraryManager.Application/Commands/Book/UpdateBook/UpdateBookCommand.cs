namespace LibraryManager.Application.Commands.Book.UpdateBook
{
    using LibraryManager.Application.Abstractions.Messaging;
    using System.Text.Json.Serialization;

    public sealed record UpdateBookCommand(
        string Title,
        string Description,
        DateTimeOffset PublicationDate)
        : ICommand
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
