using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using RegistroLibros.Context;
using RegistroLibros.Models;
using System.Linq.Expressions;

namespace RegistroLibros.Services
{
    public class EntradasService(IDbContextFactory<Contexto> contextFactory) : Aplicada1.Core.IServiceResult<Entradas, int>
    {
        public async Task<Result> Guardar(Entradas entrada)
        {
            if (!await Existe(entrada.EntradaId))
            {
                var Existio = await Insertar(entrada);
                return Result.Success(Existio);
            }
            else
            {
                var Modifico = await Modificar(entrada);
                return Result.Success(Modifico);
            }
        }
        private async Task<bool> Existe(int entradaId)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            return await contexto.Entradas.AnyAsync(e => e.EntradaId == entradaId);
        }

        private async Task<bool> Insertar(Entradas entradas)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            contexto.Entradas.Add(entradas);
            await AfectarEntrada(entradas.EntradasDetalle.ToArray(), TipoOperacion.Suma);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task AfectarEntrada(EntradasDetalle[] detalle, TipoOperacion tipoOperacion )
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            foreach(var item in detalle)
            {
                var libro = await contexto.Libros.SingleAsync(l => l.LibroId == item.LibroId);
                if (tipoOperacion == TipoOperacion.Suma)
                    libro.Cantidad += item.Cantidad;
                else
                    libro.Cantidad -= item.Cantidad;
            }
        }

        private async Task<bool> Modificar(Entradas entrada)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            contexto.Update(entrada);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<Result<Entradas?>> Buscar(int EntradaId)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            return await contexto.Entradas
                .Include(e => e.EntradasDetalle)
                .FirstOrDefaultAsync(e => e.EntradaId == EntradaId);
        }

        public async Task<Result> Eliminar(int EntradaId)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            var Entrada = await contexto.Entradas
                .Include(e => e.EntradasDetalle)
                .FirstOrDefaultAsync(e => e.EntradaId == EntradaId);

            if (Entrada == null)
                return Result.Success(false);

            await AfectarEntrada(Entrada.EntradasDetalle.ToArray(), TipoOperacion.Resta);

            contexto.EntradasDetalle.RemoveRange(Entrada.EntradasDetalle);
            contexto.Entradas.Remove(Entrada);

            var result = await contexto.SaveChangesAsync() > 0;
            return Result.Success(result);
        }

        public Task <Result<List<Entradas>>> GetList(Expression<Func<Entradas,bool>> criterio)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Entradas>> Listar(Expression<Func<Entradas, bool>> criterio)
        {
            await using var contexto = await contextFactory.CreateDbContextAsync();
            return await contexto.Entradas
                .Include(e => e.EntradasDetalle)
                .Where(criterio)
                .AsNoTracking()
                .ToListAsync();
        }
        public enum TipoOperacion
        {
            Suma = 1, Resta = 2
        }
    }
}
