using System;
using System.ComponentModel.DataAnnotations;

namespace ApiEcommerce.Models.User;

public class UserRegisterDto
{
    public string? ID { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required string Username { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Email { get; set; }
}