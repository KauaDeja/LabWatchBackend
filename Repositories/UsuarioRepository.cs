using Experimental.System.Messaging;
using labware_webapi.Contexts;
using labware_webapi.Domains;
using labware_webapi.Interfaces;
using labware_webapi.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace labware_webapi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        LabWatchContext ctx = new LabWatchContext();

        private readonly IConfiguration configuration;
        public UserRepository(IDatabaseSetting DB, IConfiguration configuration)
        {
            this.configuration = configuration;
            var userClient = new (DB.ConnectionString);
            var Db = userClient.GetDatabase(DB.DatabaseName);
            User = Db.GetCollection<RegisterModel>("User");
        }

        public void AprovarRecusar(int idUsuario, bool ativo)
        {
            Usuario usuarioBuscado = ctx.Usuarios.FirstOrDefault(i => i.IdUsuario == idUsuario);

            if (ativo)
            {
                usuarioBuscado.Ativo = true;
            }
            else
            {
                usuarioBuscado.Ativo = false;
            }

            ctx.Update(usuarioBuscado);

            ctx.SaveChanges();
        }

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
                usuarioBuscado.FotoUsuario = usuarioAtualizado.FotoUsuario;
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

        //public void EsqueciMinhaSenha(string sennha)
        //{
        //    var esqueciSenha = ctx.Usuarios.
        //}

        public void AlterarSenha(string senha, int idUsuario)
        {
            Usuario userBuscado = ctx.Usuarios.FirstOrDefault(v => v.IdUsuario == idUsuario);


            if (userBuscado != null)
                userBuscado.Senha = senha;

            ctx.Usuarios.Update(userBuscado);

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

        public void AlterarTipoUsuario(int idUsuario, int IdTipoUsuario)
        {
            Usuario userBuscado = ctx.Usuarios.FirstOrDefault(v => v.IdUsuario == idUsuario);


            if (userBuscado != null)
                userBuscado.IdTipoUsuario = IdTipoUsuario;

            ctx.Usuarios.Update(userBuscado);

            ctx.SaveChanges();
        }

        public string GenerateToken(string email)
        {
            byte[] key = Encoding.UTF8.GetBytes(this.configuration["SecretKey"]);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                      { new Claim(ClaimTypes.Email, email) }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        public void SendMSMQ()
        {
            MessageQueue msgqueue;
            if (MessageQueue.Exists(@".\Private$\BookStore"))
            {
                msgqueue = new MessageQueue(@".\Private$\BookStore");
            }
            else
            {
                msgqueue = MessageQueue.Create(@".\Private$\BookStore");
            }

            msgqueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            string body = "This is Password reset link. Reset Link => ";
            msgqueue.Label = "Mail Body";
            msgqueue.Send(body);
        }


        public string ReceiveMSMQ()
        {
            MessageQueue msgqueue = new MessageQueue(@".\Private$\BookStore");
            var receivemessage = msgqueue.Receive();
            receivemessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            return receivemessage.Body.ToString();
        }

        public async Task<bool> Forget(string email)
        {
            try
            {
                var check = ctx.Usuarios.AsQueryable().Where(x => x.Email == email).FirstOrDefault();
                if (check != null)
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress(this.configuration["Credentials:Email"]);
                    mail.To.Add(email);
                    mail.Subject = "Reset Password for Lab";
                    this.SendMSMQ();
                    mail.Body = this.ReceiveMSMQ();

                    SmtpServer.Host = "smtp.gmail.com";
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(this.configuration["Credentials:Email"], this.configuration["Credentials:Password"]);
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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
    


