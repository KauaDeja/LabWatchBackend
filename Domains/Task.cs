using System;
using System.Collections.Generic;

#nullable disable

namespace labware_webapi.Domains
{
    public partial class Task
    {
        public Task()
        {
            Comentarios = new HashSet<Comentario>();
        }

        public int IdTask { get; set; }
        public int? IdProjeto { get; set; }
        public int? IdTag { get; set; }
        public int? IdStatusTask { get; set; }
        public int? IdUsuario { get; set; }
        public string TituloTask { get; set; }
        public string Descricao { get; set; }
        public decimal TempoTrabalho { get; set; }

        public virtual Projeto IdProjetoNavigation { get; set; }
        public virtual StatusTask IdStatusTaskNavigation { get; set; }
        public virtual Tag IdTagNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
    }
}
