
using Microsoft.EntityFrameworkCore;
using RegistroLibros.Data;
using System.Text.Json.Serialization;

namespace GestionLibrosApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddControllers()
                .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            var ConStr = builder.Configuration.GetConnectionString("ConStr");
            builder.Services.AddDbContext<RegistroLibrosDbContext>(o => o.UseSqlServer(ConStr));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
