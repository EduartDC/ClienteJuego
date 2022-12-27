using ClienteJuego.ConnectService;
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
    /// Interaction logic for FriendListView.xaml
    /// </summary>
    public partial class FriendListView : Page, ConnectService.INotificationServiceCallback
    {
        public string codeInvitation { get; set; }

        private readonly NotificationServiceClient notificationServiceClient;


        public FriendListView(string code)
        {
            this.codeInvitation = code;
            InitializeComponent();

            List<PlayerServer> friends = new List<PlayerServer>();
            PlayerServer d = new PlayerServer();
            d.userName = "cide";
            friends.Add(d);
            PlayerServer f = new PlayerServer();
            f.userName = "carlitos";
            friends.Add(f);
            PlayerServer g = new PlayerServer();
            g.userName = "revo";
            friends.Add(g);
            foreach (var player in friends)
            {
                listFriends.Items.Add(player);

            }

            notificationServiceClient = new NotificationServiceClient(new InstanceContext(this));
        }

        public void notification(string username, string code)
        {
            throw new NotImplementedException();
        }

        private void btnDeleatFriend_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            PlayerServer player = button.CommandParameter as PlayerServer;

            MessageBox.Show(player.userName);

        }

        private void btnSendInvitation_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            PlayerServer player = button.CommandParameter as PlayerServer;

            notificationServiceClient.NotificationUsers(player.userName, this.codeInvitation);
        }

        public void LoadLobby(PlayerServer[] players, string code)
        {
            throw new NotImplementedException();
        }
    }
}
