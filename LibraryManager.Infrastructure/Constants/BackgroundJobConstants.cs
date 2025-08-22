namespace LibraryManager.Infrastructure.Constants
{
    public static class BackgroundJob
    {
        public static class Default
        {
            public const string RetryCountKey = "RetryCount";
        }

        public static class Email
        {
            public const string QueuedEmailIdKey = "QueuedEmailId";
            public const string QueuedEmailIdentity = "SendEmail-{0}";
        }
    }
}