using ClienteJuego.ConnectService;
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
    /// Interaction logic for InicioView.xaml
    /// </summary>
    public partial class InicioView : Page
    {

        private Player playerInfo;
        public InicioView()
        {
            InitializeComponent();
            
            
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/GameModeView.xaml", UriKind.Relative));
        }

        private void btnOptions_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/MenuOptionsView.xaml", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/GameModeView.xaml", UriKind.Relative));
        }

        private void btnScore_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/ScoreView.xaml", UriKind.Relative));
        }

        public void PlayerLogin(Player player)
        {
            Console.WriteLine(player.userName);
            Console.WriteLine(player.email);
            lblNameUsuario.Content = player.userName;
            this.playerInfo = player;
        }

        
    }
}
