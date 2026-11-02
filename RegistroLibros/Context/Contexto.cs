using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RegistroLibros.Models;

namespace RegistroLibros.Context;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {

    }

    public DbSet<Libros> Libros { get; set; }
    public DbSet<Estudiantes> Estudiantes { get; set; }
    public DbSet<Prestamos> Prestamos { get; set; }
    public DbSet<Entradas> Entradas { get; set; }
    public DbSet<EntradasDetalle> EntradasDetalle { get; set; }
}
