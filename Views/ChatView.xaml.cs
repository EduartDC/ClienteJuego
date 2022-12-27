using ClienteJuego.ConnectService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.ServiceModel;
using System.ServiceModel.Channels;
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
            var code = "1";
            try
            {
                chatServiceClient.Connect(playerData, code);
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

        public void Receive(MessageServer message)
        {

            ChatTextBox.AppendText("From " + message.Sender + ": " + message.Content + "\n");

        }

        public void ReceiveWhisper(MessageServer message)
        {
            ChatTextBox.AppendText("Wisp from " + message.Sender + ": " + message.Content + "\n");
        }

        public void RefreshClients(PlayerServer[] players)
        {
            listPlayers.Items.Clear();
            foreach (var player in players)
            {
                listPlayers.Items.Add(player.userName);
            }

        }

        public void UserJoin(PlayerServer player)
        {
            ChatTextBox.AppendText("System: " + "Bienvenido " + player.userName + "\n");
        }

        public void UserLeave(PlayerServer player)
        {
            ChatTextBox.AppendText("System: " + player.userName + " Se ha desconectado\n");
        }

        private void SendButtonClick(object sender, RoutedEventArgs e)
        {
            MessageServer msg = new MessageServer();
            msg.Sender = playerData.userName;

            if (textMessage.Text.Contains(":"))
            {
                string[] words = textMessage.Text.Split(':');
                var userName = words[0];
                msg.Content = words[1];
                try
                {
                    chatServiceClient.Whisper(msg, userName);
                }
                catch (CommunicationException)
                {
                    MessageBox.Show("Su mensaje no fue entregado, Intentelo más tarde");
                }
            }
            else
            {
                msg.Content = textMessage.Text;
                try
                {
                    chatServiceClient.Say(1, msg);
                }
                catch (CommunicationException)
                {
                    MessageBox.Show("Su mensaje no fue entregado, Intentelo más tarde");
                }
            }

            textMessage.Text = "";
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            var roomchat = (MainWindow)App.Current.MainWindow;
            roomchat.ContenedorChat.Content = null;

            chatServiceClient.Disconnect(playerData);
        }
    }
}
