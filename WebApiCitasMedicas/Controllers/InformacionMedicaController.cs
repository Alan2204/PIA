using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCitasMedicas.DTOs;
using WebApiCitasMedicas.Entidades;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace WebApiCitasMedicas.Controllers
{
    [ApiController]
    [Route("informacionmedica")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsPaciente")]
    public class InformacionMedicaController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public InformacionMedicaController(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            this.dbContext = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }
       

        [HttpGet("/lectura/{id:int}")]
        public async Task<ActionResult<GetInformacionMedicaDTO>> Get(int id)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;

            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioId = usuario.Id;


            var x = await dbContext.Pacientes.Where(x => x.UsuarioId == usuarioId).SingleAsync();

            if(x.Id != id)
            {
                return BadRequest("No puedes ingresar el id de otro paciente");
            }

            var info = await dbContext.InformacionMedica.FirstOrDefaultAsync(x => x.PacienteId == id);
            if (info == null)
            {
                return NotFound();
            }
            return mapper.Map<GetInformacionMedicaDTO>(info);
        }

        [HttpPost]
        public async Task<ActionResult> Post(InformacionMedicaDTO informacionmedicadto)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;

            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioId = usuario.Id;

            var existe = await dbContext.Pacientes.AnyAsync(paciente => paciente.UsuarioId == usuarioId);
            if (!existe)
            {
                return BadRequest("No puedes registrar infromacion medica sin antes registrar tus datos.");
            }

            var x = await dbContext.Pacientes.Where(x => x.UsuarioId == usuarioId).SingleAsync();
          
            var info = mapper.Map<InformacionMedica>(informacionmedicadto);
            info.PacienteId = x.Id;
            dbContext.Add(info);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(InformacionMedicaDTO informacionMedicaDTO, int id)
        {
            var exist = await dbContext.InformacionMedica.AnyAsync(x => x.Id == id);


            if (!exist)
            {
                return BadRequest("El id no coincide con el establecido en la url.");
            }

            var info = mapper.Map<InformacionMedica>(informacionMedicaDTO);

            info.Id = id;
            dbContext.Update(info);
            await dbContext.SaveChangesAsync();
            return Ok();

        }
    }
}
