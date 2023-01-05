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

        private string userName;

        public GuestView()
        {
            InitializeComponent();
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
            PlayerServer player = new PlayerServer
            {
                userName = "Guest",
                password = "123-.0-"
            };
            userName = player.userName;
            try
            {
                var result = client.UserConnect(player);
                if (result == null)
                {
                    MessageBox.Show("Error de conexion con el servidor, Intentelo mas tarde");
                }
                else if (result.userName.Equals(player.userName))
                {
                    (App.Current as App).DeptName = player.userName;

                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Error de conexion con el servidor, Intentelo mas tarde");
            }

        }


        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
            var code = textInvitationCode.Text;

            if (!code.Equals(""))
            {
                var result = client.ValidateLobby(textInvitationCode.Text);
                if (result == 1)
                {
                    var window = (MainWindow)Application.Current.MainWindow;
                    window.Contenedor.Navigate(new LobbyView(code));
                }
                else
                {
                    MessageBox.Show("El codigo no es valido");
                }
            }
            else
            {
                MessageBox.Show("El campo de texto esta vacio");
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
            try
            {
                client.UserDisconect((App.Current as App).DeptName);
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
