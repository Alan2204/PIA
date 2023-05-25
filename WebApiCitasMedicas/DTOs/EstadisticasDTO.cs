using System.ComponentModel.DataAnnotations;

namespace WebApiCitasMedicas.DTOs
{
    public class EstadisticasDTO
    {
        [Required(ErrorMessage = "Es necesario el campo {0}. ")]
        public int TotalConsultas { get; set; }
        [Required(ErrorMessage = "Es necesario el campo {0}. ")]
        [Range(0, 100, ErrorMessage = "No puedes tener mas de 100 pacientes.")]
        public int TotalPacientes { get; set; }
        [Required(ErrorMessage = "Es necesario el campo {0}. ")]
        public DateTime FechaActualizacion { get; set; }
    }
}
