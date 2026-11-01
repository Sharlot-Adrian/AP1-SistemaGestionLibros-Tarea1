using Aplicada1.Core;
using GestionLibrosApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RegistroLibros.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionLibrosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    { 
        private readonly LibrosService librosService;

        public LibrosController(LibrosService librosService)
        {
            this.librosService = librosService;
        }
        // GET: api/<LibrosController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var libroResult = await librosService.GetList(l => l.LibroId > 0);
            return Ok(libroResult.Value);
        }

        // GET api/<LibrosController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await librosService.Buscar(id);
            return Ok(result.Value);
        }

        // POST api/<LibrosController>
        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] Libro libro)
        {
            var result = await librosService.Guardar(libro);
            return Ok(result);
        }

        // PUT api/<LibrosController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Libro libro)
        {
            libro.LibroId = id;
            var result = await librosService.Guardar(libro);
            return Ok(result);
        }

        // DELETE api/<LibrosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await librosService.Eliminar(id);
            return Ok(result);
        }
    }
}
