using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiCitasMedicas.Validaciones;

namespace WebApiCitasMedicas.Entidades
{
    public class Paciente
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Es necesario el campo {0}. ")]
        [StringLength(maximumLength: 50, ErrorMessage = "El campo {0} solo puede tener hasta 50 caracteres")]
        [Mayusculas]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Es necesario el campo {0}. ")]
        [StringLength(maximumLength: 50, ErrorMessage = "El campo {0} solo puede tener hasta 50 caracteres")]
        [Mayusculas]
        public string Apellidos { get; set; }
        public string Sexo { get; set; }
        [Required(ErrorMessage = "Es necesario el campo {0}. ")]
        public DateTime FechaNacimeinto { get; set; }
        public string Direccion { get; set; }
        public string UsuarioId { get; set; }
        public IdentityUser Usuario { get; set; }
        public List<InformacionMedica> informacionMedica { get; set; }
        public List<CitasAgendadas> citasAgendadas { get; set; }


    }
}
