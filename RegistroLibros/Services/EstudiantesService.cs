using Microsoft.EntityFrameworkCore;
using RegistroLibros.Context;
using RegistroLibros.Models;
using System.Linq.Expressions;

namespace RegistroLibros.Services
{
    public class EstudiantesService(IDbContextFactory<Contexto> contextFactory): Aplicada1.Core.IService<Estudiantes, int>
    {
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

        public async Task<bool> Guardar(Estudiantes estudiante)
        {
            if (await ExisteNombre(estudiante.Nombres, estudiante.EstudianteId))
            {
                return false;
            }

            if(!await Existe(estudiante.EstudianteId))
            {
                return await Insertar(estudiante);
            }
            else
            {
                return await Modificar(estudiante);
            }
          
        }
        public async Task<Estudiantes?> Buscar(int estudianteId)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            return await contexto.Estudiantes.FirstOrDefaultAsync(e => e.EstudianteId == estudianteId);
        }

        public async Task<bool> Eliminar(int estudianteId)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            return await contexto.Estudiantes.Where(i => i.EstudianteId == estudianteId).ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Estudiantes>> GetList(Expression<Func<Estudiantes,bool>> criterio)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            return await contexto.Estudiantes.Where(criterio).AsNoTracking().ToListAsync();
        }

    }
}
