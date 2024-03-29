﻿using ClienteJuego.ConnectService;
using ClienteJuego.Properties;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

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
            (App.Current as App).codeDeep = code;
            userName = (App.Current as App).nameDeep;
            textUserName.Text = userName;
            labelTextCode.Content = code;

            if (userName.Equals("Guest"))
            {
                btnKick.Visibility = Visibility.Collapsed;
            }

            ImageSource imageSource = new ImageSourceConverter().ConvertFromString(Accessories.LoadConfigPlayer(userName)) as ImageSource;
            imgAvatar.Source = imageSource;

            matchServiceClient = new MatchServiceClient(new InstanceContext(this));
            try
            {
                matchServiceClient.SetCallbackMatch(userName);
                matchServiceClient.StartLobby(userName, codeInvitation);
            }
            catch (CommunicationObjectFaultedException)
            {
                throw new EndpointNotFoundException();

            }
            catch (EndpointNotFoundException)
            {
                throw new EndpointNotFoundException();

            }
        }

        private void btnListFriends_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var window = (MainWindow)App.Current.MainWindow;
                window.ContenedorList.Navigate(new FriendListView(codeInvitation));
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                var window = (MainWindow)Application.Current.MainWindow;
                window.Contenedor.Navigate(new LoginView());
            }


        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                matchServiceClient.DisconnectFromLobby(userName, codeInvitation);

                if (userName.Equals("Guest"))
                {
                    ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
                    try
                    {
                        client.UserDisconect(userName);
                        var window = (MainWindow)Application.Current.MainWindow;
                        window.Contenedor.Navigate(new LoginView());
                    }
                    catch (EndpointNotFoundException)
                    {
                        var window = (MainWindow)Application.Current.MainWindow;
                        window.Contenedor.Navigate(new LoginView());
                    }

                }
                else
                {
                    var window = (MainWindow)Application.Current.MainWindow;
                    window.Contenedor.Navigate(new InicioView());
                }
            }
            catch (CommunicationObjectFaultedException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                var window = (MainWindow)Application.Current.MainWindow;
                window.Contenedor.Navigate(new LoginView());
            }

            var inviViewm = (MainWindow)App.Current.MainWindow;
            inviViewm.ContenedorList.Content = null;

            var chatView = (MainWindow)App.Current.MainWindow;
            chatView.ContenedorChat.Content = null;

            NavigationService.Navigate(new Uri("Views/GameModeView.xaml", UriKind.Relative));
        }

        public void UpdateLobby(PlayerServer[] plyers)
        {

            listPlayersLobby.Items.Clear();
            foreach (var playersList in plyers)
            {
                listPlayersLobby.Items.Add(playersList);

            }
        }

        private void btnChat_Click(object sender, RoutedEventArgs e)
        {
            var roomchat = (MainWindow)App.Current.MainWindow;
            roomchat.ContenedorChat.Navigate(new ChatView());

        }

        public void LoadMatch(MatchServer match)
        {
            try
            {
                var room = (MainWindow)App.Current.MainWindow;
                room.Contenedor.Navigate(new TableroView(match));
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                var window = (MainWindow)Application.Current.MainWindow;
                window.Contenedor.Navigate(new LoginView());

            }

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            var inviViewm = (MainWindow)App.Current.MainWindow;
            inviViewm.ContenedorList.Content = null;

            var chatView = (MainWindow)App.Current.MainWindow;
            chatView.ContenedorChat.Content = null;
            var conte = listPlayersLobby.Items.Count;
            if (conte == 2)
            {
                try
                {
                    matchServiceClient.StartMatch(codeInvitation);
                }
                catch (CommunicationObjectFaultedException)
                {
                    MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                    var window = (MainWindow)Application.Current.MainWindow;
                    window.Contenedor.Navigate(new LoginView());
                }

            }
            else
            {
                MessageBox.Show(Properties.Resources.messageBoxErrorLobby);
            }

        }

        private void btnKick_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                matchServiceClient.KickFromLobby(userName, codeInvitation);
                matchServiceClient.StartLobby(userName, codeInvitation);
            }
            catch (CommunicationObjectFaultedException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                var window = (MainWindow)Application.Current.MainWindow;
                window.Contenedor.Navigate(new LoginView());
            }
        }

        public void Kicked()
        {

            if (userName.Equals("Guest"))
            {
                ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
                try
                {
                    client.UserDisconect(userName);
                    var window = (MainWindow)Application.Current.MainWindow;
                    window.Contenedor.Navigate(new LoginView());
                }
                catch (EndpointNotFoundException)
                {
                    var window = (MainWindow)Application.Current.MainWindow;
                    window.Contenedor.Navigate(new LoginView());
                }

            }
            else
            {
                var window = (MainWindow)Application.Current.MainWindow;
                window.Contenedor.Navigate(new InicioView());
            }
        }
    }
}
