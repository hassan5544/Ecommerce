using Domain.Entities;

namespace Domain.Repositories;

public interface IJwtTokenGenerator
{
    string GenerateToken(Users user);

}