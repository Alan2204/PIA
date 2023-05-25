using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiCitasMedicas.Entidades;
using WebApiCitasMedicas.Validaciones;

namespace WebApiCitasMedicas.DTOs
{
    public class PacienteDTO
    {
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


    }
}
