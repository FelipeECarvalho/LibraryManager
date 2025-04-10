using System.ComponentModel.DataAnnotations;

namespace Library.Application.InputModels.Loans
{
    public class LoanUpdateInputModel
    {
        [Required]
        public DateTime EndDate { get; set; }
    }
}
