using System.Collections.Generic;
using FilmesWPF.Models;

namespace FilmesWPF.Repositories
{
    public interface IFilmeRepository
    {
        IEnumerable<Filme> ObterTodos();
        Filme ObterPorId(int id);
        IEnumerable<Filme> Pesquisar(string titulo, string genero, int? ano, double? notaMinima);
        void Adicionar(Filme filme);
        void Atualizar(Filme filme);
        void Remover(int id);
        int ContarTotal();
        int ContarPorGenero(string genero);
        double NotaMedia();
        IEnumerable<string> ListarGeneros();
    }
}
