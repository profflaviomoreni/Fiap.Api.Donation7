using Fiap.Api.Donation7.Model;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.Donation7.Data
{
    public class DataContext : DbContext
    {

        public DbSet<CategoriaModel> Categorias { get; set; }

        public DbSet<UsuarioModel> Usuarios { get; set; }

        public DbSet<ProdutoModel> Produtos { get; set; }

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


            #region Usuario
            modelBuilder.Entity<UsuarioModel>(entity =>
            {
                entity.ToTable("Usuario");
                entity.HasKey(e => e.UsuarioId);

                entity.Property(e => e.UsuarioId).ValueGeneratedOnAdd();

                entity.Property(e => e.NomeUsuario)
                            .IsRequired()
                            .HasMaxLength(100);

                entity.Property(e => e.EmailUsuario)
                            .IsRequired()
                            .HasMaxLength(120);

                entity.Property(e => e.Senha)
                            .IsRequired()
                            .HasMaxLength(24);

                entity.Property(e => e.Regra)
                            .IsRequired()
                            .HasMaxLength(50);

                entity.HasIndex(e => e.EmailUsuario).IsUnique();

                entity.HasIndex(e => new {
                    e.EmailUsuario,
                    e.Senha
                });
            });


            modelBuilder.Entity<UsuarioModel>().HasData(
                new UsuarioModel(1, "admin@admin", "Admin", "123456", "admin"),
                new UsuarioModel(2, "fmoreni@gmail.com", "Flavio Moreni", "123456", "admin")
            );

            #endregion


            #region Produto
            modelBuilder.Entity<ProdutoModel>(entity =>
            {
                entity.ToTable("Produto");
                entity.HasKey(e => e.ProdutoId);
                entity.Property(e => e.ProdutoId).ValueGeneratedOnAdd();

                entity.Property(e => e.Nome)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.SugestaoTroca)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.Disponivel);

                entity.Property(e => e.Valor)
                      .IsRequired()
                      .HasPrecision(18, 2);

                entity.Property(e => e.DataCadastro)
                      .IsRequired();

                entity.Property(e => e.DataExpiracao)
                      .IsRequired();


                // relacionamento categoria
                entity.HasOne( e => e.Categoria )
                        .WithMany()
                        .HasForeignKey(e => e.CategoriaId)
                        .IsRequired();

                // relacionamento usuario
                entity.HasOne(e => e.Usuario)
                        .WithMany()
                        .HasForeignKey(e => e.UsuarioId)
                        .IsRequired();

                // indice
                entity.HasIndex(e => e.Nome).IsUnique();
            });


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
