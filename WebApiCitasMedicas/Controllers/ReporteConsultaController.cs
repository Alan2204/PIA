using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCitasMedicas.DTOs;
using WebApiCitasMedicas.Entidades;

namespace WebApiCitasMedicas.Controllers
{
    [Route("reporte")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsMedico")]
    public class ReporteConsultaController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;


        public ReporteConsultaController(ApplicationDbContext dbContext, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetReporteConsulta>> Get(int id)
        {
            var info = await dbContext.ReporteConsulta.FirstOrDefaultAsync(x => x.CitasAgendadasId == id);
            if(info == null)
            {
                return NotFound();
            }

            return mapper.Map<GetReporteConsulta>(info);
        }

        [HttpPost("hacerReporte")]
        public async Task<ActionResult> Post(ReporteConsultaDTO reporteConsultaDTO)
        {
            var reporte = mapper.Map<ReporteConsulta>(reporteConsultaDTO);
            dbContext.Add(reporte);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
