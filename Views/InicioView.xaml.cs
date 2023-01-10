using ClienteJuego.ConnectService;
using ClienteJuego.Properties;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace ClienteJuego.Views
{
    /// <summary>
    /// Interaction logic for InicioView.xaml
    /// </summary>
    public partial class InicioView : Page, ConnectService.INotificationServiceCallback
    {

        private string userName;
        private readonly ConnectService.NotificationServiceClient notificationClient;

        public InicioView()
        {
            InitializeComponent();
            try
            {
                Accessories.PlayMusic();

                userName = (App.Current as App).DeptName;
                TextUserName.Text = "Hola de nuevo " + userName;

                ImageSource imageSource = new ImageSourceConverter().ConvertFromString(Accessories.LoadConfigPlayer(userName)) as ImageSource;

                imgAvatar.Source = imageSource;

                InstanceContext context = new InstanceContext(this);

                notificationClient = new NotificationServiceClient(context);

                notificationClient.SetCallBack(userName);
            }
            catch (EndpointNotFoundException)
            {
                throw new EndpointNotFoundException();

            }


        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Accessories.PlaySoundsEffects();
                NavigationService.Navigate(new Uri("Views/GameModeView.xaml", UriKind.Relative));
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
            }

        }

        private void btnOptions_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            NavigationService.Navigate(new Uri("Views/MenuOptionsView.xaml", UriKind.Relative));
        }

        private void btnScore_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            NavigationService.Navigate(new Uri("Views/ScoreView.xaml", UriKind.Relative));
        }

        public void Notification(string username, string code)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            window.ContenedorInvi.Navigate(new InvitationView(username, code));

        }

        public void LoadLobby(PlayerServer[] players, string code)
        {
            try
            {
                var window = (MainWindow)Application.Current.MainWindow;
                window.Contenedor.Navigate(new LobbyView(code));

            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                var window = (MainWindow)Application.Current.MainWindow;
                window.Contenedor.Navigate(new LoginView());
            }

        }
    }
}
