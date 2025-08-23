namespace LibraryManager.Application.Commands.Auth.RefreshToken
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Abstractions.Repositories;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class RefreshTokenCommandHandler
        : ICommandHandler<RefreshTokenCommand, AuthResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenProvider _tokenProvider;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RefreshTokenCommandHandler(
            IUnitOfWork unitOfWork,
            ITokenProvider tokenProvider,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _unitOfWork = unitOfWork;
            _tokenProvider = tokenProvider;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<Result<AuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshTokenRepository
                .GetByTokenAsync(request.RefreshToken);

            if (refreshToken is null || refreshToken.IsExpired())
            {
                return Result.Failure<AuthResponse>(DomainErrors.General.ExpiredRefreshToken);
            }

            var token = await _tokenProvider.GenerateTokenAsync(refreshToken.User);
            refreshToken.Update(_tokenProvider.GenerateRefreshToken());

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new AuthResponse(token, refreshToken.Token);
        }
    }
}
