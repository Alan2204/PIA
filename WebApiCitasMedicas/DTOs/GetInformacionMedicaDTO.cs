namespace WebApiCitasMedicas.DTOs
{
    public class GetInformacionMedicaDTO
    {
        public float Peso { get; set; }
        public float Altura { get; set; }
        public string Alergias { get; set; }
        public string Enfermedades { get; set; }
        public DateTime FechaAtualizacion { get; set; }
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
    }
}
