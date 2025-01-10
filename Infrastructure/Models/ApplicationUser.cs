using System.Runtime.Serialization;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Models;

public class ApplicationUser : IdentityUser
{
    public required string FullName { get; set; }
    public string Role { get; set; }

    public List<Orders> Orders { get; set; } = new List<Orders>();

}