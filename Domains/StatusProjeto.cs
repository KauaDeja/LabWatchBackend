using System;
using System.Collections.Generic;

#nullable disable

namespace labware_webapi.Domains
{
    public partial class StatusProjeto
    {
        public StatusProjeto()
        {
            Projetos = new HashSet<Projeto>();
        }

        public int IdStatusProjeto { get; set; }
        public string StatusProjeto1 { get; set; }

        public virtual ICollection<Projeto> Projetos { get; set; }
    }
}
