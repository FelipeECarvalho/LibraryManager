namespace LibraryManager.Application.Commands.Book.UpdateBookStock
{
    using LibraryManager.Application.Abstractions.Messaging;
    using System;

    public sealed record UpdateBookStockCommand(Guid Id, int StockNumber)
        : ICommand;
}
