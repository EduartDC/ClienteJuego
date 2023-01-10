using ClienteJuego.ConnectService;
using System;
using System.ServiceModel;
using System.Windows;
using Page = System.Windows.Controls.Page;

namespace ClienteJuego.Views
{
    /// <summary>
    /// Interaction logic for InvitationView.xaml
    /// </summary>
    public partial class InvitationView : Page, ConnectService.IMatchServiceCallback
    {

        public string codeInvitation { get; set; }
        public string username { get; set; }


        private readonly ConnectService.MatchServiceClient client;
        public InvitationView()
        {
            InitializeComponent();

            client = new MatchServiceClient(new InstanceContext(this));
        }

        public InvitationView(string username, string code) : this()
        {
            this.codeInvitation = code;
            this.username = username;

        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                client.AddToLobby(this.username, this.codeInvitation);
                var roomchat = (MainWindow)App.Current.MainWindow;
                roomchat.ContenedorInvi.Content = null;
            }
            catch (CommunicationObjectFaultedException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                var window = (MainWindow)Application.Current.MainWindow;
                window.Contenedor.Navigate(new LoginView());
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var roomchat = (MainWindow)App.Current.MainWindow;
            roomchat.ContenedorInvi.Content = null;
        }

        public void UpdateLobby(PlayerServer[] plyers)
        {
            throw new NotImplementedException();
        }

        public void LoadMatch(MatchServer match)
        {
            throw new NotImplementedException();
        }

        public void Kicked()
        {
            throw new NotImplementedException();
        }
    }
}
