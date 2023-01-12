using ClienteJuego.ConnectService;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace ClienteJuego.Views
{
    /// <summary>
    /// Interaction logic for FriendListView.xaml
    /// </summary>
    public partial class FriendListView : Page, ConnectService.INotificationServiceCallback
    {
        int errorConnection = 404;
        public string codeInvitation { get; set; }

        private readonly NotificationServiceClient notificationServiceClient;

        public FriendListView(string code)
        {
            this.codeInvitation = code;
            notificationServiceClient = new NotificationServiceClient(new InstanceContext(this));
            InitializeComponent();
            try
            {
                UpdateFriendList();
            }
            catch (CommunicationObjectFaultedException)
            {
                var inviViewm = (MainWindow)App.Current.MainWindow;
                inviViewm.ContenedorList.Content = null;
                throw new EndpointNotFoundException();


            }
            catch (EndpointNotFoundException)
            {
                var inviViewm = (MainWindow)App.Current.MainWindow;
                inviViewm.ContenedorList.Content = null;
                throw new EndpointNotFoundException();


            }
        }

        void UpdateFriendList()
        {
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
            var username = (App.Current as App).nameDeep;
            try
            {
                var friends = client.MatchingFriends(username);
                if (friends == null)
                {
                    MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                    var window = (MainWindow)Application.Current.MainWindow;
                    window.Contenedor.Navigate(new LoginView());
                }
                else
                {
                    listFriends.Items.Clear();
                    foreach (var player in friends)
                    {
                        listFriends.Items.Add(player);

                    }
                }

            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                var window = (MainWindow)Application.Current.MainWindow;
                window.Contenedor.Navigate(new LoginView());
            }
        }

        public void Notification(string username, string code)
        {
            throw new NotImplementedException();
        }

        private void btnSendInvitation_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            PlayerServer player = button.CommandParameter as PlayerServer;
            try
            {
                notificationServiceClient.NotificationUsers(player.userName, this.codeInvitation);
            }
            catch (CommunicationObjectFaultedException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                var window = (MainWindow)Application.Current.MainWindow;
                window.Contenedor.Navigate(new LoginView());
            }

        }

        public void LoadLobby(PlayerServer[] players, string code)
        {
            throw new NotImplementedException();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            var roomchat = (MainWindow)App.Current.MainWindow;
            roomchat.ContenedorList.Content = null;
        }

        private void btnAddFriend_Click(object sender, RoutedEventArgs e)
        {
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
            var username = (App.Current as App).nameDeep;

            var name = textNameFriend.Text;
            try
            {
                if (!name.Equals(null))
                {
                    var friend = client.SearchPlayer(name);
                    if (friend == null)
                    {
                        MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                        var window = (MainWindow)Application.Current.MainWindow;
                        window.Contenedor.Navigate(new LoginView());
                    }
                    else
                    {
                        if (friend.userName != null)
                        {
                            var idFriend = friend.idPlayer;
                            var player = client.SearchPlayer(username);

                            FriendServer newFriend = new FriendServer();
                            newFriend.gameFriend = idFriend;
                            newFriend.ownerPlayer = player.idPlayer;
                            newFriend.creationDate = DateTime.Now;

                            client.AddFriend(newFriend);
                            UpdateFriendList();
                        }
                        else
                        {
                            MessageBox.Show(Properties.Resources.messageBoxSearch);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(Properties.Resources.messageBoxEmptyFields);
                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
            }
            catch (CommunicationObjectFaultedException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
            }
            catch (CommunicationException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
            }

        }

        private void btnDeleatFriend_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
            PlayerServer player = button.CommandParameter as PlayerServer;


            var username = (App.Current as App).nameDeep;

            try
            {
                var result = client.DeleteFriend(player.idPlayer, username);

                if (result == errorConnection)
                {
                    MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                    var window = (MainWindow)Application.Current.MainWindow;
                    window.Contenedor.Navigate(new LoginView());
                }
                else
                {
                    UpdateFriendList();
                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
            }


        }
    }
}
