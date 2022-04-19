using System;
using System.Collections.Generic;

#nullable disable

namespace labware_webapi.Domains
{
    public partial class Cliente
    {
        public Cliente()
        {
            Projetos = new HashSet<Projeto>();
        }

        public int IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public string Descricao { get; set; }
        public string FotoCliente { get; set; }
        public DateTime? DataCadastro { get; set; }

        public virtual ICollection<Projeto> Projetos { get; set; }
    }
}
