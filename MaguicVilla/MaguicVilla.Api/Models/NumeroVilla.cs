using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaguicVilla.Api.Models
{
    public class NumeroVilla
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VillaNo { get; set; }

        public int VillaId { get; set; }

        [ForeignKey("VillaId")]
        public Villa Villa { get; set; }

        public string DetalleEspecial { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaActualizacion { get; set; }
    }
}
