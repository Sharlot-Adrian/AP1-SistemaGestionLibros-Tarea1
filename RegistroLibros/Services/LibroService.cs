using Aplicada1.Core;
using BlazorBootstrap;
using Microsoft.EntityFrameworkCore;
using RegistroLibros.Context;
using RegistroLibros.Models;
using System.Linq.Expressions;

namespace RegistroLibros.Services;

public class LibroService(IDbContextFactory<Contexto> contextFactory) : Aplicada1.Core.IServiceResult<Libros,int>
{
    public async Task<Result> Guardar(Libros libro)
    {
        if (await ExisteTitulo(libro.Titulo, libro.LibroId))
        {
            return Result.Success(false);
        }

        if (!await Existe(libro.LibroId))
        {
            var Inserto = await Insertar(libro);
            return Result.Success(Inserto);
        }
        else
        {
            var Modifico = await Modificar(libro);
            return Result.Success(Modifico);
        }

    }

    private async Task<bool> Existe(int libroId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Libros.AnyAsync(p => p.LibroId == libroId);
    }

    public async Task<Result> Eliminar(int libroId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        var elimino = await contexto.Libros
            .Where(l => l.LibroId == libroId)
            .ExecuteDeleteAsync() > 0;
        return Result.Success(elimino);
    }

    public async Task<Result<List<Libros>>> GetList(Expression<Func<Libros, bool>> criterio)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Libros
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    private async Task<bool> Insertar(Libros libro)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Libros.Add(libro);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Libros libro)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Libros.Update(libro);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Result<Libros?>> Buscar(int libroId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Libros.FirstOrDefaultAsync(l => l.LibroId == libroId);
    }

    private async Task<bool> ExisteTitulo(string titulo, int libroId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Libros.AnyAsync(p => p.Titulo.ToLower() == titulo.ToLower() && p.LibroId != libroId);
    }
}
