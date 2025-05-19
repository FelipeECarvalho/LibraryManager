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

            public static Error DescriptionTooLong => new(
                "Author.DescriptionTooLong",
                $"The author description must be less than 256 characters long.",
                ErrorType.Validation);
        }

        public static class Name
        {
            public static Error FirstNameRequired => new(
                "Name.FirstNameRequired",
                $"The first name is required",
                ErrorType.Validation);

            public static Error LastNameRequired => new(
                "Name.LastNameRequired",
                $"The last name is required",
                ErrorType.Validation);

            public static Error FirstNameLengthError => new(
                "Name.FirstNameLengthError",
                $"The first name must be between 2 and 100 characters long",
                ErrorType.Validation);

            public static Error LastNameLengthError => new(
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

            public static Error TitleTooLong => new(
                "Book.TitleTooLong",
                "The book title must be between 2 and 100 characters long.",
                ErrorType.Validation);

            public static Error IsbnRequired => new(
                "Book.IsbnRequired",
                "The book ISBN is required.",
                ErrorType.Validation);

            public static Error IsbnTooLong => new(
                "Book.IsbnTooLong",
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
        }
    }
}
