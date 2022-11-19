using ClienteJuego.Properties;
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
    /// Interaction logic for MenuOptionsView.xaml
    /// </summary>
    public partial class MenuOptionsView : Page
    {
        public MenuOptionsView()
        {
            InitializeComponent();
        }

        private void btnAccount_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            NavigationService.Navigate(new Uri("Views/AccountView.xaml", UriKind.Relative));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            NavigationService.Navigate(new Uri("Views/InicioView.xaml", UriKind.Relative));
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            NavigationService.Navigate(new Uri("Views/ReportView.xaml", UriKind.Relative));
        }

        private void btnCredits_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            NavigationService.Navigate(new Uri("Views/CreditsView.xaml", UriKind.Relative));
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            NavigationService.Navigate(new Uri("Views/SettingsView.xaml", UriKind.Relative));
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            NavigationService.Navigate(new Uri("Views/LoginView.xaml", UriKind.Relative));
        }
    }
}
