namespace GestionLibrosApi.DTOs;

public class LibroDto
{
    public int LibroId { get; set; }

    public string Titulo { get; set; } = null!;

    public string Autor { get; set; } = null!;

    public int AnoPublicacion { get; set; }

    public bool Disponible { get; set; }

    public int Cantidad { get; set; }
}
