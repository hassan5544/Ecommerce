using Domain.Entities;
using Infrastructure.Interceptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        => optionsBuilder
            .AddInterceptors(new SoftDeleteInterceptor());
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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
            ;

        modelBuilder.Entity<OrderItems>()
            .HasOne<Orders>()
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Restrict); // Optional: specify delete behavior
        ;

 

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