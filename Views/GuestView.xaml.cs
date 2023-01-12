using ClienteJuego.ConnectService;
using ClienteJuego.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
    /// Interaction logic for GuestView.xaml
    /// </summary>
    public partial class GuestView : Page
    {

        public GuestView()
        {
            InitializeComponent();
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();


            try
            {

                var player = client.GuestUser();
                if (player == null)
                {
                    MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                }
                else
                {
                    (App.Current as App).nameDeep = player.userName;
                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
            }

        }


        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
            var code = textInvitationCode.Text;
            int validateLobby = 1;
            if (!string.IsNullOrEmpty(code))
            {
                var result = client.ValidateLobby(textInvitationCode.Text);
                if (result == validateLobby)
                {
                    var window = (MainWindow)Application.Current.MainWindow;
                    window.Contenedor.Navigate(new LobbyView(code));
                }
                else
                {
                    MessageBox.Show(Properties.Resources.messageBoxErrorMessage);
                }
            }
            else
            {
                MessageBox.Show(Properties.Resources.messageBoxEmptyFields);
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
            try
            {
                client.UserDisconect((App.Current as App).nameDeep);
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
