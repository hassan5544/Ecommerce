using Domain.Entities;
using Infrastructure.Interceptors;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Orders> Orders { get; set; }
    public DbSet<Payments> Payments { get; set; }
    public DbSet<OrderItems> OrderItems { get; set; }
    public DbSet<Products> Products { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .AddInterceptors(new SoftDeleteInterceptor());
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
        
        modelBuilder.Entity<OrderItems>()
            .HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict); 
            ;

        modelBuilder.Entity<OrderItems>()
            .HasOne<Orders>()
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Restrict); // Optional: specify delete behavior
        ;

        modelBuilder.Entity<Payments>(entity =>
        {
            entity.HasOne(p => p.Order)
                .WithMany()
                .HasForeignKey(p => p.OrderId);
            
            entity.Property(o => o.Status)
                .HasConversion<int>();
        });
            

        modelBuilder.Entity<Orders>(entity =>
        {
            entity.Property(o => o.Status)
                .HasConversion<int>();
            
            entity.HasOne<ApplicationUser>()
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.CustomerId);    
            
        });
        
        
            
    }

}