using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persitence;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>().HasKey(c => c.Id);
        modelBuilder.Entity<Customer>().OwnsOne(c => c.Name);
        modelBuilder.Entity<Customer>().OwnsOne(c => c.Dni);
        modelBuilder.Entity<Customer>().OwnsOne(c => c.Address);
        modelBuilder.Entity<Customer>().OwnsOne(c => c.ContactInfo);

        
        modelBuilder.Entity<Customer>().HasData(new
        {
            Id = new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),

        });

  
        modelBuilder.Entity<Customer>().OwnsOne(c => c.Name).HasData(new
        {
            CustomerId = new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
            Value = "John Doe" 
        });

        modelBuilder.Entity<Customer>().OwnsOne(c => c.Dni).HasData(new
        {
            CustomerId = new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
            Value = "12345678A"
        });

        modelBuilder.Entity<Customer>().OwnsOne(c => c.Address).HasData(new
        {
            CustomerId = new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
            Street = "Main St",
            State = "State",
            City = "City"
        });

        modelBuilder.Entity<Customer>().OwnsOne(c => c.ContactInfo).HasData(new
        {
            CustomerId = new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
            Phone = "123456789",
            Mobile = "987654321",
            Email = "john.doe@example.com"
        });
    }
}
