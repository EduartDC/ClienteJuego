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
    /// Interaction logic for ValidateMailView.xaml
    /// </summary>
    public partial class ValidateMailView : Page
    {
        String userName;
        PlayerServer playerInfo = new PlayerServer();
        string code = "MDss" + Accessories.GenerateRandomCode();
        int connectionError = 404;
        public ValidateMailView()
        {
            InitializeComponent();
            userName = (App.Current as App).DeptName;
            LoadData();
            try
            {
                ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
                var result = client.SendMail(playerInfo, code);

                if (result == connectionError)
                {
                    MessageBox.Show("");
                    btnValidate.IsEnabled = false;
                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
            }

        }

        PlayerServer LoadData()
        {

            try
            {
                ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
                playerInfo = client.SearchPlayer(userName);

            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
            }
            return playerInfo;
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            window.Contenedor.Navigate(new AccountView());

        }

        private void btnValidate_Click(object sender, RoutedEventArgs e)
        {
            var inCode = textCode.Text;
            if (code.Equals(inCode))
            {
                try
                {
                    ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
                    playerInfo.status = true;
                    client.UpdatePlayer(playerInfo);
                    MessageBox.Show(Properties.Resources.messageCorrectCode);

                    var window = (MainWindow)Application.Current.MainWindow;
                    window.Contenedor.Navigate(new AccountView());
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                }

            }
            else
            {
                MessageBox.Show(Properties.Resources.messageIncorrectCode);
            }


        }
    }
}
