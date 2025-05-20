namespace LibraryManager.Application.Queries.User.GetUsers
{
    using LibraryManager.Application.Abstractions.Messaging;

    public sealed record GetUsersQuery
        : IQuery<IList<UserResponse>>;
}
