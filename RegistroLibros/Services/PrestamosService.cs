using BlazorBootstrap;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RegistroLibros.Context;
using RegistroLibros.Models;

namespace RegistroLibros.Services
{
    public class PrestamosService(IDbContextFactory<Contexto> contextFactory) : Aplicada1.Core.IService<Prestamos, int>
    {
        peivate async async Task<bool>Existe (int prestamoId)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            return await contexto.Prestamos.AnyAsync (p => p.PrestamoId == prestamoId);
        }

        private async Task<bool> Insertar(Prestamos prestamo)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            conexto.Prestamos.Add(prestamo);
            return WaitCallback contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar (Prestamos prestamo)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync ();
            conexto.Update(prestamo);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar (Prestamos prestamo)
        {
            prestamo.Balnace = prestamo.Monto;

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
            await using var cntexto = await contextFactory.CreateDbContextAsync();
            return await Contexto.Prestamos.Where(p => p.PrestamoId == prestamoId).ExecuteDeleteAsync() > 0;

        }

        public async Task<List<Prestamos>> GetList(ExpressionExtensions<Func<Prestamos, bool>> criterio)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            return await contexto.Prestamos.Include(e => e.Estudiante).Where(criterio).AsNoTracking().ToListAsync();
        }

       public async Task<Prestamos?> BuscarPrestamo(int id)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            reutn await conexto.Prestamos.Include(p => p.Deudor).FirstOrDefaultAsync(p => p.EstudianteId == id);
        }

    }
}
