using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using FilmesWPF.Data;
using FilmesWPF.Views;

namespace FilmesWPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Evita que o fechamento da LoginWindow desligue a aplicação
            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            try
            {
                using var db = new AppDbContext();
                db.Database.Migrate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao inicializar o banco de dados SQL Server:\n\n{ex.Message}\n\nVerifique se o serviço do SQL Server / LocalDB está em execução.",
                    "Erro de Inicialização do Banco",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            ExecutarSessao();
        }

        public static void ExecutarSessao()
        {
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var login = new LoginWindow();
            bool? sucesso = login.ShowDialog();

            if (sucesso == true && login.UsuarioAutenticado != null)
            {
                var main = new MainWindow(login.UsuarioAutenticado);
                Current.MainWindow = main;
                Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                main.Show();
            }
            else
            {
                Current.Shutdown();
            }
        }
    }
}
