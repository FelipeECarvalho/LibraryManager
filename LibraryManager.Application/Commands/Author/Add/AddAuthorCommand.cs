namespace LibraryManager.Application.Commands.Author.Add
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.ValueObjects;

    public class AddAuthorCommand : ICommand
    {
        public AddAuthorCommand(Name name, string description)
        {
            Name = name;
            Description = description;
        }

        public Name Name{ get; set; }

        public string Description { get; set; }
    }
}
