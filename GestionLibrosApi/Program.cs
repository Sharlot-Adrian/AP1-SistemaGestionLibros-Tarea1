using Microsoft.EntityFrameworkCore;
using RegistroLibros.Data;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;
using GestionLibrosApi.Service;
namespace GestionLibrosApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddControllers()
                .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            var ConStr = builder.Configuration.GetConnectionString("ConStr");
            builder.Services.AddDbContextFactory<RegistroLibrosDbContext>(o => o.UseSqlServer(ConStr));
            builder.Services.AddScoped<EstudiantesService>();
            builder.Services.AddScoped<LibrosService>();
            builder.Services.AddScoped<PrestamosService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
