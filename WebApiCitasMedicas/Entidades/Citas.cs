namespace WebApiCitasMedicas.Entidades
{
    public class Citas
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int MedicosId { get; set; }
        public int PacienteId { get; set; }       
        public Medicos medicos { get; set; }
        public Paciente paciente { get; set; }
        public List<ReporteConsulta> reporteconsulta { get; set; }
    }
}
