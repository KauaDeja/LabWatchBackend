using labware_webapi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labware_webapi.Interfaces
{
    interface IStatusUsuarioRepository
    {
        List<StatusUsuario> ListarTodos();
        void AtualizarPeloId(int idStatus, StatusUsuario statusAtualizado);
    }
}
