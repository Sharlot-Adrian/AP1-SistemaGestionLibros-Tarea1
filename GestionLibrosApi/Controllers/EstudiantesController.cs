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
    public class EstudiantesController : ControllerBase
    {
        private readonly EstudiantesService estudiantesService;
        
        public EstudiantesController(EstudiantesService estudiantesService)
        {
            this.estudiantesService = estudiantesService;
        }
        // GET: api/<EstudiantesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var estudiantesResult = await estudiantesService.GetList(d => d.EstudianteId > 0);
            return Ok(estudiantesResult.Value);
        }

        // GET api/<EstudiantesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var estudianteResult = await estudiantesService.Buscar(id);
            return Ok(estudianteResult.Value);
        }

        // POST api/<EstudiantesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Estudiante estudiante)
        {
            var resultado= await estudiantesService.Guardar(estudiante);
            return Ok(resultado);
        }

        // PUT api/<EstudiantesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Estudiante estudiante)
        {
            estudiante.EstudianteId = id;
            var resultado = await estudiantesService.Guardar(estudiante);
            return Ok(resultado);
        }

        // DELETE api/<EstudiantesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await estudiantesService.Eliminar(id);
            return Ok(eliminado);
        }
    }
}
