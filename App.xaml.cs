using System.Windows;
using FilmesWPF.Models;
using FilmesWPF.Views;

namespace FilmesWPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ExecutarSessao();
        }

        public static void ExecutarSessao()
        {
            var login = new LoginWindow();
            if (login.ShowDialog() == true)
            {
                var main = new MainWindow(login.UsuarioAutenticado);
                main.Show();
            }
            else
            {
                Current?.Shutdown();
            }
        }
    }
}
