using labware_webapi.Domains;
using labware_webapi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace labware_webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioEquipesController : ControllerBase
    {
        private IUsuarioEquipeRepository _repository { get; set; }
        public UsuarioEquipesController(IUsuarioEquipeRepository repo)
        {
            _repository = repo;
        }

        [HttpGet]
        public IActionResult ListarTodas()
        {
            try
            {
                return Ok(_repository.ListarTodos());
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(UsuarioEquipe novaEquipe)
        {
            try
            {
                if (novaEquipe == null)
                {
                    return BadRequest("Não foi possível cadastrar");
                }
                _repository.Cadastrar(novaEquipe);
                return StatusCode(201);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpDelete("{idEquipe}")]
        public IActionResult Deletar(int idEquipe)
        {
            try
            {
                _repository.Deletar(idEquipe);
                return StatusCode(204);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }

        }

        [HttpPatch("{idUsuario}")]
        public IActionResult MudarEquipe(int idUsuario, UsuarioEquipe EquipeAtualizada)
        {
            try
            {
                _repository.MudarEquipe(idUsuario, EquipeAtualizada);

                return StatusCode(200);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpGet("{idEquipe}")]
        public IActionResult BuscarPorId(int idEquipe)
        {
            try
            {
                return Ok(_repository.Buscar(idEquipe));
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

    }
}
