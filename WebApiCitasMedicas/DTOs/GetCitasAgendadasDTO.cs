using WebApiCitasMedicas.Entidades;

namespace WebApiCitasMedicas.DTOs
{
    public class GetCitasAgendadasDTO  
    {

        public int PacienteId { get; set; }
        public GetCitasDTO getcitasdto { get; set; }
    }
}
