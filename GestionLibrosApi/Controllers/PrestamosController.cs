using BlazorBootstrap;
using GestionLibrosApi.Service;
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
        return Ok(result.Value);
    }

    // GET api/<PrestamosController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await prestamosService.Buscar(id);
        return Ok(result.Value);
    }

    // POST api/<PrestamosController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Prestamo prestamo)
    {
        var result = await prestamosService.Guardar(prestamo);
        return Ok(result);
    }

    // PUT api/<PrestamosController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Prestamo prestamo)
    {
        prestamo.PrestamoId = id;
        var result = await prestamosService.Guardar(prestamo);
        return Ok(result);
    }

    // DELETE api/<PrestamosController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await prestamosService.Eliminar(id);
        return Ok(result);
    }
}
