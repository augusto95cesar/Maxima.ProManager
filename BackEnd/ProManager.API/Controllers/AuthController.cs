using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProManager.Application.DTOs.Input;
using ProManager.Application.EventHandlers;
using ProManager.Application.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProManager.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _configuration;
        private UsuarioService _usuarioService;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
            _usuarioService = new UsuarioService();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var validationResult = login.ValidateUsuario();
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var user = await _usuarioService.FindAsync(login.User);

            if (user == null) return BadRequest("Usuario não existe!");
            if (user.SenhaSystem != login.Password) return BadRequest("Erro ao digitar a Senha!");

            // Acessar a chave do appsettings.json
            var key = _configuration["JwtSettings:SecretKey"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];

            // Configurações do token
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                  new Claim("idUser", $"{user.Id}"),
                  new Claim("Login", $"{user.UserSystem}"),
                  //new Claim("TypeUsuario", $"{user.TypeUserSystem}"),
                  new Claim(ClaimTypes.Role, $"{user.TypeUserSystem}"),
                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            var tokenWrite = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { token = "Bearer " + tokenWrite });

        }
    }
}
