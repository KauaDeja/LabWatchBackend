using labware_webapi.Domains;
using System.Collections.Generic;

namespace labware_webapi.Interfaces
{
    public interface IProjetoRepository
    {
        List<Projeto> ListarTodos();
        Projeto Buscar(int idProjeto);
        void Cadastrar(Projeto novoProjeto);
        void Deletar(int idProjeto);
        void Atualizar(Projeto projetoAtualizado, int idProjeto);
        void AtualizarFoto(Projeto projetoAtualizado, int idProjeto);
        public List<Projeto> VerMinhas(int idUsuario);
        void MudarSituacao(int statusProjeto, int idProjeto);
    }
}
