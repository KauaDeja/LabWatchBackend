using labware_webapi.Contexts;
using labware_webapi.Domains;
using labware_webapi.Interfaces;
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
            return ctx.Tasks.Include(p => p.IdProjetoNavigation).Include(c => c.IdStatusTaskNavigation).Include(d => d.IdTagNavigation).ToList();
        }

        public List<Task> VerMinhas(int idUsuario)
        {
            return ctx.Tasks.Include(p => p.IdProjetoNavigation).Include(s => s.IdStatusTaskNavigation).Include(t => t.IdTagNavigation)
                  .Select(c => new Task()
                  {
                      IdTask = c.IdTask,
                      IdStatusTask = c.IdStatusTask,
                      IdProjeto = c.IdProjeto,
                      IdUsuario = c.IdUsuario,
                      TituloTask = c.TituloTask,
                      Descricao = c.Descricao,
                      TempoTrabalho = c.TempoTrabalho,
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

                      }
                  }).Where(p => p.IdUsuarioNavigation.IdUsuario == idUsuario).ToList();
        }
    

}
}
