using ClienteJuego.ConnectService;
using ClienteJuego.Properties;
using Elasticsearch.Net;
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
    /// Interaction logic for LobbyView.xaml
    /// </summary>
    /// 


    public partial class LobbyView : Page, ConnectService.IMatchServiceCallback
    {

        private readonly ConnectService.MatchServiceClient matchServiceClient;
        public string codeInvitation { get; set; }
        private string userName;

        public LobbyView(string code)
        {
            this.codeInvitation = code;
            InitializeComponent();
            Accessories.PlayMusic();

            userName = (App.Current as App).DeptName;
            TextUserName.Text = userName;

            ImageSource imageSource = new ImageSourceConverter().ConvertFromString(Accessories.LoadConfigPlayer(userName)) as ImageSource;
            imgAvatar.Source = imageSource;

            matchServiceClient = new MatchServiceClient(new InstanceContext(this));

            matchServiceClient.SetCallbackMatch(userName);
            matchServiceClient.StartLobby(userName, codeInvitation);

        }

        private void btnListFriends_Click(object sender, RoutedEventArgs e)
        {

            var window = (MainWindow)Application.Current.MainWindow;
            window.ContenedorList.Navigate(new FriendListView(codeInvitation));

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/GameModeView.xaml", UriKind.Relative));
        }

        public void UpdateLobby(PlayerServer[] plyers)
        {
            foreach (var playersList in plyers)
            {
                Console.WriteLine(playersList.userName);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var roomchat = (MainWindow)App.Current.MainWindow;
            roomchat.ContenedorChat.Navigate(new ChatView());

        }
    }
}
