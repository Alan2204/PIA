using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCitasMedicas.DTOs;
using WebApiCitasMedicas.Entidades;

namespace WebApiCitasMedicas.Controllers
{
    [ApiController]
    [Route("medicos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy ="EsMedico")]
    //[Authorize(Policy = "EsAdmin")]
    public class MedicosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;


        public MedicosController(ApplicationDbContext dbContext, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userManager = userManager;
        }


        [HttpGet]
        public async Task<ActionResult<List<GetMedicosDTO>>> Get()
        {
            var medicos = await dbContext.Medicos.ToListAsync();
            return mapper.Map<List<GetMedicosDTO>>(medicos);
        }

        [HttpPost]
        public async Task<ActionResult> Post(MedicosDTO medicosdto)
        {

            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;
            
            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioId = usuario.Id;

            var existe = await dbContext.Medicos.AnyAsync(claseDB => claseDB.UsuarioId == usuarioId);
            if (existe)
            {
                return BadRequest("No puede realizar esta accion.");
            }

            var medicos = mapper.Map<Medicos>(medicosdto);
            medicos.UsuarioId = usuarioId;
            dbContext.Add(medicos);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

       /* [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(MedicosDTO medicosdto, int id)
        {
            var exist = await dbContext.Medicos.AnyAsync(x => x.Id == id );
            if (!exist)
            {
                return BadRequest("El id no coincide con el establecido en la url.");
            }

            var medicos = mapper.Map<Medicos>(medicosdto);

            medicos.Id = id;
            dbContext.Update(medicos);
            await dbContext.SaveChangesAsync();
            return Ok();
        }*/

       /* [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {

            var exist = await dbContext.Medicos.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Medicos()
            {
                Id = id
            });

            await dbContext.SaveChangesAsync();
            return Ok();
        }*/




    }
}
