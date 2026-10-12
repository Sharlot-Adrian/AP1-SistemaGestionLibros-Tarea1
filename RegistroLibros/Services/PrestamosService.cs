using Aplicada1.Core;
using BlazorBootstrap;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RegistroLibros.Context;
using RegistroLibros.Models;
using System.Linq.Expressions;

namespace RegistroLibros.Services;

public class PrestamosService(IDbContextFactory<Contexto> contextFactory
    ) : Aplicada1.Core.IServiceResult<Prestamos, int>
{
    public async Task<Result> Guardar(Prestamos prestamo)
    {

        if (!await Existe(prestamo.PrestamoId))
        {
            var Inserto = await Insertar(prestamo);
            return Result.Success(Inserto);
        }
        else
        {
            var Modifico = await Modificar(prestamo);
            return Result.Success(Modifico);
        }
    }

    private async Task<bool>Existe (int prestamoId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Prestamos.AnyAsync (p => p.PrestamoId == prestamoId);
    }

    private async Task<bool> Insertar(Prestamos prestamo)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        var libro = await contexto.Libros.FirstOrDefaultAsync(l => l.LibroId == prestamo.LibroId);
        if (libro != null)
        {
            libro.Disponible = false;
            contexto.Libros.Update(libro);
        }
        contexto.Prestamos.Add(prestamo);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<Result> Eliminar(int prestamoId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        var prestamo = await contexto.Prestamos.FirstOrDefaultAsync(p => p.PrestamoId == prestamoId);

        if (prestamo == null)
            return Result.Success(false);

        var libro = await contexto.Libros.FirstOrDefaultAsync(l => l.LibroId == prestamo.LibroId);

        if (libro != null)
        {
            libro.Disponible = true;
            contexto.Libros.Update(libro);
            await contexto.SaveChangesAsync();
        }

        var Eliminado = await contexto.Prestamos.Where(p => p.PrestamoId == prestamoId).ExecuteDeleteAsync() > 0;
        return Result.Success(Eliminado);
    }

    public async Task<Result<List<Prestamos>>> GetList(Expression<Func<Prestamos, bool>> criterio)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Prestamos
            .Include(e => e.Estudiante)
            .Include(l => l.Libro)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }


    private async Task<bool> Modificar (Prestamos prestamo)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync ();

        var PrestamoAnterior = await contexto.Prestamos.AsNoTracking().FirstOrDefaultAsync(p => p.PrestamoId ==  prestamo.PrestamoId);

        if (PrestamoAnterior == null)
            return false;
        
        if(PrestamoAnterior.LibroId != prestamo.LibroId)
        {
            var LibroAnterior = await contexto.Libros.FirstOrDefaultAsync(l => l.LibroId == PrestamoAnterior.LibroId);

            if(LibroAnterior != null)
            {
                LibroAnterior.Disponible = true;
                contexto.Libros.Update(LibroAnterior);
            }

            var LibroActual = await contexto.Libros.FirstOrDefaultAsync(l => l.LibroId == prestamo.LibroId);
            if(LibroActual != null)
            {
                LibroActual.Disponible = false;
                contexto.Libros.Update(LibroActual);
            }

            prestamo.Libro = null;
            prestamo.Estudiante = null;
        }
        contexto.Update(prestamo);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Result<Prestamos?>> Buscar(int prestamoId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Prestamos
            .Include(e => e.Estudiante)
            .Include(l=> l.Libro)
            .FirstOrDefaultAsync(p=> p.PrestamoId == prestamoId);
    }

    
   public async Task<Prestamos?> BuscarPrestamo(int id)
   {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Prestamos.Include(p => p.Estudiante).FirstOrDefaultAsync(p => p.EstudianteId == id);
   }

   public async Task<Double> CalcularPromedio()
   {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        
        var prestamos = await contexto.Prestamos
            .Where(f => f.FechaDevolucion != null)
            .ToListAsync();

        if (!prestamos.Any())
            return 0;

        return prestamos.Average(f => (f.FechaDevolucion!.Value.Date - f.FechaPrestamo.Date).Days);
   }

}
