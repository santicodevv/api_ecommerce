using System;
namespace ApiEcommerce.Interfaces.User;
using ApiEcommerce.Models.User;

public interface IUserServices
{
    Task<List<User>> GetUsersAsync();
    Task<User?> GetUserAsync(int id);
    Task<bool> IsUniqueUser(string username);
    Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto);
    Task<User> Register(CreateUserDto userLoginDto);
}
