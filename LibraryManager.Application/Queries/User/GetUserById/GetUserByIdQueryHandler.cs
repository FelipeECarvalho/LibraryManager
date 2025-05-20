namespace LibraryManager.Application.Queries.User.GetUserById
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetUserByIdQueryHandler
        : IQueryHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken ct)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, ct);

            if (user == null)
            {
                return Result.Failure<UserResponse>(DomainErrors.User.NotFound(request.Id));
            }

            return UserResponse.FromEntity(user);
        }
    }
}
