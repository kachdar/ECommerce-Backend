﻿using AutoMapper;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Dtos.UserDtos;

namespace WebApplication1.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly PostgreSqlContext postgreSqlContext;
        private readonly IMapper mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(
            PostgreSqlContext postgreSqlContext, 
            IMapper mapper, 
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor
            ) 
        {
            this.postgreSqlContext = postgreSqlContext;
            this.mapper = mapper;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<string> CreateUser(AddUserDto newUser)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            newUser.Password = passwordHash;

            var user = mapper.Map<User>( newUser );
            
            postgreSqlContext.Users.Add( user );
            await postgreSqlContext.SaveChangesAsync();

            string token = CreateToken(mapper.Map<User>(newUser));

            return token; 
        }

        public async Task<string> Login(AddUserDto user)
        {
            var result = postgreSqlContext.Users.FirstOrDefault(u => u.Email == user.Email);

            if (result == null)
                throw new Exception("User's not found");

            if (!BCrypt.Net.BCrypt.Verify(user.Password, result.Password))
                throw new Exception("Wrong credentials");

            string token = CreateToken(result);

            return token;
        }

        public async Task<GetUserDto> GetUserInfo() 
        {
            if (_httpContextAccessor.HttpContext is not null)
            {
                var result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);

                var user = postgreSqlContext.Users.FirstOrDefault(u => u.Email == result);

                if (user == null)
                    return null;

                return mapper.Map<GetUserDto>(user);
            }
            return null;
        }

        public async Task<GetUserDto?> GetUserDetails(int id)
        {
            var user = postgreSqlContext.Users.FirstOrDefault(u => u.Id == id);

            if (user is null)
                return null;

            return mapper.Map<GetUserDto>(user);
        }

        private string CreateToken(User user) 
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
