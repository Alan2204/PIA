using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiCitasMedicas.DTOs;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace WebApiCitasMedicas.Controllers
{
    [Route("admin")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class AdminController :ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        

        public AdminController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;           
        }

      /*  [HttpPost("HacerAdmin")]
        public async Task<ActionResult> HacerAdmin(EditarUsuarioDTO editarAdminDTO)
        {
            var usuario = await userManager.FindByEmailAsync(editarAdminDTO.Email);

            await userManager.AddClaimAsync(usuario, new Claim("EsAdmin", "1"));

            return NoContent();
        }*/

        [HttpPost("HacerMedico")]
        public async Task<ActionResult> HacerMedico(EditarUsuarioDTO editarUsuarioDTO)
        {
            var usuario = await userManager.FindByEmailAsync(editarUsuarioDTO.Email);

            await userManager.AddClaimAsync(usuario, new Claim("EsMedico", "1"));

            return NoContent();
        }

        [HttpPost("RemoverMedico")]
        public async Task<ActionResult> RemoverMedico(EditarUsuarioDTO editarUsuarioDTO)
        {
            var usuario = await userManager.FindByEmailAsync(editarUsuarioDTO.Email);

            await userManager.RemoveClaimAsync(usuario, new Claim("EsMedico", "1"));

            return NoContent();
        }
        
    }

}


