namespace LibraryManager.Infrastructure.BackgroundJobs
{
    public class BackgroundJobOptions
    {
        public const string SectionName = "BackgroundJobs";

        public int DefaultMaxRetries { get; set; } = 3;
    }
}
