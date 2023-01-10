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
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);

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

                    MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                    var window = (MainWindow)Application.Current.MainWindow;
                    window.Contenedor.Navigate(new LoginView());

                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
            }
            return playerInfo;
        }

        public void Receive(MessageServer message)
        {

            textChat.AppendText("From " + message.Sender + ": " + message.Content + "\n");

        }

        public void ReceiveWhisper(MessageServer message)
        {
            textChat.AppendText(Properties.Resources.messageWisp + message.Sender + ": " + message.Content + "\n");
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
            textChat.AppendText(Properties.Resources.messageSystem + " " + player.userName + "\n");
        }

        public void UserLeave(PlayerServer player)
        {
            textChat.AppendText("System: " + player.userName + Properties.Resources.messageSystem + "\n");
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
                    MessageBox.Show(Properties.Resources.messageBoxErrorMessage);
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
                    MessageBox.Show(Properties.Resources.messageBoxErrorMessage);
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
                MessageBox.Show(Properties.Resources.messageBoxErrorMessage);
            }

        }
    }
}
