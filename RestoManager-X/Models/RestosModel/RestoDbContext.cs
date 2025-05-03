using Microsoft.EntityFrameworkCore;
using RestoManager_X.Models.RestosModel;

namespace RestoManager_X.Models.RestosModel
{
    public class RestosDbContext : DbContext
    {
        public RestosDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; } = null!;
        public DbSet<Proprietaire> Proprietaires { get; set; } = null!;
        public DbSet<Avis> Avis { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Proprietaire configuration
            modelBuilder.Entity<Proprietaire>(entity =>
            {
                entity.ToTable("TProprietaire", "resto");

                entity.HasKey(p => p.Numéro);

                entity.Property(p => p.Nom)
                      .HasColumnName("NomProp")
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(p => p.Email)
                      .HasColumnName("EmailProp")
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(p => p.Gsm)
                      .HasColumnName("GsmProp")
                      .HasMaxLength(8)
                      .IsRequired();

                entity.HasMany(p => p.LesRestos)
                      .WithOne(r => r.Leroprietaire)
                      .HasForeignKey(r => r.Numprop)
                      .HasConstraintName("Relation_Proprio_Restos")
                      .IsRequired();
            });

            // Restaurant configuration
            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("TRestaurant", "resto");

                entity.HasKey(r => r.CodeResto);

                entity.Property(r => r.NomResto)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(r => r.Specialite)
                      .HasColumnName("SpecResto")
                      .HasMaxLength(20)
                      .HasDefaultValue("Tunisienne")
                      .IsRequired();

                entity.Property(r => r.Ville)
                      .HasColumnName("VilleResto")
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(r => r.Tel)
                      .HasColumnName("TelResto")
                      .HasMaxLength(8)
                      .IsRequired();

                entity.HasMany(r => r.LesAvis)
                      .WithOne(a => a.LeResto)
                      .HasForeignKey(a => a.NumResto)
                      .HasConstraintName("Relation_Resto_Avis")
                      .IsRequired();
            });

            // Avis configuration
            modelBuilder.Entity<Avis>(entity =>
            {
                entity.ToTable("TAvis", "resto");

                entity.HasKey(a => a.CodeAvis);

                entity.Property(a => a.NomPersonne)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(a => a.Note)
                      .IsRequired();

                entity.Property(a => a.Commentaire)
                      .HasMaxLength(500);

                entity.HasCheckConstraint("CK_Avis_Note", "[Note] BETWEEN 1 AND 5");
            });
        }
    }
}
