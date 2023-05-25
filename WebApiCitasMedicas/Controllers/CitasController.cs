using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using WebApiCitasMedicas.DTOs;
using WebApiCitasMedicas.Entidades;

namespace WebApiCitasMedicas.Controllers
{
    //[ResponseCache]
    [ApiController]
    [Route("citas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CitasController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly UserManager<IdentityUser> userManager;

        public CitasController(ApplicationDbContext dbcontext, IMapper mapper, IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbcontext;
            this.mapper = mapper;
            this.configuration = configuration;
            this.userManager = userManager;
        }

        [HttpGet("Todaslascitas")]
        public async Task<ActionResult<List<GetCitasDTO>>> Get()
        {        
            var cita = await dbContext.Citas.ToListAsync();
            return mapper.Map<List<GetCitasDTO>>(cita);
        }

       /* [HttpGet("citasporfechadisponibles/{fecha:DateTime}")]
        public async Task<ActionResult<List<GetCitasDTO>>> GetFecha(DateTime fecha)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;

            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioId = usuario.Id;

            var medicoid = await dbContext.Medicos.Where(x => x.UsuarioId == usuarioId).SingleAsync();

            var val = DateOnly.FromDateTime(fecha);
            var citasAgendadasid = await dbContext.CitasAgendadas.Select(q => q.CitasId).ToListAsync();
            var filtro = await dbContext.Citas.Where(h => !citasAgendadasid.Contains(h.Id) && h.MedicosId == medicoid.Id && h.Fecha == fecha).ToListAsync();


            return mapper.Map<List<GetCitasDTO>>(filtro);
        }*/


        /*[HttpGet("citasporfechaagendadas/{fecha:DateTime}")]
        public async Task<ActionResult<List<GetCitasDTO>>> Get(DateTime fecha)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;

            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioId = usuario.Id;

            var medicoid = await dbContext.Medicos.Where(x => x.UsuarioId == usuarioId).SingleAsync();

            var val = DateOnly.FromDateTime(fecha);
            var citasAgendadasid = await dbContext.CitasAgendadas.Select(q => q.CitasId).ToListAsync();
            var filtro = await dbContext.Citas.Where(h => citasAgendadasid.Contains(h.Id) && h.MedicosId == medicoid.Id && h.Fecha == fecha).ToListAsync();


            return mapper.Map<List<GetCitasDTO>>(filtro);
        }*/



        [HttpGet("citasdisponibles/{id:int}")]
        public async Task<ActionResult<List<GetCitasDTO>>> Get(int id)
        {
            //Fitra las citas para que solo muestre las disponibles
            var x = await dbContext.CitasAgendadas.Select(q => q.CitasId).ToListAsync();
            var filtro = await dbContext.Citas.Where(h => !x.Contains(h.Id)  && h.MedicosId == id).ToListAsync();
       
                          
            return mapper.Map<List<GetCitasDTO>>(filtro);
        }

        [HttpGet("citasnodisponibles/{id:int}")]
        public async Task<ActionResult<List<GetCitasDTO>>> Get2(int id)
        {
            //Fitra las citas para que solo muestre las disponibles
            var x = await dbContext.CitasAgendadas.Select(q => q.CitasId).ToListAsync();
            var filtro = await dbContext.Citas.Where(h => x.Contains(h.Id) && h.MedicosId == id).ToListAsync();


            return mapper.Map<List<GetCitasDTO>>(filtro);
        }



        [Authorize(Policy ="EsMedico")]
        [HttpPost("crearcita")]
        public async Task<ActionResult> Post(CitasDTO citasdto)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;

            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioId = usuario.Id;

            var existe = await dbContext.Medicos.AnyAsync(claseDB => claseDB.UsuarioId == usuarioId);
            if (!existe)
            {
                return BadRequest("No puedes registrar una cita sin antes registrar tus datos.");
            }
            var x = await dbContext.Medicos.Where(x=> x.UsuarioId == usuarioId).SingleAsync();

            var citas = mapper.Map<Citas>(citasdto);

            citas.MedicosId = x.Id;
            dbContext.Add(citas);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Policy = "EsMedico")]
        [HttpPut("actualizarfechas/{id:int}")]
        public async Task<ActionResult> Put(CitasDTO citasdto, int id)
        {
            var exist = await dbContext.Citas.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return BadRequest("El id no coincide con el establecido en la url.");
            }

            var citas = mapper.Map<Citas>(citasdto);

            citas.Id = id;
            dbContext.Update(citas);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Policy = "EsMedico")]
        [HttpDelete("eliminarcita/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Citas.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound();
            }

           /* var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;

            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioId = usuario.Id;

            var medico = await dbContext.Medicos.Where(x => x.UsuarioId == usuarioId).SingleAsync();
            var cita = await dbContext.Citas.Where(x => x.Id == id).SingleAsync();

            if (medico.Id != cita.MedicosId)
            {
                return BadRequest("No puedes eliminar las citas de otros medicos.");
            }*/

            dbContext.Remove(new Citas()
            {
                Id = id
            });

            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
