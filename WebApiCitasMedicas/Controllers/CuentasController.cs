﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiCitasMedicas.DTOs;

namespace WebApiCitasMedicas.Controllers
{
    [ApiController]
    [Route("cuentas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public CuentasController(UserManager<IdentityUser> userManager, IConfiguration configuration,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }


        [AllowAnonymous]
        [HttpPost("HacerPaciente")]
        public async Task<ActionResult> HacerPaciente(EditarUsuarioDTO editarUsuarioDTO)
        {
            var usuario = await userManager.FindByEmailAsync(editarUsuarioDTO.Email);

            await userManager.AddClaimAsync(usuario, new Claim("EsPaciente", "1"));

            return NoContent();
        }


        [Authorize(Policy = "EsMedico")]
        [HttpPost("RemoverPaciente")]
        public async Task<ActionResult> RemoverAdmin(EditarUsuarioDTO editarUsuarioDTO)
        {
            var usuario = await userManager.FindByEmailAsync(editarUsuarioDTO.Email);

            await userManager.RemoveClaimAsync(usuario, new Claim("EsPaciente", "1"));

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("registrar")]
        public async Task<ActionResult<RespuestaAutenticacion>> Registrar(CredencialesUsuario credenciales)
        {
            var user = new IdentityUser { UserName = credenciales.Email, Email = credenciales.Email };
            var result = await userManager.CreateAsync(user, credenciales.Password);

            if (result.Succeeded)
            {
                //Se retorna el Jwt (Json Web Token) especifica el formato del token que hay que devolverle a los clientes
                return await ConstruirToken(credenciales);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<RespuestaAutenticacion>> Login(CredencialesUsuario credencialesUsuario)
        {
            var result = await signInManager.PasswordSignInAsync(credencialesUsuario.Email,
                credencialesUsuario.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return await ConstruirToken(credencialesUsuario);
            }
            else
            {
                return BadRequest("Login Incorrecto");
            }

        }

        [HttpGet("RenovarToken")]
        public async Task<ActionResult<RespuestaAutenticacion>> Renovar()
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;

            var credenciales = new CredencialesUsuario()
            {
                Email = email
            };

            return await ConstruirToken(credenciales);

        }

        private async Task<RespuestaAutenticacion> ConstruirToken(CredencialesUsuario credencialesUsuario)
        {
            //Informacion del usuario en la cual podemos confiar
            //En los claim se pueden declarar cualquier variable, sin embargo, no debemos de declarar informacion
            //del cliente sensible como pudiera ser una Tarjeta de Credito o contraseña

            var claims = new List<Claim>
            {
                new Claim("email", credencialesUsuario.Email)
            };

            var usuario = await userManager.FindByEmailAsync(credencialesUsuario.Email);
            var claimsDB = await userManager.GetClaimsAsync(usuario);

            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyjwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiration, signingCredentials: creds);

            return new RespuestaAutenticacion()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiration
            };
        }
    }

}



