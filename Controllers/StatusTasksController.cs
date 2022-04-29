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
    public class StatusTasksController : ControllerBase
    {
        private IStatusTaskRepository _repository { get; set; }
        public StatusTasksController()
        {
            _repository = new StatusTaskRepository();
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


        [HttpPost]
        public IActionResult Cadastrar(StatusTask task)
        {
            try
            {

                if (task == null)
                {
                    return BadRequest("Não foi possível cadastrar");
                };
                _repository.Cadastrar(task);
                return StatusCode(201);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }




    }
}
