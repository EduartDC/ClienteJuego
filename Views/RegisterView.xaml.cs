﻿using ClienteJuego.ConnectService;
using ClienteJuego.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Page
    {
        public RegisterView()
        {
            InitializeComponent();
        }
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
            Player player = PlayerData();

            if (!ValidateFields())
            {
           

            }
            else if(!ValidatePassword(textPassword.Password))
            {
                MessageBox.Show("Error ocurred, the password does not meet the requirements");
            }
            else if (!ValidateEmail(player.email))
            {
                MessageBox.Show("Error ocurred, invalid email");
            }
            else if (!ValidatePlayerExistence(player))
            {

            }
            else
            {
                try
                {
                    int result = client.AddPlayer(player);

                    if (result == 0)
                    {
                        MessageBox.Show("Error occurred, registration didn't take effect");
                    }
                    else
                    {
                        MessageBox.Show("Successful registration.");
                    }
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show("Error de conexion con el servidor, Intentelo mas tarde");
                }
                    
                
            }


        }

        private bool ValidatePlayerExistence(Player player)
        {
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
            var result = true;
            var consultResult = client.ValidateExistantPlayer(player);
            if (consultResult == 1)
            {
                result = false;
            }
            return result;
        }

        private Player PlayerData()
        {
            var password = Accessories.Hash(textPassword.Password.ToString());

            Player player = new Player
            {
                firstName = textFirsName.Text,
                lastName = textLastName.Text,
                email = textEmail.Text,
                userName = textUserName.Text,
                password = password,
                status = true
            };

            return player;
        }

        public bool ValidateFields()
        {
            bool result = false;
            if (textFirsName.Text.Length <= 0 || textLastName.Text.Length <= 0 || textEmail.Text.Length <= 0 ||
                textUserName.Text.Length <= 0 || textPassword.Password.Length <= 0)
            {
                MessageBox.Show("An empty field was detected, to continue you must fill all text fields.");
            }
            else
            {
                result = true;
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
            try
            {
                var mailAddress = new MailAddress(emailAddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("Views/LoginView.xaml", UriKind.Relative));

        }

        private void TextFirsName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!textFirsName.Text.Equals("") || !textFirsName.Text.Equals(null))
            {
                lblExampleFirstName.Visibility = Visibility.Hidden;
            }
        }

        private void TextFirsName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textFirsName.Text.Equals("") || textFirsName.Text.Equals(null))
            {
                lblExampleFirstName.Visibility = Visibility.Visible;
            }
        }

        private void TextLastName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textLastName.Text.Equals("") || textLastName.Text.Equals(null))
            {
                lblExampleLastName.Visibility = Visibility.Visible;
            }
        }

        private void TextLastName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!textLastName.Text.Equals("") || !textLastName.Text.Equals(null))
            {
                lblExampleLastName.Visibility = Visibility.Hidden;
            }
        }

        private void TextEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!textEmail.Text.Equals("") || !textEmail.Text.Equals(null))
            {
                lblExampleEmail.Visibility = Visibility.Hidden;
            }
        }

        private void TextEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textEmail.Text.Equals("") || textEmail.Text.Equals(null))
            {
                lblExampleEmail.Visibility = Visibility.Visible;
            }
        }

        private void TextUserName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!textUserName.Text.Equals("") || !textUserName.Text.Equals(null))
            {
                lblExampleUser.Visibility = Visibility.Hidden;
            }
        }

        private void TextUserName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textUserName.Text.Equals("") || textUserName.Text.Equals(null))
            {
                lblExampleUser.Visibility = Visibility.Visible;
            }
        }

    }

}