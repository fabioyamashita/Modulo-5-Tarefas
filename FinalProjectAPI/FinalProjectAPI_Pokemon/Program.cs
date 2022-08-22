using FinalProjectAPI_Pokemon.Data;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAPI_Pokemon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            
            // Add SQL Server service
            builder.Services.AddDbContext<PokemonContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("PokemonContext")
                ?? throw new InvalidOperationException("Connection string 'PokemonContext' not found.")));

            // Creating seed
            

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            // Seeding inital data from dataComplete.json
            AppDbInitializer.Seed(app);

            app.Run();
        }

    }
}