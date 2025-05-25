namespace LibraryManager.Application.Commands.Loan.UpdateLoanStatus
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Enums;
    using System.Text.Json.Serialization;

    public sealed record UpdateLoanStatusCommand(LoanStatus Status)
        : ICommand
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
