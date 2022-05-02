using labware_webapi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labware_webapi.Interfaces
{
    public interface IEquipeRepository
    {
        List<Equipe> ListarTodos();
        Equipe Buscar(int idEquipe);
        void Cadastrar(Equipe novaEquipe);
        void Deletar(int idEquipe);
        void AtualizarPeloId(int idEquipe, Equipe EquipeAtualizada);
    }
}
