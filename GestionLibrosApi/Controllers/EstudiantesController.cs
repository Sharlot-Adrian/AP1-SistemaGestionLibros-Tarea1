using Aplicada1.Core;
using GestionLibrosApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RegistroLibros.Data;
using Mapster;
using GestionLibrosApi.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionLibrosApi.Controllers;

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
        var result = await estudiantesService.GetList(d => d.EstudianteId > 0);
        var resultDto = result.Value.Adapt<List<EstudianteDto>>();
        return Ok(resultDto);
    }

    // GET api/<EstudiantesController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await estudiantesService.Buscar(id);
        var resultDto = result.Value.Adapt<EstudianteDto>();
        return Ok(resultDto);
    }

    // POST api/<EstudiantesController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] EstudianteDto estudianteDto)
    {
        var estudiante = estudianteDto.Adapt<Estudiante>();
        estudiante.EstudianteId = 0;
        var result= await estudiantesService.Guardar(estudiante);

        if (!result.IsSuccess)
            return BadRequest("No se pudo guardar el estudiante.");

        var resultDto = estudiante.Adapt<EstudianteDto>();
        return Ok(resultDto);
    }

    // PUT api/<EstudiantesController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] EstudianteDto estudianteDto)
    {
        var estudiante = estudianteDto.Adapt<Estudiante>();
        estudiante.EstudianteId = id;
        var result = await estudiantesService.Guardar(estudiante);

        if (!result.IsSuccess)
            return BadRequest("No se pudo guardar el estudiante.");

        var resultDto = estudiante.Adapt<EstudianteDto>();
        return Ok(resultDto);
    }

    // DELETE api/<EstudiantesController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await estudiantesService.Eliminar(id);
        return Ok(result);
    }
}
