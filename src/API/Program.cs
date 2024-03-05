using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Persitence;
using Infrastructure.Persitence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add EF Core InMemory Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("ApplicationDb")
        .EnableSensitiveDataLogging());

// Register repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Register services
builder.Services.AddScoped<ICustomerService, CustomerService>();

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();


// Seed data
//Infrastructure.Seeding.DataSeeder.SeedData(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

