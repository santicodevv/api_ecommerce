using System;
namespace ApiEcommerce.Interfaces.User;
using ApiEcommerce.Models.User;

public interface IUserServices
{
    Task<User?> GetUserAsync(int id);
    Task<List<User>> GetUsersAsync(PaginationParams pagination);
    Task<bool> IsUniqueUser(string username);
    Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto);
    Task<User> Register(CreateUserDto userLoginDto);
}
