using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ClienteJuego.Views
{
    /// <summary>
    /// Interaction logic for InvitationView.xaml
    /// </summary>
    public partial class InvitationView : Page, ConnectService.IMatchServiceCallback
    {
        public InvitationView()
        {
            InitializeComponent();
        }

        public void RespondInvitation(bool respond)
        {
            throw new NotImplementedException();
        }

        public void ShowInvitation(PlayerServer friend, string code)
        {
            throw new NotImplementedException();
        }

        public void UpdateLobby(PlayerServer friend, string code)
        {
            throw new NotImplementedException();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
