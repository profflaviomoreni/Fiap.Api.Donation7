using Fiap.Api.Donation7.Model;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.Donation7.Data
{
    public class DataContext : DbContext
    {

        public DbSet<CategoriaModel> Categorias { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Categoria
            modelBuilder.Entity<CategoriaModel>(entity => {

                entity.ToTable("Categorias");
                
                entity.HasKey( c => c.CategoriaId);
                entity.Property(c => c.CategoriaId).ValueGeneratedOnAdd();
                
                entity.Property(c => c.NomeCategoria)
                                .IsRequired()
                                .HasMaxLength(80);

                entity.Property(c => c.Descricao)
                                .HasMaxLength(100);

                entity.HasIndex(c => c.NomeCategoria)
                                .IsUnique();
            });

            modelBuilder.Entity<CategoriaModel>().HasData(
                new CategoriaModel
                {
                    CategoriaId = 1,
                    NomeCategoria = "Alimentos",
                    Descricao = "Doação de alimentos não perecíveis"
                },
                new CategoriaModel
                {
                    CategoriaId = 2,
                    NomeCategoria = "Roupas",
                    Descricao = "Doação de roupas em bom estado"
                }
            );

            #endregion



            base.OnModelCreating(modelBuilder);
        }



        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected DataContext()
        {
        }
    }
}
