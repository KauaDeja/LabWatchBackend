using System;
using System.Collections.Generic;

#nullable disable

namespace labware_webapi.Domains
{
    public partial class UsuarioEquipe
    {
        public int IdusuarioEquipe { get; set; }
        public int? IdEquipe { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Equipe IdEquipeNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
