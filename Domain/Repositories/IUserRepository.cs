using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository
{
    Task<Users> GetByEmailAsync(string email);
    Task AddUserAsync(Users user);
    Task<bool> EmailExistsAsync(string email);
}