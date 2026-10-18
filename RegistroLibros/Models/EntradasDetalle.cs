using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroLibros.Models
{
    public class EntradasDetalle
    {
        [Key]
        public int EntradaDetalleId { get; set; }
        public int EntradaId { get; set; }
        public int LibroId { get; set; }
        public int Cantidad { get; set; }

        [ForeignKey("EntradaId")]
        [InverseProperty("EntradasDetalle")]
        public virtual Entradas Entrada { get; set; } = null!;

        [ForeignKey("LibroId")]
        [InverseProperty("EntradasDetalle")]
        public virtual Libros libro { get; set; } = null!;

    }
}
