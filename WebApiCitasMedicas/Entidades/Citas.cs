using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;

namespace WebApiCitasMedicas.Entidades
{
    public class Citas
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Es necesario el campo {0}.")]
        public DateTime Fecha { get; set; }
        public int MedicosId { get; set; }
        public Medicos medicos { get; set; }
        public List<CitasAgendadas> citasagendadas { get; set; }
       
    }
}
