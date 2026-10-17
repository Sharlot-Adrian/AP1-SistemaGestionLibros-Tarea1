using System.ComponentModel.DataAnnotations;

namespace RegistroLibros.Models
{
    public class Entradas
    {
        [Key]
        public int EntradasId { get; set; }
        public DateTime Fecha { get; set; }
        public string Concepto { get; set; }
        public double Total { get; set; }

    }
}
