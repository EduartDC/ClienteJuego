using ClienteJuego.Avatars;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClienteJuego.Views
{
    /// <summary>
    /// Interaction logic for EditAccountView.xaml
    /// </summary>
    public partial class EditAccountView : Page
    {
        String userName;
        public EditAccountView()
        {
            InitializeComponent();
            userName = (App.Current as App).DeptName;
            
            Player player = LoadData();

            textFirstName.Text = player.firstName;
            textLastName.Text = player.lastName;
            textUserName.Text = player.userName;
            LoadCombo();
            
        }

        Player LoadData()
        {
            Player playerInfo = new Player();
            try
            {
                ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
                playerInfo = client.SearchPlayer(userName);
                
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Error de conexion con el servidor, Intentelo mas tarde");
            }
            return playerInfo;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Avatar avatar = (Avatar)comBoxAvatar.SelectedItem;
            Console.WriteLine(avatar.Url );
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            
            int resultado = (int)MessageBox.Show("¿Estás seguro(a) de salir? No se guardarán las modificaciones.", "Cuidado!", MessageBoxButton.YesNo);
            if (resultado == 6)
            {
                NavigationService.Navigate(new Uri("Views/AccountView.xaml", UriKind.Relative));
            }
        }
        private void DataPlayer()
        {

        }
        private void LoadCombo()
        {
            List<Avatar> avatars = new List<Avatar>();
            Avatar avatarDef = new Avatar();
            avatarDef.Name = "one";
            avatarDef.Url = "/Avatars/avatarDef.png";
            avatars.Add(avatarDef);

            Avatar avatarOne = new Avatar();
            avatarOne.Name = "one";
            avatarOne.Url = "/Avatars/avatarUno.jpg";
            avatars.Add(avatarOne);

            Avatar avatarTwo = new Avatar();
            avatarTwo.Name = "two";
            avatarTwo.Url = "/Avatars/avatarDos.jpg";
            avatars.Add(avatarTwo);

            Avatar avatarThree = new Avatar();
            avatarThree.Name = "three";
            avatarThree.Url = "/Avatars/avatarTres.jpg";
            avatars.Add(avatarThree);

            Avatar avatarFour = new Avatar();
            avatarFour.Name = "four";
            avatarFour.Url = "/Avatars/avatarCuatro.jpg";
            avatars.Add(avatarFour);

            Avatar avatarFive = new Avatar();
            avatarFive.Name = "five";
            avatarFive.Url = "/Avatars/avatarCinco.jpg";
            avatars.Add(avatarFive);

            Avatar avatarSix = new Avatar();
            avatarSix.Name = "five";
            avatarSix.Url = "/Avatars/avatarSeis.jpg";
            avatars.Add(avatarSix);

            Avatar avatarSeven = new Avatar();
            avatarSeven.Name = "five";
            avatarSeven.Url = "/Avatars/avatarSiete.jpg";
            avatars.Add(avatarSeven);

            Avatar avatarEight = new Avatar();
            avatarEight.Name = "five";
            avatarEight.Url = "/Avatars/avatarOcho.jpg";
            avatars.Add(avatarEight);

            comBoxAvatar.ItemsSource = avatars;
            comBoxAvatar.SelectedItem = avatarDef;
        }

        private void textPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textUserName.Text.Equals("") || textUserName.Text.Equals(null))
            {
                lblExamplePassword.Visibility = Visibility.Visible;
            }
        }

        private void textPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!textUserName.Text.Equals("") || !textUserName.Text.Equals(null))
            {
                lblExamplePassword.Visibility = Visibility.Hidden;

            }
        }
    }
}
