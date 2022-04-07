using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = labware_webapi.Domains.Task;

namespace labware_webapi.Interfaces
{
    interface ITaskRepository
    {
        List<Task> ListarTodos();
        List<Task> ListarMinhas(int idUsuario);
        void Cadastrar(Task novaTask);
        void Deletar(int idTask);
        void Atualizar(int idTask, Task taskAtualizada);
        Task BuscarPorId(int id);
    }
}
