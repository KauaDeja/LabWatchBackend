using labware_webapi.Contexts;
using labware_webapi.Domains;
using labware_webapi.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace labware_webapi.Repositories
{
    public class TagRepository : ITagRepository
    {
        LabWatchContext ctx = new LabWatchContext();
        public void Atualizar(int id, Tag tag)
        {
            Tag tagBuscada = ctx.Tags.Find(id);

            if (tag.TituloTag != null)
            {
                tagBuscada.TituloTag = tag.TituloTag;
                ctx.Tags.Update(tagBuscada);
                ctx.SaveChanges();
            }
        }

        public Tag BuscarPorId(int id)
        {
            return ctx.Tags.FirstOrDefault(t => t.IdTag == id);
        }

        public void Cadastrar(Tag tag)
        {
            ctx.Tags.Add(tag);
            ctx.SaveChanges();
        }

        public void Deletar(int id)
        {
            ctx.Tags.Remove(BuscarPorId(id));
            ctx.SaveChanges();
        }

        public List<Tag> Listar()
        {
            return ctx.Tags.ToList();
        }
    }
}
