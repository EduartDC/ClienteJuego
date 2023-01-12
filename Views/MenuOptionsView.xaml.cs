using ClienteJuego.Properties;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

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
            try
            {
                Accessories.PlaySoundsEffects();
                var window = (MainWindow)Application.Current.MainWindow;
                window.Contenedor.Navigate(new InicioView());
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                var window = (MainWindow)Application.Current.MainWindow;
                window.Contenedor.Navigate(new LoginView());
            }
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
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
            try
            {
                var userName = (App.Current as App).nameDeep;
                client.UserDisconect(userName);
                var window = (MainWindow)Application.Current.MainWindow;
                window.Contenedor.Navigate(new LoginView());
            }
            catch (EndpointNotFoundException)
            {
                var window = (MainWindow)Application.Current.MainWindow;
                window.Contenedor.Navigate(new LoginView());
            }

        }
    }
}
