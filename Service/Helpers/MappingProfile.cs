using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Service.DTOs.Account;
using Service.DTOs.Book;

namespace Service.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookCreateDto, Book>();
            CreateMap<BookEditDto, Book>();
            CreateMap<AppUser, UserDto>();
            CreateMap<IdentityRole, RoleDto>();
        }
    }
}
