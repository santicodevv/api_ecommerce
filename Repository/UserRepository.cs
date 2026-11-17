using System;
using System.Text;
using ApiEcommerce.Interfaces.User;
using ApiEcommerce.Models.User;
using Microsoft.EntityFrameworkCore;

namespace ApiEcommerce.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;
    public UserRepository(ApplicationDbContext dbContext)
    {
        _db = dbContext;
    }

    public async Task<User?> GetUserAsync(int id)
    {
        return await _db.Users.FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<List<User>> GetUsersAsync(int page = 1, int pageSize = 10)
    {
        var result = _db.Users.OrderBy(user => user.Id)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        
        return await result;
    }

    public async Task<bool> IsUniqueUser(string username)
    {
        return await _db.Users.AnyAsync(user => user.Username.ToLower().Trim() == username.ToLower().Trim());
    }

    public async Task<User> Login(UserLoginDto userLoginDto)
    {
        var user = await _db.Users.FirstOrDefaultAsync(user => user.Username.ToLower().Trim() == userLoginDto.Username.ToLower().Trim());

        return user!;
    }

    public async Task<User> Register(CreateUserDto createUserDto)
    {
        var encrypedPassword = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);
        var user = new User
        {
            Username = createUserDto.Username ?? "No username",
            Name = createUserDto.Name,
            Role = createUserDto.Role,
            Email = createUserDto.Email,
            Password = encrypedPassword
        };

        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();

        return user;
    }
}
