using Microsoft.EntityFrameworkCore;
using RegistroLibros.Context;
using RegistroLibros.Models;

namespace RegistroLibros.Services
{
    public class LibroService(IDbContextFactory<Contexto> contextFactory) : Aplicada1.Core.IService<Libro,int>
    {
        private async Task<bool> Existe(int libroId)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            return await contexto.Libros.AnyAsync(p => p.LibroId == libroId);
        }

        private async Task<bool> Insertar(Libro libro)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            contexto.Libros.Add(libro);
            return await contexto.SaveChangesAsync() > 0;

        }

    }
}
