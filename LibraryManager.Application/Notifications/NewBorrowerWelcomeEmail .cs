namespace LibraryManager.Application.Notifications
{
    using LibraryManager.Core.Entities;

    public sealed class NewBorrowerWelcomeEmail : EmailBase
    {
        private readonly Borrower _data;

        public NewBorrowerWelcomeEmail(Borrower data)
            : base(data?.Email)
        {
            ArgumentNullException.ThrowIfNull(data);
            ArgumentNullException.ThrowIfNull(data.Name);

            _data = data;
        }

        public override string Subject =>
            "Welcome to LibraryManager!";

        public override string Body => $@"
            Hello, {_data.Name.FullName},

            Welcome and thank you for registering with LibraryManager! We are thrilled to have you on board.

            Our platform gives you access to a vast collection of hundreds of books at fair prices, right at your fingertips. We are constantly updating our catalog to bring you the latest releases and timeless classics.

            Feel free to start exploring our collection today and find your next great read.

            Happy reading!

            Best regards,
            The LibraryManager Team";
    }
}