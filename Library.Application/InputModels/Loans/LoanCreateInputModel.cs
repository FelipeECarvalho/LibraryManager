namespace Library.Application.InputModels.Loans
{
    using System.ComponentModel.DataAnnotations;
    
    public class LoanCreateInputModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
