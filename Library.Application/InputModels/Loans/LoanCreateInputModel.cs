using System.ComponentModel.DataAnnotations;

namespace Library.Application.InputModels.Loans
{
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
