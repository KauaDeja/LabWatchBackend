using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


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

        [Required(ErrorMessage = "Por favor, informe a senha")]
        [StringLength(256, MinimumLength = 8, ErrorMessage = "O campo senha precisa ter no mínimo 8 caracteres")]
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
