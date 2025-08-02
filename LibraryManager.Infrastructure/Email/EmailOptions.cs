namespace LibraryManager.Infrastructure.Email
{
    internal sealed class EmailOptions
    {
        public string SenderEmail { get; init; } = default;
        public string Sender { get; init; } = default;
        public string Host { get; init; } = default;
        public int Port { get; init; }
    }
}
