using labware_webapi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labware_webapi.Interfaces
{
    public interface ITagRepository
    {
        List<Tag> Listar();
        void Cadastrar(Tag tag);
        void Atualizar(int id, Tag tag);
        void Deletar(int id);
        Tag BuscarPorId(int id);
    }
}
