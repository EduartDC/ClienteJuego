using ClienteJuego.Views;
using System.Windows;

namespace ClienteJuego
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Contenedor.NavigationService.Navigate(new LoginView());

        }

    }
}
