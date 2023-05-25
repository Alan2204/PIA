using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using WebApiCitasMedicas.DTOs;
using WebApiCitasMedicas.Entidades;

namespace WebApiCitasMedicas.Controllers
{
    [ApiController]
    [Route("Pacientes")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PacientesController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;

        public PacientesController(UserManager<IdentityUser> userManager, ApplicationDbContext context, IMapper mapper, SignInManager<IdentityUser>
            signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.dbContext = context;
            this.mapper = mapper;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpGet("/misdatos")]
        public async Task<ActionResult<GetPacienteDTO>> GetDatos()
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;

            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioId = usuario.Id;


            var x = await dbContext.Pacientes.Where(x => x.UsuarioId == usuarioId).SingleAsync();


            var info = await dbContext.InformacionMedica.FirstOrDefaultAsync(x => x.PacienteId == x.Id);
   
            return mapper.Map<GetPacienteDTO>(x);
        }


        [HttpGet("miscitas")]
        public async Task<ActionResult<List<GetCitasAgendadasDTO>>> Get()
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;

            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioId = usuario.Id;

            var idpaciente = await dbContext.Pacientes.Where(x => x.UsuarioId == usuarioId).SingleAsync();
            var idcitas = await dbContext.CitasAgendadas.Where(x => x.PacienteId == idpaciente.Id).Include(citas => citas.citas).ToListAsync();
            
            //var y = await dbContext.Citas.Include(miscitas => idcitas.Contains(miscitas.Id));

            
            return mapper.Map<List<GetCitasAgendadasDTO>>(idcitas);
        }

 

        [HttpPost("/registrarpaciente")]
        public async Task<ActionResult> Post(PacienteDTO pacientedto)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;

            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioId = usuario.Id;

            var existe = await dbContext.Pacientes.AnyAsync(paciente => paciente.UsuarioId == usuarioId);
            if (existe)
            {
                return BadRequest("Ya tienes registrada inrofmacion medica.");
            }

            var paciente = mapper.Map<Paciente>(pacientedto);
            paciente.UsuarioId = usuarioId;
            dbContext.Add(paciente);
            await dbContext.SaveChangesAsync();

            return Ok();
          
        }

    

        [HttpPut("/actualizardatospaciente/{id:int}")]
        public async Task<ActionResult> Put(PacienteDTO pacientedto, int id)
        {
            var exist = await dbContext.Pacientes.AnyAsync(x => x.Id == id);
            

            if (!exist)
            {
                return BadRequest("El id con el establecido en la url.");
            }

            var paciente = mapper.Map<Paciente>(pacientedto);

            paciente.Id = id;
            dbContext.Update(paciente);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {

            var exist = await dbContext.Pacientes.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Paciente()
            {
                Id = id
            });

            await dbContext.SaveChangesAsync();
            return Ok();
        }




    }
}
