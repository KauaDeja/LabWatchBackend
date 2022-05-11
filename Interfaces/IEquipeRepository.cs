using labware_webapi.Domains;
using System.Collections.Generic;

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
