namespace LibraryManager.Application.Queries.User.GetUserById
{
    using LibraryManager.Application.Abstractions.Messaging;

    public sealed record GetUserByIdQuery(Guid Id) 
        : IQuery<UserResponse>;
}
