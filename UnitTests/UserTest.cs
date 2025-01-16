using Domain.Entities;
using Domain.Repositories;
using Helpers.Interfaces;
using Moq;

namespace UnitTests;

public class UserTest
{
    [Fact]
    public void VerifyPassword_ValidPassword_ShouldReturnTrue()
    {
        // Arrange
        var passwordHasher = new Mock<IPasswordHasher>();
        passwordHasher.Setup(p => p.HashPassword(It.IsAny<string>())).Returns("hashedPassword");
        passwordHasher.Setup(p => p.VerifyPassword("hashedPassword", "password")).Returns(true);
        
        var user = Users.CreateCustomer("John Doe", "john.doe@example.com", "password", passwordHasher.Object);

        // Act
        var result = user.VerifyPassword("password", passwordHasher.Object);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void VerifyPassword_InvalidPassword_ShouldReturnFalse()
    {
        // Arrange
        var passwordHasher = new Mock<IPasswordHasher>();
        passwordHasher.Setup(p => p.HashPassword(It.IsAny<string>())).Returns("hashedPassword");
        passwordHasher.Setup(p => p.VerifyPassword("hashedPassword", "wrongPassword")).Returns(false);
        
        var user = Users.CreateCustomer("John Doe", "john.doe@example.com", "password", passwordHasher.Object);

        // Act
        var result = user.VerifyPassword("wrongPassword", passwordHasher.Object);

        // Assert
        Assert.False(result);
    }
}