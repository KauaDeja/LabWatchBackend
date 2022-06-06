using labware_webapi.Domains;
using labware_webapi.Interfaces;
using labware_webapi.Repositories;
using Microsoft.AspNetCore.Authorization;
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
    public class ClientesController : ControllerBase
    {
        private IClienteRepository _repository { get; set; }
        public ClientesController()
        {
            _repository = new ClienteRepository();
        }


        [Authorize]
        [HttpGet]
    public IActionResult ListarTodos()
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

        [Authorize(Roles = "1,3")]
        [HttpGet("{idCliente}")]
        public IActionResult BuscarPorId(int idCliente)
        {
            try
            {
                return Ok(_repository.Buscar(idCliente));
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [Authorize(Roles = "1,3")]
        [HttpPut("{idCliente}")]
        public IActionResult Atualizar(int idCliente, Cliente cliente)
        {
            try
            {
                _repository.AtualizarPeloId(idCliente, cliente);
                return StatusCode(204);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [Authorize(Roles = "1,3")]
        [HttpDelete("{idCliente}")]
        public IActionResult Deletar(int idCliente)
        {
            try
            {
                _repository.Deletar(idCliente);
                return StatusCode(204);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }

        }

    }


}

