namespace WebApiCitasMedicas.DTOs
{
    public class GetInformacionMedicaDTO
    {
        public int Id { get; set; }
        public float Peso { get; set; }
        public float Altura { get; set; }
        public string Alergias { get; set; }
        public string Enfermedades { get; set; }
        public DateTime FechaAtualizacion { get; set; }
        public int MedicoId { get; set; }
    }
}
