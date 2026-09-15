using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RegistroLibros.Models;

namespace RegistroLibros.Models
{
    public class Jugadores
    {
        [Key]
        public int JugadorId { get; set; }

        public string Usuario { get; set; } = null!;

        public int Clasificacion { get; set; }


    }
}
