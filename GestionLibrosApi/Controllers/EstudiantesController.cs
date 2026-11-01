using GestionLibrosApi.Service;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<List<Estudiante>> Get()
        {
            var estudiantesResult = await estudiantesService.GetList(d => d.EstudianteId > 0);
            return estudiantesResult.Value;
        }

        // GET api/<EstudiantesController>/5
        [HttpGet("{id}")]
        public async Task<Estudiante?> Get(int id)
        {
            var estudianteResult = await estudiantesService.Buscar(id);
            return estudianteResult.Value;
        }

        // POST api/<EstudiantesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EstudiantesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EstudiantesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
