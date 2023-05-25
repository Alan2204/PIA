using System.ComponentModel.DataAnnotations;

namespace WebApiCitasMedicas.DTOs
{
    public class EditarUsuarioDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
