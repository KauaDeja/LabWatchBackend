using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = labware_webapi.Domains.Task;

namespace labware_webapi.Interfaces
{
    public interface ITaskRepository
    {
        List<Task> ListarTodos();
        void Cadastrar(Task novaTask);
        void Deletar(int idTask);
        void Atualizar(int idTask, Task taskAtualizada);
        Task BuscarPorId(int id);
        public List<Task> VerMinhas(int idUsuario);
        void AlterarResponsavel(int idUsuario, Task task);
        void MudarSituacao(int idTask, int idSituacao);
    }
}
