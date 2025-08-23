namespace LibraryManager.Application.Commands.Auth.Login
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Abstractions.Repositories;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class LoginCommandHandler
        : ICommandHandler<LoginCommand, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenProvider _tokenProvider;
        private readonly ILibraryRepository _libraryRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public LoginCommandHandler(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            ITokenProvider tokenProvider,
            IPasswordHasher passwordHasher,
            ILibraryRepository libraryRepository,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _libraryRepository = libraryRepository;
            _tokenProvider = tokenProvider;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<Result<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var library = await _libraryRepository.GetById(request.LibraryId, cancellationToken);

            if (library == null)
            {
                return Result.Failure<AuthResponse>(DomainErrors.Library.IdNotFound(request.LibraryId));
            }

            var user = await _userRepository.GetByEmail(request.Email, library.Id, cancellationToken);

            if (user == null)
            {
                return Result.Failure<AuthResponse>(DomainErrors.User.LoginFailedInvalidEmailOrPassword);
            }

            if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
            {
                return Result.Failure<AuthResponse>(DomainErrors.User.LoginFailedInvalidEmailOrPassword);
            }

            user.UpdateLastLogin();

            var token = await _tokenProvider.GenerateTokenAsync(user);

            var refreshToken = new Core.Entities.RefreshToken(
                user,
                _tokenProvider.GenerateRefreshToken());

            _refreshTokenRepository.Add(refreshToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new AuthResponse(token, refreshToken.Token);
        }
    }
}
