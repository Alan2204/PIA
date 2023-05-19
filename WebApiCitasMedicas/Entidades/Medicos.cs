namespace WebApiCitasMedicas.Entidades
{
    public class Medicos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Sexo { get; set; }
        public string Especialidad { get; set; }
        public int Telefono { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public List<Citas> citas { get; set; }
        public List<Estadisticas> estadisticas { get; set; }
        public List<InformacionMedica> informacionmedica { get; set; }


    }
}
