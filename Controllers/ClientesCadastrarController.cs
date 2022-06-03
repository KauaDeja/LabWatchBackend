using labware_webapi.Contexts;
using labware_webapi.Domains;
using labware_webapi.Utils;
using Microsoft.AspNetCore.Authorization;
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
    public class ClientesCadastrarController : ControllerBase
    {
        private readonly LabWatchContext _context;
        public ClientesCadastrarController(LabWatchContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "1,3")]
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente([FromForm] Cliente cliente, IFormFile arquivo)
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

            cliente.FotoCliente = uploadResultado;
            #endregion
            #region Moderador de conteúdo
            bool nomeCliente = Moderador.ModerarTexto(cliente.NomeCliente);
            bool descricao = Moderador.ModerarTexto(cliente.Descricao);

            if (nomeCliente || descricao)
            {
                return BadRequest("Texto inaproripado, por favor reescreva sem usar palavras inadequadas ou dados sensíveis.");
            }

            #endregion

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return Created("Cliente", cliente);


        }
    }
}
