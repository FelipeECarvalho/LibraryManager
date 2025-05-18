namespace LibraryManager.Application.Commands.Author.UpdateAuthor
{
    using LibraryManager.Core.Common;
    using LibraryManager.Core.ValueObjects;
    using MediatR;
    using System.Text.Json.Serialization;

    public sealed record UpdateAuthorCommand(Name Name, string Description) : IRequest<Result>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
