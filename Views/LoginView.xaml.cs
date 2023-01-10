using ClienteJuego.ConnectService;
using ClienteJuego.Properties;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ClienteJuego.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Page
    {

        public LoginView()
        {
            InitializeComponent();

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();

            String userName = textUserName.Text;
            String password = textPassword.Password.ToString();

            password = Accessories.Hash(password);

            PlayerServer player = new PlayerServer
            {
                userName = userName,
                password = password
            };

            if (ValidateFields())
            {
                try
                {
                    var result = client.UserConnect(player);
                    if (result == null)
                    {
                        MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                    }
                    else if (result.userName != null && result.userName.Equals(player.userName))
                    {
                        (App.Current as App).DeptName = player.userName;
                        NavigationService.Navigate(new Uri("Views/InicioView.xaml?value=15", UriKind.Relative));

                    }
                    else
                    {
                        MessageBox.Show(Properties.Resources.messageBoxErrorLogin);
                    }
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                }
                catch (CommunicationException)
                {
                    MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                }

            }

        }

        public bool ValidateFields()
        {
            bool result = false;
            String userName = textUserName.Text;
            String password = textPassword.Password.ToString();


            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show(Properties.Resources.messageBoxEmptyFields);
            }
            else
            {
                result = true;
            }
            return result;
        }

        private void textUserName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!textUserName.Text.Equals("") || !textUserName.Text.Equals(null))
            {
                labelExampleUserName.Visibility = Visibility.Hidden;
            }
        }

        private void textUserName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textUserName.Text.Equals("") || textUserName.Text.Equals(null))
            {
                labelExampleUserName.Visibility = Visibility.Visible;
            }
        }

        private void textPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textUserName.Text.Equals("") || textUserName.Text.Equals(null))
            {
                labelExamplePassword.Visibility = Visibility.Visible;
            }
        }

        private void textPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!textUserName.Text.Equals("") || !textUserName.Text.Equals(null))
            {
                labelExamplePassword.Visibility = Visibility.Hidden;
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            NavigationService.Navigate(new Uri("Views/RegisterView.xaml", UriKind.Relative));

        }

        private void TextUserName_KeyDown(object sender, KeyEventArgs e)
        {
            Accessories.RegexSpecial(e);
        }

        private void TextPassword_KeyDonw(object sender, KeyEventArgs e)
        {
            Accessories.RegexSpecial(e);
        }

        public void notification(string username)
        {
            throw new NotImplementedException();
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/GuestView.xaml", UriKind.Relative));
        }


    }
}
