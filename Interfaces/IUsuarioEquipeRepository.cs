using labware_webapi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labware_webapi.Interfaces
{
    interface IUsuarioEquipeRepository
    {
        List<UsuarioEquipe> ListarTodos();
        UsuarioEquipe Buscar(int idEquipe);
        void Cadastrar(UsuarioEquipe novaEquipe);
        void Deletar(int idEquipe);
        void MudarEquipe(int idUsuario, UsuarioEquipe EquipeAtualizada);
    }
}
