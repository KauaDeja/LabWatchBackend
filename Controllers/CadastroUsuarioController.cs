using labware_webapi.Contexts;
using labware_webapi.Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUser([FromForm] Usuario usuario, IFormFile arquivo)
        {

            #region Upload da Imagem com extensões permitidas apenas
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

            usuario.FotoUsuario = uploadResultado;
            #endregion

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Created("Usuario", usuario);


        }
    }
}
