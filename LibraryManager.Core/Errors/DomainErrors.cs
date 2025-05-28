namespace LibraryManager.Core.Errors
{
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Enums;

    public static class DomainErrors
    {
        public static class Author
        {
            public static readonly Func<Guid, Error> NotFound = id => new(
                "Author.NotFound",
                $"The author with the ID {id} was not found.",
                ErrorType.NotFound);

            public static Error DescriptionInvalidLength => new(
                "Author.DescriptionInvalidLength",
                $"The author description must be less than 256 characters long.",
                ErrorType.Validation);
        }

        public static class User
        {
            public static readonly Func<Guid, Error> NotFound = id => new(
                "User.NotFound",
                $"The user with the ID {id} was not found.",
                ErrorType.NotFound);

            public static Error DocumentInvalidLength => new(
                "User.DocumentInvalidLength",
                "The document must be between 2 and 30 characters long.",
                ErrorType.Validation);

            public static Error DocumentRequired => new(
                "User.DocumentRequired",
                "The document is required.",
                ErrorType.Validation);

            public static Error EmailAlreadyExists => new(
                "User.EmailAlreadyExists",
                "A user with this email already exists.",
                ErrorType.Validation);

            public static Error DocumentAlreadyExists => new(
                "User.DocumentAlreadyExists",
                "A user with this document already exists.",
                ErrorType.Validation);

            public static Error EmailInvalidLength => new(
                "User.EmailInvalidLength",
                "The email must be between 2 and 50 characters long",
                ErrorType.Validation);

            public static Error EmailRequired => new(
                "User.EmailRequired",
                "The email is required.",
                ErrorType.Validation);

            public static Error InvalidEmail => new(
                "User.EmailInvalid",
                "Invalid email format.",
                ErrorType.Validation);
        }

        public static class Loan
        {
            public static readonly Func<Guid, Error> NotFound = id => new(
                "Loan.NotFound",
                $"The loan with the ID {id} was not found.",
                ErrorType.NotFound);

            public static Error UserIdRequired => new(
                "Loan.UserIdRequired",
                "The user ID is required.",
                ErrorType.Validation);

            public static Error BookIdRequired => new(
                "Loan.BookIdRequired",
                "The book ID is required.",
                ErrorType.Validation);

            public static Error InvalidStartDate => new(
                "Loan.InvalidStartDate",
                "The start date must be earlier than the end date.",
                ErrorType.Validation);

            public static Error StartDateInPast => new(
                "Loan.StartDateInPast",
                "The start date cannot be in the past.",
                ErrorType.Validation);

            public static Error CannotReturnWhenNotBorrowed => new(
                "Loan.CannotReturnWhenNotBorrowed",
                "Cannot return a loan when the book is not currently borrowed by the user.",
                ErrorType.Conflict);

            public static Error BookAlreadyLoaned => new(
                "Loan.BookAlreadyLoaned",
                "This book is already loaned to the user.",
                ErrorType.Validation);

            public static Error CannotApproveWhenNotRequested => new(
                "Loan.CannotApproveWhenNotRequested",
                "The loan can only be approved if it is currently in the 'Requested' status.",
                ErrorType.Validation);

            public static Error CannotCancelInThisStatus => new(
                "Loan.CannotCancelInThisStatus",
                "The loan cannot be cancelled in its current status.",
                ErrorType.Validation);

            public static Error CannotBorrowWhenNotApproved => new(
                "Loan.CannotBorrowWhenNotApproved",
                "The loan can only be marked as borrowed if it is currently approved.",
                ErrorType.Validation);
        }

        public static class Address
        {
            public static Error AddressRequired => new(
                "Address.AddressRequired",
                "The address is required.",
                ErrorType.Validation);

            public static Error StreetRequired => new(
                "Address.StreetRequired",
                "The street is required.",
                ErrorType.Validation);

            public static Error StreetInvalidLength => new(
                "Address.StreetInvalidLength",
                "The street must be between 2 and 50 characters long",
                ErrorType.Validation);

            public static Error NumberRequired => new(
                "Address.NumberRequired",
                "The number is required.",
                ErrorType.Validation);

            public static Error NumberInvalidLength => new(
                "Address.NumberInvalidLength",
                "The number must be between 1 and 15 characters long",
                ErrorType.Validation);

            public static Error DistrictRequired => new(
                "Address.DistrictRequired",
                "The district is required.",
                ErrorType.Validation);

            public static Error DistrictInvalidLength => new(
                "Address.DistrictInvalidLength",
                "The district must be between 2 and 50 characters long",
                ErrorType.Validation);

            public static Error CityRequired => new(
                "Address.CityRequired",
                "The city is required.",
                ErrorType.Validation);

            public static Error CityInvalidLength => new(
                "Address.CityInvalidLength",
                "The city must be between 2 and 50 characters long",
                ErrorType.Validation);

            public static Error StateRequired => new(
                "Address.StateRequired",
                "The state is required.",
                ErrorType.Validation);

            public static Error StateInvalidLength => new(
                "Address.StateInvalidLength",
                "The state must be between 2 and 50 characters long",
                ErrorType.Validation);

            public static Error CountryCodeRequired => new(
                "Address.CountryCodeRequired",
                "The country code is required.",
                ErrorType.Validation);

            public static Error CountryCodeInvalidLength => new(
                "Address.CountryCodeInvalidLength",
                "The country must be between 2 and 5 characters long",
                ErrorType.Validation);

            public static Error ZipCodeRequired => new(
                "Address.ZipCodeRequired",
                "The zip code is required.",
                ErrorType.Validation);

            public static Error ZipCodeInvalidLength => new(
                "Address.ZipCodeInvalidLength",
                "The zip code must be between 2 and 20 characters long",
                ErrorType.Validation);
        }

        public static class Name
        {
            public static Error NameRequired => new(
                "Name.Required",
                $"The name is required",
                ErrorType.Validation);

            public static Error FirstNameRequired => new(
                "Name.FirstNameRequired",
                $"The first name is required",
                ErrorType.Validation);

            public static Error LastNameRequired => new(
                "Name.LastNameRequired",
                $"The last name is required",
                ErrorType.Validation);

            public static Error FirstNameInvalidLength => new(
                "Name.FirstNameLengthError",
                $"The first name must be between 2 and 100 characters long",
                ErrorType.Validation);

            public static Error LastNameInvalidLength => new(
                "Name.LastNameLengthError",
                $"The last name must be between 2 and 100 characters long",
                ErrorType.Validation);
        }

        public static class Book
        {
            public static readonly Func<Guid, Error> NotFound = id => new(
                "Book.NotFound",
                $"The book with the ID {id} was not found.",
                ErrorType.NotFound);

            public static readonly Func<IList<Guid>, Error> NotFoundList = ids => new(
                "Book.NotFound",
                $"The following book IDs were not found: {string.Join(", ", ids)}.",
                ErrorType.NotFound);

            public static Error TitleRequired => new(
                "Book.TitleRequired",
                "The book title is required.",
                ErrorType.Validation);

            public static Error TitleInvalidLength => new(
                "Book.TitleInvalidLength",
                "The book title must be between 2 and 100 characters long.",
                ErrorType.Validation);

            public static Error IsbnRequired => new(
                "Book.IsbnRequired",
                "The book ISBN is required.",
                ErrorType.Validation);

            public static Error IsbnInvalidLength => new(
                "Book.IsbnInvalidLength",
                "The book ISBN must be between 2 and 50 characters long.",
                ErrorType.Validation);

            public static Error PublicationDateRequired => new(
                "Book.PublicationDateRequired",
                "The book Publication Date is required.",
                ErrorType.Validation);

            public static Error StockNumberInvalid => new(
                "Book.StockNumberInvalid",
                "The book Stock number is invalid.",
                ErrorType.Validation);

            public static Error NotAvaliableForLoan => new(
                "Book.NotAvaliableForLoan",
                "The book is not currently avaliable for loan.",
                ErrorType.Validation);
        }
    }
}
