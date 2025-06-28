
using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Middleware;
using Dsw2025Tpi.Application.Services;
using Dsw2025Tpi.Data;
using Dsw2025Tpi.Data.Extensions;
using Dsw2025Tpi.Data.Repositories;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Dsw2025Tpi.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddDbContext<Dsw2025TpiContext>(options =>
           options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Dsw2025Tpi;Integrated Security=True;",
           x=>x.MigrationsAssembly("Dsw2025Tpi.Api")
           )
          
           );
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<IRepository, EfRepository>();
        builder.Services.AddTransient<ProductServices>();
        builder.Services.AddTransient<OrderServices>();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHealthChecks();

        var app = builder.Build();
       using (var scope = app.Services.CreateScope())
        {
            try {

                var context = scope.ServiceProvider.GetRequiredService<Dsw2025TpiContext>();

                context.Database.EnsureCreatedAsync();

                // Solo esta línea para Customer
                context.InicializateJson<Customer, CustomerModel.request>("D:\\Facultad\\3er nivel\\DSW\\TPI\\Dsw2025Tpi\\Dsw2025Tpi.Application\\Dtos\\Customers.json",
                    dto => new Customer(dto.Name, dto.Email, dto.PhoneNumber));
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
           
        }
       

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseAuthorization();

        app.MapControllers();
        
        app.MapHealthChecks("/healthcheck");

        app.Run();
    }
}
