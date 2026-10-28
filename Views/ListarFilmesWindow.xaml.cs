using System.Linq;
using System.Windows;
using FilmesWPF.Models;
using FilmesWPF.Repositories;

namespace FilmesWPF.Views
{
    public partial class ListarFilmesWindow : Window
    {
        private readonly IFilmeRepository _repo = new FilmeRepository();

        public ListarFilmesWindow()
        {
            InitializeComponent();
            Carregar();
        }

        private void Carregar()
        {
            var lista = _repo.ObterTodos().ToList();
            DgFilmes.ItemsSource = lista;
            TxtTotal.Text = $"Total: {lista.Count} filmes cadastrados";
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (DgFilmes.SelectedItem is not Filme selecionado)
            {
                MessageBox.Show("Selecione um filme na lista para editar.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var form = new CadastrarFilmeWindow(selecionado) { Owner = this };
            if (form.ShowDialog() == true)
                Carregar();
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (DgFilmes.SelectedItem is not Filme selecionado)
            {
                MessageBox.Show("Selecione um filme na lista para excluir.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var confirmar = MessageBox.Show(
                $"Deseja realmente excluir o filme '{selecionado.Titulo}'?",
                "Confirmar Exclusão",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (confirmar != MessageBoxResult.Yes)
                return;

            try
            {
                _repo.Remover(selecionado.Id);
                Carregar();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
