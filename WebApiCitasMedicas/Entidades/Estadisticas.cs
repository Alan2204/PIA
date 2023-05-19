namespace WebApiCitasMedicas.Entidades
{
    public class Estadisticas
    {
        public int Id { get; set; }
        public int TotalConsultas { get; set; }
        public int TotalPacientes { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public int MedicosId { get; set; }
        public Medicos medicos { get; set; }
    }
}
