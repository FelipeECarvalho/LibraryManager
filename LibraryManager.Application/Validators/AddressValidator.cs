namespace LibraryManager.Application.Validators
{
    using FluentValidation;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.ValueObjects;

    internal sealed class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator() 
        {
            RuleFor(x => x.Street)
                .NotEmpty().WithMessage(DomainErrors.Address.StreetRequired)
                .MaximumLength(50).WithMessage(DomainErrors.Address.StreetTooLong);

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage(DomainErrors.Address.NumberRequired)
                .MaximumLength(15).WithMessage(DomainErrors.Address.NumberTooLong);

            RuleFor(x => x.District)
                .NotEmpty().WithMessage(DomainErrors.Address.DistrictRequired)
                .MaximumLength(50).WithMessage(DomainErrors.Address.DistrictTooLong);

            RuleFor(x => x.City)
                .NotEmpty().WithMessage(DomainErrors.Address.CityRequired)
                .MaximumLength(50).WithMessage(DomainErrors.Address.CityTooLong);

            RuleFor(x => x.State)
                .NotEmpty().WithMessage(DomainErrors.Address.StateRequired)
                .MaximumLength(50).WithMessage(DomainErrors.Address.StateTooLong);

            RuleFor(x => x.CountryCode)
                .NotEmpty().WithMessage(DomainErrors.Address.CountryCodeRequired)
                .MaximumLength(5).WithMessage(DomainErrors.Address.CountryCodeTooLong);

            RuleFor(x => x.ZipCode)
                .NotEmpty().WithMessage(DomainErrors.Address.ZipCodeRequired)
                .MaximumLength(20).WithMessage(DomainErrors.Address.ZipCodeTooLong);
        }
    }
}
