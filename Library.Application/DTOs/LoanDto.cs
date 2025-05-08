namespace Library.Application.DTOs
{
    public class LoanDto
    {
        public int Id { get; }
        public BookDto Book { get; }
        public UserDto User { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
    }
}
