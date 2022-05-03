using System;
using System.Collections.Generic;

#nullable disable

namespace labware_webapi.Domains
{
    public partial class Usuario
    {
        public Usuario()
        {
            Comentarios = new HashSet<Comentario>();
            Tasks = new HashSet<Task>();
            UsuarioEquipes = new HashSet<UsuarioEquipe>();
        }

        public int IdUsuario { get; set; }
        public int? IdTipoUsuario { get; set; }
        public int? IdStatus { get; set; }
        public string NomeUsuario { get; set; }
        public string SobreNome { get; set; }
        public decimal CargaHoraria { get; set; }
        public decimal HorasTrabalhadas { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string FotoUsuario { get; set; }
        public bool? Ativo { get; set; }

        public virtual StatusUsuario IdStatusNavigation { get; set; }
        public virtual TipoUsuario IdTipoUsuarioNavigation { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<UsuarioEquipe> UsuarioEquipes { get; set; }
    }
}
