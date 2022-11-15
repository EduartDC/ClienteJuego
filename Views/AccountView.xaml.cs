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
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class AccountView : Page
    {
        String userName;
        
        public AccountView()
        {
            InitializeComponent();
            userName = (App.Current as App).DeptName;
            Player player = LoadData();
            
            textNombre.Text = player.firstName + " " + player.lastName;
            textEmail.Text = player.email;
            textUserName.Text = player.userName;

            
        }

        Player LoadData()
        {
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
            Player playerInfo = client.SearchPlayer(userName);
            return playerInfo;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/MenuOptionsView.xaml", UriKind.Relative));
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/EditAccountView.xaml", UriKind.Relative));
        }
    }
}
