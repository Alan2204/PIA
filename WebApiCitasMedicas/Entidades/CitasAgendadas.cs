using Microsoft.AspNetCore.Identity;

namespace WebApiCitasMedicas.Entidades
{
    public class CitasAgendadas
    {
        public int Id { get; set; }
        public int CitasId { get; set; }
        public int PacienteId { get; set; }
        public Citas citas { get; set; }
        public Paciente paciente { get; set; }
        public List<ReporteConsulta> reporte { get; set; }
    }
}
