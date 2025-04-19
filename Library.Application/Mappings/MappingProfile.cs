using AutoMapper;
using Library.Application.DTOs;
using Library.Application.InputModels.Books;
using Library.Core.Entities;

namespace Library.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<Author, AuthorDto>();

            CreateMap<BookCreateInputModel, Book>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
