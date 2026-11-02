using BlazorBootstrap;
using GestionLibrosApi.DTOs;
using GestionLibrosApi.Service;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using RegistroLibros.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionLibrosApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrestamosController : ControllerBase
{
    private readonly PrestamosService prestamosService;

    public PrestamosController(PrestamosService prestamosService)
    {
        this.prestamosService = prestamosService;
    }

    // GET: api/<PrestamosController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await prestamosService.GetList(p => p.PrestamoId > 0);
        var resultDto = result.Value.Adapt<List<PrestamoDto>>();
        return Ok(resultDto);
    }

    // GET api/<PrestamosController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await prestamosService.Buscar(id);
        var resultDto = result.Value.Adapt<PrestamoDto>();
        return Ok(resultDto);
    }

    // POST api/<PrestamosController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PrestamoDto prestamoDto)
    {
        var prestamo = prestamoDto.Adapt<Prestamo>();
        prestamo.PrestamoId = 0;
        var result = await prestamosService.Guardar(prestamo);

        if (!result.IsSuccess)
        {
            return BadRequest("No se ha podido agregar el prestamo.");
        }
        var resultDto = prestamo.Adapt<PrestamoDto>();
        return Ok(resultDto);
    }

    // PUT api/<PrestamosController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] PrestamoDto prestamoDto)
    {
        var prestamo = prestamoDto.Adapt<Prestamo>();
        prestamo.PrestamoId = id;
        var result = await prestamosService.Guardar(prestamo);

        if (!result.IsSuccess)
        {
            return BadRequest("No se ha podido agregar el prestamo.");
        }
        var resultDto = prestamo.Adapt<PrestamoDto>();
        return Ok(resultDto);
    }

    // DELETE api/<PrestamosController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await prestamosService.Eliminar(id);
        return Ok(result);
    }
}
