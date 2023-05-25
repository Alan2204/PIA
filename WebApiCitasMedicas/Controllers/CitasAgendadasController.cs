using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiCitasMedicas.DTOs;
using WebApiCitasMedicas.Entidades;

namespace WebApiCitasMedicas.Controllers
{
    [Route("citasagendadas")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsPaciente")]
    public class CitasAgendadasController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;


        public CitasAgendadasController(ApplicationDbContext dbContext, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpPost("agendarcita/{id:int}")]
        public async Task<ActionResult> Post(CitasAgendadasDTO citasAgendadasDTO, int id)
        {
             var agendar = mapper.Map<CitasAgendadas>(citasAgendadasDTO);
             agendar.CitasId = id;
             dbContext.Add(agendar);
             await dbContext.SaveChangesAsync();
             return Ok();
        }


    }
}
