using labware_webapi.Domains;
using labware_webapi.Interfaces;
using labware_webapi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace labware_webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EquipesController : ControllerBase
    {
        private IEquipeRepository _repository { get; set; }
        public EquipesController(IEquipeRepository repo)
        {
            _repository = repo;
        }

        [Authorize]
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


        [Authorize]
        [HttpGet("{idEquipe}")]
        public IActionResult BuscarPorId(int idEquipe)
        {
            try
            {
                return Ok(_repository.Buscar(idEquipe));
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }



        [Authorize(Roles = "1,3")]
        [HttpPost]
        public IActionResult Cadastrar(Equipe equipe)
        {
            try
            {
                if (equipe == null)
                {
                    return BadRequest("Não foi possível cadastrar");
                };

                _repository.Cadastrar(equipe);
                return StatusCode(201);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }


        [Authorize(Roles = "1,3")]
        [HttpPut("{idEquipe}")]
        public IActionResult Atualizar(int idEquipe, Equipe equipe)
        {
            try
            {
                _repository.AtualizarPeloId(idEquipe, equipe);
                return StatusCode(204);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [Authorize(Roles = "1,3")]
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

        //[HttpPatch("{idUsuario}")]
        //public IActionResult MudarEquipe(int idUsuario, Equipe EquipeAtualizada)
        //{
        //    try
        //    {
        //        _repository.MudarEquipe(idUsuario, EquipeAtualizada);

        //        return StatusCode(200);
        //    }
        //    catch (Exception erro)
        //    {
        //        return BadRequest(erro);
        //    }
        //}
        }

    }
