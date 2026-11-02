namespace GestionLibrosApi.DTOs;

public class PrestamoDto
{
    public int PrestamoId { get; set; }

    public int EstudianteId { get; set; }

    public int LibroId { get; set; }

    public int CantidadLibros { get; set; }

    public DateTime FechaPrestamo { get; set; }

    public DateTime? FechaDevolucion { get; set; }

}
