﻿

namespace Helpers.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid id , string email , string fullName , string role);

}