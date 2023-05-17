namespace WebApiCitasMedicas.Entidades
{
    public class Estadisticas
    {
        public int Id { get; set; }
        public int TotalConsultas { get; set; }
        public int TotalPacientes { get; set; }
        public DateOnly FechaActualizacion { get; set; }
        public int IdMedico { get; set; }
    }
}
