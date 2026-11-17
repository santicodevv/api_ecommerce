using System;
using ApiEcommerce.Interfaces.User;
using ApiEcommerce.Models.User;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;


namespace ApiEcommerce.Services;

public class UserServices : IUserServices
{
    private readonly IUserRepository _users;
    private string? SecretKey;
    public UserServices(IUserRepository userRepository, IConfiguration configuration)
    {
        _users = userRepository;
        SecretKey = configuration.GetValue<string>("ApiSettings:SecretKey");

    }

    public Task<User?> GetUserAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<User>> GetUsersAsync(PaginationParams pagination)
    {
        return await _users.GetUsersAsync(pagination.Page, pagination.PageSize);
    }

    public Task<bool> IsUniqueUser(string username)
    {
        throw new NotImplementedException();
    }

    public async Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto)
    {
        if (string.IsNullOrEmpty(userLoginDto.Username))
        {
            return new UserLoginResponseDto
            {
                Token = "",
                User = null,
                Message = "El username es requerido"
            };
        }

        var user = await _users.Login(userLoginDto);
        if (user == null)
        {
          return new UserLoginResponseDto
            {
                Token = "",
                User = null,
                Message = "Usuario no encontrado"
            };  
        }

        if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.Password))
        {
           return new UserLoginResponseDto
            {
                Token = "",
                User = null,
                Message = "Las creedenciales son incorrectas"
            };   
        }

        // JWT
        var handlerToken = new JwtSecurityTokenHandler();

        if (string.IsNullOrWhiteSpace(SecretKey))
        {
            throw new InvalidOperationException("SecretKey no esta configurada");
        }
        var key = Encoding.UTF8.GetBytes(SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity( new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("username", user.Username),
                new Claim(ClaimTypes.Role, user.Role),
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = handlerToken.CreateToken(tokenDescriptor);

        return new UserLoginResponseDto()
        {
            Token = handlerToken.WriteToken(token),
            User = new UserRegisterDto
            {
                Username = user.Username,
                Name = user.Name,
                Role = user.Role,
                Password = user.Password ?? ""
            },
            Message = "Usuario logueado correctamente"
        };

    }

    public Task<User> Register(CreateUserDto userLoginDto)
    {
        throw new NotImplementedException();
    }
}
