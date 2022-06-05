using labware_webapi.Contexts;
using System.IO;
using labware_webapi.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Patrimonio.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labware_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadastroUsuarioController : ControllerBase
    {
        private readonly LabWatchContext _context;
        public CadastroUsuarioController(LabWatchContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "1,3")]
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUser([FromForm] Usuario usuario, IFormFile arquivo)
        {

            #region Upload da Imagem com extensões permitidas apenas
            string[] extensoesPermitidas = { "jpg", "png", "jpeg", "gif" };
            string uploadResultado = Upload.UploadFile(arquivo, extensoesPermitidas);
            
            
            if (usuario.FotoUsuario == null)
            {
                usuario.FotoUsuario = Path.Combine("StaticFiles", "Images", "padrao.png");
            }


            if (uploadResultado == "")
            {
                return BadRequest("Arquivo não encontrado");
            }

            if (uploadResultado == "Extensão não permitida")
            {
                return BadRequest("Extensão de arquivo não permitida");
            }

            usuario.FotoUsuario = uploadResultado;
            #endregion
            _context.Usuarios.Add(usuario);
           
            await _context.SaveChangesAsync();

            return Created("Usuario", usuario);


        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Usuario>> PostUser([FromForm] Usuario usuarioAtualizado, IFormFile arquivo, int id)
        {
            if (id != usuarioAtualizado.IdUsuario)
            {
                return BadRequest();
            }

            try
            {
                #region 
                string[] extensoesPermitidas = { "jpg", "png", "jpeg", "gif" };
                string uploadResultado = Upload.UploadFile(arquivo, extensoesPermitidas);

                if (uploadResultado == "")
                {
                    return BadRequest("Arquivo não encontrado");
                }

                if (uploadResultado == "Extensão não permitida")
                {
                    return BadRequest("Extensão de arquivo não permitida");
                }

                usuarioAtualizado.FotoUsuario = uploadResultado;
                #endregion

                _context.Entry(usuarioAtualizado).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}
