using ClienteJuego.ConnectService;
using ClienteJuego.Properties;
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

namespace ClienteJuego.Views
{
    /// <summary>
    /// Interaction logic for LobbyView.xaml
    /// </summary>
    public partial class LobbyView : Page
    {

        public PlayerServer playerInfo;
        private string userName;
        public LobbyView()
        {
            InitializeComponent();
            Accessories.PlayMusic();
            userName = (App.Current as App).DeptName;
            TextUserName.Text = userName;
            ImageSource imageSource = new ImageSourceConverter().ConvertFromString(Accessories.LoadConfigPlayer(userName)) as ImageSource;
            imgAvatar.Source = imageSource;
        }

        private void btnListFriends_Click(object sender, RoutedEventArgs e)
        {
            PlayerServer friend = new PlayerServer();
            //friend.idPlayer =
        }
    }
}
