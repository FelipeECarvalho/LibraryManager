namespace Library.Application.InputModels.Loans
{
    using System.ComponentModel.DataAnnotations;

    public class LoanCreateInputModel
    {
        [Required]
        public Guid UserId { get; init; }

        [Required]
        public Guid BookId { get; init; }

        [Required]
        public DateTime StartDate { get; init; }

        [Required]
        public DateTime EndDate { get; init; }
    }
}
