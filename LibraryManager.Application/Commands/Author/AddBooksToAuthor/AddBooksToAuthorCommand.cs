namespace LibraryManager.Application.Commands.Author.AddBooksToAuthor
{
    using LibraryManager.Core.Common;
    using MediatR;
    using System.Text.Json.Serialization;

    public sealed record AddBooksToAuthorCommand(IList<Guid> BookIds) : IRequest<Result>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
