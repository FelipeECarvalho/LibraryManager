namespace LibraryManager.Infrastructure.BackgroundJobs.ScheduledJobs
{
    public class JobSchedulesOptions
    {
        public const string SectionName = "BackgroundJobs:Schedules";

        public string ProcessCancelLoansJob { get; set; } = string.Empty;
        public string ProcessNearOverdueLoansJob { get; set; } = string.Empty;
        public string ProcessOverdueLoansJob { get; set; } = string.Empty;
        public string ProcessOverdueLoansFeeJob { get; set; } = string.Empty;
    }
}
