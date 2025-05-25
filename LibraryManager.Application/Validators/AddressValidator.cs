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
                .Length(2, 50).WithMessage(DomainErrors.Address.StreetInvalidLength);

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage(DomainErrors.Address.NumberRequired)
                .Length(1, 15).WithMessage(DomainErrors.Address.NumberInvalidLength);

            RuleFor(x => x.District)
                .NotEmpty().WithMessage(DomainErrors.Address.DistrictRequired)
                .Length(2, 50).WithMessage(DomainErrors.Address.DistrictInvalidLength);

            RuleFor(x => x.City)
                .NotEmpty().WithMessage(DomainErrors.Address.CityRequired)
                .Length(2, 50).WithMessage(DomainErrors.Address.CityInvalidLength);

            RuleFor(x => x.State)
                .NotEmpty().WithMessage(DomainErrors.Address.StateRequired)
                .Length(2, 50).WithMessage(DomainErrors.Address.StateInvalidLength);

            RuleFor(x => x.CountryCode)
                .NotEmpty().WithMessage(DomainErrors.Address.CountryCodeRequired)
                .Length(2, 5).WithMessage(DomainErrors.Address.CountryCodeInvalidLength);

            RuleFor(x => x.ZipCode)
                .NotEmpty().WithMessage(DomainErrors.Address.ZipCodeRequired)
                .Length(2, 20).WithMessage(DomainErrors.Address.ZipCodeInvalidLength);
        }
    }
}
