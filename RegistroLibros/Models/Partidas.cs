using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroLibros.Models
{
    public class Partidas
    {
        public int PartidaId { get; set;  }

        public DateTime Fecha { get; set; }

        public int Puntuacion { get; set; }

        [ForeignKey("JugadorId")]
        [InverseProperty("Partidas")]
        public virtual Jugadores jugador { get; set; } = null!;

    }
}
