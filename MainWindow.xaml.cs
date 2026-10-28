using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using FilmesWPF.Models;
using FilmesWPF.Repositories;
using FilmesWPF.Views;

namespace FilmesWPF
{
    public partial class MainWindow : Window
    {
        private readonly IFilmeRepository _repo = new FilmeRepository();
        public Usuario UsuarioAtual { get; }

        public MainWindow(Usuario usuario = null)
        {
            InitializeComponent();
            UsuarioAtual = usuario;
            TxtUsuarioLogado.Text = usuario?.Nome ?? usuario?.Username ?? "admin";
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AtualizarMetricasIniciais();
        }

        private void AtualizarMetricasIniciais()
        {
            try
            {
                int total = _repo.ContarTotal();
                double media = _repo.NotaMedia();
                var todos = _repo.ObterTodos().ToList();

                TxtHeaderTotal.Text = $"{total} Filme(s)";
                TxtHeaderMedia.Text = $"★ {media:0.0} Média";
                TxtBadgeCatalogo.Text = $"{total} TÍTULO(S)";
                TxtBadgeEstatisticas.Text = $"★ {media:0.0} MÉDIA";

                if (todos.Any())
                {
                    var ultimo = todos.OrderByDescending(f => f.Id).First();
                    TxtUltimoFilme.Text = $"Último filme: \"{ultimo.Titulo}\" ({ultimo.Ano})";
                }
                else
                {
                    TxtUltimoFilme.Text = "Nenhum filme cadastrado";
                }
            }
            catch (Exception ex)
            {
                TxtUltimoFilme.Text = "Erro ao ler métricas: " + ex.Message;
            }
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            var confirma = MessageBox.Show("Deseja realmente sair da sessão atual?", "Encerrar Sessão", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirma == MessageBoxResult.Yes)
            {
                Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
                Close();
                App.ExecutarSessao();
            }
        }

        private void BtnCardCatalogo_Click(object sender, RoutedEventArgs e)
        {
            var win = new ListarFilmesWindow { Owner = this };
            win.ShowDialog();
            AtualizarMetricasIniciais();
        }

        private void BtnCardCadastrar_Click(object sender, RoutedEventArgs e)
        {
            var win = new CadastrarFilmeWindow { Owner = this };
            if (win.ShowDialog() == true)
            {
                AtualizarMetricasIniciais();
            }
        }

        private void BtnCardPesquisar_Click(object sender, RoutedEventArgs e)
        {
            var win = new PesquisarFilmeWindow { Owner = this };
            win.ShowDialog();
            AtualizarMetricasIniciais();
        }

        private void BtnCardEstatisticas_Click(object sender, RoutedEventArgs e)
        {
            var win = new EstatisticasWindow { Owner = this };
            win.ShowDialog();
            AtualizarMetricasIniciais();
        }
    }
}
