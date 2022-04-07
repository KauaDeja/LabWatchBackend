using System;
using System.Collections.Generic;

#nullable disable

namespace labware_webapi.Domains
{
    public partial class StatusUsuario
    {
        public StatusUsuario()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdStatus { get; set; }
        public string StatusUsuario1 { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
