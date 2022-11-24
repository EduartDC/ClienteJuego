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
        private Player playerData;
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
        private Player LoadData()
        {
            Player playerInfo = new Player();
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

        public void Receive(Message[] messages)
        {
            ChatTextBox.Clear();
            foreach (var message in messages)
            {
                ChatTextBox.AppendText(message.Sender + ": " + message.Content + "\n");
            }
            
        }

        public void ReceiveWhisper(Message msg, Player receiver)
        {
            throw new NotImplementedException();
        }

        public void RefreshClients(Player[] clients)
        {
            
            listPlayers.Items.Add(clients);
        }

        public void UserJoin(Player client)
        {
            
            ChatTextBox.AppendText("Bienvenido" + ": " + client.userName + "\n");
        }

        public void UserLeave(Player client)
        {
            throw new NotImplementedException();
        }

        private void SendButtonClick(object sender, RoutedEventArgs e)
        {
            Message msg = new Message();
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
