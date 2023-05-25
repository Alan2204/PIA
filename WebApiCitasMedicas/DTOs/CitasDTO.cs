using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;

namespace WebApiCitasMedicas.DTOs
{
    public class CitasDTO
    {
        [Required(ErrorMessage = "Es necesario el campo {0}.")]
        public DateTime Fecha { get; set; }
        public int MedicosId { get; set; }
    }
}
