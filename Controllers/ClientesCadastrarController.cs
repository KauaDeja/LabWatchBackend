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
    public class ClientesCadastrarController : ControllerBase
    {
        private readonly LabWatchContext _context;
        public ClientesCadastrarController(LabWatchContext context)
        {
            _context = context;
        }

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
          
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return Created("Cliente", cliente);


        }
    }
}
