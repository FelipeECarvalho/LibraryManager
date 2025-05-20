namespace LibraryManager.Application.Commands.User.UpdateUser
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class UpdateUserCommandHandler
        : ICommandHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken ct)
        {
            var validationResult = Validate(request);

            if (validationResult.IsFailure)
            {
                return validationResult;
            }

            var user = await _userRepository.GetByIdAsync(request.Id, ct);

            if (user == null)
            {
                return Result.Failure(DomainErrors.User.NotFound(request.Id));
            }

            user.Update(request.Name, request.Address);

            await _unitOfWork.SaveChangesAsync(ct);

            return Result.Success();
        }

        private static Result Validate(UpdateUserCommand request)
        {
            if (request == null)
            {
                return Result.Failure(Error.NullValue);
            }

            if (request.Name == null)
            {
                return Result.Failure(DomainErrors.Name.NameRequired);
            }

            if (string.IsNullOrWhiteSpace(request.Name?.FirstName))
            {
                return Result.Failure(DomainErrors.Name.FirstNameRequired);
            }

            if (request.Name.FirstName.Length > 100 || request.Name.FirstName.Length < 2)
            {
                return Result.Failure(DomainErrors.Name.FirstNameLengthError);
            }

            if (string.IsNullOrWhiteSpace(request.Name?.LastName))
            {
                return Result.Failure(DomainErrors.Name.LastNameRequired);
            }

            if (request.Name.LastName.Length > 100 || request.Name.LastName.Length < 2)
            {
                return Result.Failure(DomainErrors.Name.LastNameLengthError);
            }

            if (request.Address is null)
            {
                return Result.Failure(DomainErrors.Address.AddressRequired);
            }

            if (string.IsNullOrWhiteSpace(request.Address.Street))
            {
                return Result.Failure(DomainErrors.Address.StreetRequired);
            }

            if (request.Address.Street.Length > 50)
            {
                return Result.Failure(DomainErrors.Address.StreetTooLong);
            }

            if (string.IsNullOrWhiteSpace(request.Address.Number))
            {
                return Result.Failure(DomainErrors.Address.NumberRequired);
            }

            if (request.Address.Number.Length > 15)
            {
                return Result.Failure(DomainErrors.Address.NumberTooLong);
            }

            if (string.IsNullOrWhiteSpace(request.Address.District))
            {
                return Result.Failure(DomainErrors.Address.DistrictRequired);
            }

            if (request.Address.District.Length > 50)
            {
                return Result.Failure(DomainErrors.Address.DistrictTooLong);
            }

            if (string.IsNullOrWhiteSpace(request.Address.City))
            {
                return Result.Failure(DomainErrors.Address.CityRequired);
            }

            if (request.Address.City.Length > 50)
            {
                return Result.Failure(DomainErrors.Address.CityTooLong);
            }

            if (string.IsNullOrWhiteSpace(request.Address.State))
            {
                return Result.Failure(DomainErrors.Address.StateRequired);
            }

            if (request.Address.State.Length > 50)
            {
                return Result.Failure(DomainErrors.Address.StateTooLong);
            }

            if (string.IsNullOrWhiteSpace(request.Address.CountryCode))
            {
                return Result.Failure(DomainErrors.Address.CountryCodeRequired);
            }

            if (request.Address.CountryCode.Length > 5)
            {
                return Result.Failure(DomainErrors.Address.CountryCodeTooLong);
            }

            if (string.IsNullOrWhiteSpace(request.Address.ZipCode))
            {
                return Result.Failure(DomainErrors.Address.ZipCodeRequired);
            }

            if (request.Address.ZipCode.Length > 20)
            {
                return Result.Failure(DomainErrors.Address.ZipCodeTooLong);
            }

            return Result.Success();
        }
    }
}
