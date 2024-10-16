using Microsoft.EntityFrameworkCore;

namespace personapi_dotnet.Models.Entities
{
    public partial class PersonaDbContext : DbContext
    {
        public PersonaDbContext(DbContextOptions<PersonaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Estudio> Estudios { get; set; }
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Profesion> Profesions { get; set; }
        public virtual DbSet<Telefono> Telefonos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Si no se ha configurado la cadena de conexión, puedes agregarla aquí para debugging
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=db;Database=persona_db;User Id=nuevo_usuario;Password=TuContraseñaFuerte;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estudio>(entity =>
            {
                entity.HasKey(e => new { e.IdProf, e.CcPer }).HasName("PK__estudios__FB3F71A61B7904EE");
                entity.ToTable("estudios");

                entity.Property(e => e.IdProf).HasColumnName("id_prof");
                entity.Property(e => e.CcPer).HasColumnName("cc_per");
                entity.Property(e => e.Fecha).HasColumnName("fecha");
                entity.Property(e => e.Univer)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("univer");

                entity.HasOne(d => d.CcPerNavigation).WithMany(p => p.Estudios)
                    .HasForeignKey(d => d.CcPer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("estudio_persona_fk");

                entity.HasOne(d => d.IdProfNavigation).WithMany(p => p.Estudios)
                    .HasForeignKey(d => d.IdProf)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("estudio_prof_fk");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.Cc).HasName("PK__persona__3213666DF030B278");
                entity.ToTable("persona");

                entity.Property(e => e.Cc)
                    .ValueGeneratedNever()
                    .HasColumnName("cc");
                entity.Property(e => e.Apellido)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("apellido");
                entity.Property(e => e.Edad).HasColumnName("edad");
                entity.Property(e => e.Genero)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("genero");
                entity.Property(e => e.Nombre)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Profesion>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__profesio__3213E83F935FD897");
                entity.ToTable("profesion");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");
                entity.Property(e => e.Des)
                    .HasColumnType("text")
                    .HasColumnName("des");
                entity.Property(e => e.Nom)
                    .HasMaxLength(90)
                    .IsUnicode(false)
                    .HasColumnName("nom");
            });

            modelBuilder.Entity<Telefono>(entity =>
            {
                entity.HasKey(e => e.Num).HasName("PK__telefono__DF908D65E7B276AF");
                entity.ToTable("telefono");

                entity.Property(e => e.Num)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("num");
                entity.Property(e => e.Dueno).HasColumnName("duenio");
                entity.Property(e => e.Oper)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("oper");

                // Configurar la relación con Persona
                entity.HasOne<Persona>()
                    .WithMany(p => p.Telefonos)
                    .HasForeignKey(t => t.Dueno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("telefono_persona_fk");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
