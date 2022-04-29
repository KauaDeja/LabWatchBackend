using labware_webapi.Contexts;
using labware_webapi.Domains;
using labware_webapi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labware_webapi.Repositories
{
    public class StatusTaskRepository : IStatusTaskRepository
    {
        LabWatchContext ctx = new LabWatchContext();
        public List<StatusTask> ListarTodos()
        {
            return ctx.StatusTasks.ToList();
        }

         public void Cadastrar(StatusTask task)
        {
            ctx.StatusTasks.Add(task);
            ctx.SaveChanges();
        }
    }
}
