﻿using ClienteJuego.Avatars;
using ClienteJuego.ConnectService;
using ClienteJuego.Properties;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ClienteJuego.Views
{
    /// <summary>
    /// Interaction logic for EditAccountView.xaml
    /// </summary>
    public partial class EditAccountView : Page
    {
        String userName;
        int errorConnection = 404;
        int resultYes = 6;
        PlayerServer playerInfo = new PlayerServer();
        public EditAccountView()
        {
            InitializeComponent();
            userName = (App.Current as App).nameDeep;

            PlayerServer player = LoadData();

            textFirstName.Text = player.firstName;
            textLastName.Text = player.lastName;
            textUserName.Text = player.userName;
            LoadCombo();

        }

        PlayerServer LoadData()
        {

            try
            {
                ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
                playerInfo = client.SearchPlayer(userName);

            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
            }
            return playerInfo;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            var password = HashPassword();
            var name = ValidateUserName();
            var validOperation = 1;
            if (ValidateFields())
            {
                MessageBox.Show(Properties.Resources.messageBoxEmptyFields);
            }
            else if (password == null)
            {
                MessageBox.Show(Properties.Resources.messageBoxInvalidPassword);
            }
            else if (name == null)
            {
                MessageBox.Show(Properties.Resources.messageBoxInvalidName);
            }
            else
            {

                Avatar avatar = comBoxAvatar.SelectedItem as Avatar;
                Accessories.SaveProfileAvatar(playerInfo.userName, avatar.Url);
                Accessories.LoadConfigPlayer(playerInfo.userName);
                ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
                PlayerServer player = DataPlayer();

                if (!ValidateInfo(player))
                {
                    try
                    {
                        var result = client.UpdatePlayer(player);
                        if (result == validOperation)
                        {
                            MessageBox.Show(Properties.Resources.messageBoxSavedChanges);
                            NavigationService.Navigate(new Uri("Views/AccountView.xaml", UriKind.Relative));
                        }
                        else if (result == errorConnection)
                        {

                            MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                            var window = (MainWindow)Application.Current.MainWindow;
                            window.Contenedor.Navigate(new LoginView());

                        }
                        else
                        {
                            MessageBox.Show(Properties.Resources.messageBoxErrorRegister);
                        }
                    }
                    catch (EndpointNotFoundException)
                    {
                        MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                    }

                }
                else
                {
                    MessageBox.Show(Properties.Resources.messageBoxSavedChanges);
                    NavigationService.Navigate(new Uri("Views/AccountView.xaml", UriKind.Relative));
                }

            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();

            int result = (int)MessageBox.Show(Properties.Resources.messageBoxLeave, Properties.Resources.messageBoxCare, MessageBoxButton.YesNo);
            if (result == resultYes)
            {
                NavigationService.Navigate(new Uri("Views/AccountView.xaml", UriKind.Relative));
            }
        }

        private bool ValidateInfo(PlayerServer player)
        {
            var result = false;
            if (playerInfo.firstName.Equals(player.firstName) &&
            playerInfo.lastName.Equals(player.lastName) &&
            playerInfo.userName.Equals(player.userName) &&
            playerInfo.password.Equals(player.password))
            {
                result = true;
            }


            return result;
        }

        private PlayerServer DataPlayer()
        {
            PlayerServer player = new PlayerServer();
            player.idPlayer = playerInfo.idPlayer;
            player.firstName = textFirstName.Text;
            player.lastName = textLastName.Text;
            player.userName = textUserName.Text;
            player.password = HashPassword();

            return player;
        }

        private string ValidateUserName()
        {
            string name = null;
            int userInUse = 1;
            PlayerServer player = new PlayerServer();
            player.userName = textUserName.Text;
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
            var result = client.ValidateUserNamePlayer(player);
            try
            {
                if (playerInfo.userName.Equals(textUserName.Text))
                {
                    name = playerInfo.userName;
                }
                else if (result == userInUse)
                {
                    MessageBox.Show(Properties.Resources.messageBoxInvalidName);

                }
                else if (result == errorConnection)
                {
                    MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                    var window = (MainWindow)Application.Current.MainWindow;
                    window.Contenedor.Navigate(new LoginView());
                }
                else
                {
                    name = textUserName.Text;
                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
            }

            return name;
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
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
            }
            else if (playerInfo.password.Equals(Accessories.Hash(textPassword.Text)))
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
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

        private void textFirstName_KeyDown(object sender, KeyEventArgs e)
        {
            Accessories.RegexSpecial(e);
        }

        private void textLastName_KeyDown(object sender, KeyEventArgs e)
        {
            Accessories.RegexSpecial(e);
        }

        private void textUserName_KeyDown(object sender, KeyEventArgs e)
        {
            Accessories.RegexSpecial(e);
        }

        private void textPassword_KeyDown(object sender, KeyEventArgs e)
        {
            Accessories.RegexSpecial(e);
        }
    }
}
