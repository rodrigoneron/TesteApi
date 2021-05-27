using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRodrigoNeronFranca
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> context) : base(context)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }    
        public DbSet<Pedido> Pedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .Property(p => p.Valor)
                .HasPrecision(10, 2);
            modelBuilder.Entity<Pedido>()
               .Property(p => p.Desconto)
               .HasPrecision(10, 2);
            modelBuilder.Entity<Pedido>()
                .Property(p => p.Valor)
                .HasPrecision(10, 2);



            modelBuilder.Entity<Cliente>()
                .HasData(
                new Cliente { Id = 1, Nome = "Naruto", Email = "rodrigo@teste.com.br", Aldeia = "Fogo" },
                new Cliente { Id = 2, Nome = "Sakura", Email = "bruna@teste.com.br", Aldeia = "Fogo" }
                );

            modelBuilder.Entity<Produto>()
                .HasData(
                new Produto { Id = 1, Descricao = "Kunai", Valor = 7.90m, Foto = "https://img.elo7.com.br/product/zoom/1E00A54/kit-naruto-especial-2-kunais-porta-kunai-anime.jpg" },
                new Produto { Id = 2, Descricao = "Shuriken", Valor = 5.89m, Foto = "https://http2.mlstatic.com/D_NQ_NP_2X_979837-MLB26491141661_122017-F.webp" }
                );

        }
    }
}
