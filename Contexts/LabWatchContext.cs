using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using labware_webapi.Domains;

#nullable disable

namespace labware_webapi.Contexts
{
    public partial class LabWatchContext : DbContext
    {
        public LabWatchContext()
        {
        }

        public LabWatchContext(DbContextOptions<LabWatchContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Comentario> Comentarios { get; set; }
        public virtual DbSet<Equipe> Equipes { get; set; }
        public virtual DbSet<Projeto> Projetos { get; set; }
        public virtual DbSet<StatusProjeto> StatusProjetos { get; set; }
        public virtual DbSet<StatusTask> StatusTasks { get; set; }
        public virtual DbSet<StatusUsuario> StatusUsuarios { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<UsuarioEquipe> UsuarioEquipes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=srv-db-labwatch.database.windows.net; initial catalog=DBLabWatch; user Id=labuser; pwd=Labwatch132;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__Cliente__885457EE092CDEC8");

                entity.ToTable("Cliente");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.DataCadastro)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dataCadastro");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("descricao");

                entity.Property(e => e.FotoCliente)
                    .IsUnicode(false)
                    .HasColumnName("fotoCliente");

                entity.Property(e => e.NomeCliente)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nomeCliente");
            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.HasKey(e => e.IdComentario)
                    .HasName("PK__Comentar__C74515DAFEA8EEEC");

                entity.ToTable("Comentario");

                entity.Property(e => e.IdComentario).HasColumnName("idComentario");

                entity.Property(e => e.Comentario1)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("comentario");

                entity.Property(e => e.IdTask).HasColumnName("idTask");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.HasOne(d => d.IdTaskNavigation)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.IdTask)
                    .HasConstraintName("FK__Comentari__idTas__6CD828CA");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Comentari__idUsu__6DCC4D03");
            });

            modelBuilder.Entity<Equipe>(entity =>
            {
                entity.HasKey(e => e.IdEquipe)
                    .HasName("PK__Equipe__981ACF451ECFD781");

                entity.ToTable("Equipe");

                entity.Property(e => e.IdEquipe).HasColumnName("idEquipe");

                entity.Property(e => e.HorasTrabalhadas)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("horasTrabalhadas");

                entity.Property(e => e.NomeEquipe)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nomeEquipe");
            });

            modelBuilder.Entity<Projeto>(entity =>
            {
                entity.HasKey(e => e.IdProjeto)
                    .HasName("PK__Projeto__8FCCED763678D67C");

                entity.ToTable("Projeto");

                entity.Property(e => e.IdProjeto).HasColumnName("idProjeto");

                entity.Property(e => e.DataConclusao)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dataConclusao");

                entity.Property(e => e.DataInicio)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dataInicio");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("descricao");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.IdEquipe).HasColumnName("idEquipe");

                entity.Property(e => e.IdStatusProjeto).HasColumnName("idStatusProjeto");

                entity.Property(e => e.TituloProjeto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tituloProjeto");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Projetos)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__Projeto__idClien__367C1819");

                entity.HasOne(d => d.IdEquipeNavigation)
                    .WithMany(p => p.Projetos)
                    .HasForeignKey(d => d.IdEquipe)
                    .HasConstraintName("FK__Projeto__idEquip__3587F3E0");

                entity.HasOne(d => d.IdStatusProjetoNavigation)
                    .WithMany(p => p.Projetos)
                    .HasForeignKey(d => d.IdStatusProjeto)
                    .HasConstraintName("FK__Projeto__idStatu__3493CFA7");
            });

            modelBuilder.Entity<StatusProjeto>(entity =>
            {
                entity.HasKey(e => e.IdStatusProjeto)
                    .HasName("PK__StatusPr__F803DD2A4F5A0A7E");

                entity.ToTable("StatusProjeto");

                entity.Property(e => e.IdStatusProjeto).HasColumnName("idStatusProjeto");

                entity.Property(e => e.StatusProjeto1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("statusProjeto");
            });

            modelBuilder.Entity<StatusTask>(entity =>
            {
                entity.HasKey(e => e.IdStatusTask)
                    .HasName("PK__StatusTa__8E7D8B7A42F17D65");

                entity.ToTable("StatusTask");

                entity.Property(e => e.IdStatusTask).HasColumnName("idStatusTask");

                entity.Property(e => e.StatusTask1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("statusTask");
            });

            modelBuilder.Entity<StatusUsuario>(entity =>
            {
                entity.HasKey(e => e.IdStatus)
                    .HasName("PK__StatusUs__01936F74449B714F");

                entity.ToTable("StatusUsuario");

                entity.Property(e => e.IdStatus).HasColumnName("idStatus");

                entity.Property(e => e.StatusUsuario1)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("statusUsuario");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.IdTag)
                    .HasName("PK__Tag__020FEDB89D8E21DE");

                entity.ToTable("Tag");

                entity.Property(e => e.IdTag).HasColumnName("idTag");

                entity.Property(e => e.TituloTag)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tituloTag");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(e => e.IdTask)
                    .HasName("PK__Task__C3E0F4DA4D5E4CF9");

                entity.ToTable("Task");

                entity.Property(e => e.IdTask).HasColumnName("idTask");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("descricao");

                entity.Property(e => e.IdProjeto).HasColumnName("idProjeto");

                entity.Property(e => e.IdStatusTask).HasColumnName("idStatusTask");

                entity.Property(e => e.IdTag).HasColumnName("idTag");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.TempoTrabalho)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("tempoTrabalho");

                entity.Property(e => e.TituloTask)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tituloTask");

                entity.HasOne(d => d.IdProjetoNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.IdProjeto)
                    .HasConstraintName("FK__Task__idProjeto__671F4F74");

                entity.HasOne(d => d.IdStatusTaskNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.IdStatusTask)
                    .HasConstraintName("FK__Task__idStatusTa__690797E6");

                entity.HasOne(d => d.IdTagNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.IdTag)
                    .HasConstraintName("FK__Task__idTag__681373AD");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Task__idUsuario__69FBBC1F");
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasKey(e => e.IdTipoUsuario)
                    .HasName("PK__TipoUsua__03006BFFC8AC4693");

                entity.ToTable("TipoUsuario");

                entity.Property(e => e.IdTipoUsuario).HasColumnName("idTipoUsuario");

                entity.Property(e => e.TituloTipoUsuario)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tituloTipoUsuario");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__645723A635400420");

                entity.ToTable("Usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.CargaHoraria)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("cargaHoraria");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FotoUsuario)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("fotoUsuario");

                entity.Property(e => e.HorasTrabalhadas)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("horasTrabalhadas");

                entity.Property(e => e.IdStatus).HasColumnName("idStatus");

                entity.Property(e => e.IdTipoUsuario).HasColumnName("idTipoUsuario");

                entity.Property(e => e.NomeUsuario)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nomeUsuario");

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("senha");

                entity.Property(e => e.SobreNome)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("sobreNome");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdStatus)
                    .HasConstraintName("FK__Usuario__idStatu__607251E5");

                entity.HasOne(d => d.IdTipoUsuarioNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdTipoUsuario)
                    .HasConstraintName("FK__Usuario__idTipoU__5F7E2DAC");
            });

            modelBuilder.Entity<UsuarioEquipe>(entity =>
            {
                entity.HasKey(e => e.IdusuarioEquipe)
                    .HasName("PK__usuarioE__1B89B66269D82CE8");

                entity.ToTable("usuarioEquipe");

                entity.Property(e => e.IdusuarioEquipe).HasColumnName("idusuarioEquipe");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.HasOne(d => d.IdEquipeNavigation)
                    .WithMany(p => p.UsuarioEquipes)
                    .HasForeignKey(d => d.IdEquipe)
                    .HasConstraintName("FK__usuarioEq__IdEqu__634EBE90");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioEquipes)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__usuarioEq__idUsu__6442E2C9");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
