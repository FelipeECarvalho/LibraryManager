namespace Library.Application.DTOs
{
    public class LoanDto
    {
        public int Id { get; set; }
        public BookDto Book { get; set; }
        public UserDto User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
