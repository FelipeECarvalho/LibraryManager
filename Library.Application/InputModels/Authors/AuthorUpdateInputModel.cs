namespace Library.Application.InputModels.Authors
{
    using Library.Core.ValueObjects;
    using System.ComponentModel.DataAnnotations;
    
    public class AuthorUpdateInputModel
    {
        public Name Name { get; set; }

        public string Description { get; set; }
    }
}