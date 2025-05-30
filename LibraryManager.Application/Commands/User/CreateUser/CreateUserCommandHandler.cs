﻿namespace LibraryManager.Application.Commands.User.CreateUser
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.User;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Errors;
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
            var validationResult = await ValidateAsync(request, ct);

            if (validationResult.IsFailure)
            {
                return Result.Failure<UserResponse>(validationResult.Error);
            }

            var user = new User(request.Name, request.Document, request.Email, request.BirthDate, request.Address);

            _userRepository.Add(user);

            await _unitOfWork.SaveChangesAsync(ct);

            return UserResponse.FromEntity(user);
        }

        private async Task<Result> ValidateAsync(CreateUserCommand request, CancellationToken ct)
        {
            var isEmailUnique = await _userRepository.IsEmailUnique(request.Email, ct);

            if (!isEmailUnique)
            {
                return Result.Failure(DomainErrors.User.EmailAlreadyExists);
            }

            var isDocumentUnique = await _userRepository.IsDocumentUnique(request.Document, ct);

            if (isDocumentUnique)
            {
                return Result.Failure(DomainErrors.User.DocumentAlreadyExists);
            }

            return Result.Success();
        }
    }
}
