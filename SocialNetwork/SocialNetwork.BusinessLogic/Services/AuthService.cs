using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.BusinessLogic.Interfaces;
using SocialNetwork.Common.DTOs.AuthDTOs;
using SocialNetwork.Common.DTOs.UserDTOs;
using SocialNetwork.Models;
using SocialNetwork.Models.Model;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace SocialNetwork.BusinessLogic.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationContext _context;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public AuthService(IConfiguration configuration, ApplicationContext context, IMapper mapper, IUserService userService)
    {
        _configuration = configuration;
        _context = context;
        _userService = userService;
        _mapper = mapper;
    }
    
    public string Login(AuthUserDTO model)
    {
        var user = GetUser(model);

        if (user != null)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.CurrentCulture)),
                new Claim("Id", user.Id.ToString()),
                new Claim("Name", user.Name),
                new Claim("Email", user.Email),
                new Claim("Password", user.Password)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        return null;
    }
    
    public void Register(RegisterUserDTO model)
    {
        var user = _mapper.Map<RegisterUserDTO, CreateUserDTO>(model);
        
        _userService.Create(user);
    }

    private User GetUser(AuthUserDTO model)
    {
        return _context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
    }
}