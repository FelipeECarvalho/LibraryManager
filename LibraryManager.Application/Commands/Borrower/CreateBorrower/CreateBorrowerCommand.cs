﻿namespace LibraryManager.Application.Commands.Borrower.CreateBorrower
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Borrower;
    using LibraryManager.Core.ValueObjects;

    public sealed record CreateBorrowerCommand(
        Name Name,
        string Document,
        string Email,
        DateTimeOffset BirthDate,
        Address Address) : ICommand<BorrowerResponse>
    {
        public Guid LibraryId { get; set; }
    }
}
