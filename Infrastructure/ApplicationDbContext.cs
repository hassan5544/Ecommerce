using Domain.Entities;
using Helpers.Implementations;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }


    public DbSet<Orders> Orders { get; set; }
    public DbSet<OrderItems> OrderItems { get; set; }
    public DbSet<Products> Products { get; set; }
    public DbSet<Users> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings => 
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }
      
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        var passwordHasher = new PasswordHasher();

        // Seed an admin user
        var adminUser = Domain.Entities.Users.CreateCustomer(
            fullName: "Admin User",
            email: "admin@gmail.com",
            password: "12345",
            passwordHasher: passwordHasher
        );

        // Change the role to Admin
        adminUser.GetType().GetProperty(nameof(adminUser.Role))?.SetValue(adminUser, "Admin");

        // Ensure the ID is consistent across migrations
        adminUser.GetType().GetProperty(nameof(adminUser.Id))?.SetValue(adminUser, Guid.NewGuid());

        modelBuilder.Entity<Users>().HasData(adminUser);
        modelBuilder.Entity<Users>()
            .ToTable("Users")
            .HasMany(u => u.Orders)
            .WithOne(o => o.Customers)
            .HasForeignKey(o => o.CustomerId);
        
        modelBuilder.Entity<OrderItems>()
            .HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict); 
            

        modelBuilder.Entity<OrderItems>()
            .HasOne<Orders>()
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<Orders>(entity =>
        {
            entity.Property(o => o.Status)
                .HasConversion<int>();
            
            entity.HasOne(o => o.Customers)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();   
            
        });
    }
}