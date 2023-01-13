using ClienteJuego.ConnectService;
using ClienteJuego.Properties;
using System;
using System.Numerics;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace ClienteJuego.Views
{
    /// <summary>
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class AccountView : Page
    {
        String userName;

        public AccountView()
        {
            InitializeComponent();
            userName = (App.Current as App).nameDeep;
            PlayerServer player = LoadData();
            if (player != null)
            {
                textNombre.Text = player.firstName + " " + player.lastName;
                textEmail.Text = player.email;
                textUserName.Text = player.userName;

                ImageSource imageSource = new ImageSourceConverter().ConvertFromString(Accessories.LoadConfigPlayer(userName)) as ImageSource;
                imgAvatar.Source = imageSource;

                if (player.status)
                {
                    btnValidateEmail.Visibility = Visibility.Collapsed;
                }
            }

        }

        PlayerServer LoadData()
        {
            PlayerServer playerInfo = new PlayerServer();
            try
            {
                ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
                playerInfo = client.SearchPlayer(userName);
                if (playerInfo == null)
                {

                    MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                    var window = (MainWindow)Application.Current.MainWindow;
                    window.Contenedor.Navigate(new LoginView());

                }

            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
            }
            return playerInfo;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            NavigationService.Navigate(new Uri("Views/MenuOptionsView.xaml", UriKind.Relative));
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            NavigationService.Navigate(new Uri("Views/EditAccountView.xaml", UriKind.Relative));
        }

        private void btnValidateEmail_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/ValidateMailView.xaml", UriKind.Relative));
        }
    }
}
