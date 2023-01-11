using ClienteJuego.ConnectService;
using ClienteJuego.Properties;
using System;
using System.Net.Mail;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ClienteJuego.Views
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Page
    {

        int errorConnection = 404;
        public RegisterView()
        {
            InitializeComponent();

        }
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
            PlayerServer player = PlayerData();

            if (!ValidateFields())
            {
                MessageBox.Show(Properties.Resources.messageBoxEmptyFields);

            }
            else if (!ValidatePassword(textPassword.Password))
            {
                MessageBox.Show(Properties.Resources.messageBoxInvalidPassword);
            }
            else if (!ValidateEmail(player.email))
            {
                MessageBox.Show(Properties.Resources.messageBoxInvalidEmail);
            }
            else if (!ValidatePlayerExistence(player))
            {
                MessageBox.Show(Properties.Resources.messageBoxVerifyNameAndEmail);
            }
            else
            {
                try
                {
                    int result = client.AddPlayer(player);
                    int error = 0;
                    if (result == error)
                    {
                        MessageBox.Show(Properties.Resources.messageBoxErrorRegister);
                    }
                    else if (result == errorConnection)
                    {

                        MessageBox.Show(Properties.Resources.messageBoxConnectionError);

                    }
                    else
                    {
                        MessageBox.Show(Properties.Resources.messageBoxSuccessfulRegistration);
                        ClearFields();
                        Accessories.SaveProfileAvatar(player.userName, "/Avatars/avatarDef.png");
                    }
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                }


            }


        }

        private bool ValidatePlayerExistence(PlayerServer player)
        {
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
            var result = true;

            try
            {
                var validateEmail = false;
                var validateUser = false;

                var emailResult = client.ValidateEmailPlayer(player);
                int inUse = 1;
                if (emailResult == inUse)
                {
                    MessageBox.Show(Properties.Resources.messageBoxMailInUse);
                    validateEmail = true;
                }
                else if (emailResult == errorConnection)
                {
                    MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                }

                var userResult = client.ValidateUserNamePlayer(player);
                if (userResult == inUse)
                {
                    MessageBox.Show(Properties.Resources.messageBoxInvalidName);
                    validateUser = true;
                }
                else if (emailResult == errorConnection)
                {
                    MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                }

                if (validateEmail || validateUser)
                {
                    result = false;
                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
            }
            return result;
        }

        private PlayerServer PlayerData()
        {
            var password = Accessories.Hash(textPassword.Password.ToString());

            PlayerServer player = new PlayerServer
            {
                firstName = textFirsName.Text,
                lastName = textLastName.Text,
                email = textEmail.Text,
                userName = textUserName.Text,
                password = password,
                status = false
            };

            return player;
        }

        public bool ValidateFields()
        {
            bool result = true;
            var firsName = textFirsName.Text;
            var lastName = textLastName.Text;
            var email = textEmail.Text;
            var userName = textUserName.Text;
            var password = textPassword.Password.ToString();

            if (string.IsNullOrEmpty(firsName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                result = false;
            }

            return result;
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

        public static bool ValidateEmail(string emailAddress)
        {
            var result = false;
            try
            {
                var mailAddress = new MailAddress(emailAddress);

                result = true;
            }
            catch (FormatException)
            {
                result = false;
            }
            return result;
        }
        public void ClearFields()
        {
            textFirsName.Text = "";
            textLastName.Text = "";
            textEmail.Text = "";
            textUserName.Text = "";
            textPassword.Password = "";

        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            NavigationService.Navigate(new Uri("Views/LoginView.xaml", UriKind.Relative));

        }

        private void TextFirsName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!textFirsName.Text.Equals("") || !textFirsName.Text.Equals(null))
            {
                labelExampleFirstName.Visibility = Visibility.Hidden;
            }
        }

        private void TextFirsName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textFirsName.Text.Equals("") || textFirsName.Text.Equals(null))
            {
                labelExampleFirstName.Visibility = Visibility.Visible;
            }
        }

        private void TextLastName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textLastName.Text.Equals("") || textLastName.Text.Equals(null))
            {
                labelExampleLastName.Visibility = Visibility.Visible;
            }
        }

        private void TextLastName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!textLastName.Text.Equals("") || !textLastName.Text.Equals(null))
            {
                labelExampleLastName.Visibility = Visibility.Hidden;
            }
        }

        private void TextEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!textEmail.Text.Equals("") || !textEmail.Text.Equals(null))
            {
                labelExampleEmail.Visibility = Visibility.Hidden;
            }
        }

        private void TextEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textEmail.Text.Equals("") || textEmail.Text.Equals(null))
            {
                labelExampleEmail.Visibility = Visibility.Visible;
            }
        }

        private void TextUserName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!textUserName.Text.Equals("") || !textUserName.Text.Equals(null))
            {
                labelExampleUser.Visibility = Visibility.Hidden;
            }
        }

        private void TextUserName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textUserName.Text.Equals("") || textUserName.Text.Equals(null))
            {
                labelExampleUser.Visibility = Visibility.Visible;
            }
        }

        private void TextFisrtName_KeyDown(object sender, KeyEventArgs e)
        {


            Accessories.RegexSpecial(e);

        }

        private void TextLastName_KeyDown(object sender, KeyEventArgs e)
        {
            Accessories.RegexSpecial(e);
        }

        private void TextUserName_KeyDown(object sender, KeyEventArgs e)
        {
            Accessories.RegexSpecial(e);
        }

        private void TextPassword_KeyDown(object sender, KeyEventArgs e)
        {
            Accessories.RegexSpecial(e);
        }

        private void textPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!textUserName.Text.Equals("") || !textUserName.Text.Equals(null))
            {
                labelExamplePassword.Visibility = Visibility.Hidden;

            }
        }
        private void textPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textUserName.Text.Equals("") || textUserName.Text.Equals(null))
            {
                labelExamplePassword.Visibility = Visibility.Visible;
            }
        }
    }

}
