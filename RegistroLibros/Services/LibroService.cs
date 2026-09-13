using Microsoft.EntityFrameworkCore;
using RegistroLibros.Context;
using RegistroLibros.Models;
using System.Linq.Expressions;

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

        private async Task<bool> Modificar(Libro libro)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            contexto.Libros.Update(libro);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(Libro libro)
        {
            if(!await ExisteTitulo(libro.Titulo))
            {
                return await Insertar(libro);
            }
            else
            {
                return await Modificar(libro);
            }

        }

        public async Task<Libro?> Buscar(int libroId)
        {
            using var contexto = await contextFactory.CreateDbContextAsync();
            return await contexto.Libros.FirstOrDefaultAsync(l => l.LibroId == libroId);
        }

        public async Task<bool> Eliminar(int libroId)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            return await contexto.Libros.Where(l => l.LibroId == libroId).ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Libro>> GetList (Expression<Func<Libro, bool>> criterio)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            return await contexto.Libros.Where(criterio).AsNoTracking().ToListAsync();
        }

        private async Task<bool> ExisteTitulo(string titulo)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            return await contexto.Libros.AnyAsync(p => p.Titulo == titulo);
        }
    }
}
