namespace LibraryManager.Application.Queries.User.GetUsers
{
    using LibraryManager.Application.Abstractions.Messaging;

    public sealed record GetUsersQuery(int Limit = 100, int Offset = 1)
        : IQuery<IList<UserResponse>>;
}
