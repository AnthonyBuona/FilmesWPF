using System.Linq;
using System.Windows;
using FilmesWPF.Repositories;

namespace FilmesWPF.Views
{
    public partial class EstatisticasWindow : Window
    {
        private readonly IFilmeRepository _repo = new FilmeRepository();

        public EstatisticasWindow()
        {
            InitializeComponent();
            Carregar();
        }

        private void Carregar()
        {
            TxtTotal.Text = _repo.ContarTotal().ToString();
            TxtMedia.Text = _repo.NotaMedia().ToString("0.0");

            var generos = _repo.ListarGeneros().ToList();
            TxtGeneros.Text = generos.Count.ToString();

            LstGeneros.ItemsSource = generos.Select(g => $"{g} ({_repo.ContarPorGenero(g)} filmes)").ToList();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
