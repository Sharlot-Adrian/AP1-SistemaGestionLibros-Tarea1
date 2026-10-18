using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroLibros.Models;

public class Libros
{
    [Key]
    public int LibroId { get; set; }
    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string Titulo { get; set; } = null!;
    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string Autor { get; set; } = null!;
    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public int AnoPublicacion { get; set; }
    public bool Disponible { get; set; } = true;

    [InverseProperty("Libro")]
    public virtual ICollection<EntradasDetalle> EntradasDetalle { get; set; } = [];
}
