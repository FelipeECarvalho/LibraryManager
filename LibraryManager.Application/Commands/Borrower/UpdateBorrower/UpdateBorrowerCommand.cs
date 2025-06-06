namespace LibraryManager.Application.Commands.Borrower.UpdateBorrower
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.ValueObjects;
    using System.Text.Json.Serialization;

    public sealed record UpdateBorrowerCommand(
        Name Name,
        Address Address)
        : ICommand
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
