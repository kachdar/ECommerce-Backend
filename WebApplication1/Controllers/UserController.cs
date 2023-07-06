using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos.CategoryDtos;
using WebApplication1.Dtos.UserDtos;
using WebApplication1.Services.UserService;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<GetUserDto>> GetUserInfo()
        {
            var result = await _userService.GetUserInfo();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> GetUserDetails(int id)
        {
            var result = await _userService.GetUserDetails(id);
            return Ok(result);
        }

    }
}
