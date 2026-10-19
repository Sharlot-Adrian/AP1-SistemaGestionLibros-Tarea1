using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroLibros.Models
{
    public class Entradas
    {
        [Key]
        public int EntradaId { get; set; }
        public DateTime Fecha { get; set; }
        [Required(ErrorMessage ="Es obligatorio agregar un concepto.")]
        public string Concepto { get; set; } = null!;
        public int Total { get; set; }

        [InverseProperty("Entrada")]
        public ICollection<EntradasDetalle> EntradasDetalle { get; set; } = [];
    }
}
