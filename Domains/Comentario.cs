using System;
using System.Collections.Generic;

#nullable disable

namespace labware_webapi.Domains
{
    public partial class Comentario
    {
        public int IdComentario { get; set; }
        public int? IdTask { get; set; }
        public int? IdUsuario { get; set; }
        public string Comentario1 { get; set; }

        public virtual Task IdTaskNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
