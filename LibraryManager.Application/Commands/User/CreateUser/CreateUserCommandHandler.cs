namespace LibraryManager.Application.Commands.User.CreateUser
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.User;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class CreateUserCommandHandler
        : ICommandHandler<CreateUserCommand, UserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<Result<UserResponse>> Handle(CreateUserCommand request, CancellationToken ct)
        {
            var user = new User(request.Name, request.Document, request.Email, request.BirthDate, request.Address);

            _userRepository.Add(user);

            await _unitOfWork.SaveChangesAsync(ct);

            return UserResponse.FromEntity(user);
        }
    }
}
