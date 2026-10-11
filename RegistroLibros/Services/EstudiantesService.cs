using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using RegistroLibros.Context;
using RegistroLibros.Models;
using System.Linq.Expressions;

namespace RegistroLibros.Services;

public class EstudiantesService(IDbContextFactory<Contexto> contextFactory): Aplicada1.Core.IServiceResult<Estudiantes, int>
{
    public async Task<Result> Guardar(Estudiantes estudiante)
    {
        if (await ExisteNombre(estudiante.Nombres, estudiante.EstudianteId))
        {
            return Result.Success(false);
        }

        if (!await Existe(estudiante.EstudianteId))
        {
            var Existio = await Insertar(estudiante);
            return Result.Success(Existio);
        }
        else
        {
            var Modifico = await Modificar(estudiante);
            return Result.Success(Modifico);
        }

    }
    private async Task<bool> Existe(int EstudianteId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Estudiantes.AnyAsync(e => e.EstudianteId == EstudianteId);
    }

    private async Task<bool> Insertar(Estudiantes estudiante)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Estudiantes.Add(estudiante);
        return await contexto.SaveChangesAsync() > 0;   
    }

    public async Task<Result> Eliminar(int estudianteId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        var Elimino = await contexto.Estudiantes
            .Where(i => i.EstudianteId == estudianteId)
            .ExecuteDeleteAsync() > 0;
        return Result.Success(Elimino);
    }

    public async Task<Result<List<Estudiantes>>> GetList(Expression<Func<Estudiantes, bool>> criterio)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Estudiantes
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    private async Task<bool> Modificar(Estudiantes estudiante)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Estudiantes.Update(estudiante);
        return await contexto.SaveChangesAsync() > 0;

    }

    private async Task<bool> ExisteNombre(string nombre, int estudianteId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Estudiantes.AnyAsync(e => e.Nombres.ToLower() == nombre.ToLower() && e.EstudianteId != estudianteId);
    }

    public async Task<Result<Estudiantes?>> Buscar(int estudianteId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Estudiantes.FirstOrDefaultAsync(e => e.EstudianteId == estudianteId);
    }

}
