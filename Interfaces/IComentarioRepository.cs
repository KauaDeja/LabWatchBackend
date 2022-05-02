using labware_webapi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labware_webapi.Interfaces
{
    public interface IComentarioRepository
    {
        List<Comentario> ListarTodos();
        void Cadastrar(Comentario novoComentario);
        void Deletar(int idComentario);
        Comentario BuscarPorId(int idComentario);
    }
}
