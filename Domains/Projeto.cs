using System;
using System.Collections.Generic;

#nullable disable

namespace labware_webapi.Domains
{
    public partial class Projeto
    {
        public Projeto()
        {
            Tasks = new HashSet<Task>();
        }

        public int IdProjeto { get; set; }
        public int? IdEquipe { get; set; }
        public int? IdStatusProjeto { get; set; }
        public string TituloProjeto { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataConclusao { get; set; }
        public string nomeCliente { get; set; }
        public string fotoCliente { get; set; }

        public string Descricao { get; set; }

        public virtual Equipe IdEquipeNavigation { get; set; }
        public virtual StatusProjeto IdStatusProjetoNavigation { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
