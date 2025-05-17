namespace LibraryManager.Application.Commands.AddAuthor
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.ValueObjects;

    public class AddAuthorCommand : ICommand
    {
        public Name Name{ get; set; }
        public string Description { get; set; }
    }
}
