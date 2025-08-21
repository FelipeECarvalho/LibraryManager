namespace LibraryManager.Application.Interfaces
{
    public interface IEmail
    {
        public string To { get; }

        public string Subject { get; }

        public string Body { get; }

        public string Cc { get; }

        public string Bcc { get; }
    }
}
