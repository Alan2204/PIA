using System.ComponentModel.DataAnnotations;

namespace WebApiCitasMedicas.DTOs
{
    public class ReporteConsultaDTO
    {
        [Required(ErrorMessage = "Es necesario el campo {0}. ")]
        public string Motivo { get; set; }
        [Required(ErrorMessage = "Es necesario el campo {0}. ")]
        public string Tratamiento { get; set; }
        [Required(ErrorMessage = "Es necesario el campo {0}. ")]
        public string Receta { get; set; }
        public int CitasAgendadasId { get; set; }
    }
}
