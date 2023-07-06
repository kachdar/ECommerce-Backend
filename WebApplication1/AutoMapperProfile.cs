using AutoMapper;
using WebApplication1.Dtos;
using WebApplication1.Dtos.CategoryDtos;
using WebApplication1.Dtos.UserDtos;
using WebApplication1.Models;

namespace WebApplication1
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Book, GetBookDto>();

            CreateMap<Category, GetCategoryDto>();
            CreateMap<AddCategoryDto, Category>();

            CreateMap<AddUserDto, User>();
            CreateMap<User, GetUserDto>();

            CreateMap<CreateOrderDto, Order>();
            
        }
    }
}
