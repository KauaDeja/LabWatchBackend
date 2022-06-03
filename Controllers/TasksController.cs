using labware_webapi.Domains;
using labware_webapi.Interfaces;
using labware_webapi.Repositories;
using labware_webapi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace labware_webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private ITaskRepository _taskRepository { get; set; }
        public TasksController()
        {
            _taskRepository = new TaskRepository();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_taskRepository.ListarTodos());
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }


        [Authorize]
        [HttpGet("{idTask}")]
        public IActionResult BuscarPorId(int idTask)
        {
            try
            {
                return Ok(_taskRepository.BuscarPorId(idTask));
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }



        [Authorize(Roles = "1,3")]
        [HttpPost]
        public IActionResult Cadastrar(Task task)
        {
            bool tituloTask = Moderador.ModerarTexto(task.TituloTask);
            bool descricao = Moderador.ModerarTexto(task.Descricao);

            try
            {
                if (tituloTask || descricao)
                {
                    return BadRequest("Texto inaproripado, por favor reescreva sem usar palavras inadequadas ou dados sensíveis");
                };
                

                _taskRepository.Cadastrar(task);
                return StatusCode(201);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }


        [Authorize]
        [HttpPut("{idTask}")]
        public IActionResult Atualizar(int idTask, Task task)
        {
            try
            {
                _taskRepository.Atualizar(idTask, task);
                return StatusCode(204);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Authorize(Roles = "1,3")]
        [HttpDelete("{idTask}")]
        public IActionResult Deletar(int idTask)
        {
            try
            {
                _taskRepository.Deletar(idTask);
                return StatusCode(204);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }

        }

        [Authorize]
        [HttpGet("Minhas/{idUsuario}")]
        public IActionResult GetMyOwn(int idUsuario)
        {
            try
            {
                return Ok(_taskRepository.VerMinhas(idUsuario));
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [Authorize(Roles = "1,3")]
        [HttpPatch("{idUsuario}")]
        public IActionResult AlterarResponsavel(int idUsuario, Task task)
        {
            try
            {
                _taskRepository.AlterarResponsavel(idUsuario, task);
                return Ok();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }
    }
}
