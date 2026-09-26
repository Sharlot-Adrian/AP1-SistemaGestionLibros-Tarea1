using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroLibros.Models
{
    public class Prestamos
    {
        [Key]
        public int PrestamoId {  get; set; }

        [Range(1,int.MaxValue, ErrorMessage = "Debe seleccionar un estudiante valido.")]
        public Estudiantes estudiante { get; set; }

        [Range(1,int.MaxValue, ErrorMessage = "Debe seleccionar un libro valido.")]
        public Libros libro { get; set;  }

        [ForeignKey(nameof(EstudianteId))]
        [InverseProperty("Prestamos")]
        public virtual Estudiantes estudiante { get; set; }

        [ForeignKey(nameof(LibroId))]
        [InverseProperty("Prestamos")]
        public virtual Libros libro { get; set; }
    }
}
