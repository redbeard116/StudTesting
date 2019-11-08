using System.Windows;
using TestGenerator.View;

namespace TestGenerator
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MainWindow reg;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            reg = new MainWindow();
            reg.ShowDialog();
        }
    }
}
