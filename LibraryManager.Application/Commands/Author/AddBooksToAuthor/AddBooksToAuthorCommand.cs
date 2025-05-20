namespace LibraryManager.Application.Commands.Author.AddBooksToAuthor
{
    using LibraryManager.Application.Abstractions.Messaging;
    using System.Text.Json.Serialization;

    public sealed record AddBooksToAuthorCommand(IList<Guid> BookIds) : ICommand
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
