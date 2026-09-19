using Microsoft.EntityFrameworkCore;
using RegistroLibros.Context;
using RegistroLibros.Models;

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

    }
}
