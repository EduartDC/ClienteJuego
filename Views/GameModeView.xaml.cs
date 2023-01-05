using ClienteJuego.Properties;
using Elasticsearch.Net;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ClienteJuego.Views
{
    /// <summary>
    /// Interaction logic for GameModeView.xaml
    /// </summary>
    public partial class GameModeView : Page
    {
        public GameModeView()
        {
            InitializeComponent();
        }

        private void btnMultiplayerMode_Click(object sender, RoutedEventArgs e)
        {
            var codeInvitation = "inv" + Accessories.GenerateRandomCode();
            try
            {
                var window = (MainWindow)Application.Current.MainWindow;
                window.Contenedor.Navigate(new LobbyView(codeInvitation));
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Error de conexion con el servidor, Intentelo mas tarde");
                var window = (MainWindow)Application.Current.MainWindow;
                window.Contenedor.Navigate(new LoginView());
            }

        }

        private void btnSingleMode_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            NavigationService.Navigate(new Uri("Views/TableroView.xaml", UriKind.Relative));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Accessories.PlaySoundsEffects();
                var window = (MainWindow)Application.Current.MainWindow;
                window.Contenedor.Navigate(new InicioView());
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Error de conexion con el servidor, Intentelo mas tarde");
                var window = (MainWindow)Application.Current.MainWindow;
                window.Contenedor.Navigate(new LoginView());
            }

        }
    }
}
