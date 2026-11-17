using System;
namespace ApiEcommerce.Interfaces.User;
using ApiEcommerce.Models.User;

public interface IUserServices
{
    Task<ServiceResult<User?>> GetUserAsync(int id);
    Task<List<User>> GetUsersAsync(PaginationParams pagination);
    Task<bool> IsUniqueUser(string username);
    Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto);
    Task<ServiceResult<User>> Register(CreateUserDto createUserDto);
}
