using System;
using System.Globalization;
using System.Windows;
using FilmesWPF.Models;
using FilmesWPF.Repositories;

namespace FilmesWPF.Views
{
    public partial class CadastrarFilmeWindow : Window
    {
        private readonly IFilmeRepository _repo = new FilmeRepository();
        private readonly Filme _filmeExistente;

        public CadastrarFilmeWindow()
        {
            InitializeComponent();
        }

        public CadastrarFilmeWindow(Filme filme) : this()
        {
            _filmeExistente = filme;
            TxtHeader.Text = "Editar Filme";
            Title = "Editar Filme";

            TxtTitulo.Text = filme.Titulo;
            TxtGenero.Text = filme.Genero;
            TxtAno.Text = filme.Ano.ToString();
            TxtDiretor.Text = filme.Diretor;
            TxtDuracao.Text = filme.DuracaoMin.ToString();
            TxtNota.Text = filme.Nota.ToString(CultureInfo.InvariantCulture);
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtTitulo.Text))
            {
                MessageBox.Show("Informe o título do filme.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(TxtAno.Text, out int ano))
            {
                MessageBox.Show("Ano inválido.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(TxtDuracao.Text, out int duracao))
            {
                MessageBox.Show("Duração inválida.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(TxtNota.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double nota))
            {
                MessageBox.Show("Nota inválida.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var filme = new Filme
                {
                    Id = _filmeExistente?.Id ?? 0,
                    Titulo = TxtTitulo.Text.Trim(),
                    Genero = TxtGenero.Text.Trim(),
                    Ano = ano,
                    Diretor = TxtDiretor.Text.Trim(),
                    DuracaoMin = duracao,
                    Nota = nota
                };

                if (_filmeExistente == null)
                    _repo.Adicionar(filme);
                else
                    _repo.Atualizar(filme);

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
