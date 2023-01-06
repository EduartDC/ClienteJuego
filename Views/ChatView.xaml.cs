using ClienteJuego.ConnectService;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

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
        int match = 1;
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

            var code = match.ToString();
            try
            {
                chatServiceClient.Connect(playerData, code);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Error de conexion con el servidor, Intentelo más tarde");

                var chatView = (MainWindow)App.Current.MainWindow;
                chatView.ContenedorChat.Content = null;
            }
        }
        private PlayerServer LoadData()
        {
            PlayerServer playerInfo = new PlayerServer();
            try
            {
                ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
                playerInfo = client.SearchPlayer(userName);

                if (playerInfo == null)
                {

                    MessageBox.Show("Error de conexion con el servidor, Intentelo mas tarde");
                    var window = (MainWindow)Application.Current.MainWindow;
                    window.Contenedor.Navigate(new LoginView());

                }
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

        private void btnSendButton_Click(object sender, RoutedEventArgs e)
        {
            MessageServer message = new MessageServer();
            message.Sender = playerData.userName;

            if (textMessage.Text.Contains(":"))
            {
                string[] words = textMessage.Text.Split(':');
                var name = words[0];
                message.Content = words[1];
                try
                {
                    chatServiceClient.Whisper(message, name);
                }
                catch (CommunicationException)
                {
                    MessageBox.Show("Su mensaje no fue entregado, Intentelo más tarde");
                }
            }
            else
            {
                message.Content = textMessage.Text;
                try
                {
                    chatServiceClient.Say(message);
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
            try
            {
                chatServiceClient.Disconnect(playerData);
            }
            catch (CommunicationObjectFaultedException)
            {
                MessageBox.Show("Su mensaje no fue entregado, Intentelo más tarde");
            }

        }
    }
}
