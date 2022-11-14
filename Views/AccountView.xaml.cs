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

        
        public AccountView()
        {
            InitializeComponent();
            string selected_dept = (App.Current as App).DeptName;
            //ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
            MessageBox.Show(selected_dept);
            //Player playerInfo = client.SearchPlayer(player.userName);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/MenuOptionsView.xaml", UriKind.Relative));
        }
    }
}
