using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using FilmesWPF.Models;

namespace FilmesWPF.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = ObterConnectionString();
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        private static string ObterConnectionString()
        {
            try
            {
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var builder = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                var configuration = builder.Build();
                var conn = configuration.GetConnectionString("DefaultConnection");

                if (!string.IsNullOrWhiteSpace(conn))
                {
                    return conn;
                }
            }
            catch
            {
                // Em caso de falha na leitura do json, utiliza a string padrão de fallback
            }

            return "Server=(localdb)\\mssqllocaldb;Database=FilmesWPFDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Filme>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(120);
                entity.Property(e => e.Genero).IsRequired().HasMaxLength(60);
                entity.Property(e => e.Diretor).HasMaxLength(80);
                entity.Property(e => e.Nota).HasColumnType("float");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Senha).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Nome).HasMaxLength(100);
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
