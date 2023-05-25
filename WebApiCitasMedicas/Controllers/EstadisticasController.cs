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
    [Route("estadisticas")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsMedico")]
    public class EstadisticasController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;



        public EstadisticasController(ApplicationDbContext dbContext, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<ActionResult<List<GetEstadisticasDTO>>> Get()
        {
            var estadisticas = await dbContext.Estadisticas.ToListAsync();
            return mapper.Map<List<GetEstadisticasDTO>>(estadisticas);
        }

        [HttpPost("estadisticas")]
        public async Task<ActionResult> Post(EstadisticasDTO estadisticasDTO)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;

            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioId = usuario.Id;

            var existe = await dbContext.Medicos.AnyAsync(medico => medico.UsuarioId == usuarioId);
            if (!existe)
            {
                return BadRequest("No puedes registrar Estadisticas medica sin antes registrar tus datos.");
            }

            var x = await dbContext.Medicos.Where(x => x.UsuarioId == usuarioId).SingleAsync();

            var estadisticas =  mapper.Map<Estadisticas>(estadisticasDTO);
            estadisticas.MedicosId = x.Id;
            dbContext.Add(estadisticas);
            await dbContext.SaveChangesAsync();
            return Ok();
        }



    }
}
