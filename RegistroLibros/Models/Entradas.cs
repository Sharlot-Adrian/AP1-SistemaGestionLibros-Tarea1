using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroLibros.Models
{
    public class Entradas
    {
        [Key]
        public int EntradasId { get; set; }
        public DateTime Fecha { get; set; }
        public string Concepto { get; set; } = null!;
        public double Total { get; set; }

        [ForeignKey("EntradasId")]
        public ICollection<EntradasDetalle> EntradasDetalle { get; set; } = [];
    }
}
