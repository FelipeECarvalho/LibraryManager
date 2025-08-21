namespace LibraryManager.Application.Commands.Auth.Login
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Interfaces;
    using LibraryManager.Application.Interfaces.Repositories;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class LoginCommandHandler
        : ICommandHandler<LoginCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenProvider _tokenProvider;
        private readonly ILibraryRepository _libraryRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public LoginCommandHandler(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            ITokenProvider tokenProvider,
            IPasswordHasher passwordHasher,
            ILibraryRepository libraryRepository)
        {
            _userRepository = userRepository;
            _libraryRepository = libraryRepository;
            _tokenProvider = tokenProvider;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var library = await _libraryRepository.GetById(request.LibraryId, cancellationToken);

            if (library == null)
            {
                return Result.Failure<string>(DomainErrors.Library.IdNotFound(request.LibraryId));
            }

            var user = await _userRepository.GetByEmail(request.Email, library.Id, cancellationToken);

            if (user == null)
            {
                return Result.Failure<string>(DomainErrors.User.LoginFailedInvalidEmailOrPassword);
            }

            if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
            {
                return Result.Failure<string>(DomainErrors.User.LoginFailedInvalidEmailOrPassword);
            }

            user.UpdateLastLogin();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return await _tokenProvider.GenerateTokenAsync(user);
        }
    }
}
