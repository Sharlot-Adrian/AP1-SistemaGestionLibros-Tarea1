namespace GestionLibrosApi.DTOs;

public class EstudianteDto
{
    public int EstudianteId { get; set; }

    public string Nombres { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }
}
