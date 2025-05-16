namespace LibraryManager.Application.DTOs
{
    public class LoanDto
    {
        public Guid Id { get; init; }

        public BookDto Book { get; init; }

        public UserDto User { get; init; }

        public DateTime StartDate { get; init; }

        public DateTime EndDate { get; init; }

        public bool IsReturned { get; init; }
    }
}
