using labware_webapi.Contexts;
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
            return ctx.Tasks.Include(p => p.IdProjetoNavigation).Include(c => c.IdStatusTaskNavigation).ToList();
        }
    }
}
