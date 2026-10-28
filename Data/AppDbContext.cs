using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using FilmesWPF.Models;

namespace FilmesWPF.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "filmes.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Filme>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(120);
                entity.Property(e => e.Genero).IsRequired().HasMaxLength(60);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Senha).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, Username = "admin", Senha = "123", Nome = "Administrador" }
            );

            modelBuilder.Entity<Filme>().HasData(
                new Filme { Id = 1, Titulo = "O Poderoso Chefão", Genero = "Drama", Ano = 1972, Diretor = "Francis Ford Coppola", DuracaoMin = 175, Nota = 9.2 },
                new Filme { Id = 2, Titulo = "Interestelar", Genero = "Ficção Científica", Ano = 2014, Diretor = "Christopher Nolan", DuracaoMin = 169, Nota = 8.7 },
                new Filme { Id = 3, Titulo = "Matrix", Genero = "Ação", Ano = 1999, Diretor = "Lana e Lilly Wachowski", DuracaoMin = 136, Nota = 8.7 },
                new Filme { Id = 4, Titulo = "O Senhor dos Anéis: A Sociedade do Anel", Genero = "Fantasia", Ano = 2001, Diretor = "Peter Jackson", DuracaoMin = 178, Nota = 8.9 },
                new Filme { Id = 5, Titulo = "Cidade de Deus", Genero = "Drama", Ano = 2002, Diretor = "Fernando Meirelles", DuracaoMin = 130, Nota = 8.6 }
            );
        }
    }
}
