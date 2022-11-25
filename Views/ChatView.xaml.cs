using ClienteJuego.ConnectService;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClienteJuego.Views
{
    /// <summary>
    /// Interaction logic for ChatView.xaml
    /// </summary>
    public partial class ChatView : Page, ConnectService.IChatServiceCallback
    {

        private readonly ChatServiceClient chatServiceClient;
        private string userName;
        private PlayerServer playerData;
        public ChatView()
        {
            
            chatServiceClient = new ChatServiceClient(new InstanceContext(this));

            StartView();
            InitializeComponent();
            
        }

        private void StartView()
        {

            userName = (App.Current as App).DeptName;
            playerData = LoadData();
            int idMatch = 1;
            try
            {
                chatServiceClient.Connect(playerData, idMatch);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Error de conexion con el servidor, Intentelo más tarde");
            }
        }
        private PlayerServer LoadData()
        {
            PlayerServer playerInfo = new PlayerServer();
            try
            {
                ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
                playerInfo = client.SearchPlayer(userName);

            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Error de conexion con el servidor, Intentelo más tarde");
            }
            return playerInfo;
        }

        public void Receive(MessageServer[] messages)
        {
            ChatTextBox.Clear();
            foreach (var message in messages)
            {
                ChatTextBox.AppendText(message.Sender + ": " + message.Content + "\n");
            }
            
        }

        public void ReceiveWhisper(MessageServer msg, PlayerServer receiver)
        {
            throw new NotImplementedException();
        }

        public void RefreshClients(PlayerServer[] players)
        {
            
            listPlayers.Items.Add(players);
        }

        public void UserJoin(PlayerServer player)
        {
            
            ChatTextBox.AppendText("Bienvenido" + ": " + player.userName + "\n");
        }

        public void UserLeave(PlayerServer player)
        {
            throw new NotImplementedException();
        }

        private void SendButtonClick(object sender, RoutedEventArgs e)
        {
            MessageServer msg = new MessageServer();
            msg.Sender = playerData.userName;
            msg.Content = textMessage.Text;
            try
            {
                chatServiceClient.Say(1, msg);
            }
            catch (CommunicationException)
            {
                MessageBox.Show("Su mensaje no fue entregado, Intentelo más tarde");
            }
            
            textMessage.Text = "";
        }
    }
}
