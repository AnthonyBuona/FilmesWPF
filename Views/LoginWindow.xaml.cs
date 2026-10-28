using System;
using System.Linq;
using System.Windows;
using FilmesWPF.Data;
using FilmesWPF.Models;

namespace FilmesWPF.Views
{
    public partial class LoginWindow : Window
    {
        public Usuario UsuarioAutenticado { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();
            TxtUsuario.Focus();
        }

        private void BtnEntrar_Click(object sender, RoutedEventArgs e)
        {
            string usuario = TxtUsuario.Text?.Trim() ?? "";
            string senha = TxtSenha.Password?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(usuario))
            {
                ExibirErro("Por favor, digite o nome de usuário.");
                TxtUsuario.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(senha))
            {
                ExibirErro("Por favor, digite a sua senha.");
                TxtSenha.Focus();
                return;
            }

            try
            {
                using var db = new AppDbContext();
                var userDb = db.Usuarios.FirstOrDefault(u => u.Username.ToLower() == usuario.ToLower() && u.Senha == senha);

                // Autenticação com banco ou fallback padrão
                if (userDb != null || (usuario.ToLower() == "admin" && (senha == "123" || senha == "admin")))
                {
                    UsuarioAutenticado = userDb ?? new Usuario
                    {
                        Id = 1,
                        Username = "admin",
                        Nome = "Administrador"
                    };

                    DialogResult = true;
                    Close();
                    return;
                }

                ExibirErro("Usuário ou senha incorretos.");
            }
            catch (Exception ex)
            {
                // Fallback em caso de falha de conexão inicial
                if (usuario.ToLower() == "admin" && (senha == "123" || senha == "admin"))
                {
                    UsuarioAutenticado = new Usuario { Id = 1, Username = "admin", Nome = "Administrador" };
                    DialogResult = true;
                    Close();
                    return;
                }

                ExibirErro("Erro ao autenticar: " + ex.Message);
            }
        }

        private void ExibirErro(string mensagem)
        {
            TxtErro.Text = mensagem;
            BorderErro.Visibility = Visibility.Visible;
        }

        private void BtnSair_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
