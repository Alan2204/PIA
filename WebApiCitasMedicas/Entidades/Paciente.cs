using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;

namespace WebApiCitasMedicas.Entidades
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Sexo { get; set; }
        public DateTime FechaNacimeinto { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public List<Citas> citas { get; set; }
        public List<InformacionMedica> informacionMedica { get; set; }


    }
}
