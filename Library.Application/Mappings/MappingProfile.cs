namespace Library.Application.Mappings
{
    using AutoMapper;
    using Library.Application.DTOs;
    using Library.Application.InputModels.Authors;
    using Library.Application.InputModels.Books;
    using Library.Application.InputModels.Loans;
    using Library.Application.InputModels.Users;
    using Library.Core.Entities;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<User, UserDto>();
            CreateMap<Author, AuthorDto>();
            CreateMap<Loan, LoanDto>();

            CreateMap<AuthorCreateInputModel, Author>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<BookCreateInputModel, Book>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UserCreateInputModel, User>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<LoanCreateInputModel, Loan>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
