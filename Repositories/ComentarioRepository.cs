using labware_webapi.Contexts;
using labware_webapi.Domains;
using labware_webapi.Interfaces;
using labware_webapi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labware_webapi.Repositories
{
    public class ComentarioRepository : IComentarioRepository
    {
        LabWatchContext ctx = new LabWatchContext();

        public Comentario BuscarPorId(int idComentario)
        {
            return ctx.Comentarios.FirstOrDefault(t => t.IdComentario == idComentario);
        }

        public void Cadastrar(Comentario novoComentario)
        {
                ctx.Comentarios.Add(novoComentario);
               


            if (novoComentario != null)
            {
                ctx.SaveChanges();
            }


        }

        public void Deletar(int idComentario)
        {
            ctx.Comentarios.Remove(BuscarPorId(idComentario));
            ctx.SaveChanges();
        }

        public List<Comentario> ListarTodos()
        {
            return ctx.Comentarios.ToList();
        }
    }
}
