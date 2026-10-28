using System;
using System.Collections.Generic;
using System.Linq;
using FilmesWPF.Data;
using FilmesWPF.Models;

namespace FilmesWPF.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        public IEnumerable<Filme> ObterTodos()
        {
            using var db = new AppDbContext();
            return db.Filmes.OrderBy(f => f.Titulo).ToList();
        }

        public Filme ObterPorId(int id)
        {
            using var db = new AppDbContext();
            return db.Filmes.Find(id);
        }

        public IEnumerable<Filme> Pesquisar(string titulo, string genero, int? ano, double? notaMinima)
        {
            using var db = new AppDbContext();
            var query = db.Filmes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(titulo))
                query = query.Where(f => f.Titulo.ToLower().Contains(titulo.ToLower()));

            if (!string.IsNullOrWhiteSpace(genero))
                query = query.Where(f => f.Genero.ToLower().Contains(genero.ToLower()));

            if (ano.HasValue)
                query = query.Where(f => f.Ano == ano.Value);

            if (notaMinima.HasValue)
                query = query.Where(f => f.Nota >= notaMinima.Value);

            return query.OrderBy(f => f.Titulo).ToList();
        }

        public void Adicionar(Filme filme)
        {
            try
            {
                using var db = new AppDbContext();
                db.Filmes.Add(filme);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao cadastrar o filme: " + ex.Message, ex);
            }
        }

        public void Atualizar(Filme filme)
        {
            try
            {
                using var db = new AppDbContext();
                var existente = db.Filmes.Find(filme.Id);
                if (existente == null)
                    throw new InvalidOperationException("Filme não encontrado para atualização.");

                existente.Titulo = filme.Titulo;
                existente.Genero = filme.Genero;
                existente.Ano = filme.Ano;
                existente.Diretor = filme.Diretor;
                existente.DuracaoMin = filme.DuracaoMin;
                existente.Nota = filme.Nota;

                db.SaveChanges();
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao atualizar o filme: " + ex.Message, ex);
            }
        }

        public void Remover(int id)
        {
            try
            {
                using var db = new AppDbContext();
                var existente = db.Filmes.Find(id);
                if (existente == null)
                    throw new InvalidOperationException("Filme não encontrado para exclusão.");

                db.Filmes.Remove(existente);
                db.SaveChanges();
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao excluir o filme: " + ex.Message, ex);
            }
        }

        public int ContarTotal()
        {
            using var db = new AppDbContext();
            return db.Filmes.Count();
        }

        public int ContarPorGenero(string genero)
        {
            using var db = new AppDbContext();
            return db.Filmes.Count(f => f.Genero.ToLower() == genero.ToLower());
        }

        public double NotaMedia()
        {
            using var db = new AppDbContext();
            return db.Filmes.Any() ? db.Filmes.Average(f => f.Nota) : 0;
        }

        public IEnumerable<string> ListarGeneros()
        {
            using var db = new AppDbContext();
            return db.Filmes.Select(f => f.Genero).Distinct().OrderBy(g => g).ToList();
        }
    }
}
