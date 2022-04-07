using labware_webapi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labware_webapi.Interfaces
{
    interface IStatusTaskRepository
    {
        List<StatusTask> ListarTodos();
    }
}
