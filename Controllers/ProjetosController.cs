using labware_webapi.Contexts;
using labware_webapi.Domains;
using labware_webapi.Interfaces;
using labware_webapi.Repositories;
using labware_webapi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Patrimonio.Utils;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace labware_webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetosController : ControllerBase
    {
        private IProjetoRepository _repository { get; set; }
        public ProjetosController()
        {
            _repository = new ProjetoRepository();
        }

        
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_repository.ListarTodos());
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }


       
        [HttpGet("{idProjeto}")]
        public IActionResult BuscarPorId(int idProjeto)
        {
            try
            {
                return Ok(_repository.Buscar(idProjeto));
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }




        /* [HttpPost]
         public IActionResult Cadastrar(Projeto projeto)
         {
             try
             {
                 if (projeto == null)
                 {
                     return BadRequest("Não foi possível cadastrar");
                 };

                 _repository.Cadastrar(projeto);
                 return StatusCode(201);
             }
             catch (Exception error)
             {
                 return BadRequest(error.Message);
             }
         }*/


        [HttpPut("{idProjeto}")]
        public IActionResult Atualizar(int idProjeto, Projeto projeto)
        {
            try
            {
                _repository.Atualizar(projeto, idProjeto);
                return StatusCode(204);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

       
        [HttpDelete("{idProjeto}")]
        public IActionResult Deletar(int idProjeto)
        {
            try
            {
                _repository.Deletar(idProjeto);
                return StatusCode(204);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }


        }

        /* [HttpPut]
         public IActionResult PutEquipamento([FromForm] Projeto projeto, IFormFile arquivo, int idProjeto)
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


             try
             {
             _repository.AtualizarFoto(projeto, idProjeto);
             return StatusCode(204);

         }
             catch (Exception error)
             {
                return BadRequest(error.Message);
             }


             }*/

        
        [HttpGet("Minhas/{idUsuario}")]
        public IActionResult GetMyOwn(int idUsuario)
        {
            try
            {
     
                return Ok(_repository.VerMinhas(idUsuario));
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        
        [HttpPost]
        public IActionResult Cadastrar(Projeto projeto)
        {
            try
            {
                if (projeto == null)
                {
                    return BadRequest("Não foi possível cadastrar");
                };

                bool titulo = Moderador.ModerarTexto(projeto.TituloProjeto);
                bool descricao = Moderador.ModerarTexto(projeto.Descricao);
                if (titulo || descricao)
                {
                    return BadRequest("Texto inaproripado, por favor reescreva sem usar palavras inadequadas ou dados sensíveis.");
                }
                _repository.Cadastrar(projeto);
                return StatusCode(201);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPatch("MudarSituacao/{idProjeto}")]
        public IActionResult MudarSituacao(int idProjeto, int statusProjeto)
        {
            try
            {
                _repository.MudarSituacao(statusProjeto, idProjeto);

                return Ok();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }
    }
}
