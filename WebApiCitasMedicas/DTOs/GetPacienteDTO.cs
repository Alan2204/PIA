using System.ComponentModel.DataAnnotations;

namespace WebApiCitasMedicas.DTOs
{
    public class GetPacienteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Sexo { get; set; }
        public DateTime FechaNacimeinto { get; set; }
        public string Direccion { get; set; }
    }
}
