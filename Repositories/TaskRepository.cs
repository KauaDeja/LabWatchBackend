using labware_webapi.Contexts;
using labware_webapi.Domains;
using labware_webapi.Interfaces;
using labware_webapi.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = labware_webapi.Domains.Task;

namespace labware_webapi.Repositories
{
    public class TaskRepository : ITaskRepository
    {

        LabWatchContext ctx = new();
        public Task BuscarPorId(int id)
        {
            return ctx.Tasks.FirstOrDefault(m => m.IdTask == id);
        }


        public void Atualizar(int idTask, Task taskAtualizada)
        {
            Task taskBuscada = ctx.Tasks.Find(idTask);

            if (taskAtualizada.TituloTask != null)
            {
                taskBuscada.IdProjeto = taskAtualizada.IdProjeto;
                taskBuscada.IdTag = taskAtualizada.IdTag;
                taskBuscada.IdStatusTask = taskAtualizada.IdStatusTask;
                taskBuscada.IdUsuario = taskAtualizada.IdUsuario;
                taskBuscada.TituloTask = taskAtualizada.TituloTask;
                taskBuscada.Descricao = taskAtualizada.Descricao;
                taskBuscada.TempoTrabalho = taskAtualizada.TempoTrabalho;
                ctx.Tasks.Update(taskBuscada);
                ctx.SaveChanges();
            }
        }

        public void Cadastrar(Task novaTask)
        {
            ctx.Tasks.Add(novaTask);
            ctx.SaveChanges();

        }

        public void Deletar(int idTask)
        {
            ctx.Tasks.Remove(BuscarPorId(idTask));
            ctx.SaveChanges();
        }

        public List<Task> ListarMinhas(int idUsuario)
        {
            throw new NotImplementedException();
        }

        public List<Task> ListarTodos()
        {
            return ctx.Tasks
                .Include(p => p.IdProjetoNavigation).Include(s => s.IdStatusTaskNavigation).Include(t => t.IdTagNavigation).Include(u => u.Comentarios).ThenInclude(v => v.IdUsuarioNavigation)
                  .Select(c => new Task()
                  {
                      IdTask = c.IdTask,
                      IdStatusTask = c.IdStatusTask,
                      IdProjeto = c.IdProjeto,
                      IdUsuario = c.IdUsuario,
                      TituloTask = c.TituloTask,
                      Descricao = c.Descricao,
                      TempoTrabalho = c.TempoTrabalho,
                      Comentarios = c.Comentarios,
                      IdProjetoNavigation = new Projeto()
                      {
                          IdProjeto = c.IdProjetoNavigation.IdProjeto,
                          IdStatusProjeto = c.IdProjetoNavigation.IdStatusProjeto,
                          IdEquipe = c.IdProjetoNavigation.IdEquipe,
                          IdCliente = c.IdProjetoNavigation.IdCliente,
                          TituloProjeto = c.IdProjetoNavigation.TituloProjeto,
                          DataInicio = c.IdProjetoNavigation.DataInicio,
                          DataConclusao = c.IdProjetoNavigation.DataConclusao,
                          Descricao = c.IdProjetoNavigation.Descricao,
                      },
                      IdTagNavigation = new Tag()
                      {
                          IdTag = c.IdTagNavigation.IdTag,
                          TituloTag = c.IdTagNavigation.TituloTag,
                      },
                      IdStatusTaskNavigation = new StatusTask()
                      {
                          IdStatusTask = c.IdStatusTaskNavigation.IdStatusTask,
                          StatusTaskE = c.IdStatusTaskNavigation.StatusTaskE,
                      },
                      IdUsuarioNavigation = new Usuario()
                      {
                          IdUsuario = c.IdUsuarioNavigation.IdUsuario,
                          IdTipoUsuario = c.IdUsuarioNavigation.IdTipoUsuario,
                          IdStatus = c.IdUsuarioNavigation.IdStatus,
                          NomeUsuario = c.IdUsuarioNavigation.NomeUsuario,
                          SobreNome = c.IdUsuarioNavigation.SobreNome,
                          CargaHoraria = c.IdUsuarioNavigation.CargaHoraria,
                          HorasTrabalhadas = c.IdUsuarioNavigation.HorasTrabalhadas,
                          Email = c.IdUsuarioNavigation.Email,
                          Senha = c.IdUsuarioNavigation.Senha,

                      },

                  }).ToList();
        }

        public List<Task> VerMinhas(int idUsuario)
        {
            return ctx.Tasks
                .Include(p => p.IdProjetoNavigation).Include(s => s.IdStatusTaskNavigation).Include(t => t.IdTagNavigation).Include(u => u.Comentarios).ThenInclude(v => v.IdUsuarioNavigation)
                  .Select(c => new Task()
                  {
                      IdTask = c.IdTask,
                      IdStatusTask = c.IdStatusTask,
                      IdProjeto = c.IdProjeto,
                      IdUsuario = c.IdUsuario,
                      TituloTask = c.TituloTask,
                      Descricao = c.Descricao,
                      TempoTrabalho = c.TempoTrabalho,
                      Comentarios = c.Comentarios,
                      IdProjetoNavigation = new Projeto()
                      {
                          IdProjeto = c.IdProjetoNavigation.IdProjeto,
                          IdStatusProjeto = c.IdProjetoNavigation.IdStatusProjeto,
                          IdEquipe = c.IdProjetoNavigation.IdEquipe,
                          IdCliente = c.IdProjetoNavigation.IdCliente,
                          TituloProjeto = c.IdProjetoNavigation.TituloProjeto,
                          DataInicio = c.IdProjetoNavigation.DataInicio,
                          DataConclusao = c.IdProjetoNavigation.DataConclusao,
                          Descricao = c.IdProjetoNavigation.Descricao,
                      },       
                      IdTagNavigation = new Tag()
                      {
                          IdTag = c.IdTagNavigation.IdTag,
                          TituloTag = c.IdTagNavigation.TituloTag,
                      },
                      IdStatusTaskNavigation = new StatusTask()
                      {
                          IdStatusTask = c.IdStatusTaskNavigation.IdStatusTask,
                          StatusTaskE = c.IdStatusTaskNavigation.StatusTaskE,
                      },
                      IdUsuarioNavigation = new Usuario()
                      {
                          IdUsuario = c.IdUsuarioNavigation.IdUsuario,
                          IdTipoUsuario = c.IdUsuarioNavigation.IdTipoUsuario,
                          IdStatus = c.IdUsuarioNavigation.IdStatus,
                          NomeUsuario = c.IdUsuarioNavigation.NomeUsuario,
                          SobreNome = c.IdUsuarioNavigation.SobreNome,
                          CargaHoraria = c.IdUsuarioNavigation.CargaHoraria,
                          HorasTrabalhadas = c.IdUsuarioNavigation.HorasTrabalhadas,
                          Email = c.IdUsuarioNavigation.Email,
                          Senha = c.IdUsuarioNavigation.Senha,

                      },
                   
                  }).Where(p => p.IdUsuarioNavigation.IdUsuario == idUsuario).ToList();
        }

        public void AlterarResponsavel(int idUsuario, Task task)
        {
            Task taskBuscada = ctx.Tasks.FirstOrDefault(c => c.IdTask == task.IdTask);
            Usuario usuarioBuscado = ctx.Usuarios.FirstOrDefault(c => c.IdUsuario == idUsuario);
            if (taskBuscada != null)
            {
                taskBuscada.IdUsuario = usuarioBuscado.IdUsuario;

                ctx.Tasks.Update(taskBuscada);

                ctx.SaveChanges();
            }
        }
    }
}
