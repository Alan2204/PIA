using AutoMapper;
using WebApiCitasMedicas.DTOs;
using WebApiCitasMedicas.Entidades;

namespace WebApiCitasMedicas.Utilidades
{
    //Existe Dto de consulta y Dto de creacion
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MedicosDTO, Medicos>();
            CreateMap<PacienteDTO, Paciente>();
            CreateMap<CitasDTO, Citas>();
            CreateMap<CitasAgendadasDTO, CitasAgendadas>();
            CreateMap<Citas, GetCitasDTO>();
            CreateMap<InformacionMedicaDTO, InformacionMedica>();
            CreateMap<InformacionMedica, GetInformacionMedicaDTO>();
            CreateMap<Medicos, GetMedicosDTO>();
            CreateMap<Paciente, GetPacienteDTO>();
            CreateMap<EstadisticasDTO, Estadisticas>();
            CreateMap<ReporteConsultaDTO, ReporteConsulta>();
            CreateMap<EstadisticasDTO, Estadisticas>();
            CreateMap<ReporteConsulta, GetReporteConsulta>();
            CreateMap<CitasAgendadas, CitasAgendadasDTO>();
            CreateMap<Estadisticas, GetEstadisticasDTO>();

        }

        
    }
}
