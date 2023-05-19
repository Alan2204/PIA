namespace WebApiCitasMedicas.Entidades
{
    public class ReporteConsulta
    {
        public int Id { get; set; }
        public string Motivo { get; set; }
        public string Tratamiento { get; set; }
        public string Receta { get; set; }
        public int CitasId { get; set; }
        public Citas citas { get; set; }
    }
}
