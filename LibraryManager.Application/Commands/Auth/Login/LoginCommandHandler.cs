namespace LibraryManager.Application.Commands.Auth.Login
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Repositories;
    using LibraryManager.Infrastructure.Auth;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class LoginCommandHandler
        : ICommandHandler<LoginCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;

        public LoginCommandHandler(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(request.Email, cancellationToken);

            if (user == null)
            {
                return Result.Failure<string>(DomainErrors.User.LoginFailedInvalidEmailOrPassword);
            }

            if (user.PasswordHash != _authService.ComputeHash(request.Password))
            {
                return Result.Failure<string>(DomainErrors.User.LoginFailedInvalidEmailOrPassword);
            }

            user.UpdateLastLogin();

            _userRepository.Update(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _authService.GenerateToken(user.Email.ToString(), user.GetType().Name);
        }
    }
}
