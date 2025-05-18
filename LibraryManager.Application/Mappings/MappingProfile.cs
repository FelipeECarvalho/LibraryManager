namespace LibraryManager.Application.Mappings
{
    using AutoMapper;
    using LibraryManager.Application.InputModels.Books;
    using LibraryManager.Application.InputModels.Loans;
    using LibraryManager.Application.InputModels.Users;
    using LibraryManager.Core.Entities;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookCreateInputModel, Book>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UserCreateInputModel, User>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<LoanCreateInputModel, Loan>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
