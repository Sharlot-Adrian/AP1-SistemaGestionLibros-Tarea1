using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroLibros.Models;

public class Prestamos
{
    [Key]
    public int PrestamoId {  get; set; }
    [Range(1,int.MaxValue, ErrorMessage = "Debe seleccionar un estudiante valido.")]
    public int EstudianteId { get; set; }

    [Range(1,int.MaxValue, ErrorMessage = "Debe seleccionar un libro valido.")]
    public int LibroId { get; set;  }

    public DateTime FechaPrestamo { get; set; } = DateTime.Now;

    public DateTime? FechaDevolucion { get; set; } = null;

    public int CantidadLibros { get; set; }

    [ForeignKey(nameof(EstudianteId))]
    public virtual Estudiantes Estudiante { get; set; }

    [ForeignKey(nameof(LibroId))]
    public virtual Libros Libro { get;set; }
}
