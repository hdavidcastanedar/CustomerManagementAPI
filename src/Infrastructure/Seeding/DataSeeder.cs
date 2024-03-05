using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using System;
using Infrastructure.Persitence;

namespace Infrastructure.Seeding
{
    public static class DataSeeder
    {
        public static void SeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                //if (!context.Customers.Any())
                //{
                //    var customer1 = new Customer(Guid.NewGuid(), new Name("John Doe"), new Dni("12345678A"), null, null);
                //    customer1.Address = new Address("Main St", "State", "City");
                //    customer1.ContactInfo = new ContactInfo("123456789", "987654321", "john.doe@example.com");
                //    context.Customers.Add(customer1);

                    

                //    context.SaveChanges();
                //}
            }
        }

    }
}