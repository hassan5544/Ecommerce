using Application.Dtos.User;

namespace Application.Interfaces;

public interface IUserRepository
{
    Task GetUserByEmailAsync(string email);
    Task<UserDto?> GetUserByIdAsync(Guid id);
    Task<bool> EmailExistsAsync(string email);
    Task AddUserAsync(RegisterDto user, string password);
}