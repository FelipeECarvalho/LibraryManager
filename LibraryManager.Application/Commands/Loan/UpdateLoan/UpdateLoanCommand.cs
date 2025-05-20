namespace LibraryManager.Application.Commands.Loan.UpdateLoan
{
    using LibraryManager.Application.Abstractions.Messaging;
    using System.Text.Json.Serialization;

    public sealed record UpdateLoanCommand(DateTime EndDate)
        : ICommand
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
