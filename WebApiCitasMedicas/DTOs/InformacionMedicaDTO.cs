using System.ComponentModel.DataAnnotations;

namespace WebApiCitasMedicas.DTOs
{
    public class InformacionMedicaDTO
    {
        [Required(ErrorMessage = "Es necesario registrar el peso.")]
        [Range(20, 200, ErrorMessage = "Ingrese un peso valido")]
        public float Peso { get; set; }
        [Range(0.50, 2.50, ErrorMessage = "Ingrese una altura valida")]
        public float Altura { get; set; }
        public string Alergias { get; set; }
        public string Enfermedades { get; set; }
        public DateTime FechaAtualizacion { get; set; }
        public int MedicoId { get; set; }
    }
}
