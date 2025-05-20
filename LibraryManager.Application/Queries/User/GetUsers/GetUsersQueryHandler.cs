namespace LibraryManager.Application.Queries.User.GetUsers
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Repositories;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class GetUsersQueryHandler
        : IQueryHandler<GetUsersQuery, IList<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<IList<UserResponse>>> Handle(GetUsersQuery request, CancellationToken ct)
        {
            var users = await _userRepository.GetAllAsync(ct);

            var usersResponse = users?
                .Select(UserResponse.FromEntity)?
                .ToList();

            return usersResponse;
        }
    }
}
