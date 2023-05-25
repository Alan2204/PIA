namespace WebApiCitasMedicas.DTOs
{
    public class GetEstadisticasDTO
    {
        public int TotalConsultas { get; set; }
        public int TotalPacientes { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public int MedicosId { get; set; }
    }
}
