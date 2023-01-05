﻿using ClienteJuego.ConnectService;
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
                throw new EndpointNotFoundException();

            }
            catch (EndpointNotFoundException)
            {
                throw new EndpointNotFoundException();

            }
        }

        void UpdateFriendList()
        {
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
            var username = (App.Current as App).DeptName;
            try
            {
                var friends = client.MatchingFriends(username);
                if (friends == null)
                {
                    MessageBox.Show("Error de conexion con el servidor, Intentelo mas tarde");
                    var window = (MainWindow)Application.Current.MainWindow;
                    window.Contenedor.Navigate(new LoginView());
                }
                else
                {
                    foreach (var player in friends)
                    {
                        listFriends.Items.Add(player);

                    }
                }

            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Error de conexion con el servidor, Intentelo mas tarde");
                var window = (MainWindow)Application.Current.MainWindow;
                window.Contenedor.Navigate(new LoginView());
            }
        }

        public void notification(string username, string code)
        {
            throw new NotImplementedException();
        }

        private void btnDeleatFriend_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            PlayerServer player = button.CommandParameter as PlayerServer;
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
            var username = (App.Current as App).DeptName;
            try
            {
                var result = client.DeleteFriend(player, username);
                if (result == 404)
                {
                    MessageBox.Show("Error de conexion con el servidor, Intentelo mas tarde");
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
                MessageBox.Show("Error de conexion con el servidor, Intentelo mas tarde");
            }

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
                MessageBox.Show("Error de conexion con el servidor, Intentelo mas tarde");
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
            var username = (App.Current as App).DeptName;

            var name = textNameFriend.Text;
            try
            {
                if (name.Equals(null))
                {
                    var friend = client.SearchPlayer(name);
                    if (friend == null)
                    {
                        MessageBox.Show("Error de conexion con el servidor, Intentelo mas tarde");
                        var window = (MainWindow)Application.Current.MainWindow;
                        window.Contenedor.Navigate(new LoginView());
                    }
                    else
                    {
                        if (friend.Equals(null))
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
                            MessageBox.Show("No se encontro ningun usario con ese nombre");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("El campo de nombre no puede esta vacio");
                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Error de conexion con el servidor, Intentelo mas tarde");
            }

        }
    }
}
