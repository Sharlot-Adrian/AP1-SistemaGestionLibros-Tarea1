using BlazorBootstrap;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RegistroLibros.Context;
using RegistroLibros.Models;
using System.Linq.Expressions;

namespace RegistroLibros.Services
{
    public class PrestamosService(IDbContextFactory<Contexto> contextFactory) : Aplicada1.Core.IService<Prestamos, int>
    {
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

        private async Task<bool> Modificar (Prestamos prestamo)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync ();
            contexto.Update(prestamo);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar (Prestamos prestamo)
        {
           
            if(!await Existe(prestamo.PrestamoId))
            {
                return await Insertar(prestamo);
            }
            else
            {
                return await Modificar(prestamo);
            }
        }

        public async Task<Prestamos?> Buscar(int prestamoId)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            return await contexto.Prestamos.Include(e => e.Estudiante).FirstOrDefaultAsync(p=> p.PrestamoId == prestamoId);
        }

        public async Task<bool> Eliminar(int prestamoId)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            return await contexto.Prestamos.Where(p => p.PrestamoId == prestamoId).ExecuteDeleteAsync() > 0;

        }

        public async Task<List<Prestamos>> GetList(Expression<Func<Prestamos, bool>> criterio)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            return await contexto.Prestamos.Include(e => e.Estudiante).Include(l => l.Libro).Where(criterio).AsNoTracking().ToListAsync();
        }

       public async Task<Prestamos?> BuscarPrestamo(int id)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            return await contexto.Prestamos.Include(p => p.Estudiante).FirstOrDefaultAsync(p => p.EstudianteId == id);
        }

    }
}
