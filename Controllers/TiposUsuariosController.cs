using labware_webapi.Domains;
using labware_webapi.Interfaces;
using labware_webapi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labware_webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TiposUsuariosController : ControllerBase
    {
            private ITipoUsuarioRepository _repository { get; set; }
            public TiposUsuariosController()
            {
                _repository = new TipoUsuarioRepository();
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



        [HttpGet("{idTipoUsuario}")]
        public IActionResult BuscarPorId(int idTipoUsuario)
        {
            try
            {
                return Ok(_repository.BuscarPorId(idTipoUsuario));
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }


      
        [HttpPost]
        public IActionResult Cadastrar(TipoUsuario tipo)
        {
            try
            {
                if (tipo == null)
                {
                    return BadRequest("Não foi possível cadastrar");
                };

                _repository.Cadastrar(tipo);
                return StatusCode(201);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

    }
}
