using Domain.Repositories;
using Domain.Shared;

namespace Domain.Entities;

public class Users : BaseEntity
{
    public string FullName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string Role { get; private set; } = "Customer";
    public List<Orders> Orders { get; private set; } = new List<Orders>();

    private Users()
    {
        // Required by EF
    }

    private Users(string fullName , string email, string password )
    {
        FullName = fullName;
        Email = email;
        PasswordHash = password;
        Role = "Customer";
        InsertDate = DateTime.UtcNow;
    }
    
   
    public static Users CreateCustomer(string fullName, string email, string password, IPasswordHasher passwordHasher)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name cannot be empty.", nameof(fullName));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.", nameof(email));

        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty.", nameof(password));

        if (passwordHasher == null)
            throw new ArgumentNullException(nameof(passwordHasher));

        return new Users(fullName, email, passwordHasher.HashPassword(password));
    }
    
    public bool VerifyPassword(string password, IPasswordHasher passwordHasher)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty.", nameof(password));

        return passwordHasher.VerifyPassword(PasswordHash, password);
    }
}