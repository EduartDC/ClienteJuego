﻿using ClienteJuego.ConnectService;
using ClienteJuego.Properties;
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
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();

            String userName = textUserName.Text;
            String password = textPassword.Password.ToString();

            password = Accessories.Hash(password);

            Player player = new Player
            {
                userName = userName,
                password = password
            };

            if (ValidateFields())
            {
                int result = client.ValidatePlayer(player);
                if (result == 0)
                {
                    MessageBox.Show("Error occurred, registration didn't take effect");
                }
                else
                {
                    NavigationService.Navigate(new Uri("Views/InicioView.xaml", UriKind.Relative));
                }
                Console.WriteLine(result);
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
                lblExampleUserName.Visibility = Visibility.Hidden;
            }
        }

        private void textUserName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textUserName.Text.Equals("") || textUserName.Text.Equals(null))
            {
                lblExampleUserName.Visibility = Visibility.Visible;
            }
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

     

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("Views/RegisterView.xaml", UriKind.Relative));

        }
    }
}