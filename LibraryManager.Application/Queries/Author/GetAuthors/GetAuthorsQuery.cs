﻿namespace LibraryManager.Application.Queries.Author.GetAuthors
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Author;

    public sealed record GetAuthorsQuery
        : Paginable, IQuery<IList<AuthorResponse>>;
}
