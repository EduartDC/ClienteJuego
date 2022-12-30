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
using ClienteJuego.ConnectService;
using Nest;
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
            client.AddToLobby(this.username, this.codeInvitation);
            var roomchat = (MainWindow)App.Current.MainWindow;
            roomchat.ContenedorInvi.Content = null;
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

        public void LoadBroad(ManagerService match)
        {
            throw new NotImplementedException();
        }

        public void ExitMatch()
        {
            throw new NotImplementedException();
        }
    }
}
