using ClienteJuego.Properties;
using System;
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
            var window = (MainWindow)Application.Current.MainWindow;
            window.Contenedor.Navigate(new LobbyView(codeInvitation));
        }

        private void btnSingleMode_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            NavigationService.Navigate(new Uri("Views/TableroView.xaml", UriKind.Relative));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            NavigationService.Navigate(new Uri("Views/InicioView.xaml", UriKind.Relative));
        }
    }
}
