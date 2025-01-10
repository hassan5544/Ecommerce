using Application.Dtos.User;

namespace Application.JWTGenrator;

public interface IJwtTokenGenerator
{
    string GenerateToken(UserDto user);

}