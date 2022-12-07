using ClienteJuego.Properties;
using ClienteJuego.ConnectService;
using System.IO;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
using Page = System.Windows.Controls.Page;

namespace ClienteJuego.Views
{
    /// <summary>
    /// Interaction logic for LobbyView.xaml
    /// </summary>
    public partial class LobbyView : Page
    {

        private string userName;

        public LobbyView()
        {
            InitializeComponent();
            fillTable();
            fillList();
        }

        private void fillList()
        {
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
            userName = (App.Current as App).DeptName;
            PlayerServer objectPlayer = client.SearchPlayer(userName);            
            var thiss = client.MatchingFriends(objectPlayer.idPlayer);

            foreach (var f in thiss)
            {
                listBFriends.Items.Add(f.userName + " " + f.firstName + " " + f.lastName);
            }

        }

        private void fillTable()
        {
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
            userName = (App.Current as App).DeptName;
            PlayerServer objectPlayer = client.SearchPlayer(userName);
            tblFriends.ItemsSource = client.MatchingFriends(objectPlayer.idPlayer);
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            NavigationService.Navigate(new Uri("Views/View.xaml", UriKind.Relative));
        }

        private void btnSearchFriend_Click(object sender, RoutedEventArgs e)
        {           
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
            userName = (App.Current as App).DeptName;
            PlayerServer playerOwner = client.SearchPlayer(userName);

            PlayerServer playerSearch = client.SearchPlayer(txtUserFriendSearch.Text);

            if (playerSearch == null)
            {
                MessageBox.Show(Properties.Resources.messageBoxFailAddFriend);
            }
            else
            {
                FriendServer friend = new FriendServer();

                friend.gameFriend = playerSearch.idPlayer;
                friend.ownerPlayer = playerOwner.idPlayer;
                friend.creationDate = DateTime.Now;

                int result = client.AddFriend(friend);

                if (result == 0)
                {
                    MessageBox.Show(Properties.Resources.messageBoxxErrorRegister);
                }
                else
                {
                    MessageBox.Show(Properties.Resources.messageBoxSuccessFriendAdd);
                    txtUserFriendSearch.Text = "";
                    fillTable();
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            NavigationService.Navigate(new Uri("Views/GameModeView.xaml", UriKind.Relative));
        }
    }
}
