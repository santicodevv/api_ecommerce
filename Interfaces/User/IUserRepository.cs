using System;
namespace ApiEcommerce.Interfaces.User;
using ApiEcommerce.Models.User;

public interface IUserRepository
{
    Task<List<User>> GetUsersAsync(int page = 1, int pageSize = 10);
    Task<User?> GetUserAsync(int id);
    Task<bool> IsUniqueUser(string username);
    Task<User> Login(UserLoginDto userLoginDto);
    Task<User> Register(CreateUserDto userLoginDto);
}
