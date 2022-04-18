using labware_webapi.Contexts;
using labware_webapi.Domains;
using labware_webapi.Utils;
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
    public class CadastrarProjetosController : ControllerBase
    {

        private readonly LabWatchContext _context;

        public CadastrarProjetosController(LabWatchContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Projeto>> PostEquipamento([FromForm] Projeto projeto, IFormFile arquivo)
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

            projeto.fotoCliente = uploadResultado;
            #endregion
            bool titulo = Moderador.ModerarTexto(projeto.TituloProjeto);
            bool descricao = Moderador.ModerarTexto(projeto.Descricao);


            if (titulo || descricao)
            {
                return BadRequest("Texto inaproripado, por favor reescreva sem usar palavras inadequadas ou dados sensíveis.");
            }


            _context.Projetos.Add(projeto);
            await _context.SaveChangesAsync();

            return Created("Projeto", projeto);


        }
    }
}
