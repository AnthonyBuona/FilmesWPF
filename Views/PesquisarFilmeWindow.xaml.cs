using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using FilmesWPF.Repositories;

namespace FilmesWPF.Views
{
    public partial class PesquisarFilmeWindow : Window
    {
        private readonly IFilmeRepository _repo = new FilmeRepository();

        public PesquisarFilmeWindow()
        {
            InitializeComponent();
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            int? ano = null;
            if (int.TryParse(TxtAno.Text, out int a))
                ano = a;

            double? notaMin = null;
            if (double.TryParse(TxtNotaMin.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double n))
                notaMin = n;

            var resultados = _repo.Pesquisar(TxtTitulo.Text, TxtGenero.Text, ano, notaMin).ToList();
            DgFilmes.ItemsSource = resultados;
            TxtResultado.Text = $"{resultados.Count} filme(s) encontrado(s)";
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
