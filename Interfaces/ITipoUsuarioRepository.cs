using labware_webapi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labware_webapi.Interfaces
{
    interface ITipoUsuarioRepository
    {
        List<TipoUsuario> ListarTodos();
        TipoUsuario BuscarPorId(int idTipoUsuario);
        void Cadastrar(TipoUsuario novoTipo);

    }
}
