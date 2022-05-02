using System;
using System.Collections.Generic;

#nullable disable

namespace labware_webapi.Domains
{
    public partial class Equipe
    {
        public Equipe()
        {
            Projetos = new HashSet<Projeto>();
            UsuarioEquipes = new HashSet<UsuarioEquipe>();
        }

        public int IdEquipe { get; set; }
        public string NomeEquipe { get; set; }
        public decimal HorasTrabalhadas { get; set; }

        public virtual ICollection<Projeto> Projetos { get; set; }
        public virtual ICollection<UsuarioEquipe> UsuarioEquipes { get; set; }
    }
}
