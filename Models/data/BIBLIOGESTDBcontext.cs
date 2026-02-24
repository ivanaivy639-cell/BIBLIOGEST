using Microsoft.EntityFrameworkCore;
using GestionProduits.Models;

namespace GestionProduits.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nom).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Auteur).IsRequired().HasMaxLength(150);
                entity.Property(e => e.ISBN).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Prix).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Categorie).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Fournisseur).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.HasIndex(e => e.Nom);
                entity.HasIndex(e => e.Auteur);
                entity.HasIndex(e => e.ISBN).IsUnique();
                entity.HasIndex(e => e.Categorie);
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.ToTable("Stock", t => t.ExcludeFromMigrations());
                entity.HasKey(e => e.Id);

                entity.Property(e => e.ProduitsId).HasColumnName("produitsId");
                entity.Property(e => e.Quantite).IsRequired();
                entity.Property(e => e.SeuilAlerte).IsRequired();
                entity.Property(e => e.DerniereMiseAJour).IsRequired();

                entity.HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey(e => e.ProduitsId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.ProduitsId).IsUnique();
            });

        }
    }
}
