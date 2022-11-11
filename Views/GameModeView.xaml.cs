using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            //NavigationService.Navigate(new Uri("Views/LobbyView.xaml", UriKind.Relative));
        }

        private void btnSingleMode_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/TableroView.xaml", UriKind.Relative));
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/InicioView.xaml", UriKind.Relative));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/InicioView.xaml", UriKind.Relative));
        }
    }
}
