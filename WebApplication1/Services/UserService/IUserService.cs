using WebApplication1.Dtos.UserDtos;

namespace WebApplication1.Services.UserService
{
    public interface IUserService
    {
        Task<string> CreateUser(AddUserDto user);
        Task<string> Login(AddUserDto user);
        Task<GetUserDto> GetUserInfo();
        Task<GetUserDto?> GetUserDetails(int id);
    }
}
