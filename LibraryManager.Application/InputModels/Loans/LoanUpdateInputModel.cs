namespace LibraryManager.Application.InputModels.Loans
{
    using System.ComponentModel.DataAnnotations;

    public class LoanUpdateInputModel
    {
        [Required]
        public DateTime EndDate { get; init; }
    }
}
