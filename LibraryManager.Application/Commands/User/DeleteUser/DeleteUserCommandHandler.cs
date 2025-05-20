namespace LibraryManager.Application.Commands.User.DeleteUser
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DeleteUserCommandHandler
        : ICommandHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken ct)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, ct);

            if (user == null)
            {
                return Result.Failure(DomainErrors.User.NotFound(request.Id));
            }

            user.SetDeleted();
            await _unitOfWork.SaveChangesAsync(ct);

            return Result.Success();
        }
    }
}
