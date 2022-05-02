using labware_webapi.Contexts;
using labware_webapi.Domains;
using labware_webapi.Interfaces;
using labware_webapi.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace labware_webapi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        LabWatchContext ctx = new LabWatchContext();

    /*    public string AtualizarFotoDir(int id_usuario)
        {
            string nome_arquivo = id_usuario.ToString() + ".png";

            string caminho = Path.Combine("perfil", nome_arquivo);

            if (File.Exists(caminho))
            {
                byte[] bytes_arquivo = File.ReadAllBytes(caminho);
                return Convert.ToBase64String(bytes_arquivo);
            }

            return null;
        }*/

        public void AtualizarPeloId(int idUsuario, Usuario usuarioAtualizado)
        {
            Usuario usuarioBuscado = ctx.Usuarios.Find(idUsuario);

            if (usuarioAtualizado.NomeUsuario != null)
            {
                usuarioBuscado.IdTipoUsuario = usuarioAtualizado.IdTipoUsuario;
                usuarioBuscado.IdStatus = usuarioAtualizado.IdStatus;
                usuarioBuscado.NomeUsuario = usuarioAtualizado.NomeUsuario;
                usuarioBuscado.SobreNome = usuarioAtualizado.SobreNome;
                usuarioBuscado.CargaHoraria = usuarioAtualizado.CargaHoraria;
                usuarioBuscado.HorasTrabalhadas = usuarioAtualizado.HorasTrabalhadas;
                usuarioBuscado.Email = usuarioAtualizado.Email;
                ctx.Usuarios.Update(usuarioBuscado);
                ctx.SaveChanges();
            }
        }

        public Usuario Buscar(int idUsuario)
        {
            return ctx.Usuarios.FirstOrDefault(m => m.IdUsuario == idUsuario);
        }

        public Usuario BuscarPorEmail(string email)
        {
            return ctx.Usuarios.FirstOrDefault(m => m.Email == email);
        }


        public void Cadastrar(Usuario novoUsuario)
        {
            ctx.Usuarios.Add(novoUsuario);
            ctx.SaveChanges();
        }

        public void Deletar(int idUsuario)
        {
            ctx.Usuarios.Remove(Buscar(idUsuario));
            ctx.SaveChanges();
        }

        public List<Usuario> ListarTodos()
        {
            return ctx.Usuarios.ToList();
        }

        public Usuario Login(string email, string senha)
        {

            var usuario = ctx.Usuarios.FirstOrDefault(u => u.Email == email);

            if (usuario.Senha[0] != '$' && usuario.Senha.Length < 32)
            {
                usuario.Senha = Criptografia.GerarHash(usuario.Senha);
                ctx.SaveChanges();
            }

            if (usuario != null)
            {
                bool confere = Criptografia.Comparar(senha, usuario.Senha);
                if (confere) return usuario;
            }

            return null;
        }

     /*   public void SalvarFotoDir(IFormFile foto, int id_usuario)
        {
            string nome_arquivo = id_usuario.ToString() + ".png ";

            using (var stream = new FileStream(Path.Combine("perfil", nome_arquivo), FileMode.Create))
            {
                foto.CopyTo(stream);
            }
        }*/

    }

}

