using labware_webapi.Contexts;
using labware_webapi.Domains;
using labware_webapi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labware_webapi.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        LabWatchContext ctx = new LabWatchContext();
        public TipoUsuario BuscarPorId(int idTipoUsuario)
        {
            return ctx.TipoUsuarios.FirstOrDefault(m => m.IdTipoUsuario == idTipoUsuario);
        }

        public void Cadastrar(TipoUsuario novoTipo)
        {
            ctx.TipoUsuarios.Add(novoTipo);
            ctx.SaveChanges();
        }

        public List<TipoUsuario> ListarTodos()
        {
            return ctx.TipoUsuarios.ToList();
        }
    }
}
