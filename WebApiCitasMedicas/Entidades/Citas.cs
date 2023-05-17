namespace WebApiCitasMedicas.Entidades
{
    public class Citas
    {
        public int Id { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public int IdMedico { get; set; }
        public int IdPaciente { get; set; }
    }
}
