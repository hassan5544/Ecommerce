using System.Security.Cryptography;
using Domain.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Infrastructure.Repositories;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        // Generate a random salt
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Derive the key
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        // Combine the salt and hash for storage
        return $"{Convert.ToBase64String(salt)}.{hashed}";
    }

    public bool VerifyPassword(string hashedPassword, string password)
    {
        // Split the stored hash into salt and hash parts
        var parts = hashedPassword.Split('.');
        if (parts.Length != 2) return false;

        byte[] salt = Convert.FromBase64String(parts[0]);
        string storedHash = parts[1];

        // Hash the provided password with the same salt
        string computedHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        // Compare the hashes
        return storedHash == computedHash;
    }
}
