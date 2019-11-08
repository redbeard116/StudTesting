using System.Windows;
using TestGenerator.Interface;
using TestGenerator.ViewModel;

namespace TestGenerator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainVM(new Repo());
        }
    }
}
