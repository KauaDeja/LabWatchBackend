using labware_webapi.Domains;
using labware_webapi.Interfaces;
using labware_webapi.Repositories;
using labware_webapi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace labware_webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public LoginController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpPost]
            public IActionResult Login(LoginViewModel login)
            {
                try
                {
                    Usuario usuarioBuscado = _usuarioRepository.Login(login.Email, login.Senha);
                    if (usuarioBuscado == null)
                    {
                        return NotFound("E-mail ou senha inválidos!");
                    }

                    var MinhaClaim = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                    new Claim(JwtRegisteredClaimNames.Name, usuarioBuscado.NomeUsuario),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Role, usuarioBuscado.IdTipoUsuario.ToString()),
                    new Claim( "role", usuarioBuscado.IdTipoUsuario.ToString() )
                };
                    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("DUoWyXzWbY-keyLabWare0193=1241"));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var meuToken = new JwtSecurityToken(
                           issuer: "labware_webapi",
                           audience: "labware_webapi",
                           claims: MinhaClaim,
                           expires: DateTime.Now.AddMinutes(30),
                           signingCredentials: creds
                       );

                    return Ok(
                        new
                        {
                            tokenGerado = new JwtSecurityTokenHandler().WriteToken(meuToken)
                        });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
        }
    }

