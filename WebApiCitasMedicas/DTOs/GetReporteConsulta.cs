using System.ComponentModel.DataAnnotations;

namespace WebApiCitasMedicas.DTOs
{
    public class GetReporteConsulta
    {
        public int Id { get; set; }
        public int CitasAgendadasId { get; set; }

        public string Motivo { get; set; }

        public string Tratamiento { get; set; }

        public string Receta { get; set; }
    }
}
