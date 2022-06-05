using labware_webapi.Domains;
using labware_webapi.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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

        [Authorize(Roles = "1,3")]
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

        [Authorize(Roles = "1,3")]
        [HttpDelete("{idUsuarioEquipe}")]
        public IActionResult Deletar(int idUsuarioEquipe)
        {
            try
            {
                _repository.Deletar(idUsuarioEquipe);
                return StatusCode(204);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }

        }

        [Authorize(Roles = "1,3")]
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

        [Authorize]
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
