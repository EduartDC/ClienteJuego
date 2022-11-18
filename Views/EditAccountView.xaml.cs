using ClienteJuego.Avatars;
using ClienteJuego.ConnectService;
using ClienteJuego.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
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
        Player playerInfo = new Player();
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
            var password = HashPassword();
            var userName = ValidateUserName();
            if (ValidateFields())
            {
                MessageBox.Show("Los campos no pueden estar vacios.");
            }
            else if(password == null)
            {
                MessageBox.Show("Contraseña incorrecta");
            }
            else if (userName == null)
            {

            }
            else
            {
                Avatar avatar = new Avatar();
                avatar = comBoxAvatar.SelectedItem as Avatar;
                Accessories.SaveProfileAvatar(playerInfo.userName, avatar.Url);
                Accessories.LoadConfigPlayer(playerInfo.userName);
                MessageBox.Show("Si jalo");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            
            int resultado = (int)MessageBox.Show("¿Estás seguro(a) de salir? No se guardarán las modificaciones.", "Cuidado!", MessageBoxButton.YesNo);
            if (resultado == 6)
            {
                NavigationService.Navigate(new Uri("Views/AccountView.xaml", UriKind.Relative));
            }
        }
        
        private string ValidateUserName()
        {
            string userName = null;
            
            Player player = new Player();
            player.userName = textUserName.Text;
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
            try
            {
                if (playerInfo.userName.Equals(textUserName.Text))
                {
                    userName = playerInfo.userName;
                }
                else if (client.ValidateUserNamePlayer(player) == 1)
                {
                    MessageBox.Show("Exte usuario ya se encuentra registrado, Utilice otro nombre de usuario.");

                }
                else
                {
                    userName = textUserName.Text;
                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Error de conexion con el servidor, Intentelo más tarde");
            }

            return userName;
        }

        private string HashPassword()
        {
            string password = null;

            if (textPassword.Text == "")
            {
                 password = playerInfo.password;
            }
            else if (!ValidatePassword(textPassword.Text))
            {
                MessageBox.Show("Error ocurred, the password does not meet the requirements");
            }
            else if (!playerInfo.password.Equals( Accessories.Hash(textPassword.Text)))
            {
                MessageBox.Show("La nueva contraseña tiene que ser diferente a la antigua.");
            }
            else
            {
                 password = Accessories.Hash(textPassword.Text);
            }


            return password;
        }

        public bool ValidatePassword(string password)
        {
            var hasUpperLetter = new Regex(@"[A-Z]+");
            var hasNumber = new Regex(@"[0-9]+");
            var hasMiniumDigits = new Regex(@".{8,}");
            var result = false;

            if (hasNumber.IsMatch(password) &&
                hasUpperLetter.IsMatch(password) &&
                hasMiniumDigits.IsMatch(password))
            {
                result = true;
            }

            return result;
        }

        private bool ValidateFields()
        {

            return string.IsNullOrEmpty(textFirstName.Text) ||
                string.IsNullOrEmpty(textLastName.Text) ||
                string.IsNullOrEmpty(textUserName.Text);
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
